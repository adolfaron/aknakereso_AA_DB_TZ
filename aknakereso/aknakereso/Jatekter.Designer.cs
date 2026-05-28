using System.Drawing;

namespace aknakereso
{
    partial class Jatekter
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
            SuspendLayout();
            // 
            // Jatekter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(407, 311);
            Name = "Jatekter";
            Text = "Jatekter";
            ResumeLayout(false);
        }

        #endregion
    }
    public class PictureBox : System.Windows.Forms.PictureBox
    {
        private bool borderLeft = false;
        private bool borderRight = false;
        private bool borderTop = false;
        private bool borderBottom = false;

        private Color borderColor = Color.Black;
        private int borderWidth = 2;

        public bool BorderBottomRight { get; set; }
        public bool BorderBottomLeft { get; set; }
        public bool BorderTopRight { get; set; }
        public bool BorderTopLeft { get; set; }

        public int CornerSize { get; set; } = 10;

        public bool BorderLeft
        {
            get => borderLeft;
            set
            {
                borderLeft = value;
                Invalidate();//újrarajzolás
            }
        }

        public bool BorderRight
        {
            get => borderRight;
            set
            {
                borderRight = value;
                Invalidate();
            }
        }

        public bool BorderTop
        {
            get => borderTop;
            set
            {
                borderTop = value;
                Invalidate();
            }
        }

        public bool BorderBottom
        {
            get => borderBottom;
            set
            {
                borderBottom = value;
                Invalidate();
            }
        }

        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        public int BorderWidth
        {
            get => borderWidth;
            set
            {
                borderWidth = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);//erederi OnPaint

            Rectangle imgRect = GetImageRectangle();//pozíció és méretet tárol

            using (Brush brush = new SolidBrush(BorderColor))//rajzolóeszköz
            {
                // BAL
                if (BorderLeft)
                {
                    pe.Graphics.FillRectangle(//beszínezés
                        brush,
                        imgRect.Left,
                        imgRect.Top,
                        BorderWidth,
                        imgRect.Height);
                }

                // JOBB
                if (BorderRight)
                {
                    pe.Graphics.FillRectangle(
                        brush,
                        imgRect.Right - BorderWidth,
                        imgRect.Top,
                        BorderWidth,
                        imgRect.Height);
                }

                // FELSŐ
                if (BorderTop)
                {
                    pe.Graphics.FillRectangle(
                        brush,
                        imgRect.Left,
                        imgRect.Top,
                        imgRect.Width,
                        BorderWidth);
                }

                // ALSÓ
                if (BorderBottom)
                {
                    pe.Graphics.FillRectangle(
                        brush,
                        imgRect.Left,
                        imgRect.Bottom - BorderWidth,
                        imgRect.Width,
                        BorderWidth);
                }

                // SARKOK
                if (BorderBottomRight)
                {
                    pe.Graphics.FillRectangle(
                        brush,
                        imgRect.Right - BorderWidth,
                        imgRect.Bottom - BorderWidth,
                        BorderWidth,
                        BorderWidth);
                }

                if (BorderBottomLeft)
                {
                    pe.Graphics.FillRectangle(
                        brush,
                        imgRect.Left,
                        imgRect.Bottom - BorderWidth,
                        BorderWidth,
                        BorderWidth);
                }

                if (BorderTopRight)
                {
                    pe.Graphics.FillRectangle(
                        brush,
                        imgRect.Right - BorderWidth,
                        imgRect.Top,
                        BorderWidth,
                        BorderWidth);
                }

                if (BorderTopLeft)
                {
                    pe.Graphics.FillRectangle(
                        brush,
                        imgRect.Left,
                        imgRect.Top,
                        BorderWidth,
                        BorderWidth);
                }
            }
        
        }
        private Rectangle GetImageRectangle()
        {
            if (Image == null)
                return ClientRectangle;

            switch (SizeMode)
            {
                case PictureBoxSizeMode.StretchImage:
                    return ClientRectangle;

                case PictureBoxSizeMode.Zoom:

                    float imageRatio = (float)Image.Width / Image.Height;
                    float boxRatio = (float)Width / Height;

                    int drawWidth;
                    int drawHeight;

                    if (imageRatio > boxRatio)
                    {
                        drawWidth = Width;
                        drawHeight = (int)(Width / imageRatio);
                    }
                    else
                    {
                        drawHeight = Height;
                        drawWidth = (int)(Height * imageRatio);
                    }

                    int x = (Width - drawWidth) / 2;
                    int y = (Height - drawHeight) / 2;

                    return new Rectangle(x, y, drawWidth, drawHeight);

                default:
                    return ClientRectangle;
            }
        }
    }

}