using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhungDuLieuVaoAnh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void rbMaHoa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMaHoa.Checked)
            {
                rbGiaiMa.Checked = false;
                tbDuLieu.ReadOnly = false;
            }
        }

        private void rbGiaiMa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGiaiMa.Checked)
            {
                rbMaHoa.Checked = false;
                tbDuLieu.ReadOnly = true;
            }
        }

        private void btTim_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Ảnh định dạng bmp (*.bmp) | *.bmp";
            od.Multiselect = false;
            if(od.ShowDialog() == DialogResult.OK)
            {
                tbDuongDan.Text = od.FileName;
            }    
        }

        private void btThucHien_Click(object sender, EventArgs e)
        {
            if(rbMaHoa.Checked)
            {
                // Nhung du lieu vao anh
                if(!File.Exists(tbDuongDan.Text))
                {
                    MessageBox.Show("Bạn phải chọn tệp tin ảnh");
                    btTim.Focus();
                    return;
                }    

                if(String.IsNullOrEmpty(tbDuLieu.Text))
                {
                    MessageBox.Show("Bạn phải nhập dữ liệu cần nhúng vào ảnh");
                    tbDuLieu.Focus();
                    return;
                }

                String bits = StringToBinary(tbDuLieu.Text);
                Bitmap bmp = new Bitmap(tbDuongDan.Text);
                for(int i = 0; i < bits.Length; i++)
                {
                    int x = i % bmp.Width;
                    int y = i / bmp.Width;
                    Color px = bmp.GetPixel(x, y);
                    int valueREncrypt = EncryptPixel(px.R, bits[i].ToString());
                    Color pxe = Color.FromArgb(px.A, valueREncrypt, px.G, px.B);
                    bmp.SetPixel(x, y, pxe);
                }

                // Ghi do dai chuoi vao pha R cua pixel cuoi cung 959,539-10
                int x1 = bmp.Width - 1;
                int y1 = bmp.Height - 1;
                Color px1 = bmp.GetPixel(x1, y1);
                Color pxe1 = Color.FromArgb(px1.A, tbDuLieu.Text.Length, px1.G, px1.B);
                bmp.SetPixel(x1, y1, pxe1);
                Color px2 = bmp.GetPixel(x1, y1);

                // Luu anh sau khi nhung
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Ảnh định dạng bmp (*.bmp) | *.bmp";
                if(sd.ShowDialog() == DialogResult.OK)
                {
                    bmp.Save(sd.FileName);
                    tbDuLieu.Text = "";
                    tbDuongDan.Text = "";
                    bmp.Dispose();

                    Bitmap bmp1 = new Bitmap(sd.FileName);
                    Color px3 = bmp1.GetPixel(x1, y1);
                    int r = px3.R;
                }   
                

            }   
            else
            {
                // Tach du lieu ra khoi anh
                if (!File.Exists(tbDuongDan.Text))
                {
                    MessageBox.Show("Bạn phải chọn tệp tin ảnh");
                    btTim.Focus();
                    return;
                }

                Bitmap bmp = new Bitmap(tbDuongDan.Text);
                Color px1 = bmp.GetPixel(bmp.Width - 1, bmp.Height - 1);
                int chieuDaiBit = px1.R * 8;
                String bits = "";
                for(int i = 0; i < chieuDaiBit; i++)
                {
                    int x = i % bmp.Width;
                    int y = i / bmp.Width;
                    Color px = bmp.GetPixel(x, y);
                    bits += DecryptPixel(px.R);
                }
                String duLieu = BinaryToString(bits);
                tbDuLieu.Text = duLieu;
            }    
        }

        private int EncryptPixel(int valueR, String bit)
        {
            if (bit.Equals("0"))
            {
                if (valueR % 2 == 0)
                {
                    return valueR;
                }
                else
                {
                    return (valueR + 1) % 256;
                }
            }
            else
            {
                if (valueR % 2 == 0)
                {
                    return (valueR + 1) % 256;
                }
                else
                    return valueR;
            }
        }

        private String DecryptPixel(int valueR)
        {
            if (valueR % 2 == 0)
                return "0";
            else
                return "1";
        }

        

        private string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        private string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }
    }
}
