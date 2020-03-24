using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace csftpserver
{
    class UserInfo//用户类
    {
        internal String user;//用户名
        internal String password;//密码
        internal String workDir;//主目录
    }

    class FtpState//FTP的各个状态标志
    {
        internal const int FS_WAIT_LOGIN = 0;
        internal const int FS_WAIT_PASS = 1;
        internal const int FS_LOGIN = 2;
        internal const int FTYPE_ASCII = 0;
        internal const int FTYPE_IMAGE = 1;
        internal const int FMODE_STREAM = 0;
        internal const int FMODE_COMPRESSED = 1;
        internal const int FSTRU_FILE = 0;
        internal const int FSTRU_PAGE = 1;
    }

    class FtpHandler//处理用户在客户端请求的命令，然后通过服务器处理返回操作的响应码
    {

    }

    class FtpConsole//承担在控制台上的所有业务逻辑
    {
        internal StreamReader cin;
        internal String conCmd;
        internal String conParam;

        internal int consoleQUIT()//退出
        {
            Environment.Exit(0);
            return 0;
        }

        internal bool consoleLISTUSER()//列出用户列表
        {
            Console.Out.WriteLine("username \t\t workdirectory");
            for(int i=0;i<FtpServer.userInfo.Count;i++)
            {
                Console.Out.Write(((UserInfo)FtpServer.userInfo[i]).user + "\t\t\t" + ((UserInfo)FtpServer.userInfo[i]).workDir);
            }
            return false;
        }

        internal bool consoleLIST()//判断是否是系统用户
        {
            int i = 0;
            for(i=0;i<FtpServer.users.Count;i++)
            {

            }
            return false;
        }

        internal bool validateUserName(String s)//根据传入的用户名称验证是否与文件中的名称匹配
        {
            for(int i=0;i<FtpServer.userInfo.Count;i++)
            {
                if (((UserInfo)FtpServer.userInfo[i]).user.Equals(s))
                    return false;
            }
            return true;
        }

        internal bool consoleADDUSER()//增加用户
        {

        }

        internal void saveUserInfo()//保存用户信息函数
        {

        }

        internal bool consoleDELUSER()//删除用户
        {

        }

        internal bool consoleHELP()//显示帮助
        {

        }

        internal bool consoleERR()//出错函数
        {

        }

        public FtpConsole()//构造函数
        {

        }

        public void ConsoleThread()//控制台线程函数
        {

        }

        internal int parseInput(String s)//解析输入的函数
        {

        }
    }

    public class FtpServer
    {
        public static String initDir;
        public static ArrayList users = new ArrayList();
        public static ArrayList usersInfo = new ArrayList();

        public FtpServer()
        {
            
        }

        public void loadUsersInfo()//加载用户信息
        {
            String s = new Uri(Path.GetFullPath("user.cfg")).ToString();//该文件保存用户名密码等信息
            s = s.Substring(6, s.Length - 6);
            int p1 = 0;
            int p2 = 0;
            bool tmpBool;
            if (File.Exists(s))
            {
                tmpBool = true;
            }
            else
            {
                tmpBool = Directory.Exists(s);
            }
            if (tmpBool)
            {
                try
                {
                    StreamReader fin = new StreamReader(new StreamReader(new FileStream(s, FileMode.Open, FileAccess.Read), Encoding.Default).BaseStream, new StreamReader(new FileStream(s, FileMode.Open, FileAccess.Read), Encoding.Default).CurrentEncoding);
                    String line;
                    String field;
                    int i = 0;
                    while ((line = fin.ReadLine()) != null)
                    {
                        UserInfo tempUserInfo = new UserInfo();
                        p1 = 0;
                        p2 = 0;
                        i = 0;
                        if (line.StartsWith("#"))
                            continue;
                        while ((p2 = line.IndexOf("|", p1)) != -1)
                        {
                            field = line.Substring(p1, (p2) - (p1));
                            p2 = p2 + 1;
                            p1 = p2;
                            switch (i)
                            {
                                case 0:
                                    tempUserInfo.user = field;
                                    break;
                                case 1:
                                    tempUserInfo.password = field;
                                    break;
                                case 2:
                                    tempUserInfo.workDir = field;
                                    break;
                            }
                            i++;
                        }
                        usersInfo.Add(tempUserInfo);
                    }
                    fin.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                finally
                {

                }
            }
        }

        public static void Main(string[] args)
        {
            initDir = "c:/";
            FtpServer ftpServer = new FtpServer();
        }
    }
}
