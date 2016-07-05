using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _Entidades
{
    public class TicketsDetalle
    {
        public int id { get; set; }

        [Display(Name = "Num")]
        [Required(ErrorMessage = "El campo Sec. Respuesta es obligatorio")]
        public int SecRespta { get; set; }

        [Display(Name = "Ticket")]
        [Required(ErrorMessage = "El campo ticket es obligatorio")]
        public int TicketId { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        public DateTime fechaING { get; set; }

        [Display(Name = "ID Teamviewer")]
        [Required(ErrorMessage = "El campo TeamViewer es obligatorio")]
        public string teamViewer { get; set; }

        [Display(Name = "Minutos")]
        [Required(ErrorMessage = "Ingrese el tiempo invertido")]
        public int Minutos { get; set; }


        [Display(Name = "Mensaje")]
        [Required(ErrorMessage = "Debe ingresar un mensaje ")]
        [StringLength(160, ErrorMessage = "El mensaje debe tener máximo 15 caracteres")]
        public string Mensaje { get; set; }

        [Display(Name = "Observación")]
        [StringLength(2000, ErrorMessage = "La solicitud debe tener máximo 2000 caracteres")]
        public string observacion { get; set; }

        [Display(Name = "User")]
        [Required(ErrorMessage = "El código de usuario es obligatorio")]
        public int UsuarioId { get; set; }

        [Display(Name = "Archivo 1")]
        public string File1 { get; set; }

        [Display(Name = "Archivo 2")]
        public string File2 { get; set; }

        [Display(Name = "Archivo 3")]
        public string File3 { get; set; }

        [Display(Name = "Leído")]
        [Required(ErrorMessage = "El campo leído es obligatorio")]
        public bool isReaded { get; set; }

        [Display(Name = "Origen")]
        [Required(ErrorMessage = "El campo origen es obligatorio")]
        [StringLength(7,ErrorMessage ="Máximo 7 caracteres")]
        public string whoSend { get; set; }

        [Display(Name = "UUID")]
        [Required(ErrorMessage = "El campo UUID es obligatorio")]
        [StringLength(150, ErrorMessage = "Máximo 150 caracteres")]
        public string UUID { get; set; }


        [Display(Name = "Activo")]
        [Required(ErrorMessage = "El estado del registro es obligatoria")]
        public bool EstReg { get; set; }

       

        //Relaciones
        public virtual Ticket Ticket { get; set; }

    }
}
