using Castle.Core.Internal;
using IndividuellOppg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IndividuellOppg.DAL
{
    public class HendvendelseRepository : IHendvendelseRepository
    {
        private readonly KundeServiceContext _db;

        public HendvendelseRepository(KundeServiceContext db)
        {
            _db = db;
        }

        public async Task<List<Hendvendelse>> HentAlle()
        {
            try
            {
                List<Hendvendelse> alleHendvendelser = await _db.Hendvendelse.ToListAsync();
                if (alleHendvendelser.IsNullOrEmpty())
                {
                    return null;
                }
                return alleHendvendelser;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> LagHendvendelse(Hendvendelse innHenvendelse)
        {
            try
            {
                var sjekkKategori = await _db.Kategori.FindAsync(innHenvendelse.Kategori.Id);
                if(sjekkKategori != null)
                {
                    var nyHendvendelse = new Hendvendelse
                    {
                        Kategori = sjekkKategori,
                        Email = innHenvendelse.Email,
                        Melding = innHenvendelse.Melding,
                        Status = innHenvendelse.Status
                    };
                    _db.Hendvendelse.Add(nyHendvendelse);
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

        public bool SendMail(Mail mail)
        {

            using (MailMessage emailMessage = new MailMessage())
            {
                var fraAddresse = new MailAddress("NORWAY.ITPE3200@gmail.com", "NOR-WAY");
                var fraPassord = "*c*S%vX6PSXr6mw9tjy!tstfF";
                var tilAddresse = new MailAddress("fail@fail.com");

                //Prøver å lage MailAddress objekt, går som validering på backend
                try
                {
                    tilAddresse = new MailAddress(mail.TilAddresse);
                }
                catch
                {
                    return false;
                }

                emailMessage.To.Add(tilAddresse);
                emailMessage.From = fraAddresse;
                emailMessage.Subject = mail.Tittel;
                emailMessage.Body = mail.Body;
                emailMessage.Priority = MailPriority.Normal;
                emailMessage.IsBodyHtml = true;

                using (SmtpClient MailClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    try
                    {
                        MailClient.EnableSsl = true;
                        MailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        MailClient.UseDefaultCredentials = false;
                        MailClient.Credentials = new System.Net.NetworkCredential(fraAddresse.Address, fraPassord);
                        MailClient.Timeout = 20000;
                        MailClient.Send(emailMessage);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public async Task<bool> EndreHendvendelse(Hendvendelse innHendvendelse)
        {
            try
            {
                Hendvendelse funnetHendvendelse = await _db.Hendvendelse.FindAsync(innHendvendelse.Id);
                if(funnetHendvendelse != null)
                {
                    funnetHendvendelse.Kategori = innHendvendelse.Kategori;
                    funnetHendvendelse.Melding = innHendvendelse.Melding;
                    funnetHendvendelse.Status = innHendvendelse.Status;
                    funnetHendvendelse.Email = innHendvendelse.Email;
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

        public async Task<bool> SlettHendvendelse(int hendvendelseId)
        {
            try
            {
                Hendvendelse funnetHendvendelse = await _db.Hendvendelse.FindAsync(hendvendelseId);

                if(funnetHendvendelse != null)
                {
                    _db.Hendvendelse.Remove(funnetHendvendelse);
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
