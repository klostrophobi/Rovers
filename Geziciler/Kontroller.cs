using System;
using System.Collections.Generic;
using System.Text;

namespace Geziciler
{
    public class Kontroller
    {
        public bool GirdiKontrolEt(int girdiNo, string girdi)
        {
            var karakterler = girdi.Replace(" ","").ToCharArray();

            if (girdiNo == 1)
            {
                if (girdi.Replace(" ", "").Length != 2)
                {
                    Console.WriteLine("Boyutu yanlış girdiniz.");
                    return false;
                }

                foreach (char c in karakterler)
                {
                    if (!Char.IsDigit(c))
                    {
                        Console.WriteLine("Girdi hatalı! Sadece sayı değerleri kabul ediliyor...");
                        return false;
                    }
                }
            }
            else if (girdiNo == 2 || girdiNo == 4)
            {
                if (girdi.Replace(" ", "").Length != 3)
                {
                    Console.WriteLine("Konumu yanlış girdiniz.");
                    return false;
                }

                if (!Char.IsDigit(karakterler[0]))
                {
                    Console.WriteLine("Girdi hatalı! İlk iki değer koordinat olduğu için sayı değerleri kabul ediliyor...");
                    return false;
                }

                if (!Char.IsDigit(karakterler[1]))
                {
                    Console.WriteLine("Girdi hatalı! İlk iki değer koordinat olduğu için sayı değerleri kabul ediliyor...");
                    return false;
                }

                if (!Char.IsLetter(karakterler[2]))
                {
                    Console.WriteLine("Girdi hatalı! Son değer yön olduğu için harf türünde değerler kabul ediliyor...");
                    return false;
                }
                else if (karakterler[2] != 'N' && karakterler[2] != 'E' && karakterler[2] != 'W' && karakterler[2] != 'S')
                {
                    Console.WriteLine("Girdi hatalı! Sadece N (North), E (East), W (West), S (South) değerleri kabul ediliyor...");
                    return false;
                }

            }
            else if (girdiNo == 3 || girdiNo == 5)
            {
                foreach (char c in karakterler)
                {
                    if (!Char.IsLetter(c))
                    {
                        Console.WriteLine("Girdi hatalı! Sadece harf türünde değerler kabul ediliyor...");
                        return false;
                    }
                    else if(c != 'L' && c != 'R' && c != 'M')
                    {
                        Console.WriteLine("Girdi hatalı! Sadece L (Left), R (Right), M (Move) değerleri kabul ediliyor...");
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
