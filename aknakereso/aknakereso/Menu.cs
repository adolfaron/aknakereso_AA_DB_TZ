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
    public partial class Menu : Form
    {
        private Button startBTN;
        private ComboBox nehezsegComb;
        private ComboBox meretComb;
        public Menu()
        {
            InitializeComponent();
            menuElemek();
        }

        private void menuElemek()
        {
            startBTN = new Button();
            startBTN.Click += (s, e) =>
            {
                int nehezsegBe = nehezsegComb.SelectedIndex;
                int meretBe = meretComb.SelectedIndex;
                int meret;
                int nehezseg;
                switch (nehezsegBe)
                {
                    case 0:
                        {
                            nehezseg = 10;
                            break;
                        }
                    case 1:
                        {
                            nehezseg = 20;
                            break;
                        }
                    case 2:
                        {
                            nehezseg = 40;
                            break;
                        }
                    default:
                        {
                            nehezseg = 20;
                            break;
                        }

                }

                switch (meretBe)
                {
                    case 0:
                        {
                            meret = 16;
                            break;
                        }
                    case 1:
                        {
                            meret = 32;
                            break;
                        }
                    case 2:
                        {
                            meret = 64;
                            break;
                        }
                    default:
                        {
                            meret = 16;
                            break;
                        }

                }
                Jatekter jatekter = new Jatekter(nehezseg, meret);
                jatekter.ShowDialog();

            };
            startBTN.Text = "Start";
            startBTN.Size = new Size(50,100);

            Controls.Add(startBTN);

            nehezsegComb = new ComboBox
            { 
            
            };

            meretComb = new ComboBox
            { 
            
            };
        }

    }
}
