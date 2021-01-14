using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IndividuellOppg.Models
{
    public class Hendvendelse
    {
        public int Id { get; set; }

        virtual public Kategori Kategori { get; set; }
        
        //Det blir laget et MailAddress objekt som validering
        public string Email { get; set; }

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{10,2000}$")]
        public string Melding { get; set; }

        public bool Status { get; set; }
    }
}
