using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BingoR
{
    internal class BingoJatekos
    {
        string nev;

        public string Nev { get => nev; set => nev = value; }

        public string[,] Szamok;
        public bool[,] Kihuzva;

        public BingoJatekos(string forras)
        {
            Szamok = new string[5, 5];
            Kihuzva = new bool[5, 5];
            Kihuzva[2, 2] = true;
            Nev = forras.Split('.')[0];

            try
            {
                using FileStream fs = File.OpenRead(forras);
                using StreamReader sr = new StreamReader(fs);
                int sorIndex = 0;
                    while (!sr.EndOfStream)
                {
                    string[] st = sr.ReadLine().Split(';');
                    for (int oszlopindex = 0; oszlopindex < st.Length; oszlopindex++)
                    {
                        Szamok[sorIndex,oszlopindex] = st[oszlopindex];
                    }
                    sorIndex++;

                }
                    fs.Close();
                    sr.Close();


            }
            catch (Exception ex) { MessageBox.Show("A fájl nem található!"); }
        }

        public void SoroltSzamotJelol(int kiszam)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (Szamok[i,j] == kiszam.ToString())
                    {
                        Kihuzva[i,j] = true;
                    }
                }
            }
        }
        public bool BingoEll()
        {
            for ( int sor = 0;sor < 5;sor++)
            {
                if (Kihuzva[sor,0] && Kihuzva[sor,1] &&
                    Kihuzva[sor, 2] && Kihuzva[sor, 3] &&
                    Kihuzva[sor, 4]
                    
                    ) return true;
            }
            for (int oszlop = 0; oszlop < 5; oszlop++)
            {
                 if(Kihuzva[oszlop, 0] && Kihuzva[oszlop, 1] &&
                    Kihuzva[oszlop, 2] && Kihuzva[oszlop, 3] &&
                    Kihuzva[oszlop, 4]
                    ) return true;
            }
            if (Kihuzva[0,0] && Kihuzva[1,1] && Kihuzva[3,3] && Kihuzva[4,4]
                ) return true;
               if( Kihuzva[0, 4] && Kihuzva[1, 3] && Kihuzva[3, 2] && Kihuzva[4, 0]
                )return true;
            return false;
        }
    }
}
