using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models.Models;

namespace EntityFrame
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    public class UniversityContext : DbContext
    {
        private readonly IOptions<RepositoryOptions> _options;

        public UniversityContext(IOptions<RepositoryOptions> options)
        {
            _options = options;
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_options.Value.DefaultConnectionString);
            optionsBuilder.UseLazyLoadingProxies();
        }
    public DbSet <Course> Courses { get; set; }
    public DbSet<HomeTask> HomeTasks { get; set; }
    public DbSet<Student> Students { get; set; }
    }
}
