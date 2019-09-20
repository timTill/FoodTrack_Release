using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTracker.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		[Display(Name = "Category Name")]
		[Required]
		public string Name { get; set; }

		/*
		public override bool Equals(object obj)
		{
			Category catparam = (Category)obj;
			return ((this.Name == catparam.Name) && (this.Id == catparam.Id));
		}
		*/
	}

}
