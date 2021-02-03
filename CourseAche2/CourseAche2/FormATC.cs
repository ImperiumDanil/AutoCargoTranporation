using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace CourseAche2
{
    public partial class FormATC : System.Windows.Forms.Form
    {
        public FormATC()
        {
            InitializeComponent();
        }

        private void FormATC_Load(object sender, EventArgs e)
        {
            UploadTableView();
            UploadTableWaybill();
            UploadTableCars();
            UploadTableCustomers();
            UploadTableCargo();
            UploadTableDrivers();
            UploadTableTypeOfCargo();
            UploadTableTypeOfCars();

            CBInfoUpload();//для последнего отчёта
        }

        private void UploadTableView()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DB database = new DB();
            try
            {
                SqlCommand command = new SqlCommand("select Waybill.numberOfWaybill, TypeOfCargo.name_tс, Cargo.name_cargo, Drivers.name_d, Cars.carrying, Waybill.finalAddress, " +
                                                     "Waybill.costkm, Waybill.kilometrage, Customers.customerName, Waybill.dataACT from Waybill join Cargo on Waybill.id_cargo = Cargo.Id " +
                                                    "join TypeOfCargo on Cargo.id_type = TypeOfCargo.Id join Cars on Waybill.id_car = Cars.Id join Drivers on Cars.id_driver = Drivers.Id " +
                                                    "join Customers on Waybill.id_customer = Customers.Id  ", database.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                DGView.DataSource = table.DefaultView;
                DGView.Columns[0].HeaderText = "№ путевого листа";
                DGView.Columns[1].HeaderText = "Тип груза";
                DGView.Columns[2].HeaderText = "Груз";
                DGView.Columns[3].HeaderText = "Водитель грузовика";
                DGView.Columns[4].HeaderText = "Грузоподъёмность автомобиля";
                DGView.Columns[5].HeaderText = "Конечный адресс";
                DGView.Columns[6].HeaderText = "Стоимость за км.";
                DGView.Columns[7].HeaderText = "Километраж";
                DGView.Columns[8].HeaderText = "Заказчик";
                DGView.Columns[9].HeaderText = "Дата";

                int wigthColumns = 75; //75, т.к. столбец выбоора строки не учитывается в цикле.
                foreach (DataGridViewColumn column in DGView.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    wigthColumns += column.Width;
                }
                this.Width = wigthColumns;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)//кнопка обновления просмотра
        {
            UploadTableView();
        }

        private void UploadTableWaybill()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DB database = new DB();
            try
            {
                SqlCommand command = new SqlCommand("select Waybill.numberOfWaybill, Cars.number_car, Customers.customerName, Cargo.name_cargo, Waybill.finalAddress, Waybill.kilometrage," +
                                                    " Waybill.costkm, Waybill.weight_cargo, Waybill.dataACT, Waybill.Id FROM Waybill JOIN Cars on Waybill.id_car = cars.Id " +
                                                    "JOIN Customers on Waybill.id_customer = Customers.Id JOIN Cargo on Waybill.id_cargo = Cargo.Id", database.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                DGWaybill.DataSource = table.DefaultView;
                DGWaybill.Columns[0].HeaderText = "Номер Путевого листа";
                DGWaybill.Columns[1].HeaderText = "Номер автомобиля";
                DGWaybill.Columns[2].HeaderText = "Клиент";
                DGWaybill.Columns[3].HeaderText = "Груз";
                DGWaybill.Columns[4].HeaderText = "Конечный адресс";
                DGWaybill.Columns[5].HeaderText = "Километраж";
                DGWaybill.Columns[6].HeaderText = "Стоимость за км.";
                DGWaybill.Columns[7].HeaderText = "Вес груза";
                DGWaybill.Columns[8].HeaderText = "Дата";
                DGWaybill.Columns[9].Visible = false;
                foreach (DataGridViewColumn column in DGWaybill.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UploadTableCars()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DB database = new DB();
            try
            {
                SqlCommand command = new SqlCommand("SELECT Drivers.name_d, TypeOfCars.name_tCar, Cars.carrying, Cars.Id, Cars.number_car FROM Cars JOIN Drivers on Cars.id_driver = Drivers.Id " +
                                                    "JOIN TypeOfCars on TypeOfCars.Id = Cars.id_typeCar", database.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                DGCars.DataSource = table.DefaultView;
                DGCars.Columns[0].HeaderText = "Водитель";
                DGCars.Columns[1].HeaderText = "Тип автомобиля";
                DGCars.Columns[2].HeaderText = "Грузоподъёмность";
                DGCars.Columns[4].HeaderText = "Номер автомобиля";
                DGCars.Columns[3].Visible = false;
                foreach (DataGridViewColumn column in DGCars.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UploadTableCustomers()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DB database = new DB();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * from Customers", database.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                DGCustomers.DataSource = table.DefaultView;
                DGCustomers.Columns[0].Visible = false;
                DGCustomers.Columns[1].HeaderText = "ФИО клиента";
                DGCustomers.Columns[2].HeaderText = "Номер телефона";
                DGCustomers.Columns[3].HeaderText = "Адрес";
                foreach (DataGridViewColumn column in DGCustomers.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UploadTableCargo()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DB database = new DB();
            try
            {
                SqlCommand command = new SqlCommand("SELECT TypeOfCargo.name_tс, Cargo.name_cargo, Cargo.Id from Cargo JOIN TypeOfCargo on Cargo.id_type = TypeOfCargo.Id", database.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                DGCargo.DataSource = table.DefaultView;
                DGCargo.Columns[0].HeaderText = "Тип груза";
                DGCargo.Columns[1].HeaderText = "Груз";
                DGCargo.Columns[2].Visible = false;
                foreach (DataGridViewColumn column in DGCargo.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UploadTableDrivers()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DB database = new DB();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * from Drivers", database.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                DGDrivers.DataSource = table.DefaultView;
                DGDrivers.Columns[0].Visible = false;
                DGDrivers.Columns[1].HeaderText = "ФИО водителя";
                DGDrivers.Columns[2].HeaderText = "Номер телефона";
                DGDrivers.Columns[3].HeaderText = "Адрес";
                foreach (DataGridViewColumn column in DGDrivers.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UploadTableTypeOfCargo()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DB database = new DB();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * from TypeOfCargo", database.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                DGTypeOfCargo.DataSource = table.DefaultView;
                DGTypeOfCargo.Columns[0].Visible = false;
                DGTypeOfCargo.Columns[1].HeaderText = "Тип груза";
                foreach (DataGridViewColumn column in DGTypeOfCargo.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UploadTableTypeOfCars()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DB database = new DB();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * from TypeOfCars", database.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                DGTypeOfCars.DataSource = table.DefaultView;
                DGTypeOfCars.Columns[0].Visible = false;
                DGTypeOfCars.Columns[1].HeaderText = "Тип автомобиля";
                foreach (DataGridViewColumn column in DGTypeOfCars.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)//выход через инструменты
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти из программы?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        private void btAddTOC_Click_1(object sender, EventArgs e)//Добавление TypeOfCargo
        {
            FormAdd form = new FormAdd("TypeOfCargo");
            GlobalVar.addupd = true;
            form.ShowDialog();
            UploadTableTypeOfCargo();
        }

        private void btUpdTOC_Click_1(object sender, EventArgs e)//Изменение TypeOfCargo
        {
            FormAdd form = new FormAdd("TypeOfCargo");
            try
            {
                GlobalVar.tb1 = DGTypeOfCargo[1, DGTypeOfCargo.CurrentRow.Index].Value.ToString();
                GlobalVar.id = DGTypeOfCargo.CurrentRow.Cells[0].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GlobalVar.addupd = false;
            form.ShowDialog();
            UploadTableTypeOfCargo();
        }

        private void btDelTOC_Click_1(object sender, EventArgs e)//Удаление TypeOfCargo
        {
            try
            {
                GlobalVar.id = DGTypeOfCargo.CurrentRow.Cells[0].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (GlobalVar.id != "")
            {
                DB db = new DB();
                SqlCommand cmd = new SqlCommand($"SELECT Count(TypeOfCargo.Id) From TypeOfCargo JOIN Cargo ON TypeOfCargo.Id = Cargo.id_type WHERE TypeOfCargo.Id = '{GlobalVar.id}'", db.GetConnection());
                db.OpenConnection();
                if (cmd.ExecuteScalar().ToString() != "0")
                {
                    db.CloseConnection();
                    MessageBox.Show("Перестаньте использовать данный тип груза в другой таблице, чтобы удалить его!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (MessageBox.Show("Вы уверены, что хотите удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand(@"DELETE FROM TypeOfCargo WHERE Id = @ID", db.GetConnection());
                    cmd.Parameters.AddWithValue("ID", GlobalVar.id);
                    cmd.ExecuteNonQuery();
                    db.CloseConnection();
                    MessageBox.Show("Запись была успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UploadTableTypeOfCargo();
                }

            }
        }

        private void btAddCargo_Click(object sender, EventArgs e)
        {
            FormAdd form = new FormAdd("Cargo");
            GlobalVar.addupd = true;
            form.ShowDialog();
            UploadTableCargo();
        }

        private void btUpdCargo_Click(object sender, EventArgs e)
        {
            {
                FormAdd form = new FormAdd("Cargo");
                GlobalVar.addupd = false;
                try
                {
                    GlobalVar.tb1 = DGCargo[1, DGCargo.CurrentRow.Index].Value.ToString();
                    GlobalVar.tb2 = DGCargo[0, DGCargo.CurrentRow.Index].Value.ToString();
                    GlobalVar.id = DGCargo[2, DGCargo.CurrentRow.Index].Value.ToString();

                }
                catch
                {
                    MessageBox.Show("Ошибка!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                form.ShowDialog();
                UploadTableCargo();
            }
        }

        private void btDelCargo_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVar.id = DGCargo.CurrentRow.Cells[2].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (GlobalVar.id != "")
            {
                DB db = new DB();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM Waybill JOIN Cargo ON Cargo.Id = Waybill.id_cargo WHERE Cargo.Id = '{GlobalVar.id}'", db.GetConnection());
                db.OpenConnection();
                if (cmd.ExecuteScalar().ToString() != "0")
                {
                    db.CloseConnection();
                    MessageBox.Show("Перестаньте использовать данный груз в другой таблице, чтобы удалить его!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (MessageBox.Show("Вы уверены, что хотите удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand(@"DELETE FROM Cargo WHERE Id = @ID", db.GetConnection());
                    cmd.Parameters.AddWithValue("ID", GlobalVar.id);
                    cmd.ExecuteNonQuery();
                    db.CloseConnection();
                    MessageBox.Show("Запись была успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UploadTableCargo();
                }

            }
        }

        private void DGCargo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DGCargo.CurrentRow.Selected = true;
        }

        private void btAddDrivers_Click(object sender, EventArgs e)
        {
            FormAdd form = new FormAdd("Drivers");
            GlobalVar.addupd = true;
            form.ShowDialog();
            UploadTableDrivers();
        }

        private void btUpdDrivers_Click(object sender, EventArgs e)
        {
            FormAdd form = new FormAdd("Drivers");
            try
            {
                GlobalVar.id = DGDrivers[0, DGDrivers.CurrentRow.Index].Value.ToString();
                GlobalVar.tb1 = DGDrivers[1, DGDrivers.CurrentRow.Index].Value.ToString();
                GlobalVar.tb2 = DGDrivers[2, DGDrivers.CurrentRow.Index].Value.ToString();
                GlobalVar.tb3 = DGDrivers[3, DGDrivers.CurrentRow.Index].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GlobalVar.addupd = false;
            form.ShowDialog();
            UploadTableDrivers();
        }

        private void btDelDrivers_Click(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) From Drivers JOIN Cars ON Drivers.Id = Cars.id_driver WHERE Drivers.Id = {DGDrivers[0, DGDrivers.CurrentRow.Index].Value.ToString()}", db.GetConnection());
                db.OpenConnection();
                if (cmd.ExecuteScalar().ToString() == "0")
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand($"DELETE FROM Drivers WHERE Drivers.Id = @Id", db.GetConnection());
                        cmd.Parameters.AddWithValue("Id", DGDrivers[0, DGDrivers.CurrentRow.Index].Value.ToString());
                        cmd.ExecuteNonQuery();
                        db.CloseConnection();
                        UploadTableDrivers();
                        MessageBox.Show("Запись была успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    db.CloseConnection();
                    MessageBox.Show("Данный водитель используется в перевозках! Уберите его из таблицы автомобилей, чтобы удалить.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGDrivers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DGDrivers.CurrentRow.Selected = true;
        }

        private void btAddCustomers_Click(object sender, EventArgs e)
        {
            FormAdd form = new FormAdd("Customers");
            GlobalVar.addupd = true;
            form.ShowDialog();
            UploadTableCustomers();
        }

        private void btUpdCustomers_Click(object sender, EventArgs e)
        {
            FormAdd form = new FormAdd("Customers");
            try
            {
                GlobalVar.id = DGCustomers[0, DGCustomers.CurrentRow.Index].Value.ToString();
                GlobalVar.tb1 = DGCustomers[1, DGCustomers.CurrentRow.Index].Value.ToString();
                GlobalVar.tb2 = DGCustomers[2, DGCustomers.CurrentRow.Index].Value.ToString();
                GlobalVar.tb3 = DGCustomers[3, DGCustomers.CurrentRow.Index].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GlobalVar.addupd = false;
            form.ShowDialog();
            UploadTableCustomers();
        }

        private void btDelCustomers_Click(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) From Customers JOIN Waybill ON Customers.Id = Waybill.id_customer WHERE Customers.Id = {DGCustomers[0, DGCustomers.CurrentRow.Index].Value.ToString()}", db.GetConnection());
                db.OpenConnection();
                if (cmd.ExecuteScalar().ToString() == "0")
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand($"DELETE FROM Customers WHERE Customers.Id = @Id", db.GetConnection());
                        cmd.Parameters.AddWithValue("Id", DGCustomers[0, DGCustomers.CurrentRow.Index].Value.ToString());
                        cmd.ExecuteNonQuery();
                        db.CloseConnection();
                        UploadTableCustomers();
                        MessageBox.Show("Запись была успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    db.CloseConnection();
                    MessageBox.Show("Данному клиенту уже осуществляется грузоперевозка! Уберите его из таблицы путевых листов, чтобы удалить.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DGCustomers.CurrentRow.Selected = true;
        }

        private void btAddCars_Click(object sender, EventArgs e)
        {
            FormAdd form = new FormAdd("Cars");
            GlobalVar.addupd = true;
            form.ShowDialog();
            UploadTableCars();
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "FormShowInfo")
                {
                    f.Close();
                    break;
                }
            }
        }

        private void btUpdCars_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVar.tb1 = DGCars[0, DGCars.CurrentRow.Index].Value.ToString();
                GlobalVar.tb2 = DGCars[1, DGCars.CurrentRow.Index].Value.ToString();
                GlobalVar.tb3 = DGCars[2, DGCars.CurrentRow.Index].Value.ToString();
                GlobalVar.tb4 = DGCars[4, DGCars.CurrentRow.Index].Value.ToString();
                GlobalVar.id = DGCars[3, DGCars.CurrentRow.Index].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            FormAdd form = new FormAdd("Cars");
            GlobalVar.addupd = false;
            form.ShowDialog();
            UploadTableCars();
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "FormShowInfo")
                {
                    f.Close();
                    break;
                }
            }
        }

        private void btDelCars_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM Cars JOIN Waybill ON Cars.Id = Waybill.id_car WHERE Cars.Id = {DGCars[3, DGCars.CurrentRow.Index].Value.ToString()}", db.GetConnection());
            db.OpenConnection();
            if (cmd.ExecuteScalar().ToString() == "0")
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("DELETE FROM Cars WHERE Id = @Id", db.GetConnection());
                    cmd.Parameters.AddWithValue("Id", DGCars[3, DGCars.CurrentRow.Index].Value.ToString());
                    cmd.ExecuteNonQuery();
                    db.CloseConnection();
                    UploadTableCars();
                    MessageBox.Show("Запись была успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show("Данный автомобиль используется в путевом листе! Чтобы удалить, перестаньте его использовать", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            db.CloseConnection();
        }

        private void DGCars_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DGCars.CurrentRow.Selected = true;
        }

        private void btAddToCars_Click(object sender, EventArgs e)
        {
            FormAdd form = new FormAdd("TypeOfCars");
            GlobalVar.addupd = true;
            form.ShowDialog();
            UploadTableTypeOfCars();
        }

        private void btUpdToCars_Click(object sender, EventArgs e)
        {
            FormAdd form = new FormAdd("TypeOfCars");
            try
            {
                GlobalVar.tb1 = DGTypeOfCars[1, DGTypeOfCars.CurrentRow.Index].Value.ToString();
                GlobalVar.id = DGTypeOfCars[0, DGTypeOfCars.CurrentRow.Index].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Ошибка!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GlobalVar.addupd = false;
            form.ShowDialog();
            UploadTableTypeOfCars();
        }

        private void btDelToCars_Click(object sender, EventArgs e)
        {
            try
            {
                DB db = new DB();
                SqlCommand cmd = new SqlCommand($"SELECT Count(*) From TypeOfCars JOIN Cars ON TypeOfCars.Id = Cars.id_typeCar WHERE TypeOfCars.Id = '{DGTypeOfCars.CurrentRow.Cells[0].Value.ToString()}'", db.GetConnection());
                db.OpenConnection();
                if (cmd.ExecuteScalar().ToString() != "0")
                {
                    db.CloseConnection();
                    MessageBox.Show("Перестаньте использовать данный тип кузова в другой таблице, чтобы удалить его!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (MessageBox.Show("Вы уверены, что хотите удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand(@"DELETE FROM TypeOfCars WHERE Id = @ID", db.GetConnection());
                    cmd.Parameters.AddWithValue("ID", DGTypeOfCars.CurrentRow.Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();
                    db.CloseConnection();
                    MessageBox.Show("Запись была успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UploadTableTypeOfCars();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGWaybill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DGWaybill.CurrentRow.Selected = true;
        }

        private void btAddWaybill_Click(object sender, EventArgs e)
        {
            FormAdd form = new FormAdd("Waybill");

            GlobalVar.addupd = true;
            form.ShowDialog();
            UploadTableWaybill();
            for (int i = 3; i > 0; i--)
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name == "FormShowInfo")
                    {
                        f.Close();
                        break;
                    }
                }
        }

        private void btUpdWaybill_Click(object sender, EventArgs e)
        {
            FormAdd form = new FormAdd("Waybill");
            GlobalVar.addupd = false;

            try
            {
                GlobalVar.tb1 = DGWaybill[4, DGWaybill.CurrentRow.Index].Value.ToString();
                GlobalVar.tb2 = DGWaybill[5, DGWaybill.CurrentRow.Index].Value.ToString();
                GlobalVar.tb3 = DGWaybill[6, DGWaybill.CurrentRow.Index].Value.ToString();
                GlobalVar.tb4 = DGWaybill[7, DGWaybill.CurrentRow.Index].Value.ToString();
                GlobalVar.tb5 = DGWaybill[0, DGWaybill.CurrentRow.Index].Value.ToString();
                GlobalVar.tb6 = DGWaybill[8, DGWaybill.CurrentRow.Index].Value.ToString();
                GlobalVar.tb7 = DGWaybill[2, DGWaybill.CurrentRow.Index].Value.ToString();
                GlobalVar.tb8 = DGWaybill[3, DGWaybill.CurrentRow.Index].Value.ToString();
                GlobalVar.id2 = DGWaybill[1, DGWaybill.CurrentRow.Index].Value.ToString();
                GlobalVar.id = DGWaybill[9, DGWaybill.CurrentRow.Index].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            form.ShowDialog();
            UploadTableWaybill();
            for (int i = 3; i > 0; i--)
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name == "FormShowInfo")
                    {
                        f.Close();
                        break;
                    }
                }
        }

        private void btDelWaybill_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить эту строку?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DB db = new DB();
                SqlCommand cmd = new SqlCommand(@"DELETE FROM Waybill WHERE Id = @Id", db.GetConnection());
                cmd.Parameters.AddWithValue("Id", DGWaybill[9, DGWaybill.CurrentRow.Index].Value.ToString());
                db.OpenConnection();
                cmd.ExecuteNonQuery();
                db.CloseConnection();
                UploadTableWaybill();
                MessageBox.Show("Запись была успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btInfo1_Click(object sender, EventArgs e)
        {
            try
            {
                DB database = new DB();
                SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Waybill WHERE dataACT BETWEEN '{dtfirst1.Value}' AND '{dtsecond1.Value}'", database.GetConnection());
                database.OpenConnection();
                if (command.ExecuteScalar().ToString() != "0")
                {
                    labelinfo1.Text = command.ExecuteScalar().ToString();
                    database.CloseConnection();
                    command = new SqlCommand("SELECT dataACT, numberOfWaybill, costkm, kilometrage, costkm * kilometrage FROM Waybill " +
                                                $"WHERE dataACT BETWEEN '{dtfirst1.Value}' AND '{dtsecond1.Value}' ORDER BY dataACT", database.GetConnection());
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    int sum = 0;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        sum += Convert.ToInt32(table.Rows[i][4].ToString());
                    }
                    labelinfo1_2.Text = Convert.ToString(sum) + " руб.";

                    dgInfo1.DataSource = table.DefaultView;
                    dgInfo1.Columns[0].HeaderText = "Дата";
                    dgInfo1.Columns[1].HeaderText = "№ путевого листа";
                    dgInfo1.Columns[2].HeaderText = "Стоимость км";
                    dgInfo1.Columns[3].HeaderText = "Километраж";
                    dgInfo1.Columns[4].HeaderText = "Стоимость перевозки";
                    foreach (DataGridViewColumn column in dgInfo1.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    GlobalVar.save1 = true;
                }
                else
                {
                    database.CloseConnection();
                    MessageBox.Show("В данном промежутке перевозок не совершалось.", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btinfo2_Click(object sender, EventArgs e)
        {
            try
            {
                DB database = new DB();
                SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Waybill WHERE dataACT BETWEEN '{dtfirst1.Value}' AND '{dtsecond1.Value}'", database.GetConnection());
                database.OpenConnection();
                if (command.ExecuteScalar().ToString() != "0")
                {
                    database.CloseConnection();
                    command = new SqlCommand("SELECT Cars.number_car,SUM(Waybill.kilometrage) FROM Waybill JOIN Cars ON Cars.Id = Waybill.id_car " +
                                            $"WHERE dataACT BETWEEN '{dtfirst2.Value}' AND '{dtsecond2.Value}' GROUP BY number_car", database.GetConnection());
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    int sum = 0;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        sum += Convert.ToInt32(table.Rows[i][1].ToString());
                    }
                    labelinfo2.Text = Convert.ToString(sum) + " км.";

                    dginfo2_1.DataSource = table.DefaultView;
                    dginfo2_1.Columns[0].HeaderText = "Номер автомобиля";
                    dginfo2_1.Columns[1].HeaderText = "Километраж";
                    foreach (DataGridViewColumn column in dginfo2_1.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }

                    command = new SqlCommand("SELECT Drivers.name_d, sum(Waybill.kilometrage) FROM Cars JOIN Drivers ON Drivers.Id = Cars.id_driver JOIN Waybill ON Waybill.id_car = Cars.Id " +
                                            $"WHERE dataACT BETWEEN '{dtfirst2.Value}' AND '{dtsecond2.Value}' GROUP BY name_d", database.GetConnection());
                    adapter = new SqlDataAdapter(command);
                    table = new DataTable();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    dginfo2_2.DataSource = table.DefaultView;
                    dginfo2_2.Columns[0].HeaderText = "Водитель";
                    dginfo2_2.Columns[1].HeaderText = "Километраж";
                    foreach (DataGridViewColumn column in dginfo2_2.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    GlobalVar.save2 = true;
                }
                else
                {
                    database.CloseConnection();
                    MessageBox.Show("В данном промежутке перевозок не совершалось.", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btifno3_Click(object sender, EventArgs e)
        {
            try
            {
                DB database = new DB();
                SqlCommand command = new SqlCommand("SELECT Cars.number_car, SUM(Cars.carrying), SUM(Waybill.weight_cargo) FROM CARS JOIN Waybill ON Waybill.id_car = Cars.Id GROUP BY number_car ORDER BY number_car", database.GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                int sum1 = 0;
                int sum2 = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    sum1 += Convert.ToInt32(table.Rows[i][1].ToString());
                    sum2 += Convert.ToInt32(table.Rows[i][2].ToString());
                }
                labelinfo3_1.Text = Convert.ToString(sum1/1000) + " тонн.";
                labelinfo3_2.Text = Convert.ToString(sum2/1000) + " тонн.";

                dgInfo3.DataSource = table.DefaultView;
                dgInfo3.Columns[0].HeaderText = "Номер автомобиля";
                dgInfo3.Columns[1].HeaderText = "Тоннаж кг.";
                dgInfo3.Columns[2].HeaderText = "Груз кг.";
                foreach (DataGridViewColumn column in dgInfo3.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                GlobalVar.save3 = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btinfo4_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbinfokol.Text != string.Empty)
                {
                    DB database = new DB();
                    SqlCommand command = new SqlCommand($"Select TOP({tbinfokol.Text}) numberOfWaybill, kilometrage FROM Waybill ORDER BY kilometrage DESC", database.GetConnection());
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    labelinfo4.Text = table.Rows[0][1].ToString() + " км.";
                    dgInfo4.DataSource = table.DefaultView;
                    dgInfo4.Columns[0].HeaderText = "Номер путевого листа";
                    dgInfo4.Columns[1].HeaderText = "Километраж";
                    foreach (DataGridViewColumn column in dgInfo4.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    GlobalVar.save4 = true;
                }
                else
                    MessageBox.Show("Укажите количество перевозок.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btinfo5_Click(object sender, EventArgs e)
        {
            try
            {
                DB database = new DB();
                SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Customers JOIN Waybill ON Waybill.id_customer = Customers.Id WHERE customerName = '{cbInfo.Text}'", database.GetConnection());
                database.OpenConnection();
                if (command.ExecuteScalar().ToString() != "0")
                {
                    database.CloseConnection();
                    database = new DB();
                    command = new SqlCommand("SELECT Customers.customerName, Waybill.numberOfWaybill, Waybill.kilometrage * Waybill.costkm FROM Customers " +
                                                        $"JOIN Waybill ON Waybill.id_customer = Customers.Id WHERE customerName = '{cbInfo.Text}'", database.GetConnection());
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    labelinfo4.Text = table.Rows[0][1].ToString() + " км.";
                    dgInfo5.DataSource = table.DefaultView;
                    dgInfo5.Columns[0].HeaderText = "Имя клиента";
                    dgInfo5.Columns[1].HeaderText = "Номер путевого листа";
                    dgInfo5.Columns[2].HeaderText = "Стоимость доставки";
                    foreach (DataGridViewColumn column in dgInfo5.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }

                    int sum = 0;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        sum += Convert.ToInt32(table.Rows[i][2].ToString());
                    }
                    labelinfo5.Text = Convert.ToString(sum) + " руб.";
                    GlobalVar.save5 = true;
                }
                else
                    MessageBox.Show("Данному клиенту не совершались грузовые перевозки!.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CBInfoUpload()
        {
            cbInfo.Items.Clear();
            DB db = new DB();
            SqlCommand cmd = new SqlCommand("SELECT Customers.customerName FROM Customers", db.GetConnection());
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbInfo.Items.Add(dt.Rows[i][0].ToString());
            }
            cbInfo.SelectedIndex = 0;
        }

        private void cbInfo_MouseClick(object sender, MouseEventArgs e)
        {
            CBInfoUpload();
        }

        private void buttonCustomers_Click(object sender, EventArgs e)
        {
            FormShowInfo form = new FormShowInfo("Customers");
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "FormShowInfo")
                {
                    f.Close();
                    break;
                }
            }
            form.Show();
        }

        private void buttonCars_Click(object sender, EventArgs e)
        {
            FormShowInfo form = new FormShowInfo("Cars");
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "FormShowInfo")
                {
                    f.Close();
                    break;
                }
            }
            form.Show();
        }

        private void buttonCars2_Click(object sender, EventArgs e)
        {
            FormShowInfo form = new FormShowInfo("Cars");
            int i = 0;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "FormShowInfo")
                {
                    i++;
                    if (i == 2)
                    {
                        f.Close();
                        i = 1;
                        break;
                    }
                }

            }
            form.Show();
        }

        private void buttonDrivers_Click(object sender, EventArgs e)
        {
            FormShowInfo form = new FormShowInfo("Drivers");
            int i = 0;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "FormShowInfo")
                {
                    i++;
                    if (i == 2)
                    {
                        f.Close();
                        i = 1;
                        break;
                    }
                }

            }
            form.Show();
        }

        private void tabcontrol_Leave(object sender, EventArgs e)
        {
            for (int i = 2; i > 0; i--)
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name == "FormShowInfo")
                    {
                        f.Close();
                        break;
                    }
                }
        }

        private void btSave1_Click(object sender, EventArgs e)
        {
            if (GlobalVar.save1)
            {
                MessageBox.Show("Расширение файла добавится автоматически! Укажите только имя.", "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SaveFileDialog save = new SaveFileDialog();
                if (save.ShowDialog() == DialogResult.OK)
                {
                    string filename = save.FileName + ".xlsx";
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Add(System.Reflection.Missing.Value);
                    Excel.Worksheet worksheet = workbook.Worksheets.get_Item(1);

                    for (int i = 0; i < dgInfo1.ColumnCount; i++)
                        app.Cells[1, i + 1] = dgInfo1.Columns[i].HeaderText;
                    for (int i = 0; i < dgInfo1.Rows.Count; i++)
                        for (int j = 0; j < dgInfo1.ColumnCount; j++)
                            app.Cells[i + 2, j + 1] = dgInfo1.Rows[i].Cells[j].Value;

                    app.Cells[1, dgInfo1.ColumnCount + 2] = labeltext1_1.Text;
                    app.Cells[2, dgInfo1.ColumnCount + 2] = labeltext1_2.Text;
                    app.Cells[1, dgInfo1.ColumnCount + 3] = labelinfo1.Text;
                    app.Cells[2, dgInfo1.ColumnCount + 3] = labelinfo1_2.Text;

                    app.Columns.AutoFit();//автоширина столбцов

                    workbook.SaveAs(filename);
                    workbook.Close();

                    MessageBox.Show("Файл успешно сохранён!", "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    app = null;
                    workbook = null;
                    worksheet = null;
                }
            }
            else
                MessageBox.Show("Для начала сформируйте отчёт!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btSave2_Click(object sender, EventArgs e)
        {
            if (GlobalVar.save2)
            {
                MessageBox.Show("Расширение файла добавится автоматически! Укажите только имя.", "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SaveFileDialog save = new SaveFileDialog();
                if (save.ShowDialog() == DialogResult.OK)
                {
                    string filename = save.FileName + ".xlsx";
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Add(System.Reflection.Missing.Value);
                    Excel.Worksheet worksheet = workbook.Worksheets.get_Item(1);

                    for (int i = 0; i < dginfo2_1.ColumnCount; i++)
                        app.Cells[1, i + 1] = dginfo2_1.Columns[i].HeaderText;
                    for (int i = 0; i < dginfo2_1.Rows.Count; i++)
                        for (int j = 0; j < dginfo2_1.ColumnCount; j++)
                            app.Cells[i + 2, j + 1] = dginfo2_1.Rows[i].Cells[j].Value;

                    for (int i = 0; i < dginfo2_2.ColumnCount; i++)
                        app.Cells[1, i + 2 + dginfo2_1.ColumnCount] = dginfo2_2.Columns[i].HeaderText;
                    for (int i = 0; i < dginfo2_2.Rows.Count; i++)
                        for (int j = 0; j < dginfo2_2.ColumnCount; j++)
                            app.Cells[i + 2, j + 2 + dginfo2_1.ColumnCount] = dginfo2_2.Rows[i].Cells[j].Value;

                    app.Cells[1, dginfo2_1.ColumnCount + dginfo2_2.ColumnCount + 3] = labeltext2.Text;
                    app.Cells[1, dginfo2_1.ColumnCount + dginfo2_2.ColumnCount + 4] = labelinfo2.Text;

                    app.Columns.AutoFit();//автоширина столбцов

                    workbook.SaveAs(filename);
                    workbook.Close();

                    MessageBox.Show("Файл успешно сохранён!", "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    app = null;
                    workbook = null;
                    worksheet = null;
                }
            }
            else
                MessageBox.Show("Для начала сформируйте отчёт!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        private void btSave3_Click(object sender, EventArgs e)
        {
            if (GlobalVar.save3)
            {
                MessageBox.Show("Расширение файла добавится автоматически! Укажите только имя.", "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SaveFileDialog save = new SaveFileDialog();
                if (save.ShowDialog() == DialogResult.OK)
                {
                    string filename = save.FileName + ".xlsx";
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Add(System.Reflection.Missing.Value);
                    Excel.Worksheet worksheet = workbook.Worksheets.get_Item(1);

                    for (int i = 0; i < dgInfo3.ColumnCount; i++)
                        app.Cells[1, i + 1] = dgInfo3.Columns[i].HeaderText;
                    for (int i = 0; i < dgInfo3.Rows.Count; i++)
                        for (int j = 0; j < dgInfo3.ColumnCount; j++)
                            app.Cells[i + 2, j + 1] = dgInfo3.Rows[i].Cells[j].Value;

                    app.Cells[1, 2 + dgInfo3.ColumnCount] = labeltext3_1.Text;
                    app.Cells[2, 2 + dgInfo3.ColumnCount] = labeltext3_2.Text;
                    app.Cells[1, 3 + dgInfo3.ColumnCount] = labelinfo3_1.Text;
                    app.Cells[2, 3 + dgInfo3.ColumnCount] = labelinfo3_2.Text;

                    app.Columns.AutoFit();//автоширина столбцов

                    workbook.SaveAs(filename);
                    workbook.Close();

                    MessageBox.Show("Файл успешно сохранён!", "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    app = null;
                    workbook = null;
                    worksheet = null;
                }
            }
            else
                MessageBox.Show("Для начала сформируйте отчёт!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btSave4_Click(object sender, EventArgs e)
        {
            if (GlobalVar.save4)
            {
                MessageBox.Show("Расширение файла добавится автоматически! Укажите только имя.", "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SaveFileDialog save = new SaveFileDialog();
                if (save.ShowDialog() == DialogResult.OK)
                {
                    string filename = save.FileName + ".xlsx";
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Add(System.Reflection.Missing.Value);
                    Excel.Worksheet worksheet = workbook.Worksheets.get_Item(1);

                    for (int i = 0; i < dgInfo4.ColumnCount; i++)
                        app.Cells[1, i + 1] = dgInfo4.Columns[i].HeaderText;
                    for (int i = 0; i < dgInfo4.Rows.Count; i++)
                        for (int j = 0; j < dgInfo4.ColumnCount; j++)
                            app.Cells[i + 2, j + 1] = dgInfo4.Rows[i].Cells[j].Value;

                    app.Cells[1, 2 + dgInfo4.ColumnCount] = "Самая дл. перевозка:";
                    app.Cells[1, 3 + dgInfo4.ColumnCount] = labelinfo4.Text;

                    app.Columns.AutoFit();//автоширина столбцов

                    workbook.SaveAs(filename);
                    workbook.Close();

                    MessageBox.Show("Файл успешно сохранён!", "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    app = null;
                    workbook = null;
                    worksheet = null;
                }
            }
            else
                MessageBox.Show("Для начала сформируйте отчёт!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btSave5_Click(object sender, EventArgs e)
        {
            if (GlobalVar.save5)
            {
                MessageBox.Show("Расширение файла добавится автоматически! Укажите только имя.", "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SaveFileDialog save = new SaveFileDialog();
                if (save.ShowDialog() == DialogResult.OK)
                {
                    string filename = save.FileName + ".xlsx";
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Add(System.Reflection.Missing.Value);
                    Excel.Worksheet worksheet = workbook.Worksheets.get_Item(1);

                    for (int i = 0; i < dgInfo5.ColumnCount; i++)
                        app.Cells[1, i + 1] = dgInfo5.Columns[i].HeaderText;
                    for (int i = 0; i < dgInfo5.Rows.Count; i++)
                        for (int j = 0; j < dgInfo5.ColumnCount; j++)
                            app.Cells[i + 2, j + 1] = dgInfo5.Rows[i].Cells[j].Value;

                    app.Cells[1, 2 + dgInfo5.ColumnCount] = "Общ. стоимость:";
                    app.Cells[1, 3 + dgInfo5.ColumnCount] = labelinfo5.Text;

                    app.Columns.AutoFit();//автоширина столбцов

                    workbook.SaveAs(filename);
                    workbook.Close();

                    MessageBox.Show("Файл успешно сохранён!", "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    app = null;
                    workbook = null;
                    worksheet = null;
                }
            }
            else
                MessageBox.Show("Для начала сформируйте отчёт!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void просмотрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("В окне просмотра вы можете просмотреть все путевые листы, с полной информацией о них. Для правки данных листов, перейдите во вкладку \"Справочники\"!", "Просмотр", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void справочникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("С помощью справочников вы можете добавлять, изменять, а также удалять различную информацию о грузовых перевозках. В каждой вкладке есть своя таблица, у каждой таблицы есть кнопки правки. " +
                            "В некоторых окнах правки добавлены специальные кнопки с выводом дополнительной информации, для более удобного форматирования данных.", "Просмотр", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void отчётыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для вывода данных в Excel файл, нужно создать отчёт, с помощью кнопки \"сформировать отчёт\", предварительно выбрав нужные критерии " +
                            "и после этого сохранить отчёт, нажав кнопку  \"сохранить отчёт\".", "Просмотр", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}