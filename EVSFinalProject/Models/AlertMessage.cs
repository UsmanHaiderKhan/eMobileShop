using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SirAliClass.Models
{
    public class AlertMessage
    {
        public AlertMessage() { }

        public AlertMessage(string contents,AlertMessageType type)
        {
            switch (type)
            {
                case AlertMessageType.Error:
                    cssClass = "alert-danger";
                    heading = "Error!";
                    break;
                case AlertMessageType.Success:
                    cssClass = "alert-success";
                    heading = "Success!";
                    break;
                case AlertMessageType.Warning:
                    cssClass = "alert-warning";
                    heading = "Warning!";
                    break;
                default:
                    cssClass = "alert-info";
                    heading = "Information!";
                    break;
            }
            this.contents = contents;
        }


        private string cssClass;
        public string CSSClass { get { return cssClass; } }

        private string heading;
        public string Heading { get { return heading; } }

        private string contents;
        public string Content { get { return contents; } }
    }

    public enum AlertMessageType
    {
        Success,
        Information,
        Error,
        Warning
    }
}