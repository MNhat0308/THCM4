using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace THCM4.Models
{
    public class GioHang
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal  DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }

        public GioHang(int IMaSP)
        {
            using (WebSite1Entities dt = new WebSite1Entities())
            {
                this.MaSP = IMaSP;
                SanPham sp = dt.SanPham.Single(n => n.MaSP == IMaSP);
                TenSP = sp.TenSP;
                DonGia = sp.DonGia.Value;
                HinhAnh = sp.HinhAnh;
                this.SoLuong = 1;
                
                this.ThanhTien = DonGia * SoLuong;

            }
        }
        public  GioHang(int IMaSP,int sl)
        {
            using (WebSite1Entities dt=new WebSite1Entities())
            {
                this.MaSP = IMaSP;
                SanPham sp = dt.SanPham.Single(n => n.MaSP == IMaSP);
                TenSP = sp.TenSP;
                DonGia = sp.DonGia.Value;
                HinhAnh = sp.HinhAnh;
                this.SoLuong = sl;
                this.ThanhTien = DonGia * SoLuong;

            }
        }
        public GioHang()
        {

        }


    }
}