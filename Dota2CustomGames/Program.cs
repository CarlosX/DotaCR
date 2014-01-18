using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Net;

namespace Dota2CustomRealms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.ThreadException += Application_ThreadException;
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Clipboard.SetText("Message:" + e.Exception.Message + "\nStack Trace:" + e.Exception.StackTrace + (e.Exception.InnerException != null ? "\nInnerException Message" + e.Exception.InnerException.Message + "\nInnerException Stack Trace:" + e.Exception.InnerException.StackTrace : ""));
            try
            {
                MessageBox.Show("An unexpected error has occured and Dota 2 Custom Realms cannot continue.\nThe error details are currently being sent to the developers.\nIf the details are sent successfully, another messagebox will appear with a reference code.\nIf you have any extra details on what might have caused this error then post them along with the reference code on the subreddit (http://www.reddit.com/r/dotacr).");
                using (WebClient client = new WebClient())
                {

                    byte[] response = client.UploadValues("http://www.dota2cr.com/log_error.php", new NameValueCollection()
                {
                    { "error", "Message:" + e.Exception.Message + "\nStack Trace:" + e.Exception.StackTrace + (e.Exception.InnerException != null ? "\nInnerException Message" + e.Exception.InnerException.Message + "\nInnerException Stack Trace:" + e.Exception.InnerException.StackTrace : "") },
                });
                    if (response != null)
                    {
                        string code = System.Text.Encoding.UTF8.GetString(response, 0, response.Length);
                        MessageBox.Show("This error's reference code is: " + code);
                    }
                }
            }
            catch
            {
                MessageBox.Show("There was an error while sending the error report =(");
            }
            finally
            {
                MessageBox.Show("If you were attempting to host a game, you should inform the other players that the game isn't going to work.\nIt is strongly recommended that you restart D2CR as soon as possible.");
            }
        }
    }
}
