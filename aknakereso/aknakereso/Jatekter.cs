using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace aknakereso
{
    public partial class Jatekter : Form
    {
        int cellaMeret = 30;//px
        int MaxSuruseg = 20;//% (min 3) alap 20
        PictureBox[,] helyek;
        List<string>[,] racs;
        int meret;
        bool felfedve = false;
        bool letrehozva = false;
        Random rnd = new Random();
        List<string> ellenorizve = new List<string>();
        public Jatekter(int ujnehezseg, int ujmeret)
        {
            InitializeComponent();
            meret = ujmeret;
            MaxSuruseg = ujnehezseg;
            helyek = new PictureBox[meret, meret];
            racs = new List<string>[meret, meret];

            Text = "Aknakereső";
            this.ClientSize = new Size(meret * cellaMeret, meret * cellaMeret);
            //this.ClientSize = new Size(400, 400);
            TableLayoutPanel tabla = new TableLayoutPanel//táblázat létrehozása
            {
                RowCount = meret + 1,
                ColumnCount = meret + 1,
                Dock = DockStyle.Fill,//szülőelemhez igazítás
                //Height = meret * cellaMeret,
            };
            for (int i = 0; i < meret; i++)
            {
                /*tabla.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / meret + 1));
                tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / meret + 1));*/
                tabla.RowStyles.Add(new RowStyle());
                tabla.ColumnStyles.Add(new ColumnStyle());
            }
            bool vilagos = true;
            for (int sor = 0; sor < meret; sor++)//elemek létrehozása és eljelyezése
            {
                for (int oszlop = 0; oszlop < meret; oszlop++)
                {
                    vilagos = !vilagos;
                    PictureBox cellaTart = new PictureBox
                    {
                        Margin = new Padding(0),
                        Size = new Size(cellaMeret, cellaMeret),
                        Tag = vilagos ? "vilagos" : "sotet",
                        BorderBottom = false,
                        BorderLeft = false,
                        BorderRight = false,
                        BorderTop = false,
                        BorderBottomLeft = false,
                        BorderBottomRight = false,
                        BorderTopLeft = false,
                        BorderTopRight = false,

                        BorderColor = Color.FromArgb(135, 175, 58),
                    };
                    cellaTart.MouseDown += Kattintas;
                    cellaTart.SizeMode = PictureBoxSizeMode.StretchImage;
                    cellaTart.Image = vilagos ? Image.FromFile("img/zold.png") : Image.FromFile("img/sotetZold.png");

                    tabla.Controls.Add(cellaTart, oszlop, sor);
                    helyek[sor, oszlop] = cellaTart;
                    racs[sor, oszlop] = new List<string> { "ures", "semmi" };//állapot tartaloms
                }
                vilagos = meret % 2 == 0 ? !vilagos : vilagos;
            }
            //meretez();
            Controls.Add(tabla);
        }
        private void Kattintas(object sender, MouseEventArgs e)
        {
            (int sor, int oszlop) koordinata = (0, 0);
            PictureBox aktkep = (PictureBox)sender;

            for (int sor = 0; sor < meret; sor++)
                for (int oszlop = 0; oszlop < meret; oszlop++)
                    if (helyek[sor, oszlop] == aktkep)
                    {
                        koordinata = (sor, oszlop);
                        //MessageBox.Show(racs[sor, oszlop][0] +"\n"+ racs[sor, oszlop][1]);
                    }


            if (!felfedve)
            {
                if (e.Button == MouseButtons.Left)//bal kattintás, felfedés
                {
                    if (racs[koordinata.sor, koordinata.oszlop][0] == "ures" || racs[koordinata.sor, koordinata.oszlop][0] == "szam")
                    {
                        aknaLetrehoz(koordinata);
                        racs[koordinata.sor, koordinata.oszlop][0] = "felfedett";
                        //felfed(true);
                        //return;
                        if (racs[koordinata.sor, koordinata.oszlop][1] == "akna")
                        {
                            felfed(true);
                            MessageBox.Show("Aknára léptél! Játék vége!");
                        }
                        else//ha üres a mező vagy szám
                        {
                            aktkep.Image = Image.FromFile("img/" + (aktkep.Tag == "vilagos" ? "vilagos" : "sotet") + racs[koordinata.sor, koordinata.oszlop][1] + ".png");
                            //aktkep.Image = aktkep.Tag == "vilagos" ? Image.FromFile("kepek/vilagosures.png") : Image.FromFile("kepek/sotetures.png"); 
                            ellenorizve.Clear();
                            //ellSzam = 0;
                            ellenoriz(koordinata.sor, koordinata.oszlop);
                            for (int s = 0; s < meret; s++)
                            {
                                for (int o = 0; o < meret; o++)
                                {
                                    if (int.TryParse(racs[s, o][1], out _)
                                        && racs[s, o][0] == "felfedett")
                                    {
                                        helyek[s, o].BorderTop = false;
                                        helyek[s, o].BorderBottom = false;
                                        helyek[s, o].BorderLeft = false;
                                        helyek[s, o].BorderRight = false;

                                        helyek[s, o].BorderTopLeft = false;
                                        helyek[s, o].BorderTopRight = false;
                                        helyek[s, o].BorderBottomLeft = false;
                                        helyek[s, o].BorderBottomRight = false;

                                        if (s - 1 >= 0 &&
                                            racs[s - 1, o][0] != "felfedett")
                                            helyek[s, o].BorderTop = true;

                                        if (s + 1 < meret &&
                                            racs[s + 1, o][0] != "felfedett")
                                            helyek[s, o].BorderBottom = true;

                                        if (o - 1 >= 0 &&
                                            racs[s, o - 1][0] != "felfedett")
                                            helyek[s, o].BorderLeft = true;

                                        if (o + 1 < meret &&
                                            racs[s, o + 1][0] != "felfedett")
                                            helyek[s, o].BorderRight = true;

                                        // Bal felső
                                        if (s - 1 >= 0 &&
                                            o - 1 >= 0 &&
                                            racs[s - 1, o - 1][0] != "felfedett")
                                        {
                                            helyek[s, o].BorderTopLeft = true;
                                        }

                                        // Jobb felső
                                        if (s - 1 >= 0 &&
                                            o + 1 < meret &&
                                            racs[s - 1, o + 1][0] != "felfedett")
                                        {
                                            helyek[s, o].BorderTopRight = true;
                                        }

                                        // Bal alsó
                                        if (s + 1 < meret &&
                                            o - 1 >= 0 &&
                                            racs[s + 1, o - 1][0] != "felfedett")
                                        {
                                            helyek[s, o].BorderBottomLeft = true;
                                        }

                                        // Jobb alsó
                                        if (s + 1 < meret &&
                                            o + 1 < meret &&
                                            racs[s + 1, o + 1][0] != "felfedett")
                                        {
                                            helyek[s, o].BorderBottomRight = true;
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                else if (letrehozva)//jobb kattintás, zászlózás
                {
                    if (racs[koordinata.sor, koordinata.oszlop][0] == "ures" || racs[koordinata.sor, koordinata.oszlop][0] == "szam")
                    {
                        racs[koordinata.sor, koordinata.oszlop][0] = "zaszlo";
                        aktkep.Image = aktkep.Tag == "vilagos" ? Image.FromFile("img/vilagosZaszlo.png") : Image.FromFile("img/sotetZaszlo.png");
                    }
                    else if (racs[koordinata.sor, koordinata.oszlop][0] == "zaszlo")
                    {
                        racs[koordinata.sor, koordinata.oszlop][0] = "ures";
                        aktkep.Image = aktkep.Tag == "vilagos" ? Image.FromFile("img/zold.png") : Image.FromFile("img/sotetZold.png");


                    }
                }
                teleEllenoriz();
            }
        }

        private void felfed(bool folrobbant)
        {
            int XSzama = 0;
            int aknaszam = 0;
            int zaszloSzam = 0;
            if (!felfedve)
            {
                for (int sor = 0; sor < meret; sor++)
                    for (int oszlop = 0; oszlop < meret; oszlop++)
                    {
                        PictureBox aktMezo = helyek[sor, oszlop];
                        if (racs[sor, oszlop][1] == "akna")
                        {
                            aknaszam++;
                            if (racs[sor, oszlop][0] != "zaszlo")
                            {
                                aktMezo.Image = Image.FromFile("img/akna" + rnd.Next(1, 9) + ".png");
                            }
                            else if (racs[sor, oszlop][0] == "zaszlo")
                            {
                                zaszloSzam++;
                            }

                        }
                        else
                        {
                            if (racs[sor, oszlop][0] == "zaszlo")
                            {
                                aktMezo.Image = aktMezo.Tag == "vilagos" ? Image.FromFile("img/vilagosX.png") : Image.FromFile("img/sotetX.png");
                                XSzama++;
                            }
                        }

                        //összes szám megmutatása:
                        //aktMezo.Image = Image.FromFile("img/" + (aktMezo.Tag == "vilagos" ? "vilagos" : "sotet") + racs[sor, oszlop][1] + ".png");

                    }
                felfedve = true;
                if (!folrobbant)
                    MessageBox.Show("Játék vége!" + (XSzama == 0 && zaszloSzam == aknaszam ? "\nGratulálok megtaláltad az összes aknát és csak azokat jelölted meg!" : "") + (XSzama != 0 && zaszloSzam == aknaszam ? "\nMegtaláltad az összes aknát de nem csak azokat jelölted meg!" : ""));
            }
        }

        private void aknaLetrehoz((int sor, int oszlop) koordinata)
        {
            if (!letrehozva)//aknák elhelyezése
            {
                letrehozva = true;
                int minVel = (int)meret / 2;//kb 2 soronként  1
                int maxVel = (int)(Math.Pow(meret, 2) / 2);
                maxVel = (int)(Math.Pow(meret, 2) * (MaxSuruseg / 100.0));
                int aknaszama = rnd.Next(minVel, maxVel);
                for (int i = 0; i < aknaszama; i++)
                {
                    int Vsor, Voszlop;
                    do//addig ad új véletlent amíg a kattintottra jön ki egy akna
                    {
                        Vsor = rnd.Next(0, meret);
                        Voszlop = rnd.Next(0, meret);
                        //} while ((Vsor == koordinata.sor && Voszlop == koordinata.oszlop));
                    } while (aknaKorulotte(Vsor, Voszlop, koordinata.sor, koordinata.oszlop));
                    racs[Vsor, Voszlop][1] = "akna";
                }
                //MessageBox.Show(racs[koordinata.sor, koordinata.oszlop][1] + " létrehozva");

                for (int sor = 0; sor < meret; sor++)
                    for (int oszlop = 0; oszlop < meret; oszlop++)
                    {
                        if (racs[sor, oszlop][1] != "akna")
                        {
                            int aknaSzam = 0;
                            for (int i = -1; i <= 1; i++)
                            {
                                for (int j = -1; j <= 1; j++)
                                {
                                    if (i == 0 && j == 0) continue; // ne a saját cellát nézzük
                                    int s = sor + i;
                                    int o = oszlop + j;
                                    if (s >= 0 && s < meret && o >= 0 && o < meret)
                                    {
                                        if (racs[s, o][1] == "akna") aknaSzam++;
                                    }
                                }
                            }

                            if (aknaSzam == 0)
                            {
                                racs[sor, oszlop][1] = "ures";
                            }
                            else
                            {
                                racs[sor, oszlop][0] = "szam";
                                racs[sor, oszlop][1] = aknaSzam.ToString();
                            }
                        }
                    }

            }
        }

        bool aknaKorulotte(int Vsor, int Voszlop, int sor, int oszlop)
        {
            //return racs[Vsor, Voszlop][1] == "akna" || (Vsor == sor && Voszlop == oszlop); ;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int s = sor + i;
                    int o = oszlop + j;
                    if (s >= 0 && s < meret && o >= 0 && o < meret)
                    {
                        if (s == Vsor && o == Voszlop)
                        {
                            //Console.WriteLine("akna a közelben");
                            return true;

                        }
                        ;
                    }
                }
            }
            //aknaSzama++;
            //Console.WriteLine("nincs akna"+ aknaSzama);
            return false; //racs[Vsor, Voszlop][1] == "akna" || (Vsor == sor && Voszlop == oszlop);
        }

        void ellenoriz(int sor, int oszlop)
        {
            //ellSzam++;
            //Console.WriteLine("ellenőrzés száma: " + ellSzam);
            //if (ellenorizve.Contains((sor + "" + oszlop)))
            //return;

            ellenorizve.Add((sor + " " + oszlop));
            if (racs[sor, oszlop][1] == "ures")
            {
                //MessageBox.Show("üres");
                //racs[koordinata.sor, koordinata.oszlop][1];
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0) continue; // ne a saját cellát nézzük
                        int s = sor + i;
                        int o = oszlop + j;
                        if (s >= 0 && s < meret && o >= 0 && o < meret)
                        {
                            if (racs[s, o][1] == "ures")
                            {
                                helyek[s, o].Image = Image.FromFile("img/" + (helyek[s, o].Tag == "vilagos" ? "vilagos" : "sotet") + racs[s, o][1] + ".png");
                                racs[s, o][0] = "felfedett";
                                if (!ellenorizve.Contains((s + " " + o)))
                                    ellenoriz(s, o);
                                //aktkep.Image = Image.FromFile("kepek/" + (aktkep.Tag == "vilagos" ? "vilagos" : "sotet") + racs[koordinata.sor, koordinata.oszlop][1] + ".png");
                            }
                            else if (int.TryParse(racs[s, o][1], out int szam))//szám-e 
                            {
                                racs[s, o][0] = "felfedett";
                                helyek[s, o].Image = Image.FromFile("img/" + (helyek[s, o].Tag == "vilagos" ? "vilagos" : "sotet") + racs[s, o][1] + ".png");

                            }
                        }
                    }
                }
            }
        }

        void teleEllenoriz()
        {
            int felfedett = 0;
            for (int sor = 0; sor < meret; sor++)
                for (int oszlop = 0; oszlop < meret; oszlop++)
                {
                    if (racs[sor, oszlop][0] == "felfedett" || racs[sor, oszlop][0] == "zaszlo")
                    {
                        felfedett++;
                    }
                }
            if (felfedett == meret * meret)
            {

                //MessageBox.Show("Kész!");
                felfed(false);
            } /*else
            {
                 Console.WriteLine("felfedett: " + felfedett);
            }*/
        }

        void meretez()
        {
            for (int sor = 0; sor < meret; sor++)
            {
                for (int oszlop = 0; oszlop < meret; oszlop++)
                {
                    PictureBox aktualis = helyek[sor, oszlop] as PictureBox;
                    if (aktualis == null)
                    {
                        return;
                    }
                    aktualis.Size = new Size(this.ClientSize.Width / meret, this.ClientSize.Width / meret);
                    aktualis.BorderWidth = this.Width / (meret * 10);
                    this.ClientSize = new Size(this.ClientSize.Width, this.ClientSize.Width);
                    //helyek[sor, oszlop] = ;
                }
            }
        }
    }
}
