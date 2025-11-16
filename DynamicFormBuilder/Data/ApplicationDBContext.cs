using Microsoft.EntityFrameworkCore;

namespace DynamicFormBuilder.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
        }

    }
}
