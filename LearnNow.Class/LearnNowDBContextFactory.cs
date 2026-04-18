using LearnNow.Class;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class LearnNowDBContextFactory : IDesignTimeDbContextFactory<LearnNowDB>
{
    LearnNowDB IDesignTimeDbContextFactory<LearnNowDB>.CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LearnNowDB>();
        optionsBuilder.UseSqlServer("Server=DESKTOP-FOU36F5\\SQLEXPRESS;Database=LearnNowDB;Trusted_Connection=True;TrustServerCertificate=True;");

        return new LearnNowDB(optionsBuilder.Options);
    }
}