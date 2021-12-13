using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelPractice.Models;
using System.Drawing;
using System.IO;
using HotelPractice.ViewModel;
using AutoMapper;

namespace HotelPractice.Controllers.Area
{
    public class HotelController : Controller
    {
        private HotelEntities db = new HotelEntities();
        List<Hotel> data = new List<Hotel>();
        // GET: Hotel
        public ActionResult Index()
        {
            // 定義Post是來源的Class而IndexViewModel是最後結果

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<HotelMaster, Hotel>();
            });

            IMapper mapper = config.CreateMapper();
            //var source = new Source();
            //var dest = mapper.Map<Source, Dest>(source);
            // 把List<Post>轉成List<IndexViewModel>
            var viewModel2 = mapper.Map<List<Hotel>>(db.HotelMaster.ToList());

            //return View(db.HotelMaster.ToList());
            return View(viewModel2);
        }

        // GET: Hotel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelMaster hotelMaster = db.HotelMaster.Find(id);
            if (hotelMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = id;
            return View(hotelMaster);
        }

        // GET: Hotel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hotel/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HotelId,HotelName,HtlTel,HtlAddress,HtlPic")] HotelMaster hotelMaster, HttpPostedFileBase file)
        {
            //2進位
            if (file.ContentLength != 0)
            {
                string strPath = Request.PhysicalApplicationPath + @"HotelImage\" + file.FileName;
                file.SaveAs(strPath);

                //將上傳的檔案轉成二進位
                var imgSize = file.ContentLength;
                byte[] imgByte = new Byte[imgSize];
                file.InputStream.Read(imgByte, 0, imgSize);

                hotelMaster.HtlByteImage = imgByte;
                hotelMaster.HtlPic = file.FileName;
            }

            if (ModelState.IsValid)
            {
                db.HotelMaster.Add(hotelMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotelMaster);
        }

        // GET: Hotel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelMaster hotelMaster = db.HotelMaster.Find(id);
            if (hotelMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = id;
            return View(hotelMaster);
        }

        // POST: Hotel/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( HotelMaster hotelMaster, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string filename = "";
                if (file == null)
                {
                    hotelMaster.HtlByteImage = hotelMaster.HtlByteImage;
                    hotelMaster.HtlPic = hotelMaster.HtlPic;
                }
                else
                {
                    filename = file.FileName;
                    string strPath = Request.PhysicalApplicationPath + @"HotelImage\" + filename;
                    file.SaveAs(strPath);

                    //將上傳的檔案轉成二進位
                    var imgSize = file.ContentLength;
                    byte[] imgByte = new Byte[imgSize];
                    file.InputStream.Read(imgByte, 0, imgSize);

                    hotelMaster.HtlByteImage = imgByte;
                    hotelMaster.HtlPic = file.FileName;
                }
         
            

                db.Entry(hotelMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotelMaster);
        }

        // GET: Hotel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelMaster hotelMaster = db.HotelMaster.Find(id);
            if (hotelMaster == null)
            {
                return HttpNotFound();
            }
            return View(hotelMaster);
        }

        // POST: Hotel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HotelMaster hotelMaster = db.HotelMaster.Find(id);
            db.HotelMaster.Remove(hotelMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //回傳實際圖檔
        public ActionResult GetImage(string fileName)
        {
            return File("~/HotelImage/" + fileName, "image/jpeg");
        }

        //回傳二進位圖檔
        public ActionResult GetImageByte(int id = 1)
        {
            HotelMaster hotelMaster = db.HotelMaster.Find(id);
            byte[] img = hotelMaster.HtlByteImage;
            return File(img, "image/jpeg");
        }
    }
}
