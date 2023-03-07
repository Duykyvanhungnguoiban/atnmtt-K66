using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringToBit
{
    class Program
    {
        static void Main(string[] args)
        {
            String bits = StringToBinary("a");
            Console.WriteLine(bits);
            String myString = BinaryToString(bits);
            Console.WriteLine(myString);

            //String path = "D:\\1.jpg";
            //Bitmap bmp = new Bitmap(path);
            //Console.WriteLine(ReadPixel(bmp, 0, 0));
            Console.ReadLine();
        }

        static int EncryptPixel(int valueR, String bit)
        {
            if(bit.Equals("0"))
            {
                if(valueR % 2 == 0)
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

        static String DecryptPixel(int valueR)
        {
            if (valueR % 2 == 0)
                return "0";
            else
                return "1";
        }

        static string ReadPixel(Bitmap bmp, int x, int y)
        {   
            Color px = bmp.GetPixel(x, y);
            return px.R.ToString() + ":" + px.G.ToString() + ":" + px.B.ToString();
        }

        //static string StringToBits(Encoding encoding, string text)
        //{
        //    return string.Join("", encoding.GetBytes(text).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
        //}

        //static String BitsToString(Encoding encoding, String bits)
        //{

        //}

        public static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        public static string BinaryToString(string data)
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
