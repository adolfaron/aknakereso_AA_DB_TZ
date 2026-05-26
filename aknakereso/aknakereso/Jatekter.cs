using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public Jatekter(int ujnehezseg, int ujmeret)
        {
            InitializeComponent();
            meret = ujmeret;
            MaxSuruseg = ujnehezseg;
            helyek = new PictureBox[meret, meret];
            racs = new List<string>[meret, meret];

            Text = "Aknakereső";
            this.ClientSize = new Size(meret * cellaMeret, meret * cellaMeret);
            TableLayoutPanel tabla = new TableLayoutPanel//táblázat létrehozása
            {
                RowCount = meret,
                ColumnCount = meret,
                Dock = DockStyle.Top,//szülőelemhez igazítás
                Height = meret * cellaMeret
            };
            for (int i = 0; i < meret; i++)// sorok és oszlopok hozzáadása, méretezése
            {
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
                        Size = new Size(cellaMeret, cellaMeret),
                        Margin = new Padding(0),
                        Tag = vilagos ? "vilagos" : "sotet"
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
                        MessageBox.Show(racs[sor, oszlop][0] +"\n"+ racs[sor, oszlop][1]);
                    }
                        
            
            if (!felfedve)
            {
                if (e.Button == MouseButtons.Left)//bal kattintás, felfedés
                {
                    if (racs[koordinata.sor, koordinata.oszlop][0] == "ures" || racs[koordinata.sor, koordinata.oszlop][0] == "szam")
                    {
                        aknaLetrehoz(koordinata);
                    }
                }
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
                MessageBox.Show(racs[koordinata.sor, koordinata.oszlop][1] + " létrehozva");

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
    }
}
