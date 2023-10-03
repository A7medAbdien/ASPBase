using ASPBase.DataAccess.Data;
using ASPBase.DataAccess.Repository.IReopsitory;
using ASPBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPBase.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            /*if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "The Display Order cannot match the Name.");
            }*/
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }

            Category? cateforyFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            //Category? cateforyFromDb2 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? cateforyFromDb3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (cateforyFromDb == null) { return NotFound(); }

            return View(cateforyFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            Category? cateforyFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (cateforyFromDb == null) { return NotFound(); }

            return View(cateforyFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            if (id == null || id == 0) { return NotFound(); }
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null) { return NotFound(); }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
