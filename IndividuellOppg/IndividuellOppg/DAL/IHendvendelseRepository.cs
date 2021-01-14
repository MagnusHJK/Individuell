using IndividuellOppg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndividuellOppg.DAL
{
    public interface IHendvendelseRepository
    {
        Task<List<Hendvendelse>> HentAlle();

        Task<bool> LagHendvendelse(Hendvendelse innHendvendelse);

        bool SendMail(Mail mail);

        Task<bool> EndreHendvendelse(Hendvendelse innHendvendelse);

        Task<bool> SlettHendvendelse(int hendvendelseId);
    }
}
