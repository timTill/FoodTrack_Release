using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FoodTracker.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ErrorCheckController : Controller
	{
		private readonly ILogger<ErrorCheckController> logger;

		public ErrorCheckController(ILogger<ErrorCheckController> logger)
		{
			this.logger = logger;
		}
		[Route("Admin/ErrorCheck/{statusCode}")]
		public IActionResult HTTPStatusCodeHandler(int statusCode)
		  {
			var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
			switch (statusCode)
			{
				case 404:
					ViewBag.ErrorMessage = "Resource cannot be found";
					logger.LogWarning($"404 Error occured. Path = {statusCodeResult.OriginalPath} and QueryString={statusCodeResult.OriginalQueryString}");


					break;
			}
			return View("ResourceNotFound");
		}

		[Route("/Admin/ErrorCheck")]
		[AllowAnonymous]
		public IActionResult Error()
		{
			var ExceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
			logger.LogError($"The path {ExceptionDetails.Path} threw an exception {ExceptionDetails.Error}" +
				$"{ExceptionDetails.Error}");			
			return View("Error");
		}
	}
}