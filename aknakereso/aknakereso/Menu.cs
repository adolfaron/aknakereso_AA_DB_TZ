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
        public Menu()
        {
            InitializeComponent();
        }

        private void startBTN_Click(object sender, EventArgs e)
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
                        nehezseg = 20;
                        break;
                    }
                default:
                    {
                        nehezseg = 0;
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
                default : {
                        meret = 0;
                        break;
                    }

            }
            Jatekter jatekter = new Jatekter(nehezseg, meret);
        }
    }
}
