using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module1Training.Models;

namespace Module1Training.Controllers
{
	[Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

		static List<Products> _products = new List<Products>()
		{
			new Products(){ProductID=0, ProductName="Laptop",ProductPrice="200"},
			new Products(){ProductID=1, ProductName="SmartPhone",ProductPrice="100"}
		};


		public IEnumerable<Products> Get()
		{
			return _products;
		}

		[HttpPost]
		public void Post([FromBody]Products product)
		{
			_products.Add(product);
		}
    }
}