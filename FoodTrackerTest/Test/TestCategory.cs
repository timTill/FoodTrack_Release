using FoodTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTrackerTest.Test
{
	class TestCategory : Category
	{
		public override bool Equals(object obj)
		{
			TestCategory catparam = (TestCategory)obj;
			return ((this.Name == catparam.Name) && (this.Id == catparam.Id));
		}
	}
}
