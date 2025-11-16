using DynamicFormBuilder.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicFormBuilder.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
        }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormField> FormFields { get; set; }
    }
}
