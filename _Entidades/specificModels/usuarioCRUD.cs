using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using _Helpers;
using _Entidades.SentenciasSQL;

namespace _Entidades.specificModels
{
    public class usuarioCRUD
    {

        private int m_totalREG = 0;

        //Propiedades de la clase
        #region Propiedades
        public int id { get; set; }
        public int tipo { get; set; }
        public string UUID { get; set; }

        [Required(ErrorMessage="El campo RUC es obligatorio")]
        public string EmpresaRUC { get; set; }

        [Required(ErrorMessage = "El campo Empresa es obligatorio")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio")]
        [EmailAddress(ErrorMessage ="El campo email no tiene un formato válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Password es obligatorio")]
        public string Password { get; set; }

        public string Horario { get; set; }

        [Required(ErrorMessage = "El campo Estado es obligatorio")]
        public bool Estado { get; set; }

        #endregion

        public usuarioCRUD()
        {

        }

        //MÉTODOS DE LA CLASE
        #region MÉTODOS

        //Recupera todos los usuarios
        #region getUsuarios
        public List<usuarioCRUD> _getUsuarios(int jtStartIndex = 0, int jtPageSize = 0,
                                        string campoBuscar = "", string textoBuscar = "")
        {
            using (var dba = new _Context())
            {
                SqlParameter[] parametros = _Utilidades._ParamsSQL
                    (new string[] { "@startIndex","@perPage","@CampoBuscar","@valorBuscar","@TotalREG" }, 
                    new object[] { jtStartIndex, jtPageSize, campoBuscar, textoBuscar,0 },
                    new string[] { "@TotalREG" });

                DbRawSqlQuery<usuarioCRUD> data = dba.Database.SqlQuery<usuarioCRUD>
                                            (_SQLSupport.usuarioIndex, parametros);
                List<usuarioCRUD> listado = data.ToList();
                m_totalREG = Convert.ToInt32(parametros[4].Value);
                return listado;
            }
        }
        #endregion

        //Guarda el usuario
        #region saveUsuario
        public List<usuarioCRUD> _saveUsuario(string UUID,string Nombre,string Email,
            string Password,string Horario,string EmpresaRUC,bool Estado,bool isNew)
        {
            
            Password = Password.Trim() != "" ? Password._getMD5(): "";
            using (var dba = new _Context())
            {
                SqlParameter[] parametros = _Utilidades._ParamsSQL(
                            new string[] { "@UUID", "@Nombre", "@Email",
                                "@Password", "@Horario","@EmpresaRUC",
                                "@Estado",  "@isNew" },
                            new object[] { UUID, Nombre, Email, Password,
                                        Horario,EmpresaRUC,Estado,isNew});
                DbRawSqlQuery<usuarioCRUD> data = dba.Database.SqlQuery<usuarioCRUD>
                                            (_SQLSupport.usuarioSave, parametros);
                return data.ToList();
            }
        }
        #endregion

        //Valida el ingreso
        #region Acceder
        public bool _Acceder(string email, string password)
        {
            using (var dba = new _Context())
            {
                password = password._getMD5();
                SqlParameter[] parametros = _Utilidades._ParamsSQL(
                            new string[] { "@email", "@password" },
                            new object[] { email, password });
                DbRawSqlQuery<usuarioCRUD> data = dba.Database.SqlQuery<usuarioCRUD>
                                            (_SQLSupport.usuarioLogin, parametros);
                List<usuarioCRUD> userList = data.ToList();
                if (userList != null && userList.Count == 1)
                {
                    _SessionHelper.AddUserToSession(userList[0].id.ToString(), userList[0].tipo.ToString());
                    return true;
                }
            }
            return false;
        }
        #endregion

        //Recupera el usuario logueado
        #region getUsuario
        public Usuario _getUsuario()
        {
            using (var dba = new _Context())
            {
                int userID = _SessionHelper.GetUserID();
                Usuario user = dba.Usuario.Where(x => x.id == userID).SingleOrDefault();
                if (user != null)
                {
                    return user;
                }
                return user;
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
