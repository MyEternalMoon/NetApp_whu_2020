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
    public partial class MainWindow : Form
    {
        private Form1 parent;
        Point mouseOff;//用于获取鼠标位置
        bool leftFlag;//移动标识
        public MainWindow(Form1 parent)
        {
            this.parent = parent;
            this.parent.Hide();
            InitializeComponent();
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
            reqFTP.UsePassive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            //指定数据的传输类型
            reqFTP.UseBinary = true;
            //指定主动方式
            reqFTP.UsePassive = false;
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
                reqFTP.UsePassive = false;
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
                reqFTP.UsePassive = false;
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
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + this.parent.ftpServerIP + "/"));
                ftp.Credentials = new NetworkCredential(this.parent.ftpUserID, this.parent.ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                ftp.UsePassive = false;
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
            //  MessageBox.Show("ftp://" + this.parent.ftpServerIP + "/" + fileName);
            Uri u = new Uri("ftp://" + parent.ftpServerIP + "/" + fileName);
            try
            {
                //filePath是文件创建后所在的完整路径
                //fileName是所要创建的文件名
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(u);

                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = false;
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
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + this.parent.ftpServerIP + "/" + filename));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = false;
                reqFTP.Credentials = new NetworkCredential(this.parent.ftpUserID, this.parent.ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;

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
            return fileSize;
        }

        private void Rename(string currentFilename, string newFilename)//重命名方法
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + this.parent.ftpServerIP + "/" + currentFilename));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = false;
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
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.parent.Close();
        }
        private void fileListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {//如果当前行为选中行。
             //绘制选中时要显示的蓝色边框。
                Color c = Color.FromArgb(0, 111, 255);
                e.Graphics.FillRectangle(new SolidBrush(c), e.Bounds);//绘制背景
            }
            e.DrawBackground();

            e.DrawFocusRectangle();
            StringFormat strFmt = new System.Drawing.StringFormat();
            strFmt.Alignment = StringAlignment.Center; //文本垂直居中
            strFmt.LineAlignment = StringAlignment.Center; //文本水平居中
            e.Graphics.DrawString(fileListBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, strFmt);
        }
        private void fileListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 40;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            string fileName = fileListBox.SelectedItem.ToString();
            if (fileName != string.Empty)
            {
                DeleteFTP(fileName);
            }
        }

        private void getFileBtn_Click(object sender, EventArgs e)
        {
            string[] filenames = this.GetFileList();
            fileListBox.Items.Clear();
            try
            {
                foreach (string filename in filenames)
                {
                    fileListBox.Items.Add(filename);
                }
            }
            catch
            {

            }
        }

        private void uploadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog opFilDIg = new OpenFileDialog();
            if (opFilDIg.ShowDialog() == DialogResult.OK)
            {
                Upload(opFilDIg.FileName);
            }
        }

        private void downloadBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fldDlg = new FolderBrowserDialog();
            string fileName = fileListBox.SelectedItem.ToString();
            if (fileName != string.Empty)
            {
                if (fldDlg.ShowDialog() == DialogResult.OK)
                {
                    Download(fldDlg.SelectedPath, fileName);
                }
            }
            else
            {
                MessageBox.Show("请选择下载的文件！");
            }
        }
    }
}
    
