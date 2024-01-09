using ParamTech.Domain;

namespace ParamTech.Persistence.CodeFirst.Context
{
    public class ParamTechContext : DbContext
    {
        public IConfiguration _configuration { get; set; }

        public ParamTechContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}