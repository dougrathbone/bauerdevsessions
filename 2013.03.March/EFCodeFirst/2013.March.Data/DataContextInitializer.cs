using System.Data.Entity;

namespace _2013.March.Data
{
    public class DataContextInitializer
    {
        public static void Initialize()
        {
            var initializeMigrations =
                new MigrateDatabaseToLatestVersion<MyDataContext, Migrations.Configuration>();
            initializeMigrations.InitializeDatabase(new MyDataContext());
        }
    }
}
