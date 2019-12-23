using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AdministrationTool.Web.Models
{
    public class HomeViewModel
    {
        public string Message { get; set; }

        public HomeViewModel()
        {
            Message = ConfigurationManager.AppSettings["Message"];
        }
    }
}