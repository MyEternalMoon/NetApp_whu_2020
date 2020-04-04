using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace FTPApplication_WHU
{
    public partial class MainForm : Form
    {

        private Form1 parent;
        Point mouseOff;//用于获取鼠标位置
        bool leftFlag;//移动标识
        public MainForm(object parent)
        {
            // 我把父窗口类作为参数传进来了，父窗口的域里面可以存放ftp连接类对象
            this.parent = (Form1)parent;

            InitializeComponent();
            this.parent.Hide();
        }

        private void Upload(string filename)//上传方法
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://" + this.parent.ftpServerIP + "/" + fileInf.Name;
            FtpWebRequest reqFTP;

            //通过前面得到的uri创建FtpWebRequest对象
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + this.parent.ftpServerIP + "/" + fileInf.Name));
           // Console.WriteLine("Hi");
            //提供网络允许的基于密码的身份验证方案
            reqFTP.Credentials = new NetworkCredential(this.parent.ftpUserID, this.parent.ftpPassword);
            //默认情况下KeepAlive是true
            reqFTP.KeepAlive = false;
            //指定要执行的命令
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            //指定数据的传输类型
            reqFTP.UseBinary = true;
            //指定上传文件的长度
            reqFTP.ContentLength = fileInf.Length;
            //缓冲区大小设置成KB
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            //打开一个文件流来读入上传的文件
            FileStream fs = fileInf.OpenRead();

            try
            {
                //把要上传的文件写入流
                Stream strm = reqFTP.GetRequestStream();
                //从文件流中读取数据，一次读 2KB大小的数据
                contentLen = fs.Read(buff, 0, buffLength);
                // Till Stream content ends
                while (contentLen != 0)
                {
                    //把文件的内容从文件流写到FTP上传流中
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                //关闭文件流和请求流
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "上传出错");
            }
            finally
            {
            }
        }

        public void DeleteFTP(string fileName)//删除功能
        {
            try
            {
                string uri = "ftp://" + this.parent.ftpServerIP + "/" + fileName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + this.parent.ftpServerIP + "/" + fileName));

                reqFTP.Credentials = new NetworkCredential(this.parent.ftpUserID, this.parent.ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        public string[] GetFileList()//获取文件列表方法
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + this.parent.ftpServerIP + "/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.parent.ftpUserID, this.parent.ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();

                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
            finally
            {

            }
        }

        private string[] GetFilesDatailList()//获取文件详细信息列表
        {
            string[] downloadFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("fp:/" + this.parent.ftpServerIP + "/"));
                ftp.Credentials = new NetworkCredential(this.parent.ftpUserID, this.parent.ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf("n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
            finally
            {

            }
        }

        private void Download(string filePath, string fileName)//下载方法
        {
            FtpWebRequest reqFTP;
            try
            {
                //filePath是文件创建后所在的完整路径
                //fileName是所要创建的文件名
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp:/ " + this.parent.ftpServerIP + " / " + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.parent.ftpUserID, this.parent.ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }


                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private long GetFileSize(string filename)//获取文件长度方法
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" +this.parent.ftpServerIP+ "/"+filename));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.parent.ftpUserID, this.parent.ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;

                ftpStream.Close();
                response.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
            return fileSize;
        }

        private void Rename(string currentFilename, string newFilename)//重命名方法
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp:/" + this.parent.ftpServerIP+ "/" +currentFilename));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.parent.ftpUserID, this.parent.ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void MakeDir(string dirName)//新建目录方法
        {
            FtpWebRequest reqFTP;
            try
            {
                // dirName是所要创建的目录名
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + this.parent.ftpServerIP+"/" +dirName));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.parent.ftpUserID, this.parent.ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
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

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//重命名按钮
        {
            Rename(textBox2.Text.Trim(), textBox3.Text.Trim());
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        // make this form move. from csdn
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y <= 40)
            {
                mouseOff = new Point(e.X, e.Y);//获取当前鼠标位置
                leftFlag = true;//用于标记窗体是否能移动(此时鼠标按下如果说用户拖动鼠标则窗体移动)
            }
        }
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Location = new Point(Control.MousePosition.X - mouseOff.X, Control.MousePosition.Y - mouseOff.Y);
            }
        }
        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false; //释放鼠标标识为false 表示窗体不可移动
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//上传
        {
            OpenFileDialog opFilDIg = new OpenFileDialog();
            if (opFilDIg.ShowDialog() == DialogResult.OK)
            {
                Upload(opFilDIg.FileName);
            }
        }

        private void button5_Click(object sender, EventArgs e)//下载按钮
        {
            FolderBrowserDialog fldDlg = new FolderBrowserDialog();
            if (textBox1.Text.Trim().Length > 0)
            {
                if (fldDlg.ShowDialog() == DialogResult.OK)
                {
                    Download(fldDlg.SelectedPath, textBox1.Text.Trim());
                }
            }
            else
            {
                MessageBox.Show("请输入要下载的文件名称");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)//列出文件按钮
        {
            string[] filenames = GetFileList();
            listBox1.Items.Clear();
            Console.WriteLine(filenames);
            try
            {
                foreach (string filename in filenames)
                {
                    listBox1.Items.Add(filename);
                }
            }
            catch
            {

            }          
        }

        private void button6_Click(object sender, EventArgs e)//删除按钮
        {
            OpenFileDialog fldDlg = new OpenFileDialog();
            if (textBox1.Text.Trim().Length > 0)
            {
                DeleteFTP(textBox1.Text.Trim());
            }
            else
            {
                MessageBox.Show("请输入要删除的文件名称");
            }
        }

        private void button7_Click(object sender, EventArgs e)//获取文件长度按钮
        {
            long size = GetFileSize(textBox1.Text.Trim());
            MessageBox.Show(size.ToString() + "字节");
        }

        private void button3_Click(object sender, EventArgs e)//新建目录按钮
        {
            MakeDir(textBox4.Text.Trim());
        }
    }
}
