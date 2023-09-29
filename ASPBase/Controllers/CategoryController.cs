﻿using ASPBase.Data;
using ASPBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPBase.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
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
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }

            Category? cateforyFromDb = _db.Categories.Find(id);
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
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            Category? cateforyFromDb = _db.Categories.Find(id);
            if (cateforyFromDb == null) { return NotFound(); }

            return View(cateforyFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            if (id == null || id == 0) { return NotFound(); }
            Category? obj = _db.Categories.Find(id);
            if (obj == null) { return NotFound(); }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}