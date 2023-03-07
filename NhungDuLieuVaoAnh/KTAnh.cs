using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhungDuLieuVaoAnh
{
    public partial class KTAnh : Form
    {
        public KTAnh()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Ảnh định dạng BMP (*.bmp) | *.bmp";
            od.Multiselect = false;
            if (od.ShowDialog() == DialogResult.OK)
            {
                Color c = Color.FromArgb(255, 0, 255, 0);
                Bitmap bmp = new Bitmap(od.FileName);
                for(int x = 0; x < bmp.Width; x++)
                    for(int y = 0; y < bmp.Height; y++)
                    {
                        bmp.SetPixel(x, y, c);
                    }
                
                Color c1 = Color.FromArgb(255, 255, 0, 0);
                bmp.SetPixel(bmp.Width - 1, bmp.Height - 1, c1);

                
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Ảnh định dạng BMP (*.bmp) | *.bmp";
                if(sd.ShowDialog() == DialogResult.OK)
                {
                    bmp.Save(sd.FileName);

                    Bitmap bmp1 = new Bitmap(sd.FileName);
                    Color px = bmp1.GetPixel(bmp1.Width - 1, bmp1.Height - 1);                    
                    MessageBox.Show(px.R.ToString() + "," + px.G.ToString() + "," + px.B.ToString());

                }
            }
        }
    }
}
