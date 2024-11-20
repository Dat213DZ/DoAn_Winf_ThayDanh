using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class SanPhamDAL : ConnectDAL
    {
        public SanPhamDAL() {}
        public List<SanPham> LoadSP()
        {
            return qlBanHang.SanPhams.Select(l=>l).ToList();
        }
        public bool ThemSp(SanPham sanPham)
        {
            try
            {
                qlBanHang.SanPhams.InsertOnSubmit(sanPham);
                qlBanHang.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SuaSP(SanPham sanPham)
        {
            if(sanPham == null) {return false;}
            else
            {
                SanPham sp = qlBanHang.SanPhams.Where(l => l.ID_SanPham == sanPham.ID_SanPham).FirstOrDefault();
                if (sp != null)
                {
                    sp.TenSanPham = sanPham.TenSanPham;
                    sp.MoTa = sanPham.MoTa;
                    sp.Gia = sanPham.Gia;
                    sp.SoLuongTon = sanPham.SoLuongTon;
                    sp.HinhAnh = sanPham.HinhAnh;
                    sp.ID_DanhMuc = sanPham.ID_DanhMuc;
                    qlBanHang.SubmitChanges();
                    return true;
                }
            }
            return false;
        }
        public bool XoaSP(int id)
        {
            if (id == 0) return false;
            else
            {
                SanPham sp = qlBanHang.SanPhams.Where(l => l.ID_SanPham == id).FirstOrDefault();
                if (sp != null)
                {
                    qlBanHang.SanPhams.DeleteOnSubmit(sp);
                    qlBanHang.SubmitChanges();
                    return true;
                }
            }
            return false;
        }

    }
}
