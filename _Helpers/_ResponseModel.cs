using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _Helpers
{
    public class _ResponseModel
    {
        public dynamic pro_data { get; set; }
        public bool pro_isComplete { get; set; }
        public string pro_message { get; set; }

        public string pro_function{get;set;}

        public _ResponseModel()
        {
            pro_isComplete = false;
            pro_message = "Error al procesar la petición";
        }

        public void  _setResponse(bool isComplete,string message="")
        {
            this.pro_isComplete = isComplete;
            this.pro_message = message;
            if (!isComplete && message.Trim() == "")
            {
                pro_message = "Error al procesar la petición";
            }
        }


    }
}