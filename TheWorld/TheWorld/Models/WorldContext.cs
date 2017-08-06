﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TheWorld.Models
{
    public class WorldContext:DbContext
    {
        private readonly IConfigurationRoot _config;

        public WorldContext(IConfigurationRoot config,DbContextOptions options):base()
        {
            _config = config;
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:WorldDbContext"]);//connection string to connect from db
        }
    }
}
