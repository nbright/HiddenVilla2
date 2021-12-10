using DataAcesss.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        
        }
        public DbSet<YRRoom> YRRooms { get; set; }
        public DbSet<YRRoomImage> YRRoomImages { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
