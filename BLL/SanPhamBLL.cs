using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class SanPhamBLL
    {
        SanPhamDAL spDAL = new SanPhamDAL();
        public SanPhamBLL() { }
        public List<SanPham> LoadSP()
        {
            return spDAL.LoadSP();
        }
        public bool ThemSP(SanPham sp)
        {
            return spDAL.ThemSp(sp);
        }
        public bool SuaSP(SanPham sp)
        {
            return spDAL.SuaSP(sp);
        }
        public bool XoaSP(int id)
        {
            return spDAL.XoaSP(id);
        }
    }
}
