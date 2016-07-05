using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _Entidades
{
    public class Tecnico
    {
        #region Modelo

        public int id { get; set; }
        public string nombreTecnico { get; set; }

        public bool EstReg { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        
        #endregion


        public List<Tecnico> _getTecnicos()
        {
            using(var dba=new _Context())
            {
                return dba.Tecnico.Where(x => x.EstReg == true).ToList();
            }
        }
    }
}