using ASPBase.DataAccess.Data;
using ASPBase.DataAccess.Repository.IReopsitory;
using ASPBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPBase.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            /*if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "The Display Order cannot match the Name.");
            }*/
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }

            Product? cateforyFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //Product? cateforyFromDb2 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Product? cateforyFromDb3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (cateforyFromDb == null) { return NotFound(); }

            return View(cateforyFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            Product? cateforyFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (cateforyFromDb == null) { return NotFound(); }

            return View(cateforyFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            if (id == null || id == 0) { return NotFound(); }
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null) { return NotFound(); }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
