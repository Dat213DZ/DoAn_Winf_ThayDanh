using System;
using System.Collections.Generic;
using System.Linq;
using DTO;
namespace DAL
{
    public class DanhMucDAL : ConnectDAL
    {
        public DanhMucDAL() { }

        public List<DanhMucSanPham> LoadDM()
        {
            return qlBanHang.DanhMucSanPhams.Select(l => l).ToList();
        }
    }
}
