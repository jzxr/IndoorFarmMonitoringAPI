using System;
using System.Collections.Generic;
using IndoorFarmMonitoringAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IndoorFarmMonitoringAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PlantDataEntity> PlantData { get; set; }
    }
}

