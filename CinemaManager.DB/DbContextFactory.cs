using System;

namespace CinemaManager.DB
{
    public class DbContextFactory
    {
        private readonly string _dbPath;

        public DbContextFactory(string dbPath)
        {
            _dbPath = dbPath;
        }

        public AppDbContext Create() => new AppDbContext(_dbPath);
    }
}