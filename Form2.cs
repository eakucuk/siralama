using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;
using HtmlAgilityPack;


namespace nesatilir_n11
{
    public partial class Form2 : Form
    {
        string id_deger = "";
        bool hasFound = false;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            webBrowser1.Visible = false;
            pictureBox1.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            richTextBox3.Visible = false;
            richTextBox1.Visible = false;
            button2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            richTextBox3.Visible = false;
            richTextBox1.Visible = true;
            button2.Visible = true;
            string adres = textBox1.Text;
            WebRequest istek = HttpWebRequest.Create(adres);
            WebResponse cevap;
            cevap = istek.GetResponse();
            StreamReader bilgiler = new StreamReader(cevap.GetResponseStream());
            string gelen = bilgiler.ReadToEnd();
            int baslik = gelen.IndexOf("<title>") + 7;
            int baslikson = gelen.Substring(baslik).IndexOf("</title>");
            string baslik1 = gelen.Substring(baslik, baslikson);
            label2.Text = baslik1;

            HtmlAgilityPack.HtmlWeb hweb2 = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc2 = hweb2.Load(adres);

            //foreach (HtmlNode img in doc2.DocumentNode.SelectNodes("//img"))
            //{
            //    richTextBox3.Text = img.GetAttributeValue("src", null);
            //    webBrowser1.Navigate(richTextBox3.Text);

            //}
            




            List<string> links2 = new List<string>();
            if (links2 != null)
            {
                foreach (HtmlAgilityPack.HtmlNode nd2 in doc2.DocumentNode.SelectNodes("//a[@data-productid]"))
                {
                    links2.Add(nd2.Attributes["data-productid"].Value);
                }
                foreach (string str1 in links2)
                {
                    id_deger = str1;
                    label3.Text += str1 + "\n";

                }
               
            }
            
           
        }
      
        private void button2_Click(object sender, EventArgs e)
        {
            string kelime = richTextBox1.Text;
            WebRequest istek2 = HttpWebRequest.Create("https://www.n11.com/arama?q=" + kelime);
            string link = "https://www.n11.com/arama?q=" + kelime;
            WebResponse cevap2;
            cevap2 = istek2.GetResponse();
            StreamReader bilgiler2 = new StreamReader(cevap2.GetResponseStream());
            string gelen2 = bilgiler2.ReadToEnd();
            HtmlAgilityPack.HtmlWeb hweb = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = hweb.Load(link);
            List<string> links = new List<string>();
            if (links != null)
            {
                foreach (HtmlAgilityPack.HtmlNode nd in doc.DocumentNode.SelectNodes("//div[@id]"))
                {
                    links.Add(nd.Attributes["id"].Value);
                   
                    

                    if (nd.Attributes["id"].Value == "p-" + id_deger)
                    {

                        label6.Text = nd.Attributes["data-position"].Value;
                        hasFound = true;
                        break;

                    }
                    
                }
               

            }


          
        }





    }
            }

            
    
