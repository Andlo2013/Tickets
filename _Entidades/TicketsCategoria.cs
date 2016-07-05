using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _Entidades
{
    public class TicketsCategoria
    {
        #region Modelo
        public int id { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "Descripción Categoría es un campo obligatorio")]
        [StringLength(50, ErrorMessage = "Descripción Categoría debe tener máximo 50 caracteres")]
        public string Categoria { get; set; }

        [Display(Name = "Descarga")]
        [Required(ErrorMessage = "Descarga tiempo es un campo obligatorio")]
        public bool  isDescarga{ get; set; }

        [Display(Name = "Activo")]
        [Required(ErrorMessage = "Estado Categoría es un campo obligatorio")]
        public bool EstReg { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        #endregion

        public List<TicketsCategoria> _getCategorias()
        {
            using (var dba=new _Context())
            {
                return dba.TicketCategoria.Where(x => x.EstReg == true).ToList();
            }
        }

    }
}
