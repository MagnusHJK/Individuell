using IndividuellOppg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IndividuellOppg.DAL
{
    public class FAQs
    {
        [Key]
        public int Id { get; set; }

        virtual public Kategori Kategori { get; set; }

        public string Sporsmal { get; set; }

        public string Svar { get; set; }

        public int Oppstemmer { get; set; }

        public int Nedstemmer { get; set; }
    }

    public class Henvendelser
    {
        [Key]
        public int Id { get; set; }

        virtual public Kategori Kategori { get; set; }

        public string Email { get; set; }

        public string Melding { get; set; }

        public bool Status { get; set; }
    }

    public class Kategorier
    {
        [Key]
        public int Id { get; set; }

        public string Navn { get; set; }
    }



    public class KundeServiceContext : DbContext
    {
        public KundeServiceContext (DbContextOptions<KundeServiceContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<FAQ> FAQ { get; set; }
        public DbSet<Hendvendelse> Hendvendelse { get; set; }
        public DbSet<Kategori> Kategori { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
