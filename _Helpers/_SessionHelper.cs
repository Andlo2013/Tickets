using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;

namespace _Helpers
{
    public class _SessionHelper
    {
        public static bool ExistUserInSession()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public static void DestroyUserSession()
        {
            FormsAuthentication.SignOut();
        }

        public static int GetUserID()
        {
            int user_id = 0;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    string[] data = ticket.UserData.Split('|');
                    if (data.Length == 2)
                    {
                        user_id = Convert.ToInt32(data[0]);
                    }
                }
            }
            return user_id;
        }

        public static int GetUserType()
        {
            int user_type = 0;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    string[] data = ticket.UserData.Split('|');
                    if (data.Length == 2)
                    {
                        user_type = Convert.ToInt32(data[1]);
                    }
                }
            }
            return user_type;
        }

        public static void AddUserToSession(string idUsuario,string tipoUsuario)
        {
            string dataCookie = idUsuario+"|"+tipoUsuario;
            bool persist = true;
            var cookie = FormsAuthentication.GetAuthCookie("support", persist);

            cookie.Name = FormsAuthentication.FormsCookieName;
            cookie.Expires = DateTime.Now.AddHours(1);

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, dataCookie);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);

        }

    }
}