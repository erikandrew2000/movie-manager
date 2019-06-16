using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFMovie.Web.ViewModels;

namespace EFMovie.Web.Controllers
{
    public enum AlertType { success, danger, warning, info};

    public class BaseController : Controller
    {
        public void Alert(string message, AlertType type = AlertType.info)
        {
            TempData["Alert.Message"] = message;
            TempData["Alert.Type"] = type.ToString();
        }
    }
}