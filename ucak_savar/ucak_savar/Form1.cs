using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ucak_savar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int yer;
        bool a = false;
        Random rnd = new Random();
        List<PictureBox> ucaklar = new List<PictureBox>();
        List<PictureBox> mermiler = new List<PictureBox>();
        int puan =0;
        private void mermileriolustur()
        {
            PictureBox mermi = new PictureBox();
            mermi.Size = new Size(30, 30);
            mermi.SizeMode = PictureBoxSizeMode.StretchImage;
            mermi.ImageLocation = "png/mermi.png";
            int mermilocx = ucaksavar.Location.X + 18;
            int mermilocy = ucaksavar.Location.Y - 20;
            mermi.Location = new Point(mermilocx, mermilocy);
            this.Controls.Add(mermi);
            mermiler.Add(mermi);
        }
        private void mermilerifirlat()
        {
           
                for (int i = 0; i < mermiler.Count; i++)
                {
                    mermiler[i].Top = mermiler[i].Top - 4;
           
                }
          
        }
        private void oyunu_bitir()
        {
            PictureBox yanma = new PictureBox();
            yanma.Size = new Size(50, 50);
            yanma.SizeMode = PictureBoxSizeMode.StretchImage;
            yanma.ImageLocation = "png/yanma.png";

            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            for (int t = 0; t < mermiler.Count; t++)
            {
                mermiler[t].Top = 0;
                this.Controls.Remove(mermiler[t]);

            }
            for (int j = 0; j < ucaklar.Count; j++)
            {
                ucaklar[j].Top = 0;
                this.Controls.Remove(ucaklar[j]);


            }
            ucaklar.Clear();
            mermiler.Clear();
            //Ram temizleme
            GC.Collect(); //Çöp Toplayıcısı
            GC.WaitForPendingFinalizers();  //Çöp Yakıcısı
            MessageBox.Show("Oyun Bitti. Puanınız..:" + puan.ToString());
            puan = 0;
            label5.Text = puan.ToString();

        }

        private void ucaklariolustur()
        {
            yer = rnd.Next(0, 680);
            PictureBox ucak = new PictureBox();  //Düşecek nesne picturebox olacak
            ucak.Size = new Size(50, 50);    // Düşecek nesnenin boyutu
            ucak.SizeMode = PictureBoxSizeMode.StretchImage;  // Resmin genişliği
            ucak.ImageLocation = "png/ucak.png"; // Picturebox'ın içine gilecek resmin adresi
            ucak.Location = new Point(yer, 50);  // Picturebox'ın forumdaki koordinatları
            ucak.Visible = true;
            this.Controls.Add(ucak);  //
            ucaklar.Add(ucak);
        }
        private void ucaklaridusur()
        {
            for (int i = 0; i < ucaklar.Count; i++)
            {
                    ucaklar[i].Top = ucaklar[i].Top + 5;
               
                    if (ucaklar[i].Location.Y >= 760 ||  (ucaklar[i].Bounds.IntersectsWith(ucaksavar.Bounds)))//ucakların ucaksavara çarpma durumu
                    {
                        oyunu_bitir();
                    }
                
            }
        }
     
        private void timer1_Tick(object sender, EventArgs e)
        {
            mermilerifirlat();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ucaklaridusur();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            ucaklariolustur();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int xkoordinati = ucaksavar.Location.X;
            int ykoordinati = ucaksavar.Location.Y;
            if (e.KeyCode == Keys.Right) //  Klavyeden Sol Yön Tuşuna Basıldığında Uçaksavarın Sola Gitmesi İçin
            {
                xkoordinati = xkoordinati + 9;
            }
            if (e.KeyCode == Keys.Left)   // Klavyeden Sağ Yön Tuşuna Basıldığında Uçaksavarın Sağa Gitmesi İçin
            {
                xkoordinati = xkoordinati - 9;
            }
            if (xkoordinati >= 660)   // Uçaksavarın Form Ekranından Çıkmaması İçin
            {
                xkoordinati = 660;
            }
            else if (xkoordinati <= 0)
            {
                xkoordinati = 0;
            }
            ucaksavar.Location = new Point(xkoordinati, ykoordinati);

            if (e.KeyCode == Keys.Space)
            {
                timer1.Start();
                mermileriolustur();
            }
            if (e.KeyCode == Keys.Enter)
            {
                timer3.Start();
                timer2.Start();
                timer4.Start();
             }
        }



        private void timer4_Tick(object sender, EventArgs e)
        {
             for (int i = 0; i < mermiler.Count; i++)
             {
                for (int j = 0; j < ucaklar.Count; j++)
                {
                    try
                    {
                       
                        if (mermiler[i].Bounds.IntersectsWith(ucaklar[j].Bounds))//mermiler ile uçaklar çarpıştımı
                        {
                            puan += 5;
                            if (puan >= 50)
                            {
                                timer3.Interval = 1500;
                            }
                            else if (puan >= 100)
                            {
                                timer3.Interval = 1000;
                            }
                            else if (puan >= 200)
                            {
                                timer3.Interval = 750;
                            }
                            else if (puan >= 350)
                            {
                                timer3.Interval = 500;
                            }
                            label5.Text = puan.ToString();

                            this.Controls.Remove(mermiler[i]);
                            mermiler.Remove(mermiler[i]);

                            this.Controls.Remove(ucaklar[j]);
                            ucaklar.Remove(ucaklar[j]);
                            //Ram temizleme
                            GC.Collect(); //Çöp Toplayıcısı
                            GC.WaitForPendingFinalizers();  //Çöp Yakıcısı
                        }
                    }
                    catch (ArgumentOutOfRangeException)//picturebox listesinde eleman silindiği için aynı indis tekrar kontrol edildiğinde ilk aşamada null değer olduğu için return yaparak görmezden geliyoruz
                    {

                        return;
                    }
                }

            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
