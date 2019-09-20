using FoodTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTrackerTest.Test
{
	class TestSubcategory : SubCategory
	{
		public override bool Equals(object obj)
		{
			if (obj is Category)
			{
				Category categ = (Category)obj;
				return (categ.Id) == (this.Category.Id);
			}
			SubCategory subcatparam = (SubCategory)obj;
			return ((this.Name == subcatparam.Name) && (this.Id == subcatparam.Id)
				&& (this.CategoryId == subcatparam.CategoryId));
		}
	}
}
