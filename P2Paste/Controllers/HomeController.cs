using Database.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using P2Paste.Models;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace P2Paste.Controllers
{
	
	public class HomeController : Controller
	{
		
		HttpClient Client = new HttpClient();

		[Authorize]
		public IActionResult Index()
		{
			var data=Client.GetAsync("https://localhost:7083/Api/ShowData").Result;
			var jsonData=data.Content.ReadAsStringAsync().Result;
			var finalData=JsonConvert.DeserializeObject<List<EmployeeModel>>(jsonData);
			return View(finalData);
		}

		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(EmployeeModel model)
		{
			var data=JsonConvert.SerializeObject(model);
			StringContent content= new StringContent(data,System.Text.Encoding.UTF8,"application/json");

			Client.PostAsync("https://localhost:7083/Api/Add", content);
			return RedirectToAction("Index");

		}

		public IActionResult Edit(int Id)
		{
			var data = Client.GetAsync("https://localhost:7083/api/GetById?id="+Id).Result;
			var jsonData= data.Content.ReadAsStringAsync().Result;
			var finalData=JsonConvert.DeserializeObject<EmployeeModel>(jsonData);
			return View("Add",finalData);
		}

		public IActionResult Delete(int Id)
		{

			
			var res = Client.DeleteAsync("https://localhost:7083/api/DeleteData?id= " + Id).Result;
			return RedirectToAction("Index");
		

		}
		[HttpGet]
		[AllowAnonymous]
		public IActionResult LoginView()
		{
			return View();
		}
	}
}
