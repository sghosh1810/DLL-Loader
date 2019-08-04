using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using ManualMapInjection.Injection;
using System.IO;
using Authed;
using Jose.jwe;
using Newtonsoft.Json;

namespace Howling_Software
{
    public partial class Main : Form
    {
        
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        Checker check = new Checker();
        public String config_selector="None";
        public String config_name = "None";
        public double version = 1.4;

        public Main()
        {
            bool isLegit = check.CheckFiles();
            if (!isLegit)
            {
                Error.CstmError.Show("You don't have permission to access the tool due wrong/modified files!");
                Application.Exit();
            }
            
            InitializeComponent();
        }

        private void panel2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            loader Login = new loader();
            Globals.loggedoff = "true";
            this.Close();
            Login.Show();

        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (Globals.AdminName.Contains(Globals.username))
            {
                Adminbtn.Visible = true;
            }
            CheatList.Items.Add(Globals.cheat1);
            CheatList.Items.Add(Globals.cheat2);
            Welcomelbl.Text = "Welcome: " + Globals.username + " !";
        }

        
        private void Launch_button_Click(object sender, EventArgs e)
        {
            string selecedcheat = CheatList.GetItemText(CheatList.SelectedItem);
            WebClient client = new WebClient();

            if ((String)CheatList.SelectedItem == "Wanheda HvH")
            {
                client.DownloadFile(Globals.DownLink1, Globals.CheatName1);
            }


            if ((String)CheatList.SelectedItem == "Crystality HvH")
            {
                client.DownloadFile(Globals.DownLink2, Globals.CheatName2);
            }


            var name = "csgo";
            var target = Process.GetProcessesByName(name).FirstOrDefault();
             if (target == null)
             {
                 Error.CstmError.Show("Process not found");
                 return;
             }
             
            if (selecedcheat == "Wanheda HvH") 
            {
                
                var injector = new ManualMapInjector(target) { AsyncInjection = true };
                string boom = $"hmodule = 0x{injector.Inject(Howling_Software.Properties.Resources.Cheat1).ToInt64():x8}"; // insert your cheat in Resources.resx
                Application.ExitThread();
                Application.Exit();
            }

            if (selecedcheat == "Crystality HvH")
            {
                
                var injector = new ManualMapInjector(target) { AsyncInjection = true };
                string boom = $"hmodule = 0x{injector.Inject(Howling_Software.Properties.Resources.Cheat2).ToInt64():x8}"; // insert your cheat in Resources.resx
                Application.ExitThread();
                Application.Exit();
            }




            if (selecedcheat == "")
            {
                Error.CstmError.Show("No Cheat selected");
            }
            
        }

        private void CheatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((String)CheatList.SelectedItem == "Wanheda HvH")
            {
                label7.Text = "10/1/2019";
                label8.ForeColor = ColorTranslator.FromHtml("#00fa9a");
                label8.Text = "Undetected";
                config_selector = "http://duxhook.xyz/cheat/config/wan.rar";
                config_name = "wan.rar";
            }
            if ((String)CheatList.SelectedItem == "Crystality HvH")
            {
                label7.Text = "03/01/2019";
                label8.ForeColor = ColorTranslator.FromHtml("#FF00FF");
                label8.Text = "Unknown";
                config_selector = "http://duxhook.xyz/cheat/config/cry.rar";
                config_name = "cry.rar";
            }
        }

        private void Adminbtn_Click(object sender, EventArgs e)
        {
            AdminPanel Ad = new AdminPanel();
            Ad.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(config_selector=="None"||config_name=="None")
            {
                Error er = new Error("No cheat is selected");
                er.Show();
            }
            else
            {
                WebClient client = new WebClient();
                client.DownloadFile(config_selector, config_name);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("http://duxhook.xyz/cheat/version/up.txt");
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd(); 
            if(Convert.ToDouble(content)> version)
            {
                Error er = new Error("A new version is available");
                er.Show();
                client.DownloadFile("http://duxhook.xyz/cheat/loader/Release.zip","latest.zip");
                System.Threading.Thread.Sleep(50);
            }
            else
            {
                Error er = new Error("You have the latest version");
                er.Show();
            }
        }
    }
}
