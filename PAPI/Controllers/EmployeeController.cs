using Database.Dbconnect;
using Database.Entity;
using Microsoft.AspNetCore.Mvc;

namespace PAPI.Controllers
{
	public class EmployeeController : ControllerBase
	{
		private readonly ApplicationDbContext _db;
        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("Api/ShowData")]
        public List<EmployeeModel> ShowData()
        {
            var data=_db.Employees.ToList();
            return data;
        }
        [HttpPost]
        [Route("Api/Add")]
        public IActionResult Add([FromBody] EmployeeModel obj)
        {
            if (obj == null)
            {
                return BadRequest("Employee data is null");
            }
            else if (obj.Id == 0)
            {
                _db.Employees.Add(obj);
            }
            else
            {
                _db.Employees.Update(obj);
            }
            _db.SaveChanges();
            return Ok();
        }

		[HttpDelete]
		[Route("api/DeleteData")]
		public void DeleteData(int id)
		{
			var data = _db.Employees.Where(a => a.Id == id).FirstOrDefault();
			_db.Employees.Remove(data);
			_db.SaveChanges();

		}

		[HttpGet]
		[Route("api/GetById")]
		public EmployeeModel GetById(int id)
		{
			var data = _db.Employees.Where(a => a.Id == id).FirstOrDefault();
			return data;
		}



	}
}
