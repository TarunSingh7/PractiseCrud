using Microsoft.AspNetCore.Mvc;
using Practice1.Entity;
using Newtonsoft.Json;
using PractiseCore.Models;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace PractiseCore.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client=new HttpClient();
        public IActionResult GetEmp()
        {
            var data=client.GetAsync("https://localhost:7266/api/getAllEmp").Result;
            var jsonData=data.Content.ReadAsStringAsync().Result;
            var finaldata = JsonConvert.DeserializeObject<List<EmployeeModel>>(jsonData);
            return View (finaldata);
        }
        public IActionResult Add()
        {
            ViewBag.form = "create";
            return View();
        }

        [HttpPost]
        public IActionResult Add(EmployeeModel emp)
        {

            var data = JsonConvert.SerializeObject(emp);
            
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/Json");
            client.PostAsync("https://localhost:7266/api/AddEmp", content);
            return RedirectToAction("GetEmp");

        }

        public IActionResult Delete(int Id)
        {
            var res = client.DeleteAsync("https://localhost:7266/api/DeleteData?id=" + Id).Result;
            return RedirectToAction("GetEmp");

        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var data = client.GetAsync("https://localhost:7266/api/GetById?id="+Id).Result;
            var jsonData=data.Content.ReadAsStringAsync().Result;
            var finaldata=JsonConvert.DeserializeObject<EmployeeModel>(jsonData);
            return View ("Add",finaldata);
        }


    }
}
