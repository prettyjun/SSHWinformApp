using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSHWinformApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            richTextBox1.SelectionFont = new Font("Arial", 12, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.Red;
        }
        SshClient sshClient = null;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (sshClient != null)
            {
                sshClient.Disconnect();
            }
            sshClient = new SshClient(txtServer.Text, 22, txtUsername.Text, txtPassword.Text);
            string s = sshClient.ConnectionInfo.Host; ;
            //PrintLog(connMsg);

            btnLogin.Text = "重新登录";
        }

        private void SshClient_ServerIdentificationReceived(object sender, Renci.SshNet.Common.SshIdentificationEventArgs e)
        {
          
        }

        public void Execute(string command)
        {
            try
            {
                if(!sshClient.IsConnected)
                {
                    sshClient.Connect();
                }
                // 执行命令
                var result = sshClient.RunCommand(command);
                PrintLog(result.Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }

        public void PrintLog(string log)
        {
            // 将文本添加到RichTextBox并自动添加换行符
            richTextBox1.AppendText(log + Environment.NewLine);

            // 自动滚动到RichTextBox的底部
            richTextBox1.ScrollToCaret();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            Execute(txtCMD.Text);
        }
    }
}
