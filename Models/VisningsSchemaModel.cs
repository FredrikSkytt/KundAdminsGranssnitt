using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KundAdminsGranssnitt.Models
{
    public class VisningsSchemaModel
    {
     

        public List<Film> Titel { get; set; }

        public List<Salong> Salong { get; set; }

    }
}