using Microsoft.EntityFrameworkCore;

namespace Backend.DBContext
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DbContext> options) : base(options)
        {

        }

       
    }
}
