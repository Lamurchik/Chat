using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Log : Form
    {
        LogSup l;
        public Log(LogSup L)
        {
            InitializeComponent();
            l = L;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            l.name = textBox1.Text;
            l.password = textBox2.Text;
            this.Close();
        }
    }
}
