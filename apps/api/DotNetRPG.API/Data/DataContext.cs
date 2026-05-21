using Microsoft.EntityFrameworkCore;

namespace DotNetRPG.API.Data
{
    public class RpgDataContext : DbContext
    {
        public RpgDataContext(DbContextOptions<RpgDataContext> options) : base (options)
        {
             
        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        public async Task<bool> CanConnectAsync()
        {
            try
            {
                return await this.Database.CanConnectAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}