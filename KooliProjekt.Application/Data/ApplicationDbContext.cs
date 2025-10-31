﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Toode> Tooted { get; set; }
        public DbSet<Klient> Kliendid { get; set; }
        public DbSet<Arve> Arved { get; set; }
        public DbSet<Tellimus> Tellimused { get; set; }

    }
}
