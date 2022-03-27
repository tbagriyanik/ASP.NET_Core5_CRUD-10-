using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using denemeVT.Data;
using denemeVT.Models;

namespace denemeVT.Pages
{
    public class TabloIslemlerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public string mesaj = "Hoşgeldiniz (C)reate (R)ead (U)pdate (D)elete";
        public Tablo gecici;        //update için
        public string aramaMetni { get; set; }

        private float say;

        public TabloIslemlerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Tablo> Tablo { get; set; }

        public async Task OnGetAsync()
        {
            Tablo = await _context.Tablo.ToListAsync();
        }

        public void OnPostYeni()
        {
            if (!String.IsNullOrEmpty(Request.Form["metin"]) && float.TryParse(Request.Form["sayi"], out say))
            {
                Tablo tablo = new Tablo();              //yeni kayıt aç
                tablo.metin = Request.Form["metin"];
                tablo.sayi = (float)Convert.ToDouble(Request.Form["sayi"]);
                _context.Tablo.Add(tablo);              //ekle
                _context.SaveChanges();                 //kaydet
                mesaj = Request.Form["metin"] + " eklendi...";
            }
            else
            {
                mesaj = "Boş giriş veya sayı olmayan giriş vardı!";
            }

            Tablo = _context.Tablo.ToList();        //yeni halini getir
        }

        public void OnPostGuncelle()
        {
            if (!String.IsNullOrEmpty(Request.Form["metin"]) && float.TryParse(Request.Form["sayi"], out say))
            {
                string gelenMetin = Request.Form["metin"];
                string gelenSayi = Request.Form["sayi"];
                int gelenID = Convert.ToInt32(Request.Form["id"]);
                var tablo = _context.Tablo.Find(gelenID);    //bul
                tablo.metin = gelenMetin;
                tablo.sayi = (float)Convert.ToDouble(gelenSayi);
                _context.SaveChanges();                 //kaydet
                mesaj = gelenMetin + " güncellendi...";
                gecici = null;                          //güncelleme işlemi iptal olsun
            }
            else
            {
                mesaj = "Boş giriş veya sayı olmayan giriş vardı!";
            }

            Tablo = _context.Tablo.ToList();        //yeni halini getir
        }

        public void OnGetSil(int id)
        {
            var tablo = _context.Tablo.Find(id);    //bul
            if (tablo != null)
            {
                mesaj = tablo.metin.ToString() + " silindi...";
                _context.Tablo.Remove(tablo);           //sil
                _context.SaveChanges();                 //kaydet
            }
            else
            {
                mesaj = id + " numaralı silinecek kayıt bulunamadı!";
            }

            Tablo = _context.Tablo.ToList();        //yeni halini getir
        }

        public void OnGetDuzenle(int id)
        {
            gecici = _context.Tablo.Find(id);    //bul
            if (gecici != null)
            {
                mesaj = gecici.metin.ToString() + " düzenleniyor...";
            }
            else
            {
                mesaj = id + " düzenlenecek kayıt bulunamadı!";
            }

            Tablo = _context.Tablo.ToList();        //yeni halini getir
        }

        public void OnGetSirala(string siralama)
        {
            if (siralama == "metin")
            {
                mesaj = "Metin'e göre sıralandı...";
                Tablo = _context.Tablo
                    .OrderBy(x => x.metin)
                    .ToList();
            }
            else if (siralama == "sayi")
            {
                mesaj = "Sayı'ya göre sıralandı...";
                Tablo = _context.Tablo
                    .OrderBy(x => x.sayi)
                    .ToList();
            }
            else
            {
                mesaj = "Sıralama yapamadım!";
                Tablo = _context.Tablo.ToList();
            }
        }

        public void OnGetArama(string aranan)
        {
            aramaMetni = aranan;

            if (!String.IsNullOrEmpty(aranan))
            {
                Tablo = _context.Tablo
                    .Where(x => x.metin.Contains(aranan)
                            || x.sayi.ToString().Contains(aranan))
                    .ToList();
                if (Tablo.Count > 0)
                    mesaj = aranan + " arandı, " + Tablo.Count + " adet bulundu...";
                else
                {
                    Tablo = _context.Tablo.ToList();
                    mesaj = aranan + " arandı, böyle bilgi yoktur...";
                }
            }
            else
            {
                Tablo = _context.Tablo.ToList();
                mesaj = "Arama yapamadım!";
            }
        }

    }
}
