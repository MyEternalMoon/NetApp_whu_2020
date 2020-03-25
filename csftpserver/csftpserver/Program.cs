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
            for(int i=0;i<FtpServer.usersInfo.Count;i++)
            {
                Console.Out.Write(((UserInfo)FtpServer.usersInfo[i]).user + "\t\t\t" + ((UserInfo)FtpServer.usersInfo[i]).workDir);
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
            for(int i=0;i<FtpServer.usersInfo.Count;i++)
            {
                if (((UserInfo)FtpServer.usersInfo[i]).user.Equals(s))
                    return false;
            }
            return true;
        }

        internal bool consoleADDUSER()//增加用户
        {
            Console.Out.Write("please enter username:");
            try
            {
                cin = new StreamReader(new StreamReader(Console.OpenStandardInput(), Encoding.Default).BaseStream, new StreamReader(Console.OpenStandardInput(), Encoding.Default).CurrentEncoding);
                UserInfo tempUserInfo = new UserInfo();
                String line = cin.ReadLine();
                if((object)line!=(object)"")
                {
                    //用户已经存在
                    if(!validateUserName(line))
                    {
                        Console.Out.WriteLine("user" + line + "already exsits!");
                        return false;
                    }
                }
                else
                {
                    //出错用户名不能为空
                    Console.Out.WriteLine("username cannnot be null!");
                    return false;
                }
                tempUserInfo.user = line;
                Console.Out.Write("enter password:");
                line = cin.ReadLine();
                if ((Object)line != (Object)"")
                    tempUserInfo.password = line;
                else
                {
                    Console.Out.WriteLine("password cannot be null:");
                    return false;
                }
                Console.Out.Write("enter the initial directory:");
                line = cin.ReadLine();
                if((object)line!=(Object)"")
                {
                    FileInfo f = new FileInfo(line);
                    bool tmpBool;
                    if (File.Exists(f.FullName))
                        tmpBool = true;
                    else
                        tmpBool = Directory.Exists(f.FullName);
                    if(!tmpBool)
                    {
                        Directory.CreateDirectory(f.FullName);
                    }
                    tempUserInfo.workDir = line;
                }
                else
                {
                    Console.Out.WriteLine("the directory cannot be null!");
                    return false;
                }
                FtpServer.usersInfo.Add(tempUserInfo);
                saveUserInfo();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            finally
            {

            }
            return false;//上述操作都没有出现错误
        }

        internal void saveUserInfo()//保存用户信息函数
        {
            String s = "";
            try
            {
                StreamWriter fout = new StreamWriter(new StreamWriter(new FileStream("user.cfg", FileMode.Create), Encoding.Default).BaseStream, new StreamWriter(new FileStream("user.cfg", FileMode.Create), Encoding.Default).Encoding);
                for(int i=0;i<FtpServer.usersInfo.Count;i++)
                {
                    s = ((UserInfo)FtpServer.usersInfo[i]).user + "|" + ((UserInfo)FtpServer.usersInfo[i]).password + "|" + ((UserInfo)FtpServer.usersInfo[i]).workDir + "|";
                    fout.Write(s);

                    fout.WriteLine();
                }
                fout.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {

            }
        }

        internal bool consoleDELUSER()//删除用户
        {
            String s = "";
            if(conParam.Equals(""))
            {
                Console.Out.WriteLine("usage:deluser username");
                return false;
            }
            for(int i=0;i<FtpServer.usersInfo.Count;i++)
            {
                s = ((UserInfo)FtpServer.usersInfo[i]).user;
                if(s.Equals(conParam))
                {
                    Console.Out.WriteLine("User" + conParam + "deleted");
                    FtpServer.usersInfo.RemoveAt(i);
                    saveUserInfo();
                    return false;
                }
            }
            Console.Out.WriteLine("User" + conParam + "not exists");
            return false;
        }

        internal bool consoleHELP()//显示帮助
        {
            if (conParam.Equals(""))
            {
                Console.Out.WriteLine("adduser:add new user");
                Console.Out.WriteLine("deluser<username>:delete a user");
                Console.Out.WriteLine("quit :quit");
                Console.Out.WriteLine("list :list all user connect to server");
                Console.Out.WriteLine("listener:list all account of this server");
                Console.Out.WriteLine("help:show this help");
            }
            else if (conParam.Equals("adduser"))
                Console.Out.WriteLine("adduser:add new user");
            else if (conParam.Equals("deluser"))
                Console.Out.WriteLine("deluser<username>:delete a user");
            else if (conParam.Equals("qiut"))
                Console.Out.WriteLine("quit :quit");
            else if (conParam.Equals("list"))
                Console.Out.WriteLine("list :list all user connect to server");
            else if (conParam.Equals("listener"))
                Console.Out.WriteLine("listener:list all account of this server");
            else if (conParam.Equals("help"))
                Console.Out.WriteLine("help:show this help");
            else
                return false;
            return false;
        }

        internal bool consoleERR()//出错函数
        {
            Console.Out.WriteLine("bad command!");
            return false;
        }

        public FtpConsole()//构造函数
        {
            Console.Out.WriteLine("ftp server started!");
            cin = new StreamReader(new StreamReader(Console.OpenStandardInput(), Encoding.Default).BaseStream, new StreamReader(Console.OpenStandardInput(), Encoding.Default).CurrentEncoding);
        }

        public void ConsoleThread()//控制台线程函数
        {
            bool ok = false;
            String input = "";
            while(!ok)
            {
                Console.Out.Write("->");
                try
                {
                    //读入一行内容
                    input = cin.ReadLine();
                }
                catch(Exception e)
                {
                    //读入失败，打印错误信息
                    Console.WriteLine(e.ToString());
                }
                finally
                {

                }
                //判断输入命令内容
                switch(parseInput(input))
                {
                    case 1:
                        consoleQUIT();
                        break;
                    case 8:
                        ok = consoleLISTUSER();
                        break;
                    case 0:
                        ok = consoleLIST();
                        break;
                    case 2:
                        ok = consoleADDUSER();
                        break;
                    case 3:
                        ok = consoleDELUSER();
                        break;
                    case 7:
                        ok = consoleHELP();
                        break;
                    case -1:
                        ok = consoleERR();
                        break;
                }
            }
        }

        internal int parseInput(String s)//解析输入的函数
        {
            //变量初始化
            //大写命令名
            String upperCmd;
            int p = 0;
            conCmd = "";
            conParam = "";
            p = s.IndexOf(" ");
            if (p == -1)
                conCmd = s;
            else
                conCmd = s.Substring(0, (p) - (0));
            if (p >= s.Length || p == -1)
                conParam = "";
            else
                conParam = s.Substring(p + 1, (s.Length) - (p + 1));
            upperCmd = conCmd.ToUpper();

            if (upperCmd.Equals("QUIT") || upperCmd.Equals("EXIT"))
                return 1;
            else if (upperCmd.Equals("ADDUSER"))
                return 2;
            else if (upperCmd.Equals("DELUSER"))
                return 3;
            else if (upperCmd.Equals("EDITUSER"))
                return 4;
            else if (upperCmd.Equals("ADDDIR"))
                return 5;
            else if (upperCmd.Equals("REMOVEDIR"))
                return 6;
            else if (upperCmd.Equals("HELP") || upperCmd.Equals("?"))
                return 7;
            else if (upperCmd.Equals("LISTENER"))
                return 8;
            return -1;
        }
    }

    public class FtpServer
    {
        public static String initDir;
        public static ArrayList users = new ArrayList();
        public static ArrayList usersInfo = new ArrayList();

        public FtpServer()
        {
            FtpConsole fc = new FtpConsole();
            Thread t = new Thread(new ThreadStart(fc.ConsoleThread));
            t.Start();
            loadUsersInfo();
            int counter = 1;
            int i = 0;
            try
            {
                TcpListener tcpListener;
                tcpListener = new TcpListener(Dns.GetHostByName(Dns.GetHostName()).AddressList[0], 21);
                tcpListener.Start();
                TcpListener s = tcpListener;
                while(true)
                {
                    TcpClient incoming = s.AcceptTcpClient();
                    StreamReader in_Renamed = new StreamReader(new StreamReader(incoming.GetStream(), Encoding.Default).BaseStream, new StreamReader(incoming.GetStream(), Encoding.Default).CurrentEncoding);
                    StreamWriter temp_writer = new StreamWriter(incoming.GetStream(), Encoding.Default);
                    temp_writer.AutoFlush = true;
                    StreamWriter out_Renamed = temp_writer;
                    out_Renamed.WriteLine("220 Service ready for new user" + counter);

                    //创建服务线程
                    FtpHandler h = new FtpHandler(incoming, i);
                    Thread t1 = new Thread(new ThreadStart(h.HandleThread));
                    t1.Start();
                    users.Add(h);
                    counter++;
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {

            }
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
