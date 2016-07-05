using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Entidades
{
    public class Combo
    {
        #region Modelo
        public int id { get; set; }

        [StringLength(50, ErrorMessage = "Relación máximo 50 caracteres")]
        public string Relacion { get; set; }

        public int Valor { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Descripción máximo 50 caracteres")]
        public string Descripcion { get; set; }

        #endregion

        public List<Combo> _getCombo(string relacion)
        {
            using (var dba = new _Context())
            {
                List<Combo> data = dba.Combo
                                .Where(x => x.Relacion == relacion)
                                .OrderBy(x => x.Valor).ToList();
                return data;
            }
        }
    }
}
