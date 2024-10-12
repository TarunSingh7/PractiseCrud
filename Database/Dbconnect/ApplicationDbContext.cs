using Database.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Dbconnect
{
	public class ApplicationDbContext:IdentityDbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) { }

       public DbSet<EmployeeModel> Employees { get; set; }

    }
}
