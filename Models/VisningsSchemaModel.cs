using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KundAdminsGranssnitt.Models
{
    public class VisningsSchemaModel
    {
     

        public List<Film> Film { get; set; }

        public List<Salong> Salong { get; set; }

        public string Titel { get; set; }

        public string Namn { get; set; }

        public DateTime Datum { get; set; }

    }
}