using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace Dota2CustomRealms
{
    class ChatController
    {
        const string CHAT_COLOURS = "{\\colortbl;\\red79\\green157\\blue204;\\red100\\green100\\blue100;\\red28\\green148\\blue151;\\red126\\green126\\blue126;\\red237\\green73\\blue36;\\red168\\green172\\blue130;\\red17\\green46\\blue162;\\red255\\green0\\blue0;\\red0\\green\\255\\blue0;\\red115\\green115\\blue115;}";
        const string RTF_DEF = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang5129{\\fonttbl{\\f0\\fnil\\fcharset0 Microsoft Sans Serif;}}" + CHAT_COLOURS + "\r\n\\viewkind4\\uc1\\pard\\f0\\fs17\\par\r\n}";

        public string Username = "";

        private Control parent;
        private TabControl container = new TabControl();
        private Dictionary<string, ChatChannel> channels = new Dictionary<string, ChatChannel>();

        private TextBox input = new TextBox();
        private Button send = new Button();

        public class SendChatMessageArgs : EventArgs
        {
            public string Channel, Message;
            public SendChatMessageArgs(string Message, string Channel)
            {
                this.Channel = Channel;
                this.Message = Message;
            }
        }

        public event EventHandler<SendChatMessageArgs> SendChatMessage;

        public ChatController(Control Container)
        {
            parent = Container;

            input.TabIndex = 0;
            input.Multiline = true;

            input.PreviewKeyDown += input_PreviewKeyDown;

            send.Text = ">";
            send.Size = new Size(22, 22);
            send.TabIndex = 1;

            send.Click += send_Click;

            parent.Controls.Add(container);
            parent.Controls.Add(input);
            parent.Controls.Add(send);

            container.Location = new Point(0, 0);
            container.Size = new Size(parent.Width - parent.Padding.Left - parent.Padding.Right, parent.Height - parent.Padding.Top - parent.Padding.Bottom - 25);
            container.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            input.Location = new Point(0, parent.Height - parent.Padding.Bottom - 24);
            input.Size = new Size(parent.Width - parent.Padding.Left - parent.Padding.Right - 25, 20);
            input.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;

            send.Location = new Point(parent.Width - parent.Padding.Right - 24, parent.Height - parent.Padding.Bottom - 24);
            send.Size = new Size(22, 22);
            send.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        }

        void send_Click(object sender, EventArgs e)
        {
            if (input.Text.Replace("\n", "").Replace("\r", "").Trim().Length > 0)
            {
                if (SendChatMessage != null) SendChatMessage(this, new SendChatMessageArgs(input.Text.Replace("\n", "").Replace("\r", ""), ""));
                input.Clear();
                input.Select();
            }
        }

        void input_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Enter) send_Click(sender, e);
        }

        private delegate void ChannelDelegate(string name);
        private delegate void AddChannelDelegate(string name, string title);

        public void AddChannel(string name, string title = null)
        {
            if (title == null) title = name;

            if (channels.ContainsKey(name)) return;

            if (container.InvokeRequired)
            {
                container.Invoke(new AddChannelDelegate(AddChannel), new object[] { name, title });
                return;
            }

            ChatChannel chan = new ChatChannel();
            chan.Name = name;
            chan.Page = new TabPage();
            chan.Page.Text = title;
            
            chan.TextContainer  = new RichTextBox();
            chan.Page.Controls.Add(chan.TextContainer);

            chan.TextContainer.Dock = DockStyle.Fill ;
            chan.TextContainer.LinkClicked += TextContainer_LinkClicked;

            chan.TextContainer.Text = "";
            chan.TextContainer.Rtf = RTF_DEF;

            chan.TextContainer.WordWrap = true;
            chan.TextContainer.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            chan.TextContainer.ReadOnly = true;

            container.TabPages.Add(chan.Page);
            container.SelectedTab = chan.Page;

            channels.Add(name, chan);
        }

        void TextContainer_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        public void RemoveChannel(string name)
        {
            if (!channels.ContainsKey(name)) return;

            if (container.InvokeRequired)
            {
                container.Invoke(new ChannelDelegate(RemoveChannel), new object[] { name });
                return;
            }

            ChatChannel chan = channels[name];
            container.Controls.Remove(chan.Page);

        }

        private delegate void AddMessageDelegate(string Source, string Message, string Channel);

        public void AddMessage(string Source, string Message, string Channel)
        {

            if (!channels.ContainsKey(Channel)) return;

            ChatChannel chan = channels[Channel];

            if (chan.TextContainer.InvokeRequired)
            {
                chan.TextContainer.Invoke(new AddMessageDelegate(AddMessage), new object[] { Source, Message, Channel });
                return;
            }

            string strRTF = chan.TextContainer.Rtf;
            // Colour code grabbed from http://www.codeproject.com/Articles/15038/C-Formatting-Text-in-a-RichTextBox-by-Parsing-the

            /* 
                * ADD COLOUR TABLE TO THE HEADER FIRST 
                * */

            // Search for colour table info, if it exists (which it shouldn't)
            // remove it and replace with our one
            int iCTableStart = strRTF.IndexOf("colortbl;");

            // MR. RTF stuffed up, this can't run more than once or it breaks the rtf formatting :P
            //if (iCTableStart != -1) //then colortbl exists
            //{
            //    //find end of colortbl tab by searching
            //    //forward from the colortbl tab itself
            //    int iCTableEnd = strRTF.IndexOf('}', iCTableStart);
            //    strRTF = strRTF.Remove(iCTableStart, iCTableEnd - iCTableStart);

            //    //now insert new colour table at index of old colortbl tag
            //    strRTF = strRTF.Insert(iCTableStart,
            //        // CHANGE THIS STRING TO ALTER COLOUR TABLE
            //        CHAT_COLOURS);
            //}

            ////colour table doesn't exist yet, so let's make one
            //else
            if(iCTableStart == -1)
            {
                // find index of start of header
                int iRTFLoc = strRTF.IndexOf("\\rtf");
                // get index of where we'll insert the colour table
                // try finding opening bracket of first property of header first                
                int iInsertLoc = strRTF.IndexOf('{', iRTFLoc);

                // if there is no property, we'll insert colour table
                // just before the end bracket of the header
                if (iInsertLoc == -1) iInsertLoc = strRTF.IndexOf('}', iRTFLoc) - 1;

                // insert the colour table at our chosen location                
                strRTF = strRTF.Insert(iInsertLoc,
                    // CHANGE THIS STRING TO ALTER COLOUR TABLE
                    CHAT_COLOURS);
            }

            // colours
            //1 = self
            //2 = self / otherplayer text
            //3 = otherplayer
            //4 = connected
            //5 = admin
            //6 = otheropponent
            //7 = Verified Hosts
            //8 - left / dc'ed (only for non #General channels)
            //9 - Is hosting a new game message

            Message = Message.Replace("\\", "\\\\").Replace("{", "\\{").Replace("}", "\\}");
            Source = Source.Replace("\\", "\\\\").Replace("{", "\\{").Replace("}", "\\}");

            // Lets format the text!
                if(Source.StartsWith("[CR]")) // ADMIN
                {
                    Message = "\\b\\cf5" + Source + "\\b0  " + Message;
                }
                else if (Source.StartsWith("[VHost]")) // VERIFIED HOSTS
                {
                    Message = "\\b\\cf7" + Source + "\\b0\\cf2  " + Message;
                }
                else if (Source == Username) // Self
                {
                    Message = "\\b\\cf1 " + Source + "\\b0\\cf2  " + Message;
                }
                else if (Message.ToLowerInvariant().Contains(Username.ToLowerInvariant())) // Enemy
                {
                    Message = "\\b\\cf6 " + Source + "\\b0\\cf2  " + Message;
                }
                else
                {
                    Message = "\\b\\cf3 " + Source + "\\b0\\cf2  " + Message;
                }
            
            /*else // Some other kind of message
            {
                if (Channel != "#General" && (Message.EndsWith(" has quit") || Message.EndsWith(" has disconnected") || Message.EndsWith(" has left")))
                {
                    Message = "\\b\\cf8" + Message + "\\b0";
                }
                else if (Message.EndsWith(" is hosting a new game lobby."))
                {
                    Message = "\\cf9" + Message;
                }
                else if (Message.EndsWith(" is hosting a new game lobby!"))
                {
                    Message = "\\b\\cf7" + Message + "\\b0";
                }
                else
                {
                    Message = "\\cf4" + Message;
                }
            }*/
            Message = Message.Replace("\n", "\\par\r\n");
            Message += "\\cf0\\par\r\n}";

            strRTF = strRTF.Remove(strRTF.LastIndexOf('}')) + Message;

            chan.TextContainer.Rtf = strRTF;
            //rtxChatMessages.Rtf = rtxChatMessages.Rtf.Remove(rtxChatMessages.Rtf.LastIndexOf('}')) + Message;

            chan.TextContainer.SelectionStart = chan.TextContainer.Text.Length;
            chan.TextContainer.ScrollToCaret();


        }

        private class ChatChannel
        {
            public string Name;
            public TabPage Page;
            public RichTextBox TextContainer;
        }

    }
}
