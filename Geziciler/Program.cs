using System;

namespace Geziciler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Kontroller kontroller = new Kontroller();
            Gezici gezici = new Gezici();
            Gezici gezici2 = new Gezici();

            #region Plato bilgisi

            var boyut = BoyutBilgisiAl();
            if (!kontroller.GirdiKontrolEt(1, boyut))
                return;

            var platoBoyutuDegiskenleri = boyut.Replace(" ", "").ToCharArray();

            #endregion

            #region Gezici 1

            Console.WriteLine("Gezici 1 için konum giriniz. Örneğin: 1 2 N");
            var geziciKonum1 = Console.ReadLine().ToUpper();
            if (!kontroller.GirdiKontrolEt(2, geziciKonum1))
                return;

            GeziciyeBilgiAta(1, gezici, geziciKonum1.Replace(" ", "").ToCharArray());

            Console.WriteLine("Gezici 1 için hareket belirleyin. Örneğin: LMLMLMLMM");
            var geziciKomut1 = Console.ReadLine().ToUpper();
            if (!kontroller.GirdiKontrolEt(3, geziciKomut1))
                return;

            GeziciyiHareketEttir(1, gezici, geziciKomut1);

            #endregion

            #region Gezici 2

            Console.WriteLine("Gezici 2 için konum giriniz. Örneğin: 3 3 E");
            var geziciKonum2 = Console.ReadLine().ToUpper();
            if (!kontroller.GirdiKontrolEt(4, geziciKonum2))
                return;

            GeziciyeBilgiAta(2, gezici2, geziciKonum2.Replace(" ", "").ToCharArray());

            Console.WriteLine("Gezici 1 için hareket belirleyin. Örneğin: MMRMMRMRRM");
            var geziciKomut2 = Console.ReadLine().ToUpper();
            if (!kontroller.GirdiKontrolEt(5, geziciKomut2))
                return;

            GeziciyiHareketEttir(2, gezici2, geziciKomut2);

            #endregion

            #region Girilen Komutlar

            Console.WriteLine("\nGirilen Komutlar\n");
            Console.WriteLine(boyut);
            Console.WriteLine(geziciKonum1);
            Console.WriteLine(geziciKomut1);
            Console.WriteLine(geziciKonum2);
            Console.WriteLine(geziciKomut2);
            Console.WriteLine("\n");

            #endregion

            if (gezici.GeziciDurum.X > Convert.ToInt32(platoBoyutuDegiskenleri[0].ToString()) || gezici.GeziciDurum.X < 0 ||
                gezici.GeziciDurum.Y > Convert.ToInt32(platoBoyutuDegiskenleri[1].ToString()) || gezici.GeziciDurum.Y < 0)
            {
                Console.WriteLine("Gezici " + gezici.ToString() + " platodan çıkmıştır. Lütfen girdilerinizi kontrol edin. Bulunduğu koordinat: "
                    + gezici.GeziciDurum.X + "," + gezici.GeziciDurum.Y);
            }
            else
            {
                Console.WriteLine("Gezici 1 son konumu: " + gezici.GeziciDurum.X + " " + gezici.GeziciDurum.Y + " " + (Yon)gezici.GeziciDurum.Yon);
            }

            if (gezici2.GeziciDurum.X > Convert.ToInt32(platoBoyutuDegiskenleri[0].ToString()) || gezici2.GeziciDurum.X < 0 ||
                gezici2.GeziciDurum.Y > Convert.ToInt32(platoBoyutuDegiskenleri[1].ToString()) || gezici2.GeziciDurum.Y < 0)
            {
                Console.WriteLine("Gezici " + gezici2.ToString() + " platodan çıkmıştır. Lütfen girdilerinizi kontrol edin. Bulunduğu koordinat: "
                    + gezici2.GeziciDurum.X + "," + gezici2.GeziciDurum.Y);
            }
            else
            {
                Console.WriteLine("Gezici 2 son konumu: " + gezici2.GeziciDurum.X + " " + gezici2.GeziciDurum.Y + " " + (Yon)gezici2.GeziciDurum.Yon);
            }

        }
        public enum Yon
        {
            N = 90,
            E = 0,
            W = 180,
            S = 270
        }

        /// <summary>
        /// Kullanıcıdan aldığı bilgiyi plato büyüklüğü olarak döner
        /// </summary>
        /// <returns>Plato büyüklüğü</returns>
        public static string BoyutBilgisiAl()
        {
            Console.WriteLine("Plato boyutu giriniz. Örneğin: 5 5");
            var boyut = Console.ReadLine();
            return boyut;
        }

        /// <summary>
        /// Geziciye kullanıcıdan alınan konum ve geziciye verilen komutları atar.
        /// </summary>
        /// <param name="geziciNo">Gezici numarası</param>
        /// <param name="g">Gezici objesi</param>
        /// <param name="bilgi">Input bilgiler</param>
        public static void GeziciyeBilgiAta(int geziciNo, Gezici g, Char[] bilgi)
        {
            g.GeziciNo = geziciNo;

            Gezici.Durum d = new Gezici.Durum();
            d.X = Convert.ToInt32(bilgi[0].ToString());
            d.Y = Convert.ToInt32(bilgi[1].ToString());
            d.Yon = YonuSayiyaCevir(bilgi[2].ToString());

            g.GeziciDurum = d;
        }

        /// <summary>
        /// Geziciye verilen komutları sırasıyla uygular. Belli tipte komutlar kabul edilir.
        /// </summary>
        /// <param name="geziciNo">Gezici numarası</param>
        /// <param name="g">Gezici objesi</param>
        /// <param name="komut">Input bilgiler</param>
        public static void GeziciyiHareketEttir(int geziciNo, Gezici g, string komut)
        {
            var komutListesi = komut.Replace(" ", "").ToCharArray();

            for (int i = 0; i < komutListesi.Length; i++)
            {
                if (komutListesi[i] == 'L' || komutListesi[i] == 'R')
                    MevcutYonuAyarla(komutListesi[i], g);
                else if (komutListesi[i] == 'M')
                    MevcutYondeHareketEt(g);
            }
        }

        /// <summary>
        /// Gezicinin yönünü ayarlamak için kullanılır. Yön değerini açı değerine çevirir.
        /// </summary>
        /// <param name="harf">Gezicinin mevcut yönü</param>
        /// <returns>Açı değeri</returns>
        public static int YonuSayiyaCevir(string harf)
        {
            switch (harf)
            {
                case "N":
                    return 90;
                case "W":
                    return 180;
                case "S":
                    return 270;
                case "E":
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Komuta göre yön değiştirir. İki boyutlu sayı grafiği düşündüğümüzde +X ekseni 0 derece kabul edip saat yönünün tersi yönde ilerledim.
        /// </summary>
        /// <param name="gelenKomut">Dönüş yönü</param>
        /// <param name="gezici">Gezici objesi</param>
        public static void MevcutYonuAyarla(Char gelenKomut, Gezici gezici)
        {
            switch (gelenKomut)
            {
                case 'L':
                    gezici.GeziciDurum.Yon += 90;
                    break;
                case 'R':
                    gezici.GeziciDurum.Yon -= 90;
                    break;
                default:
                    break;
            }

            if (gezici.GeziciDurum.Yon >= 360)
                gezici.GeziciDurum.Yon = gezici.GeziciDurum.Yon % 360;
            else if (gezici.GeziciDurum.Yon < 0)
                gezici.GeziciDurum.Yon += 360;
        }

        /// <summary>
        /// Geziciyi belirlenen eksenlerde mevcut yönde bir birim ilerletir.
        /// </summary>
        /// <param name="gezici">Gezici objesi</param>
        public static void MevcutYondeHareketEt(Gezici gezici)
        {
            if (gezici.GeziciDurum.Yon == (int)Yon.N)
            {
                gezici.GeziciDurum.Y++;
            }
            else if (gezici.GeziciDurum.Yon == (int)Yon.E)
            {
                gezici.GeziciDurum.X++;
            }
            else if (gezici.GeziciDurum.Yon == (int)Yon.S)
            {
                gezici.GeziciDurum.Y--;
            }
            else if (gezici.GeziciDurum.Yon == (int)Yon.W)
            {
                gezici.GeziciDurum.X--;
            }
        }
    }
}
