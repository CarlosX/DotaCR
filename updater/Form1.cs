using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Reflection;
using System.Windows.Forms;
using Ionic.Zip;

namespace updater
{
    public partial class frmUpdater : Form
    {
        string updateserver = "http://dota2cr.com";
        string currentversion = "";
        string latestversion = "";
        string appPath = Path.GetDirectoryName(Application.ExecutablePath);
        WebClient client = new WebClient();
        public frmUpdater()
        {
            InitializeComponent();
        }

        public void frmUpdater_Load(object sender, EventArgs e)
        {
            /*Stream stream = client.OpenRead(updateserver + "/cur.txt");
            StreamReader reader = new StreamReader(stream);
            latestversion = reader.ReadToEnd();
            stream.Close();
            reader.Close();*/
            latestversion = client.DownloadString(updateserver + "/cur.txt");
            currentversion = AssemblyName.GetAssemblyName("Dota2CustomRealms.exe").Version.ToString();
            lblCurrentVersion.Text = "Current Version: " + currentversion;
            lblLatestVersion.Text = "Latest Version: " + latestversion;
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

            string zipp = updateserver + "/current.zip";
            Uri uri = new Uri(zipp);
            client.DownloadFileAsync(uri, appPath + "\\data\\current.zip");

            btnBegin.Text = "Working";
            btnBegin.Enabled = false;
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

            string[] IgnoreFiles = new string[] {".dll", "updater.exe" };

            if (File.Exists("data\\current.zip"))
            {
                progressBar1.Value = 0;
                int filesextracted = 0;
                using (ZipFile zip = ZipFile.Read("data\\current.zip"))
                {
                    foreach (ZipEntry x in zip)
                    {
                        bool Ignored = false;
                        foreach (string Ignore in IgnoreFiles)
                        {
                            if (x.FileName.EndsWith(Ignore))
                            {
                                Ignored = true;
                                break;
                            }
                        }
                        if (Ignored == false)
                        {
                            x.Extract(appPath, true);
                        }
                        filesextracted++;
                        double percentage = filesextracted / zip.Count * 100;
                        progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
                    }
                }
                File.Delete("data\\current.zip");

                MessageBox.Show("Download Completed");

                btnBegin.Text = "Begin";
                btnBegin.Enabled = true;
            }
        }




    }
}
