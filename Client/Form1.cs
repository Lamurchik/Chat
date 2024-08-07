using Client.ServiceChat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form, IServiceChatCallback
    {
        bool connect = false;
        int Id;
        ServiceChatClient client;
        IServiceChat chat;
        string name;

        
        public Form1()
        {
            InitializeComponent();
        }
        private void Connect()
        {
            if (!connect)
           {
                LogSup l = new LogSup();
                Log log = new Log(l);
                log.ShowDialog();
                try
                {
                    int id = client.Connect(l.name, l.password);
                    name = l.name;
                    Id = id;
                    button1.Text = "отключиться";
                    connect = true;
                    string[] a = client.GetChatHistory(id);
                    listBox1.Items.Clear();
                    foreach (string s in a)
                    {
                        MsgCallBack(s);
                    }
                    client.SendMessage($" присоединился к чату", id);
                }
                catch(FaultException ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                

                /*
                try 
                {
                    client.Connect(l.name, l.password);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message+" пользователь не найден\n поробуйте снова"); }
                */

                /*
                if (id == -1)
                {
                    MessageBox.Show("пользователь не найден\n поробуйте снова");
                }
                else
                {
                    
                    Id = id;
                    button1.Text = "отключиться";
                    connect = true;
                    string[] a= client.GetChatHistory(id);
                    listBox1.Items.Clear();
                    foreach (string s in a) 
                    {
                        MsgCallBack(s);
                    }
                    client.SendMessage($" присоеденился к чату", id);
                }
                */
                
            }
        }
        private void Disconnect()
        {
            if (connect)
            {
                
                client.Disconnect(Id);
                client.SendMessage($": [{name}]: покинул чат", 0);


                button1.Text = "подключиться";
                connect = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (connect)
                Disconnect();
            else
                Connect();
        }
        public void MsgCallBack(string message)
        {
            listBox1.Items.Add(message);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.Focus();
            listBox1.ClearSelected();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new ServiceChatClient(new InstanceContext(this), "NetTcpBinding_IServiceChat");
            //DuplexChannelFactory<IServiceChat> factory = new DuplexChannelFactory<IServiceChat>(new InstanceContext(this), "NetTcpBinding_IServiceChat");
            //chat = factory.CreateChannel();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if(connect) 
            {
                
                if (e.KeyCode == Keys.Enter)
                {
                    try 
                    {
                        string msg = richTextBox1.Text;
                        richTextBox1.Text = string.Empty;
                        client.SendMessage(msg, Id);
                    }
                    catch(FaultException) 
                    {
                        client = new ServiceChatClient(new InstanceContext(this), "NetTcpBinding_IServiceChat");
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("пользователь не найден\n сначало нужно подключиься к серверу");
                richTextBox1.Text = string.Empty;
            }
            
        }
    }
}
