using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseAche2
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
        private void btStart_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source = " + tbStart.Text + "; Initial Catalog = AutoCargoTransportation; Integrated Security = Yes");
            try
            {
                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    GlobalVar.Namesrv = tbStart.Text;
                    con.Close();
                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удаётся установить соединение с сервером! Проверьте имя сервера и повторите попытку.", "Всё плохо...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
