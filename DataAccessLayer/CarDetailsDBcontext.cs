using GlobalEntityLayer.Models;
using GlobalEntityLayer.Models.Admin;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CarDetailsDBcontext:IdentityDbContext <APIuser>
    {

        public CarDetailsDBcontext(DbContextOptions<CarDetailsDBcontext> options) : base(options) {/*do nothing*/}
        public DbSet<CarDetails> Carfluent_Cars { get; set; }
        public DbSet<CategoryPicklist> CategoryPicklist { get; set; }

    }
}

