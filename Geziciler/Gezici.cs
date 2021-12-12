using System;
using System.Collections.Generic;
using System.Text;

namespace Geziciler
{
    public class Gezici
    {
        public int GeziciNo { get; set; }
        public Durum GeziciDurum { get; set; }
        public class Durum
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Yon { get; set; }

        }
    }
}
