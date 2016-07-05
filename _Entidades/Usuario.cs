using _Entidades.SentenciasSQL;
using _Entidades.specificModels;
using _Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Entidades
{
    public class Usuario
    {


        #region Modelo
        public int id { get; set; }

        [Required(ErrorMessage ="El nombre de usuario es obligatorio")]
        [StringLength(70,ErrorMessage ="El nombre de usuario debe tener máximo 70 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El email de usuario es obligatorio")]
        [StringLength(80, ErrorMessage = "El email de usuario debe tener máximo 80 caracteres")]
        public string Email { get; set; }

        [Display(Name ="Clave")]
        [Required(ErrorMessage = "La clave de usuario es obligatorio")]
        [StringLength(40, ErrorMessage = "La clave debe tener máximo 40 caracteres")]
        public string Pwd { get; set; }

        [StringLength(70, ErrorMessage = "El horario de trabajo debe tener máximo 70 caracteres")]
        public string Horario { get; set; }

        [Required(ErrorMessage = "El tipo de usuario es obligatorio")]
        public int Tipo { get; set; }


        [Required(ErrorMessage = "El usuario debe pertencer a una Empresa")]
        public int EmpresaId { get; set; }

        [Required(ErrorMessage ="Campo UUID es obligatorio")]
        [StringLength(150,ErrorMessage ="Máximo 150 caracteres")]
        public string UUID { get; set; }

        [Display(Name ="Estado")]
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public bool EstReg { get; set; }

        public virtual Empresa Empresa { get; set; }

        #endregion

    }
}
