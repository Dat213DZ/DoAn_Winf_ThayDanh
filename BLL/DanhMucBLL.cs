using System;
using System.Collections.Generic;
using DTO;
using DAL;
namespace BLL
{
    public class DanhMucBLL
    {
        DanhMucDAL dmDAL = new DanhMucDAL();
        public DanhMucBLL() { }
        public List<DanhMucSanPham> LoadDM()
        {
            return dmDAL.LoadDM();
        }
    }
}
