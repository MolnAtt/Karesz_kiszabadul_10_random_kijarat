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
        // eljárás


        /*
        bool Van_e_előttem_baj()
		{
            return Van_e_előttem_fal() || Kilépek_e_a_pályáról();
		}
        */

        bool Van_e_előttem_baj() => Van_e_előttem_fal() || Kilépek_e_a_pályáról();
        void Hátra_arc()
		{
            Fordulj(jobbra);
            Fordulj(jobbra);
        }
        void Hátra_lépj()
		{
            Hátra_arc();
            Lépj();
            Hátra_arc();
        }

        /// <summary>
        /// 1-et próbálkozó eljárás
        /// </summary>
        /// <returns></returns>
        bool Ha_van_kavics_idelepek(int irány)
		{
			if (irány!=0)
			{
                Fordulj(irány);
			}


            if (Van_e_előttem_baj())
            {
                return false;
            }
            else // <- ennek most sok jelentősége nincs, mert a return egyből befejezi a függvényt
            {
                Lépj();
				if (Van_e_itt_Kavics())
				{
                    Vegyél_fel_egy_kavicsot();
                    return true;
				}
                else // szintén egy "felesleges" else
				{
                    Hátra_lépj();
					if (irány!=0)
					{
                        Fordulj(-irány);
					}
                    return false;
				}

			}
		}

        bool Próbálkozzunk(int irány)
        {
            if (irány != 0)
            {
                Fordulj(irány);
            }


            if (Van_e_előttem_baj())
            {
                return false;
            }
            else // <- ennek most sok jelentősége nincs, mert a return egyből befejezi a függvényt
            {
                Lépj();
                if (Van_e_itt_Kavics())
                {
                    Vegyél_fel_egy_kavicsot();

                    // Vektor helye = Robot.akit_kiválasztottak.h;
                    //Robot.akit_kiválasztottak.Mondd($"Próbálkozom");

                    /*
                    bool előre_mentem = Próbálkozzunk(előre);
                    bool jobbra_mentem = !előre_mentem && Próbálkozzunk(jobbra);
                    bool balra_mentem = !előre_mentem && !jobbra_mentem && Próbálkozzunk(balra);
                    */
                    bool felesleges_eltárolni = Próbálkozzunk(előre) || Próbálkozzunk(jobbra) || Próbálkozzunk(balra); // ez az új sor!
																													   //Robot.akit_kiválasztottak.Mondd($"Próbálkozást befejeztem, itt vagyok most: {helye}");
                    /*
					if (előre_mentem||jobbra_mentem||balra_mentem)
					{
                        Hátra_lépj();
					}

                    if (jobbra_mentem)
					{
                        Fordulj(balra);
					}

                    if (balra_mentem)
                    {
                        Fordulj(jobbra);
                    }
                    */

                    return true;
                }
                else // szintén egy "felesleges" else
                {
                    Hátra_lépj();
                    if (irány != 0)
                    {
                        Fordulj(-irány);
                    }
                    return false;
                }
            }
        }

        Random r = new Random();

        int előre = 0;
        void DIÁK_ROBOTJAI()
        {
            int szam = r.Next(1, 7);
            Robot karesz = Robot.Get("Karesz");

            karesz.Feladat = delegate ()
            {
				if (Van_e_itt_Kavics())
				{
                    Vegyél_fel_egy_kavicsot();
				}


                // vázlatos! le fogjuk majd egyszerűsíteni NAGYON!

                /*
                bool vége = false;
				while (!vége)
				{
                    bool volt_elottem_kavics = Ha_van_kavics_idelepek(előre); // itt előrelép!
                    if (!volt_elottem_kavics)
				    {
                        bool volt_jobbra_kavics = Ha_van_kavics_idelepek(jobbra);
                        if (!volt_jobbra_kavics)
					    {
                            bool volt_balra_kavics = Ha_van_kavics_idelepek(balra);
                            if (!volt_balra_kavics)
						    {
                                karesz.Mondd("ennyi volt");
                                vége = true;
						    }
					    }
				    }
				}
                */

                /*
                bool vége = false;
                while (!vége)
                {
                    if (!Ha_van_kavics_idelepek(előre) && !Ha_van_kavics_idelepek(jobbra) && !Ha_van_kavics_idelepek(balra))
                    {
                        karesz.Mondd("ennyi volt");
                        vége = true;
                    }
                }
                 */

                // Nem rekurzív megoldás
                // while (Ha_van_kavics_idelepek(előre) || Ha_van_kavics_idelepek(jobbra) || Ha_van_kavics_idelepek(balra)){}
                // Rekurzív megoldás

                bool felesleges_változó = Próbálkozzunk(előre) || Próbálkozzunk(jobbra) || Próbálkozzunk(balra);


                karesz.Mondd("ennyi volt!");

            };
        }
    }
}

/* LEGFONTOSABB PARANCSOK

MOZGÁSOK

karesz.Lépj();                          -------- Karesz előre lép egyet.
karesz.Fordulj(jobbra);                 -------- Karesz jobbra fordul.
karesz.Fordulj(balra);                  -------- Karesz balra fordul.
karesz.Vegyél_fel_egy_kavicsot();       -------- Karesz felvesz egy kavicsot.
karesz.Tegyél_le_egy_kavicsot();        -------- Karesz letesz egy fekete kavicsot
karesz.Tegyél_le_egy_kavicsot(piros);   -------- Karesz letesz egy piros kavicsot.

Minden mozgás után a robot köre véget ér és a következő robot jön. 



SZENZOROK

karesz.Előtt_fal_van();                 -------- igaz, ha Karesz fal előtt áll, egyébként hamis.
karesz.Ki_fog_lépni_a_pályáról();       -------- igaz, ha Karesz a pálya szélén kifele néz, egyébként hamis.
karesz.Alatt_van_kavics();              -------- igaz, ha Karesz épp kavicson áll, egyébként hamis.
karesz.Köveinek_száma_ebből(piros)      -------- Karesz piros köveinek a száma.
karesz.Alatt_ez_van();                  -------- a kavics színe, amin Karesz áll. (Ez igazából egy szám!)
karesz.UltrahangSzenzor();              -------- a Karesz előtt található tárgy távolsága. Ez a tárgy lehet fal vagy másik robot is. 
karesz.SzélesUltrahangSzenzor();        -------- ugyanaz, mint az ultrahangszenzor, de ez nem csak a Karesz előtti mezőket pásztázza, hanem a szomszédos mezőket is. Egy számhármast ad vissza. 
karesz.Hőmérő();                        -------- a Karesz által mért hőmérséklet. A láva forrása 1000 fok, amitől lépésenként távolodva a hőmérséklet 200 fokonként hűl. Az alapértelmezett hőmérséklet 0 fok.

A szenzorokat bármennyiszer használhatja a robot a saját körén belül.

*/