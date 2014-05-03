using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace LegacyUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            using (WebClient Client = new WebClient())
            {
                this.Hide();
                FileInfo file = new FileInfo("Dota 2 Custom Realms.application");
                Client.DownloadFile("http://www.dota2cr.com/dotacr/Dota%202%20Custom%20Realms.application", file.FullName);
                Process.Start(file.FullName);
                MessageBox.Show("The installation wizard will guide you through the installation. Please click on Install when prompted.\nA desktop shortcut will be created for you. You may delete the old directory containing the old Dota 2 Custom Realms client.");
                Application.Exit();
            }
        }
    }
}
