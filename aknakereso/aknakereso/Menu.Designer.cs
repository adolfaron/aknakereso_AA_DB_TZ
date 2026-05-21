namespace aknakereso
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            meretComb = new ComboBox();
            nehezsegComb = new ComboBox();
            startBTN = new Button();
            SuspendLayout();
            // 
            // meretComb
            // 
            meretComb.DropDownStyle = ComboBoxStyle.DropDownList;
            meretComb.FormattingEnabled = true;
            meretComb.Items.AddRange(new object[] { "Kicsi", "Közepes", "Nagy" });
            meretComb.Location = new Point(136, 141);
            meretComb.Name = "meretComb";
            meretComb.Size = new Size(121, 23);
            meretComb.TabIndex = 0;
            // 
            // nehezsegComb
            // 
            nehezsegComb.DropDownStyle = ComboBoxStyle.DropDownList;
            nehezsegComb.FormattingEnabled = true;
            nehezsegComb.Items.AddRange(new object[] { "Könnyű", "Közepes", "Nehéz" });
            nehezsegComb.Location = new Point(307, 141);
            nehezsegComb.Name = "nehezsegComb";
            nehezsegComb.Size = new Size(121, 23);
            nehezsegComb.TabIndex = 1;
            // 
            // startBTN
            // 
            startBTN.Location = new Point(253, 190);
            startBTN.Name = "startBTN";
            startBTN.Size = new Size(75, 23);
            startBTN.TabIndex = 2;
            startBTN.Text = "startBTN";
            startBTN.UseVisualStyleBackColor = true;
            startBTN.Click += startBTN_Click;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(551, 354);
            Controls.Add(startBTN);
            Controls.Add(nehezsegComb);
            Controls.Add(meretComb);
            Name = "Menu";
            Text = "Menu";
            ResumeLayout(false);
        }

        #endregion

        private ComboBox meretComb;
        private ComboBox nehezsegComb;
        private Button startBTN;
    }
}