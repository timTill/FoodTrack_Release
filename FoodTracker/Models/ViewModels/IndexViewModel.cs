using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models.ViewModels
{
	public class IndexViewModel
	{
		public IEnumerable<Food> FoodItem { get; set; }
		public IEnumerable<Category> Category { get; set; }			
	}
}
