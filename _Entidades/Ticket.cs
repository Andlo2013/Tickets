using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace _Entidades
{
    public class Ticket
    {

        public int id { get; set; }

        [Display(Name = "Contrato")]
        [Required(ErrorMessage = "El tipo de contrato es obligatorio")]
        public int ContratoId { get; set; }

        [Required(ErrorMessage ="Usuario es obligatorio")]
        public int UsuarioId { get; set; }


        [Display(Name = "FechaINI")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        public DateTime fechaINI { get; set; }

        [Display(Name = "Técnico")]
        [Required(ErrorMessage = "El campo TÉCNICO es obligatorio")]
        public int TecnicoId { get; set; }

        [Display(Name = "Fecha FIN")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime fechaFIN { get; set; }

        [Display(Name = "Prioridad")]
        [Required(ErrorMessage = "El campo PRIORIDAD es obligatorio")]
        public int cmbPrioridadId { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo Estado de ticket es obligatorio")]
        public int cmbEstadoId { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "La categoría de ticket es obligatoria")]
        public int TicketCategoriaId { get; set; }

        [Display(Name ="Nro. Respuestas")]
        [Required(ErrorMessage ="El número de detalles es obligatorio")]
        public int NumDetalle { get; set; }

        [Display(Name ="Identificador")]
        [Required(ErrorMessage ="El campo identificador es obligatorio")]
        [StringLength(150,ErrorMessage ="El identificador debe tener máximo 150 caracteres")]
        public string UUID { get; set; }

        [Display(Name = "Activo")]
        [Required(ErrorMessage = "El estado del registro es obligatoria")]
        public bool EstReg { get; set; }

        //Relaciones
        public virtual TicketsCategoria TicketCategoria {get;set;}

        public virtual Contrato Contrato { get; set; }

        public virtual Tecnico Tecnico { get; set; }

        public virtual ICollection<TicketsDetalle> TicketDetalle { get; set; }

        
    }
}