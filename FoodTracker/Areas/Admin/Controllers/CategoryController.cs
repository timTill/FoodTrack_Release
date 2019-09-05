using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTracker.Data;
using FoodTracker.Models;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin,Owner")]
	public class CategoryController : Controller
    {
		private readonly ApplicationDbContext _db;

		public CategoryController(ApplicationDbContext db)
		{
			_db = db;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _db.Category.ToListAsync());
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Category category)
		{
			if (ModelState.IsValid)
			{				
				_db.Category.Add(category);
				await _db.SaveChangesAsync();

				return RedirectToAction(nameof(Index));

			}
			return View(category);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var category = await _db.Category.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Category category)
		{
			if (ModelState.IsValid)
			{
				_db.Update(category);
				await _db.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}
			return View(category);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var category = await _db.Category.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int? id)
		{
			var category = await _db.Category.FindAsync(id);

			if (category == null)
			{
				return View();
			}
			_db.Category.Remove(category);
			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var category = await _db.Category.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}
	}
}
