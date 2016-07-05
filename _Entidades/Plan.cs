using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.SqlClient;
using _Helpers;
using System.Data.Entity.Infrastructure;
using _Entidades.specificModels;
using _Entidades.SentenciasSQL;

namespace _Entidades
{
    public class Plan
    {
        private _Context db = new _Context();

        #region Modelo
        public int id { get; set; }

        [Display(Name = "Plan")]
        [Required(ErrorMessage = "Descripción Plan es un campo obligatorio")]
        [StringLength(50, ErrorMessage = "Descripción Plan debe tener máximo 50 caracteres")]
        public string Descripcion { get; set; }

        [Display(Name = "Minutos")]
        [Required(ErrorMessage ="Minutos Plan es un campo obligatorio")]
        [Range(60,120000,ErrorMessage ="Minutos Plan entre 60 y 120000")]
        public int Minutos { get; set; }

        [Display(Name = "UUID")]
        public string UUID { get; set; }

        [Display(Name = "Activo")]
        [Required(ErrorMessage ="Estado Plan es un campo obligatorio")]
        public bool EstReg { get; set; }

        public virtual ICollection<Contrato> Contratos { get; set; }

        #endregion

        #region Plan-CRUD

        public void _deletePlan()
        {
            Plan plan = _getPlanId(id);
            db.Plan.Remove(plan);
            db.SaveChanges();
        }

        public List<Plan> _getPlans()
        {
            using (var dba = new _Context())
            {
                dba.Configuration.LazyLoadingEnabled = false;
                return dba.Plan.ToList();
            }
        }

        public List<planCMB> _getPlansCMB()
        {
            using (var dba = new _Context())
            {
                SqlParameter[] parametros = _Utilidades._ParamsSQL
                    (new string[] { "@a" }, new object[] { 1 });

                DbRawSqlQuery<planCMB> data = dba.Database.SqlQuery<planCMB>
                                            (_SQLSupport.planCMB, parametros);
                return data.ToList();
            }
        }

        public Plan _getPlanId(int id)
        {
            using (var dba = new _Context())
            {
                dba.Configuration.LazyLoadingEnabled = false;
                Plan plan = dba.Plan.Find(id);
                return plan;
            }
        }

        public List<Plan> _savePlan(Plan model,bool isNew)
        {
            using (var dba = new _Context())
            {
                SqlParameter[] parametros = _Utilidades._ParamsSQL
                    (new string[] { "@Descripcion", "@Minutos", "@UUID", "@estadoREG", "@isNew" },
                    new object[] { model.Descripcion, model.Minutos, model.UUID, true, isNew });

                DbRawSqlQuery<Plan> data = dba.Database.SqlQuery<Plan>
                                            (_SQLSupport.planGuardar, parametros);
                return data.ToList();
            }
        }      
        
        #endregion
    }
}