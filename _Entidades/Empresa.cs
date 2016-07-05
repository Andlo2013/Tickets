using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using _Entidades.SentenciasSQL;
using System.Data.SqlClient;
using _Helpers;
using System.Data.Entity.Infrastructure;

namespace _Entidades
{
    public class Empresa
    {
        private int m_totalREG = 0;

        //DEFINICIÓN DEL MODELO
        #region Modelo
        public int id { get; set; }


        [Display(Name = "RUC")]
        [Required(ErrorMessage = "RUC Empresa es un campo obligatorio")]
        [StringLength(13, ErrorMessage = "El RUC debe tener 13 dígitos", MinimumLength = 13)]
        public string EmpRuc { get; set; }

        [Display(Name = "Empresa")]
        [Required(ErrorMessage = "Nombre Empresa es un campo obligatorio")]
        [StringLength(100, ErrorMessage = "Nombre Empresa debe tener máximo 100 caracteres")]
        public string EmpNom { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Dirección Empresa es un campo obligatorio")]
        [StringLength(150, ErrorMessage = "Dirección Empresa debe tener máximo 150 caracteres")]
        public string Direccion { get; set; }


        [Display(Name = "Teléfono")]
        [StringLength(50, ErrorMessage = "Teléfono Empresa debe tener máximo 50 caracteres")]
        public string Telefono { get; set; }

        [Display(Name = "UUID")]
        public string UUID { get; set; }

        [Display(Name = "Activo")]
        [Required(ErrorMessage = "Estado Empresa es un campo obligatorio")]
        
        public bool EstReg { get; set; }
        
        public virtual ICollection<Contrato> Contratos { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

        #endregion

        //OPERACIONES CRUD
        #region Empresa-CRUD

        //LISTADO DE EMPRESAS
        #region ListadoEmpresas
        public List<Empresa> _getEmpresas(int jtStartIndex,int jtPageSize,string CampoBuscar,string ValorBuscar)
        {
            using (var dba = new _Context())
            {
                SqlParameter[] parametros = _Utilidades._ParamsSQL
                    (new string[] { "@startIndex", "@perPage", "@CampoBuscar", "@valorBuscar","@TotalREG" },
                     new object[] {jtStartIndex, jtPageSize, CampoBuscar,ValorBuscar,0},
                     new string[] { "@TotalREG" });

                DbRawSqlQuery<Empresa> data = dba.Database.SqlQuery<Empresa>
                                            (_SQLSupport.empresaIndex, parametros);

                List<Empresa> listado = data.ToList();
                m_totalREG=Convert.ToInt32(parametros[4].Value);
                return listado;
            }
        }
        #endregion

        //BUSCA UNA EMPRESA POR ID
        #region EmpresaID
        public Empresa _getEmpresaId(int id)
        {
            using (var dba = new _Context())
            {
                dba.Configuration.LazyLoadingEnabled = false;
                Empresa empresa = dba.Empresa.Find(id);
                return empresa;
            }
        }
        #endregion

        //BUSCA UNA EMPRESA POR RUC
        #region EmpresaRUC
        public Empresa _getEmpresaRUC(string RUC)
        {
            using (var dba = new _Context())
            {
                dba.Configuration.LazyLoadingEnabled = false;
                Empresa empresa = dba.Empresa.Where(x => x.EmpRuc == RUC).First();
                return empresa;
            }
        }
        #endregion

        //NUEVA EMPRESA
        #region NuevaEmpresa
        public List<Empresa> _newEmpresa(string empresaRUC,string empresaNOM,
						string empresaDIR,string empresaTEL="")
        {
            empresaTEL = empresaTEL == null ? "" : empresaTEL;
            using (var dba = new _Context())
            {
                SqlParameter []parametros = _Utilidades._ParamsSQL
                    (new string[] { "@empresaRUC", "@empresaNOM", "@empresaDIR",
                        "@empresaTEL", "@UUID", "@estadoREG", "@isNew" },
                     new object[] {empresaRUC, empresaNOM, empresaDIR,
                         empresaTEL,"--", true,true});

                DbRawSqlQuery<Empresa> data = dba.Database.SqlQuery<Empresa>
                                            (_SQLSupport.empresaSave, parametros);
                return data.ToList();
            }
        }
        #endregion

        //UPDATE EMPRESA
        #region UpdateEmpresa
        public void _updateEmpresa(string UUID, string empresaRUC, string empresaNOM,
                        string empresaDIR, string empresaTEL, bool estadoREG)
        {
            empresaTEL = empresaTEL == null ? "" : empresaTEL;
            using (var dba = new _Context())
            {
                SqlParameter[] parametros = _Utilidades._ParamsSQL
                    (new string[] { "@empresaRUC", "@empresaNOM", "@empresaDIR",
                        "@empresaTEL", "@UUID", "@estadoREG", "@isNew" },
                     new object[] {empresaRUC, empresaNOM, empresaDIR,
                         empresaTEL,UUID, estadoREG,false});
                dba.Database.ExecuteSqlCommand(_SQLSupport.empresaSave, parametros);
            }
        }
        #endregion

        #endregion

        //PROPIEDADES
        #region Propiedades
        
            //Total de registros
        #region proTotalREG
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