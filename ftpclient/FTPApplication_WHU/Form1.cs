using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Resources;


namespace FTPApplication_WHU
{
    
    public partial class Form1 : Form
    {
        /**
         * This form includes user login set IP address of FTP server.
         * Once the control connection is done, it will turn to another form to do ftp operations
         * 
         * What I'm thinking about is to create a .dat file to store the IDs and IPs user used to input, but anyway, it's optional.
         * Another suggestion is to initialize the ftpSeverIP for default thus we don't need to create .dat file.
         * 
         *
         * 
         **/
        private string ftpServerIP;
        private string ftpUserID;
        private string ftpPassword;

        Point mouseOff;//用于获取鼠标位置
        bool leftFlag;//移动标识
        public Form1()
        {
            InitializeComponent();
            this.IPBox.Text = "127.0.0.1";
            // ResourceManager rm = new ResourceManager("My_resources", typeof(Resources).Assembly);
            this.hideUser(true);
        }     

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /**
             * Try to connect to the server
             **/

            ftpServerIP = IPBox.Text;
            ftpUserID = IDBox.Text;
            ftpPassword = passwordBox.Text;

          //  if (ftpUserID.Equals("testtest"))
            {
                // just for test.
                // 如果连接成功建立了，就把返回的那个ftpRequest对象放在this的域内，然后把this传递给MainForm
                // 这样就可以继续使用了
                IDBox.Text = "OK";
                MainForm mainform = new MainForm(this);
                mainform.Show();
            }
        }


        private void hideUser(bool isHide)
        {
            if (isHide)
            {
                this.label2.Hide();
                this.label3.Hide();
                this.line1.Hide();
                this.line2.Hide();
                this.IDBox.Hide();
                this.IDBox.Clear();
                this.passwordBox.Clear();
                this.passwordBox.Hide();
                this.backBtn.Hide();
                this.button1.Location = new Point(42, 360);
                this.label5.Show();
            }
            else
            {
                this.label2.Show();
                this.label3.Show();
                this.line1.Show();
                this.line2.Show();
                this.label5.Hide();
                this.IDBox.Show();
                this.label5.Hide();
                this.backBtn.Show();
                this.passwordBox.Show();
                this.button1.Location = new Point(42, 541);
            }
        }
        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // hover effect (not as easy as QT)
        private void exitBtn_MouseEnter(object sender, EventArgs e)
        {
            this.exitBtn.BackgroundImage = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("close_clicked");
        }
        private void exitBtn_MouseLeave(object sender, EventArgs e)
        {
            this.exitBtn.BackgroundImage = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("close");
        }


        // make this form move. from csdn
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y <= 40)
            {
                mouseOff = new Point(e.X, e.Y);//获取当前鼠标位置
                leftFlag = true;//用于标记窗体是否能移动(此时鼠标按下如果说用户拖动鼠标则窗体移动)
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Location = new Point(Control.MousePosition.X - mouseOff.X, Control.MousePosition.Y - mouseOff.Y);
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false; //释放鼠标标识为false 表示窗体不可移动
            }
        }

        private void box_GetFocus(object sender, EventArgs e)
        {
            //System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
            if (sender as System.Windows.Forms.TextBox == this.IDBox)
            {
                this.line1.BackColor = Color.FromArgb(0, 111, 255);
            }
            else if(sender as System.Windows.Forms.TextBox == this.passwordBox)
            {
                this.line2.BackColor = Color.FromArgb(0, 111, 255);
            }
            else if (sender as System.Windows.Forms.TextBox == this.IPBox)
            {
                this.line3.BackColor = Color.FromArgb(0, 111, 255);
            }

        }
        private void box_LostFocus(object sender, EventArgs e)
        {
            if (sender as System.Windows.Forms.TextBox == this.IDBox)
            {
                this.line1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            }
            else if (sender as System.Windows.Forms.TextBox == this.passwordBox)
            {
                this.line2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            }
            else if (sender as System.Windows.Forms.TextBox == this.IPBox)
            {
                this.line3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.hideUser(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.hideUser(true);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
