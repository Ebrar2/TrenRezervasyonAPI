using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrenRezervasyonAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        [HttpGet]
        public string Giris()
        {
            return "Merhabalar";
        }
        [HttpPost]
        public RezervasyonSonuc RezervasyonYap(RezervasyonGiris rezervasyon)
        {
           RezervasyonSonuc rezervasyonSonuc = new RezervasyonSonuc();
           List<YerlesimAyrinti> yerlesimler = new List<YerlesimAyrinti>();
           int rezervasyonYapilacakKisiSayisi = rezervasyon.RezervasyonYapilacakKisiSayisi;
          foreach (var vagon in rezervasyon.Tren.Vagonlar)
            {
                int bosKoltukSay =(int) (vagon.Kapasite * 0.7 - vagon.DoluKoltukAdet);
                if(bosKoltukSay>0)
                {
                    if (!rezervasyon.KisilerFarkliVagonlaraYerlestirilebilir)
                    {
                        if (bosKoltukSay == rezervasyonYapilacakKisiSayisi)
                        {
                            yerlesimler.Add(new YerlesimAyrinti() { VagonAdi = vagon.Ad, KisiSayisi = rezervasyonYapilacakKisiSayisi });
                            rezervasyonSonuc.RezervasyonYapilabilir = true;
                            rezervasyonSonuc.YerlesimAyrinti = yerlesimler;
                            return rezervasyonSonuc;
                        }
                    }
                    else
                    {
                        int kisiSay = rezervasyonYapilacakKisiSayisi < bosKoltukSay ? rezervasyonYapilacakKisiSayisi :bosKoltukSay;
                        rezervasyonYapilacakKisiSayisi = rezervasyonYapilacakKisiSayisi - kisiSay;
                        yerlesimler.Add(new YerlesimAyrinti() { VagonAdi = vagon.Ad, KisiSayisi = kisiSay });
                    }
                    if(rezervasyonYapilacakKisiSayisi==0)
                    {
                        rezervasyonSonuc.RezervasyonYapilabilir = true;
                        rezervasyonSonuc.YerlesimAyrinti = yerlesimler;
                        return rezervasyonSonuc;
                    }
                }

            }
            return rezervasyonSonuc;
        }
    }
}
