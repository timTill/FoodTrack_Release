using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodTracker.Models;
using FoodTracker.Data;
using Microsoft.EntityFrameworkCore;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FoodTracker.Controllers
{
	[Authorize]
	[Area("User")]
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _db;
		public HomeController(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> Index()
		{
			IndexViewModel IndexVM = new IndexViewModel()
			{
				//FoodItem = await _db.Foods.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync(),
				FoodItem = await _db.Foods.Where(m => m.QuantityLeft != 0).Include(m => m.Category).
				Include(m => m.SubCategory).ToListAsync(),
				Category = await _db.Category.ToListAsync(),
			};
			return View(IndexVM);
		}
		

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Nullify(int? id)
		{
			if (id != null)
			{
				Food foodToNull = await _db.Foods.FindAsync(id);
				foodToNull.BestBefore = null;
				foodToNull.QuantityLeft = 0;
				foodToNull.Description = String.Empty;
				foodToNull.Unit = 0;
				await _db.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> ConfirmDeletion(int? id)
		{
			if (id != null)
			{
				Food foodToConfirm = await _db.Foods.Where(f => f.ID == id).Include(f => f.SubCategory.Category).FirstOrDefaultAsync();
				return View(foodToConfirm);
			}
			return RedirectToAction(nameof(Index));
		}

		public async Task<ViewResult> UpdateStock(int? id)
		{
			Food food = await _db.Foods.Where(f => f.ID == id).Include(f => f.SubCategory.Category).FirstOrDefaultAsync();
			return View(food);
		}

		
		[HttpPost]
		public async Task<IActionResult> UpdateStock(Food food)
		{
			if (food != null)
			{
				Food newFood = await _db.Foods.Where(f => f.ID == food.ID).Include(f => f.SubCategory.Category).FirstOrDefaultAsync();
				newFood.Description = food.Description;
				newFood.BestBefore = food.BestBefore;
				newFood.QuantityLeft = food.QuantityLeft;
				newFood.Unit = food.Unit;
				await _db.SaveChangesAsync();
			}

			return RedirectToAction(nameof(Index));
		}
		
		/*
		public async Task<IActionResult> ConfirmAction(int? id)
		{
			if (id != null)
			{
				Food foodToConfirm = await _db.Foods.Where(f => f.ID == id).Include(f => f.SubCategory.Category).FirstOrDefaultAsync();
				return View(foodToConfirm);
			}
			return RedirectToAction(nameof(Index));
		}
		*/
	}
}
