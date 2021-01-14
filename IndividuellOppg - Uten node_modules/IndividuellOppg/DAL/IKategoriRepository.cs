using IndividuellOppg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndividuellOppg.DAL
{
    public interface IKategoriRepository
    {
        Task<List<Kategori>> HentAlle();

        Task<Kategori> Lag(Kategori innKategori);
    }
}
