using System.Web.Mvc;
using WEB_GUI.Models;

namespace WEB_GUI.Controllers
{
    public class SanPhamController : Controller
    {
        private ConnectDB db = new ConnectDB();

        public ActionResult Index()
        {
            var sanPhams = db.GetAllSanPham();
            return View(sanPhams);
        }
    }
} 