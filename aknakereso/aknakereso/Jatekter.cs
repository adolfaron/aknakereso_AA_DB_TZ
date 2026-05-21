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
        public Jatekter(int nehezseg, int meret)
        {
            InitializeComponent();
            meret = meret;
            helyek = new PictureBox[meret, meret];
            racs = new List<string>[meret, meret];

            Text = "Aknakereső";
            this.ClientSize = new Size(meret * cellaMeret, meret * cellaMeret);
            var tabla = new TableLayoutPanel//táblázat létrehozása
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
                    var cellaTart = new PictureBox
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

        }
    }
}
