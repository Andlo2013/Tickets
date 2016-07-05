using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using _Entidades.SentenciasSQL;
using _Helpers;
using System.Data;

namespace _Entidades.specificModels
{
    public class contratoCRUD
    {
        private int m_totalREG = 0;

        //MODELO
        #region MODELO

        public string UUID { get; set; }

        public int EmpresaId { get; set; }

        public string EmpresaRUC { get; set; }

        public string Empresa { get; set; }

        public int PlanId { get; set; }

        public string Plan { get; set; }

        public DateTime Inicia { get; set; }

        public DateTime Termina { get; set; }

        public int Minutos { get; set; }

        public string Observaciones { get; set; }

        public bool Estado { get; set; }

        #endregion

        //MÉTODOS CRUD
        #region MÉTODOS: CRUD

        //Listado de contratos
        #region ListadoContratos
        public List<contratoCRUD> _getContratos(int jtStartIndex = 0, int jtPageSize = 0,
                                        string campoBuscar = "", string textoBuscar = "")
        {
            using (var dba = new _Context())
            {
                SqlParameter[] parametros = _Utilidades._ParamsSQL
                    (new string[] { "@startIndex", "@perPage", "@CampoBuscar", "@valorBuscar","@TotalREG" }, 
                    new object[] { jtStartIndex, jtPageSize, campoBuscar, textoBuscar,0 },
                    new string[] { "@TotalREG" });

                DbRawSqlQuery<contratoCRUD> data = dba.Database.SqlQuery<contratoCRUD>
                                            (_SQLSupport.contratoIndex, parametros);
                List<contratoCRUD> listado = data.ToList();
                m_totalREG = Convert.ToInt32(parametros[4].Value);
                return listado;
            }
        }
        #endregion

        //Busca un contrato por ID
        #region ContratoID
        public Contrato _getContratoId(int id)
        {
            using (var dba = new _Context())
            {
                dba.Configuration.LazyLoadingEnabled = false;
                Contrato contrato = dba.Contrato
                    .Include("Empresa")
                    .Include("Plan")
                    .Where(c => c.id == id)
                    .First();
                return contrato;
            }
        }
        #endregion

        //Guarda un contrato
        #region ContratoSave
        public List<contratoCRUD> _saveContrato(contratoCRUD model, bool isNew)
        {
            using (var dba = new _Context())
            {
                SqlParameter[] parametros = _Utilidades._ParamsSQL(
                            new string[] { "@UUID", "@EmpresaRUC", "@PlanID",
                                "@FechaINI", "@FechaFIN","@MinutosPLAN",
                                "@Observaciones", "@EstadoREG", "@isNew" },
                            new object[] { model.UUID, model.EmpresaRUC, model.PlanId, model.Inicia,
                            model.Termina,model.Minutos,model.Observaciones,model.Estado,isNew});
                DbRawSqlQuery<contratoCRUD> data = dba.Database.SqlQuery<contratoCRUD>
                                            (_SQLSupport.contratoSave, parametros);
                return data.ToList();
            }
        }
        #endregion

        #endregion

        //MÉTODOS EXTERNOS
        #region MÉTODOS: EXTERNOS

        //RECUPERA LA INFORMACIÓN DE UN CONTRATO
        #region InfoContrato
        public DataRow _getInfoContrato()
        {
            _SQLServer objSQLServer = new _SQLServer();
            int userID = _SessionHelper.GetUserID();
            DataTable dt_infoContrato = objSQLServer._CargaDataTable("ticket_infoPlan",
                        new string[] { "@UsuarioID" }, new object[] { userID });
            if (dt_infoContrato != null && dt_infoContrato.Rows.Count == 1)
            {
                return dt_infoContrato.Rows[0];
            }
            return null;
        }
        #endregion

        #endregion

        //PROPIEDADES
        #region PROPIEDADES

        //TOTAL DE REGISTROS
        #region TotalREG
        public int pro_getTotalREG
        {
            get
            {
                return m_totalREG;
            }
        }
        #endregion

        #endregion

    }
}
