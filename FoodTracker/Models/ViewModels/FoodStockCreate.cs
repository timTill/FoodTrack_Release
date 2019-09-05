using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models.ViewModels
{
	public class FoodStockCreate
	{
		public Food Food { get; set; }
		IEnumerable<QuantityLeft> quantityLeft { get; set; }
	}
}
