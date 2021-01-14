using IndividuellOppg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndividuellOppg.DAL
{
    public interface IFAQRepository
    {
        Task<List<FAQ>> HentAlle();

        Task<bool> Lag(FAQ innFAQ);

        Task<bool> Endre(FAQ endretFAQ);

        Task<bool> Slett(int FAQId);

    }
}
