using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using AutomatizerSQL.Core;
using AutomatizerSQL.Core.HelperClass;

namespace _Helpers
{
    public class _SQLServer
    {

        AutomatizerDataAccesSqlClient bd = null;
        string m_Server = "Servidor";
        string m_Catalog = "TicketsMVC";
        public _SQLServer()
        {
            bd = new AutomatizerDataAccesSqlClient(m_Server, m_Catalog);
        }
        //OPEN-CLOSE CONEXION
        #region Conexion: Open-Close

        //cierra la conexion         
        #region Close
        public void _Close()
        {
            bd.CerrarConexion();
        }
        #endregion

        //abre la conexion
        #region Open
        public void _Open()
        {
            if (bd.Conexion.State == ConnectionState.Closed)
            {
                bd.AbrirConexion();
            }
        }
        #endregion

        #endregion

        //METODOS DE TRANSACCION
        #region Transacciones

        #region BeginTransacction
        public void _BeginTransacction()
        {
            bd.IniciarTransaccion();
        }
        #endregion

        #region CommitTransacction
        public void _Commit()
        {
            bd.ConfirmarTransaccion();
        }
        #endregion

        #region RollbackTransacction
        public void _RollBack()
        {
            bd.RevertirTransaccion();
        }
        #endregion

        #endregion

        //OPERACIONES CRUD 
        #region OPERACIONES_CON_DATOS

        //Agrega parametros
        #region AddParametros
        private SqlParameter[] _AddParametros(string[] strNombres, object[] objValores)
        {
            SqlParameter[] parametrosSQL = null;
            if (strNombres != null && objValores != null
                && strNombres.Length == objValores.Length)
            {
                parametrosSQL = new SqlParameter[strNombres.Length];
                for (int i = 0; i < strNombres.Length; i++)
                {
                    parametrosSQL[i] = new SqlParameter(strNombres[i], objValores[i]);
                }
            }
            return parametrosSQL;
        }
        #endregion

        //Carga un DataTable
        #region CargaDataTable
        public DataTable _CargaDataTable(string strInstruccionSQL, string[] strVariables, object[] objValores)
        {
            SqlParameter[] sqlParams = _AddParametros(strVariables, objValores);
            var obj = bd.CargarDatatable(strInstruccionSQL, sqlParams);
            obj.OnErrorThrow();
            DataTable dt = obj.resultado;
            return dt;
        }
        #endregion

        //Ejecuta una instruccion contra la BD
        #region Ejecutar
        public int _Ejecutar(string strInstruccionSQL, string[] strVariables, object[] objValores)
        {
            SqlParameter[] sqlParams = _AddParametros(strVariables, objValores);
            var obj = bd.Ejecutar(strInstruccionSQL, sqlParams);
            obj.OnErrorThrow();
            return obj.Resultado;
        }
        #endregion

        #endregion

        //UTILIDADES
        #region UTILIDADES

        //calcula el secuencial 
        #region CalculaCodigo
        public int _CalculaCodigo(string strNombreTabla, string strNombreCampo, string strFiltro,
                            string[] strVariables, object[] objValores)
        {
            _Open();
            string strInstruccionSQL = "SELECT MAX(" + strNombreCampo + ") AS Codigo FROM " + strNombreTabla;
            strInstruccionSQL += strFiltro.Trim() != "" ? " " + strFiltro : "";
            object objCodigo = bd.ExecuteScalar(strInstruccionSQL, _AddParametros(strVariables, objValores));
            objCodigo = _ValidaNulo(objCodigo);
            return Convert.ToInt32(objCodigo) + 1;

        }
        #endregion

        //valida que no sea nulo un campo
        #region ValidaNulo
        private object _ValidaNulo(object objValor)
        {
            return objValor == null || objValor == DBNull.Value || objValor.ToString().Trim() == "" ? 0 : objValor;
        }
        #endregion

        #endregion
    }
}
