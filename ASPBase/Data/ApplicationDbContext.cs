using Microsoft.EntityFrameworkCore;

namespace ASPBase.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) { 
        
        }
    }
}
