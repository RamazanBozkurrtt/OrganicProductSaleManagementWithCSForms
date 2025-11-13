using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrganicProductSaleManagementV3
{
    public partial class loadingScreen : Form
    {

        private double opacityValue = 1.0;
        private const double opacityDecrement = 0.01;
        private string[] loadingMessages = {
        "Veritabanı bağlantısı kontrol ediliyor...",
        "Temel Veriler Analiz Ediliyor...",
        "Satıcı Profilleri Yükleniyor...",
        "Kaynaklar Yükleniyor...",
        "Yetkilendirme ve Kısıtlama İşlemleri Yapılıyor...",
        "Son Denetlemeler Yapılıyor...",
        "Yükleme Başarılı!"
    };

        public loadingScreen()
        {
            InitializeComponent();
        }

        private void loadingScreen_Load(object sender, EventArgs e)
        {

            this.MaximizeBox = false;
            this.MinimizeBox = true;
            progressBar1.Value = 0;
            progressBar1.Maximum = 100; 
            progressBar1.Style = ProgressBarStyle.Continuous;
            opacityTimer.Interval = 10;

            backgroundWorker1.WorkerReportsProgress = true;  
            backgroundWorker1.WorkerSupportsCancellation = true;  
            backgroundWorker1.RunWorkerAsync();
        }



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            for (int i = 1; i <= 100; i++)
            {
                if (backgroundWorker1.CancellationPending)  
                {
                    e.Cancel = true;
                    break;
                }
                if (i <= 15 && i > 0)//Veritabanı
                {
                    Thread.Sleep(150);
                }
                else if (i <= 30 && i > 15)//Temel Veriler
                {
                    Thread.Sleep(75);
                }
                else if (i <= 45 && i > 30)//Satıcı Profilleri
                {
                    Thread.Sleep(25);

                }
                else if (i <= 60 && i > 45) //Kaynaklar
                {
                    Thread.Sleep(165);

                }
                else if (i <= 75 && i > 60)//Yetkilendirme Ve Kısıtlama
                {
                    Thread.Sleep(25);

                }
                else if (i <= 90 && i > 75)//Son denetimler
                {
                    Thread.Sleep(50);

                }
                else if (i <= 100 && i > 90)
                {
                    Thread.Sleep(200);
                }
                backgroundWorker1.ReportProgress(i);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

            

                    progressBar1.Value = e.ProgressPercentage;


                    if (e.ProgressPercentage <= 15 && e.ProgressPercentage > 0)
                    {
                        label1.Text = loadingMessages[0];
                    }
                    else if (e.ProgressPercentage <= 30 && e.ProgressPercentage > 15)
                    {
                        label1.Text = loadingMessages[1];
                    }
                    else if (e.ProgressPercentage <= 45 && e.ProgressPercentage > 30)
                    {
                        label1.Text = loadingMessages[2];
                    }
                    else if (e.ProgressPercentage <= 60 && e.ProgressPercentage > 45)
                    {
                        label1.Text = loadingMessages[3];
                    }
                    else if (e.ProgressPercentage <= 75 && e.ProgressPercentage > 60)
                    {
                        label1.Text = loadingMessages[4];
                    }
                    else if (e.ProgressPercentage <= 100 && e.ProgressPercentage > 75)
                    {
                        label1.Text = loadingMessages[5];
                    }
                
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("İşlem iptal edildi!");
            }
            else
            {
                this.label1.Text = this.loadingMessages[6];
                opacityTimer.Start();
            }
        }


        private void opacityTimer_Tick(object sender, EventArgs e)
        {
            // Saydamlaştırma işlemi
            if (opacityValue > 0)
            {
                opacityValue -= opacityDecrement;
                this.Opacity = opacityValue; 
            }
            else
            {
                opacityTimer.Stop();
                this.DialogResult = DialogResult.OK;
            }
        }



    }



}
