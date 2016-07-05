using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;
using _Helpers;
using _Entidades.SentenciasSQL;
using System.Data.Entity.Infrastructure;

namespace _Entidades
{
    public partial class Contrato
    {
 
        #region Modelo
        public int id { get; set; }

        [Display(Name = "Empresa")]
        [Required(ErrorMessage ="ID_Empresa Contrato es obligatorio")]
        public int EmpresaId { get; set; }

        [Display(Name = "Plan")]
        [Required(ErrorMessage = "ID_Plan Contrato es obligatorio")]
        public int PlanId { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name ="Inicio")]
        [Required(ErrorMessage = "Fecha_Inicio Contrato es obligatorio")]
        public DateTime fecInicia { get; set; }

        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}")]
        [Display(Name = "Termina")]
        [Required(ErrorMessage = "Fecha_Termina Contrato es obligatorio")]
        public DateTime fecTermina { get; set; }

        [Display(Name = "Minutos")]
        [Required(ErrorMessage = "Minutos_Plan Contrato es obligatorio")]
        public int MinPlan { get; set; }

        [Display(Name = "Observaciones")]
        [StringLength(500, ErrorMessage = "Observaciones Contrato máximo 500 caracteres")]
        public string obsContrato { get; set; }


        [Display(Name = "UUID")]
        [StringLength(128, ErrorMessage = "UUID Contrato máximo 120 caracteres")]
        public string UUID { get; set; }


        [Display(Name = "Activo")]
        [Required (ErrorMessage = "Estado Contrato es obligatorio")]
        public bool EstReg { get; set; }
        
        public virtual Plan Plan { get; set; }

        public virtual Empresa Empresa { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        #endregion        
        
    }
}