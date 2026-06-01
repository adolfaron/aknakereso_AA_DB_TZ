using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace aknakereso
{
    public partial class Menu : Form
    {
        private Button startBTN;
        private ComboBox nehezsegComb;
        private ComboBox meretComb;
        private Label cim;
        private double kepArany;
        private bool ujrameretezes;
        public Menu()
        {
            InitializeComponent();
            menuElemek();
            MinimumSize = new Size(384, 261);
            MaximumSize = new Size(646, 439);
            ClientSizeChanged += (s, e) =>
            {
                startBTN.Font = new Font("Segoe UI", (ClientSize.Width / 20), FontStyle.Bold);
                startBTN.Height = ClientSize.Height / 5;
                nehezsegComb.Font = new Font("Segoe UI", (ClientSize.Width / 25), FontStyle.Regular);
                nehezsegComb.Height = ClientSize.Height / 10;
                meretComb.Font = new Font("Segoe UI", (ClientSize.Width / 25), FontStyle.Regular);
                meretComb.Height = ClientSize.Height / 10;

            };
            kepArany = (double)ClientSize.Height / ClientSize.Width;

            Resize += (s, e) =>
            {
                if (ujrameretezes)
                    return;

                ujrameretezes = true;

                ClientSize = new Size(
                    ClientSize.Width,
                    (int)(ClientSize.Width * kepArany));

                ujrameretezes = false;
            };
        }

        private void menuElemek()
        {
            this.Text = "Aknakereső";
            this.BackColor = Color.FromArgb(162, 209, 73);
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.RowCount = 3;
            layout.ColumnCount = 1;
            layout.Padding = new Padding(20);
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 75));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 75));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 75));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            Controls.Add(layout);

            Label cim = new Label();
            cim.Text = "AKNAKERESŐ";
            cim.ForeColor = Color.White;
            cim.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            cim.TextAlign = ContentAlignment.MiddleCenter;
            cim.Dock = DockStyle.Fill;
            layout.Controls.Add(cim, 0, 0);

            Panel beallitasPanel = new Panel();
            beallitasPanel.Dock = DockStyle.Fill;
            layout.Controls.Add(beallitasPanel, 0, 1);

            nehezsegComb = new ComboBox();
            nehezsegComb.Items.AddRange(new object[]
            {
        "Könnyű",
        "Közepes",
        "Nehéz"
            });

            nehezsegComb.SelectedIndex = 0;
            nehezsegComb.Font = new Font("Segoe UI", 12);
            nehezsegComb.Size = new Size(200, 35);
            nehezsegComb.Dock = DockStyle.Top;
            nehezsegComb.DropDownStyle = ComboBoxStyle.DropDownList;

            beallitasPanel.Controls.Add(nehezsegComb);

            meretComb = new ComboBox();
            meretComb.Items.AddRange(new object[]
            {
        "8 x 8",
        "16 x 16",
        "32 x 32",
            });

            meretComb.SelectedIndex = 0;
            meretComb.Font = new Font("Segoe UI", 12);
            meretComb.Dock = DockStyle.Bottom;
            meretComb.DropDownStyle = ComboBoxStyle.DropDownList;

            beallitasPanel.Controls.Add(meretComb);

            startBTN = new Button();
            startBTN.Text = "Játék indítása";
            startBTN.Dock = DockStyle.Fill;
            startBTN.Height = 50;

            startBTN.BackColor = Color.FromArgb(180, 220, 100);
            startBTN.ForeColor = Color.White;
            startBTN.FlatStyle = FlatStyle.Flat;
            startBTN.FlatAppearance.BorderSize = 0;

            startBTN.Font = new Font("Segoe UI", 14, FontStyle.Bold);


            layout.Controls.Add(startBTN, 0, 2);

            startBTN.Click += (s, e) =>
            {
                int nehezseg = 20;
                int meret = 16;

                switch (nehezsegComb.SelectedIndex)
                {
                    case 0:
                        nehezseg = 15;
                        break;

                    case 1:
                        nehezseg = 20;
                        break;

                    case 2:
                        nehezseg = 40;
                        break;
                }

                switch (meretComb.SelectedIndex)
                {
                    case 0:
                        meret = 8;
                        break;

                    case 1:
                        meret = 16;
                        break;

                    case 2:
                        meret = 32;
                        break;
                }

                Jatekter jatekter = new Jatekter(nehezseg, meret);
                this.Hide();
                jatekter.ShowDialog();
                this.Show();

            };
        }

    }
}