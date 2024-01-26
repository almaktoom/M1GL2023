﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebImmobilier.Models
{
    public class Bien
    {
        [Key]
        public int IdBien { get; set; }

        [Display(Name ="Description du bien "), Required(ErrorMessage ="*"), MaxLength(1000, ErrorMessage ="La taille maximale est de 1000")] 
        public string DescriptionBien { get; set; }

        [Display(Name = "Superficie")]
        public float? SuperficieBien { get; set; }

        [Display(Name = "Localite du bien "), Required(ErrorMessage = "*"), MaxLength(300, ErrorMessage = "La taille maximale est de 300")]
        public string Localite { get; set; }

        [Display(Name = "Nombre salle d'eau "), Required(ErrorMessage = "*")]
        public int NbreSalleEau { get; set; }

        [Display(Name = "Nombre de cuisine "), Required(ErrorMessage = "*")]
        public int NbreCuisine { get; set; }

        [Display(Name = "Nombre de toilette "), Required(ErrorMessage = "*")]
        public int NbreToilette { get; set; }

        public int? IdProprio { get; set; }

        [ForeignKey("IdProprio")]
        public virtual Proprietaire Proprietaire { get; set; }

}
}