using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Options;

namespace UCR_App
{
    public class DatabaseContext : DbContext
    {
        public string DbPath { get; }

        public DbSet<Test> Tests { get; set; }
    
        public DatabaseContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "database.db");
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite($"Data Source={DbPath}");
    }

    [PrimaryKey(nameof(IdTest))]
    public class Test
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTest { get; set; }
        public string Desc { get; set; }
    }
}