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
    public partial class FormShowInfo : Form
    {
        public string name;
        public FormShowInfo(string var)
        {
            InitializeComponent();
            name = var;
        }
        private void FormShowInfo_Load(object sender, EventArgs e)
        {
            switch (name)
            {
                case "Cars":
                    {
                        DB db = new DB();
                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand command = new SqlCommand("SELECT Cars.number_car, Drivers.name_d, TypeOfCars.name_tCar, Cars.carrying FROM Cars JOIN Drivers on Cars.id_driver = Drivers.Id " +
                                                    "JOIN TypeOfCars on TypeOfCars.Id = Cars.id_typeCar", db.GetConnection());
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        DGShow.DataSource = table.DefaultView;
                        DGShow.Columns[0].HeaderText = "Номер автомобиля";
                        DGShow.Columns[1].HeaderText = "Водитель";
                        DGShow.Columns[2].HeaderText = "Тип автомобиля";
                        DGShow.Columns[3].HeaderText = "Грузоподъёмность";
                        foreach (DataGridViewColumn column in DGShow.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }
                        int wigthC = 75;
                        foreach (DataGridViewColumn column in DGShow.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            wigthC += column.Width;
                        }
                        this.Width = wigthC;
                        break;
                    }
                case "Customers":
                    {
                        DB db = new DB();
                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();

                        SqlCommand command = new SqlCommand("SELECT customerName, customerPhone, customerAddress FROM Customers", db.GetConnection());
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        DGShow.DataSource = table.DefaultView;
                        DGShow.Columns[0].HeaderText = "ФИО клиента";
                        DGShow.Columns[1].HeaderText = "Номер телефона";
                        DGShow.Columns[2].HeaderText = "Адрес";
                        foreach (DataGridViewColumn column in DGShow.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }
                        int wigthC = 75;
                        foreach (DataGridViewColumn column in DGShow.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            wigthC += column.Width;
                        }
                        this.Width = wigthC;
                        break;
                    }
                case "Cargo":
                    {
                        DB db = new DB();
                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand command = new SqlCommand("SELECT Cargo.name_cargo, TypeOfCargo.name_tс from Cargo JOIN TypeOfCargo on Cargo.id_type = TypeOfCargo.Id", db.GetConnection());
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        DGShow.DataSource = table.DefaultView;
                        DGShow.Columns[0].HeaderText = "Груз";
                        DGShow.Columns[1].HeaderText = "Тип груза";
                        foreach (DataGridViewColumn column in DGShow.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }
                        int wigthC = 75; 
                        foreach (DataGridViewColumn column in DGShow.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            wigthC += column.Width;
                        }
                        this.Width = wigthC;
                        this.Height = 400;
                        break;
                    }
                case "Drivers":
                    {
                        DB db = new DB();
                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand command = new SqlCommand("SELECT name_d, phone_d, address_d FROM Drivers", db.GetConnection());
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        DGShow.DataSource = table.DefaultView;
                        DGShow.Columns[0].HeaderText = "ФИО водителя";
                        DGShow.Columns[1].HeaderText = "Номер телефона";
                        DGShow.Columns[2].HeaderText = "Адрес";
                        foreach (DataGridViewColumn column in DGShow.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }
                        int wigthC = 75;
                        foreach (DataGridViewColumn column in DGShow.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            wigthC += column.Width;
                        }
                        this.Width = wigthC;
                        break;
                    }
            }
        }
    }
}
