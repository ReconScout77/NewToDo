﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
		private ToDoListContext db = new ToDoListContext();

		public IActionResult Index()
		{
			return View(db.Items.Include(items => items.Category).ToList());
		}

		public IActionResult Details(int id)
		{
			var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
			return View(thisItem);
		}

        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


		public IActionResult Edit(int id)
		{
			var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
			ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View(thisItem);
		}
		[HttpPost]
        public IActionResult Edit(Item item)
		{
			db.Entry(item).State = EntityState.Modified;
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Delete(int id)
		{
			var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
			return View(thisItem);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
			db.Items.Remove(thisItem);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
        //[HttpGet, ActionName("Done")]
        //public IActionResult DoneTask(int id)
        //{
        //    //var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
        //    ////db.Items.Update(thisItem.Done = 1);
        //    //if (db.Items.thisItem.Done == false)
        //    //{
        //    //    db.Items.thisItem.Done = true;
        //    //}
        //    //db.SaveChanges();
        //    //return RedirectToAction("Index");
        //}
	}
}
