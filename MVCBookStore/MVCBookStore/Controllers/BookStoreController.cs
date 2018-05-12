using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBookStore.Models;

using PagedList;
using PagedList.Mvc;


namespace MVCBookStore.Controllers
{
    public class BookStoreController : Controller
    {
        // GET: BookStore
        //tao mot doi tuong chua toan bo CSDLtu dbQLBanSach
        dbQLBansachDataContext data = new dbQLBansachDataContext();

        private List<SACH> Laysachmoi(int count)
        {
           //Sap xep giam dan theo ngay cap nhat, lay count dong dau
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Index(int ? page)
        {
            //tao bien quy dinh so san pham tren moi trang
            int pageSize = 5;
            //tao bien so trang
            int pageNum = (page ?? 1);

            //Lay  quyen sach moi nhat
            var sachMoi = Laysachmoi(15);
            return View(sachMoi.ToPagedList(pageNum,pageSize));
        }

        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }
        public ActionResult Nhaxuatban()
        {
            var nhaxuatban = from nxb in data.NHAXUATBANs select nxb;
            return PartialView(nhaxuatban);
        }
        public ActionResult SPTheochude(int id)
        {
            var sach = from s in data.SACHes where s.MaCD == id select s;
            return View(sach);
        }
        public ActionResult SPTheoNXB(int id)
        {
            var sach = from s in data.SACHes where s.MaNXB == id select s;
            return View(sach);  
        }
        public ActionResult Details(int id)
        {
            var sach = from s in data.SACHes
                       where s.Masach == id
                       select s;
            return View(sach.Single());
        }
    }
}