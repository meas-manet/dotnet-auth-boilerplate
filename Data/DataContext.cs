namespace dotnet_auth_boilerplate.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var host = _configuration["DB_HOST"];
            var port = _configuration["DB_PORT"];
            var dbName = _configuration["DB_NAME"];
            var username = _configuration["DB_USERNAME"];
            var password = _configuration["DB_PASS"];

            var connectionString = $"Host={host};Port={port};Database={dbName};Username={username};Password={password}";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}