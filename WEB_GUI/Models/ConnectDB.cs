using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;
namespace WEB_GUI.Models
{
    public class ConnectDB
    {
        public ConnectDB() { }
        QuanLiBanHangDataContext qlBanHang = new  QuanLiBanHangDataContext();

    }
}