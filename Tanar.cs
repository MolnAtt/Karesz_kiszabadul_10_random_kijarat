using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace Karesz
{
    public partial class Form1 : Form
    {
        string betöltendő_pálya = "palya14.txt";
        Random dobókocka = new Random();
        void PÁLYA_ALAKÍTÁSA_TANÁRKÉNT()
        {
            pálya.tábla[40,17] = fal;
            pálya.tábla[40, dobókocka.Next(1, 29)] = üres;
        }

        void TANÁR_ROBOTJAI()
        {
            new Robot("Karesz", 1000, 10, 10, 10, 0, 16, 17, 1);
        }

    }
}