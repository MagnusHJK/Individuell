using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IndividuellOppg.Models
{
    public class FAQ
    {
        public int Id { get; set; }

        virtual public Kategori Kategori { get; set; }

        [RegularExpression(@"^.{10,500}$")]
        public string Sporsmal { get; set; }

        [RegularExpression(@"^.{10,500}$")]
        public string Svar { get; set; }

        [RegularExpression(@"[0-9]{1,10}")]
        public int Oppstemmer { get; set; }

        [RegularExpression(@"[0-9]{1,10}")]
        public int Nedstemmer { get; set; }
    }
}
