using Castle.Core.Internal;
using IndividuellOppg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndividuellOppg.DAL
{
    public class FAQRepository : IFAQRepository
    {
        private readonly KundeServiceContext _db;

        public FAQRepository(KundeServiceContext db)
        {
            _db = db;
        }

        public async Task<List<FAQ>> HentAlle()
        {
            try
            {
                List<FAQ> alleFAQs = await _db.FAQ.ToListAsync();
                if (alleFAQs.IsNullOrEmpty())
                {
                    return null;
                }
                return alleFAQs;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Lag(FAQ innFAQ)
        {
            try
            {
                var funnetKategori = await _db.Kategori.FindAsync(innFAQ.Kategori.Id);
                if(funnetKategori != null)
                {
                    var nyFAQ = new FAQ();
                    nyFAQ.Kategori = funnetKategori;
                    nyFAQ.Sporsmal = innFAQ.Sporsmal;
                    nyFAQ.Svar = innFAQ.Svar;
                    nyFAQ.Oppstemmer = innFAQ.Oppstemmer;
                    nyFAQ.Nedstemmer = innFAQ.Nedstemmer;
                    await _db.FAQ.AddAsync(nyFAQ);
                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;

            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> Endre(FAQ endretFAQ)
        {
            try
            {
                FAQ funnetFAQ = await _db.FAQ.FindAsync(endretFAQ.Id);
                if(funnetFAQ != null)
                {
                    funnetFAQ.Svar = endretFAQ.Svar;
                    funnetFAQ.Kategori = endretFAQ.Kategori;
                    funnetFAQ.Sporsmal = endretFAQ.Sporsmal;
                    funnetFAQ.Oppstemmer = endretFAQ.Oppstemmer;
                    funnetFAQ.Nedstemmer = endretFAQ.Nedstemmer;

                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Slett(int FAQId)
        {
            try
            {
                FAQ funnetFAQ = await _db.FAQ.FindAsync(FAQId);
                if (funnetFAQ != null)
                {
                    _db.FAQ.Remove(funnetFAQ);
                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;

            }
            catch
            {
                return false;
            }
        }


    }
}
