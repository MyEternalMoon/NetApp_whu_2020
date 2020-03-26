using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPApplication_WHU
{
    public partial class MainForm : Form
    {
        private Form1 parent;
        public MainForm(object parent)
        {
            // 我把父窗口类作为参数传进来了，父窗口的域里面可以存放ftp连接类对象
            this.parent = (Form1)parent;
            
            InitializeComponent();
            this.parent.Hide();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.parent.Close();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
