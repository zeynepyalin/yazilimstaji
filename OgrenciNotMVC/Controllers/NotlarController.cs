﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMVC.Models.EntityFramework;
using OgrenciNotMVC.Models;

namespace OgrenciNotMVC.Controllers
{
    public class NotlarController : Controller
    {
        // GET: Notlar
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index()
        {
            var sinavnotlari = db.TBLNOTLAR.ToList();
            return View(sinavnotlari);
        }
        [HttpGet]
        public ActionResult YeniSinav()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniSinav(TBLNOTLAR tbn)
        {
            db.TBLNOTLAR.Add(tbn);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult NotGetir(int id)
        {
            var notlar = db.TBLNOTLAR.Find(id);
            return View("NotGetir", notlar);
        }
        [HttpPost]
        public ActionResult NotGetir(Class1 model,TBLNOTLAR p, int SINAV1=0, int SINAV2=0, int SINAV3=0, int PROJE=0)
        {
            if (model.islem=="HESAPLA")
            {
                //islem 1
                int ORTALAMA = (SINAV1 + SINAV2 + SINAV3 + PROJE) / 4;
                ViewBag.ort = ORTALAMA;
            }
            if (model.islem=="NOTGUNCELLE")
            {
                //islem 2
                var snv = db.TBLNOTLAR.Find(p.NOTID);
                snv.SINAV1 = p.SINAV1;
                snv.SINAV2 = p.SINAV2;
                snv.SINAV3 = p.SINAV3;
                snv.PROJE = p.PROJE;
                snv.ORTALAMA = p.ORTALAMA;
                db.SaveChanges();
                return RedirectToAction("Index", "Notlar");
            }
            return View();
        }
    }
}