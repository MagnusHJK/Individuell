using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IndividuellOppg.Models
{
    public class Mail
    {
        public string TilAddresse { get; set; }

        [RegularExpression(@"^.{10,200}$")]
        public string Tittel { get; set; }

        [MinLength(10), MaxLength(2000)]
        public string Body { get; set; }
    }
}