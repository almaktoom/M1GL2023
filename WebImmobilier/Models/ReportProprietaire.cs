using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace WebImmobilier.Models
{
    public class ReportProprietaire
    {
     
        public int IdUser { get; set; }

        public string NomUser { get; set; }
       
        public string PrenomUser { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        //public DataTable Biens { get; set; }
    }
}