using _Entidades.SentenciasSQL;
using _Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace _Entidades.specificModels
{
    public class TicketsDETModel
    {
        private string m_originCLI = "CLIENT";
        private string m_originSI = "SUPPORT";
        private int m_totalREG = 0;

        #region Modelo
        public int id { get; set; }

        public int TicketID { get; set; }

        public string TicketUUID { get; set; }

        public string TeamViewer { get; set; }

        public DateTime Fecha { get; set; }

        public int SecRespta { get; set; }

        public int Minutos { get; set; }

        public string Usuario { get; set; }

        public string Mensaje { get; set; }

        public string Observacion { get; set; }

        public string File1 { get; set; }
        public string File2 { get; set; }
        public string File3 { get; set; }

        public string UUID{get; set;}

        #endregion

        //Respuestas
        #region Answer-Support

        //Recupera la respuesta
        #region getAnswers
        public List<TicketsDETModel> _getAnswers(string id, int jtStartIndex = 0, int jtPageSize = 0)
        {
            using (var dba = new _Context())
            {
                SqlParameter[] parametros = _Utilidades._ParamsSQL(new string[] { "@UUID", "@startIndex", "@perPage", "@whoAsked","@TotalREG" },
                                                                       new object[] { id, jtStartIndex, jtPageSize, m_originSI ,0},
                                                                       new string[] { "@TotalREG"});
                DbRawSqlQuery<TicketsDETModel> data = dba.Database.SqlQuery<TicketsDETModel>
                                                            (_SQLCliente.RecuperaTicketsDET, parametros);
                List<TicketsDETModel> listado = data.ToList();
                m_totalREG = Convert.ToInt32(parametros[4].Value);
                return listado;
            }
        }
        #endregion

        //Guarda la respuesta
        #region saveAnswer
        public List<TicketsDETModel> _saveAnswer(string id, string Mensaje, string observacion, int Minutos)
        {
            using (var dba = new _Context())
            {
                int userId = _SessionHelper.GetUserID();
                SqlParameter[] parametros = _Utilidades._ParamsSQL(
                            new string[] {"@UsuarioID","@TicketUUID","@TeamViewer","@Minutos","@Mensaje",
                                    "@Observaciones","@Archivo1","@Archivo2","@Archivo3","@whoSend" },
                            new object[] {userId,id,"Tecnico",Minutos,Mensaje,
                                observacion,"","","",m_originSI});
                DbRawSqlQuery<TicketsDETModel> data = dba.Database.SqlQuery<TicketsDETModel>
                                                       (_SQLCliente.GuardaAnswer, parametros);
                return data.ToList();
            }
        }
        #endregion

        #endregion

        #region Answer-Client
        public List<TicketsDETModel> _getAnswerCLI(string id, int jtStartIndex = 0, int jtPageSize = 0)
        {
            using (var dba = new _Context())
            {
                SqlParameter[] parametros = _Utilidades._ParamsSQL(new string[] { "@UUID", "@startIndex", "@perPage", "@whoAsked","@TotalREG" },
                                                                        new object[] { id, jtStartIndex, jtPageSize, m_originCLI,0 },
                                                                        new string[] {"@TotalREG" });
                DbRawSqlQuery<TicketsDETModel> data = dba.Database.SqlQuery<TicketsDETModel>
                                                            (_SQLCliente.RecuperaTicketsDET, parametros);
                List<TicketsDETModel> listado = data.ToList();
                m_totalREG = Convert.ToInt32(parametros[4].Value);
                return listado;
            }
        }

        public List<TicketsDETModel> _saveAnswerCLI(string id, string teamViewer, string Mensaje, string observacion)
        {
            using (var dba = new _Context())
            {
                int usuarioID = _SessionHelper.GetUserID();
                SqlParameter[] parametros = _Utilidades._ParamsSQL(
                            new string[] {"@UsuarioID","@TicketUUID","@TeamViewer","@Minutos","@Mensaje",
                                    "@Observaciones","@Archivo1","@Archivo2","@Archivo3","@whoSend" },
                            new object[] {usuarioID,id,teamViewer,0,Mensaje,
                                observacion,"","","",m_originCLI});
                DbRawSqlQuery<TicketsDETModel> data = dba.Database.SqlQuery<TicketsDETModel>
                                                       (_SQLCliente.GuardaAnswer, parametros);

                return data.ToList();
            }
        }

        public TicketsDetalle _getAnswerUUID(string UUID)
        {
            using (var dba = new _Context())
            {
                List<TicketsDetalle>data=dba.TicketDetalle
                                        .Where(x=>x.UUID==UUID)
                                        .Where(x=>x.EstReg== true)
                                        .ToList();
                if (data != null && data.Count==1)
                {
                    return data[0];
                }   
            }
            return null;
        }

        #endregion

        public void _UploadFile(string id, string img, 
                string pathForSaving, HttpPostedFileBase myFile)
        {
            if (_CreateFolderIfNeeded(pathForSaving))
            {
                myFile.SaveAs(Path.Combine(pathForSaving, myFile.FileName));
                using (var dba = new _Context())
                {
                    SqlParameter[] parametros = _Utilidades._ParamsSQL(
                    new string[] { "@id", "@imageNumber", "@FileName" },
                    new object[] { id, img, myFile.FileName });
                    dba.Database.ExecuteSqlCommand(_SQLCliente.UploadImage, parametros);
                }
            }
        }

        private bool _CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }


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