namespace TrenRezervasyonAPI
{
    public class RezervasyonSonuc
    {
        public bool RezervasyonYapilabilir { get; set; }=false;
        public List<YerlesimAyrinti> YerlesimAyrinti { get; set; } 
        public RezervasyonSonuc()
        {
            YerlesimAyrinti = new List<YerlesimAyrinti>();
        }
    }
    public class YerlesimAyrinti
    {
        public string VagonAdi { get; set; }
        public int KisiSayisi { get; set; }
    }
}
