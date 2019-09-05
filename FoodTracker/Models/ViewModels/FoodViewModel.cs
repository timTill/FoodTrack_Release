using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models.ViewModels
{
	public class FoodViewModel
	{
		public Food Food{ get; set; }
		public IEnumerable<Category> Category { get; set; }
		public IEnumerable<SubCategory> SubCategory { get; set; }
	}
}
