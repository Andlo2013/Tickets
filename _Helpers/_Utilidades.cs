using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace _Helpers
{
    public static class _Utilidades
    {
        private static string key = "solintegWeb";

        public static SqlParameter[] _ParamsSQL(string[] paramsNames, object[] paramsValues,string[] outPutParameter=null)
        {
            SqlParameter[] result = null;
            if (paramsNames != null && paramsValues != null && paramsNames.Length == paramsValues.Length)
            {
                result = new SqlParameter[paramsNames.Length];
                for (int i = 0; i < paramsNames.Length; i++)
                {
                    SqlParameter p = new SqlParameter(paramsNames[i], paramsValues[i]);
                    if(outPutParameter!=null && outPutParameter.Contains(paramsNames[i]))
                    {
                        p.Direction = System.Data.ParameterDirection.Output;
                    }
                    result[i] = p;//new SqlParameter(paramsNames[i], paramsValues[i]);
                }
            }
            return result;
        }

        public static string _getMD5(this string texto)
        {
            texto = key + texto;
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(texto));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static string _getModelErrors(this ModelStateDictionary modelo)
        {
            var errorsArray = modelo.Where(x => x.Value.Errors.Any())
                        .Select(x => x.Value.Errors).ToList();
            string message = "";
            foreach (ModelErrorCollection error in errorsArray)
            {
                foreach (ModelError e in error)
                {
                    message += "-" + e.ErrorMessage + "</br>";
                }
            }
            return message;
        }

    }
}