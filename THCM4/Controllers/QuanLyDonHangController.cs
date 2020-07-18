using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using THCM4.Models;
using System.Net.Mail;

namespace THCM4.Controllers
{
    //[Authorize(Roles = "AD,QLDH")]
    public class QuanLyDonHangController : Controller
    {
        // GET: QuanLyDonHang
        WebSite1Entities dt = new WebSite1Entities();
        public ActionResult ChuaThanhToan()
        {
            var lstChuaThanhToan = dt.DonDatHang.Where(n => n.DaThanhToan == false);

            return View(lstChuaThanhToan);
        }
        public ActionResult DaThanhToan()
        {
            var lstDaThanhToan = dt.DonDatHang.Where(n => n.DaThanhToan == true&&n.Dagiao==false);

            return View(lstDaThanhToan);
        }
        public ActionResult DaGiaoThanhToan()
        {
            var lstThanhToan = dt.DonDatHang.Where(n => n.DaThanhToan == true &&n.Dagiao==true);

            return View(lstThanhToan);
        }
        [HttpGet]
        public ActionResult DuyetDonHang(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang ddh = dt.DonDatHang.SingleOrDefault(n=>n.MaDDH==id);
            if(ddh==null)
            {
                return HttpNotFound();
            }
            var lstChiTietDonHang = dt.CTDH.Where(n => n.MaDDH == id);
            ViewBag.lstCTDH = lstChiTietDonHang;
            return View(ddh);
        }
        [HttpPost]
        public ActionResult DuyetDonHang(DonDatHang ddh,KhachHang kh)
        {
            DonDatHang ddhUpdate = dt.DonDatHang.SingleOrDefault(n => n.MaDDH == ddh.MaDDH);
            ddhUpdate.DaThanhToan = ddh.DaThanhToan;
            ddhUpdate.Dagiao = ddh.Dagiao;
            dt.SaveChanges();
            var lstChiTietDonHang = dt.CTDH.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.lstCTDH = lstChiTietDonHang;
            SendMail("xac nhan gui", "5851071050@st.utc2.edu.vn", "satthuDARK96@gmail.com", "Thegian03","xác nhận");
            return View(ddhUpdate);
        }
        public void SendMail(string Tieude,string nhan,string tk,string pw,string noidung)
        {
            MailMessage mail = new MailMessage();
        

            mail.From = new MailAddress(tk);
            mail.To.Add(nhan);
            mail.Subject = Tieude;
            mail.Body = noidung;
            mail.IsBodyHtml = true;

            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(tk, pw);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
     
        }
    }
}