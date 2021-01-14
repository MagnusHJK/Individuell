using Castle.Core.Internal;
using IndividuellOppg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndividuellOppg.DAL
{
    public class KategoriRepository : IKategoriRepository
    {
        private readonly KundeServiceContext _db;

        public KategoriRepository(KundeServiceContext db)
        {
            _db = db;
        }

        public async Task<List<Kategori>> HentAlle()
        {
            try
            {
                List<Kategori> alleKategorier = await _db.Kategori.ToListAsync();
                if (alleKategorier.IsNullOrEmpty())
                {
                    return null;
                }
                return alleKategorier;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Kategori> Lag(Kategori innKategori)
        {
            try
            {
                var nyKategori = new Kategori
                {
                    Navn = innKategori.Navn
                };

                await _db.Kategori.AddAsync(nyKategori);
                await _db.SaveChangesAsync();
                return nyKategori;
            }
            catch
            {
                return null;
            }
        }
    }
}
