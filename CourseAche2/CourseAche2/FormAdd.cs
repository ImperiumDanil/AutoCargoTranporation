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
    public partial class FormAdd : Form
    {
        public string type;
        public string Funk { get; set; }
        public FormAdd(string func)
        {
            InitializeComponent();
            Funk = func;
        }
        private void FormAdd_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            DB db = new DB();
            SqlCommand cmd = null;
            SqlDataAdapter adp = null;
            DataTable dt = null;
            switch (Funk)//Создание правильной формы
            {
                case "TypeOfCargo":
                    {
                        if (GlobalVar.addupd == false)
                        {
                            btAdd.Text = "Изменить";
                            TBAdd1.Text = GlobalVar.tb1;
                            type = "updTOC";
                        }
                        else
                            type = "addTOC";
                        this.Width = 160;
                        this.Height = 130;
                        Ladd1.Text = "Тип груза";
                        break;
                    }
                case "Cargo":
                    { 
                        this.Height = 130;
                        Ladd1.Text = "Название груза";
                        Ladd6.Visible = true;
                        Ladd6.Text = "Тип груза";
                        CBAdd1.Visible = true;
                        cmd = new SqlCommand("SELECT TypeOfCargo.name_tс, TypeOfCargo.Id FROM TypeOfCargo", db.GetConnection());
                        adp = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        adp.Fill(dt);
                        int Index = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            CBAdd1.Items.Add(dt.Rows[i][0].ToString());
                            if (dt.Rows[i][0].ToString() == GlobalVar.tb2 && GlobalVar.addupd == false)
                            {
                                Index = i;
                            }
                        }

                        if (GlobalVar.addupd)//add
                        {
                            CBAdd1.SelectedIndex = 0;
                            type = "addCargo";
                        }
                        else//upd
                        {
                            btAdd.Text = "Изменить";
                            TBAdd1.Text = GlobalVar.tb1;
                            type = "updCargo";
                            CBAdd1.SelectedIndex = Index;
                        }
                        break;
                    }
                case "Drivers":
                    {
                        this.Height = 250;
                        this.Width = 240;
                        TBAdd2.Visible = true;
                        TBAdd3.Visible = true;
                        TBAdd1.Width = 200;
                        TBAdd3.Width = 200;
                        Ladd2.Visible = true;
                        Ladd3.Visible = true;
                        Ladd1.Text = "ФИО водителя";
                        Ladd2.Text = "Номер телефона";
                        Ladd3.Text = "Адрес";
                        TBAdd2.Mask = "+7(000)000-00-00";

                        if (GlobalVar.addupd)
                        {
                            type = "addDrivers";
                        }
                        else
                        {
                            btAdd.Text = "Изменить";
                            type = "updDrivers";
                            TBAdd1.Text = GlobalVar.tb1;
                            TBAdd2.Text = GlobalVar.tb2;
                            TBAdd3.Text = GlobalVar.tb3;
                        }
                        break;
                    }
                case "Customers":
                    {
                        this.Height = 250;
                        this.Width = 240;
                        TBAdd2.Visible = true;
                        TBAdd3.Visible = true;
                        TBAdd1.Width = 200;
                        TBAdd3.Width = 200;
                        Ladd2.Visible = true;
                        Ladd3.Visible = true;
                        Ladd1.Text = "ФИО клиента";
                        Ladd2.Text = "Номер телефона";
                        Ladd3.Text = "Адрес";
                        TBAdd2.Mask = "+7(000)000-00-00";

                        if (GlobalVar.addupd)
                        {
                            type = "addCustomers";
                        }
                        else
                        {
                            btAdd.Text = "Изменить";
                            type = "updCustomers";
                            TBAdd1.Text = GlobalVar.tb1;
                            TBAdd2.Text = GlobalVar.tb2;
                            TBAdd3.Text = GlobalVar.tb3;
                        }
                        break;
                    }
                case "Cars":
                    {
                        btshow0.Visible = true;
                        this.Height = 240;
                        TBAdd1.Visible = false;
                        TBAdd2.Visible = true;
                        CBAdd1.Visible = true;
                        CBAdd2.Visible = true;
                        TBAdd3.Visible = true;
                        Ladd2.Visible = true;
                        Ladd7.Visible = true;
                        Ladd3.Visible = true;
                        this.CBAdd1.Location = new Point(10, 13);
                        CBAdd1.Width = 273;
                        Ladd1.Text = "Водитель";
                        Ladd2.Text = "Грузоподъёмность(кг)";
                        Ladd7.Text = "Тип груза";
                        Ladd3.Text = "Номер автомобиля";
                        TBAdd2.Mask = "0000000";//Не думаю, что груз может быть 10000 тонн !!!
                        TBAdd3.Mask = "00000";
                        cmd = new SqlCommand("SELECT Drivers.name_d, Drivers.Id FROM Drivers", db.GetConnection());
                        adp = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        adp.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            CBAdd1.Items.Add(dt.Rows[i][0].ToString());
                            if (GlobalVar.tb1 == dt.Rows[i][0].ToString() && GlobalVar.addupd == false) //для upd
                                CBAdd1.SelectedIndex = i;
                        }
                        
                        cmd = new SqlCommand("SELECT TypeOfCars.name_tCar, TypeOfCars.Id FROM TypeOfCars", db.GetConnection());
                        adp = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        adp.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            CBAdd2.Items.Add(dt.Rows[i][0].ToString());
                            if (GlobalVar.tb2 == dt.Rows[i][0].ToString() && GlobalVar.addupd == false) //для upd
                                CBAdd2.SelectedIndex = i;
                        }

                        cmd = new SqlCommand("SELECT Cars.number_car FROM Cars", db.GetConnection());
                        adp = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        adp.Fill(dt);
                        db.CloseConnection();

                        if (GlobalVar.addupd)
                        {
                            type = "addCars";
                            CBAdd1.SelectedIndex = 0;
                            CBAdd2.SelectedIndex = 0;
                        }
                        else
                        {
                            btAdd.Text = "Изменить";
                            type = "updCars";
                            TBAdd2.Text = GlobalVar.tb3;
                            TBAdd3.Text = GlobalVar.tb4;
                        }
                        break;
                    }
                case "TypeOfCars":
                    {
                        this.Width = 160;
                        this.Height = 130;
                        Ladd1.Text = "Тип груза";

                        if (GlobalVar.addupd)
                        {
                            type = "addTOCars";
                        }
                        else
                        {
                            btAdd.Text = "Изменить";
                            TBAdd1.Text = GlobalVar.tb1;
                            type = "updTOCars";
                        }
                        break;
                    }
                case "Waybill":
                    {
                        btshow1.Visible = true;
                        btshow2.Visible = true;
                        btshow3.Visible = true;
                        TBAdd2.Visible = true;
                        TBAdd3.Visible = true;
                        TBAdd4.Visible = true;
                        TBAdd5.Visible = true;
                        TBAdd6.Visible = true;
                        CBAdd1.Visible = true;
                        CBAdd2.Visible = true;
                        CBAdd3.Visible = true;
                        Ladd2.Visible = true;
                        Ladd3.Visible = true;
                        Ladd4.Visible = true;
                        Ladd5.Visible = true;
                        Ladd6.Visible = true;
                        Ladd7.Visible = true;
                        Ladd8.Visible = true;
                        Ladd9.Visible = true;
                        Ladd1.Text = "Конечный адрес";
                        Ladd2.Text = "Километраж";
                        Ladd3.Text = "Стоимость за км.";
                        Ladd4.Text = "Вес груза";
                        Ladd5.Text = "Номер путевого листа";
                        Ladd9.Text = "Дата ";
                        Ladd6.Text = "Номер автомобиля";
                        Ladd7.Text = "Клиент";
                        Ladd8.Text = "Груз";
                        TBAdd2.Mask = "00000";
                        TBAdd3.Mask = "0000";
                        TBAdd4.Mask = "00000";
                        TBAdd5.Mask = "00000";
                        TBAdd6.Mask = "00/00/0000";

                        cmd = new SqlCommand("SELECT Cars.number_car FROM Cars", db.GetConnection());
                        adp = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        adp.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            CBAdd1.Items.Add(dt.Rows[i][0].ToString());
                            if (dt.Rows[i][0].ToString() == GlobalVar.id2 && GlobalVar.addupd == false)
                                CBAdd1.SelectedIndex = i;
                        }

                        cmd = new SqlCommand("SELECT customerName FROM Customers", db.GetConnection());
                        adp = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        adp.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            CBAdd2.Items.Add(dt.Rows[i][0].ToString());
                            if (dt.Rows[i][0].ToString() == GlobalVar.tb7 && GlobalVar.addupd == false)
                                CBAdd2.SelectedIndex = i;
                        }

                        cmd = new SqlCommand("SELECT name_cargo FROM Cargo", db.GetConnection());
                        adp = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        adp.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            CBAdd3.Items.Add(dt.Rows[i][0].ToString());
                            if (dt.Rows[i][0].ToString() == GlobalVar.tb8 && GlobalVar.addupd == false)
                                CBAdd3.SelectedIndex = i;
                        }


                        if (GlobalVar.addupd)
                        {
                            CBAdd1.SelectedIndex = 0;
                            CBAdd2.SelectedIndex = 0;
                            CBAdd3.SelectedIndex = 0;
                            type = "addWaybill";
                        }
                        else
                        {
                            TBAdd1.Text = GlobalVar.tb1;
                            TBAdd2.Text = GlobalVar.tb2;
                            TBAdd3.Text = GlobalVar.tb3;
                            TBAdd4.Text = GlobalVar.tb4;
                            TBAdd5.Text = GlobalVar.tb5;
                            TBAdd6.Text = GlobalVar.tb6;

                            btAdd.Text = "Изменить";
                            type = "updWaybill";
                        }
                        break;
                    }
            }
        }

        private void btAdd_Click(object sender, EventArgs e)//Добавление и изменение данных
        {
            DB db = new DB();
            SqlCommand cmd = null;
            SqlDataAdapter adp = null;
            DataTable dt = null;

            
            switch (type)//Добавление и удаление данных
            {
                case "addTOC":
                    {
                        try
                        {
                            cmd = new SqlCommand($"SELECT COUNT(TypeOfCargo.name_tс) FROM TypeOfCargo WHERE name_tс = '{TBAdd1.Text}'", db.GetConnection());
                            db.OpenConnection();
                            if (cmd.ExecuteScalar().ToString() != "0")
                            {
                                db.CloseConnection();
                                MessageBox.Show("Данный тип груза уже существует!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                if (TBAdd1.Text.Trim() != string.Empty)
                                {
                                    cmd = new SqlCommand(@"INSERT INTO TypeOfCargo (name_tс) VALUES (@Name)", db.GetConnection());
                                    cmd.Parameters.AddWithValue("Name", TBAdd1.Text);
                                    cmd.ExecuteNonQuery();
                                    db.CloseConnection();
                                    MessageBox.Show("Запись была успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    db.CloseConnection();
                                    MessageBox.Show("Введите данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Close();
                        break;
                    }
                case "updTOC":
                    {
                        cmd = new SqlCommand($"SELECT Count(TypeOfCargo.Id) From TypeOfCargo JOIN Cargo ON TypeOfCargo.Id = Cargo.id_type WHERE TypeOfCargo.Id = '{GlobalVar.id}'", db.GetConnection());
                        db.OpenConnection();
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            cmd = new SqlCommand($"SELECT COUNT(TypeOfCargo.name_tс) FROM TypeOfCargo WHERE name_tс = '{TBAdd1.Text}'", db.GetConnection());
                            if (cmd.ExecuteScalar().ToString() == "0")
                            {
                                if (TBAdd1.Text.Trim() != string.Empty)
                                {
                                    cmd = new SqlCommand(@"UPDATE TypeOfCargo SET name_tс = @Name Where Id = @Id", db.GetConnection());
                                    cmd.Parameters.AddWithValue("Name", TBAdd1.Text);
                                    cmd.Parameters.AddWithValue("Id", GlobalVar.id);
                                    cmd.ExecuteNonQuery();
                                    db.CloseConnection();
                                    MessageBox.Show("Запись была успешно изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    db.CloseConnection();
                                    MessageBox.Show("Введите данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                this.Close();
                            }
                            else
                            {
                                db.CloseConnection();
                                MessageBox.Show("Данный тип груза уже существует!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            
                        }
                        else
                        {
                            db.CloseConnection();
                            MessageBox.Show("Перестаньте использовать данный тип груза в другой таблице, чтобы изменить его!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    }
                case "addCargo":
                    {
                        try
                        {
                            cmd = new SqlCommand($"SELECT COUNT(Cargo.name_cargo) FROM Cargo WHERE name_cargo = '{TBAdd1.Text}'", db.GetConnection());
                            db.OpenConnection();
                            if (cmd.ExecuteScalar().ToString() != "0")
                            {
                                db.CloseConnection();
                                MessageBox.Show("Данный груз уже существует!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                if (TBAdd1.Text.Trim() != string.Empty)
                                {
                                    cmd = new SqlCommand("SELECT TypeOfCargo.name_tс, TypeOfCargo.Id FROM TypeOfCargo", db.GetConnection());
                                    adp = new SqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    adp.Fill(dt);
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (dt.Rows[i][0].ToString() == CBAdd1.Text)
                                            GlobalVar.id = dt.Rows[i][1].ToString();
                                    }
                                    cmd = new SqlCommand(@"INSERT INTO Cargo (name_cargo, id_type) VALUES (@Name, @Id)", db.GetConnection());
                                    cmd.Parameters.AddWithValue("Name", TBAdd1.Text);
                                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(GlobalVar.id));
                                    cmd.ExecuteNonQuery();
                                    db.CloseConnection();
                                    MessageBox.Show("Запись была успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    db.CloseConnection();
                                    MessageBox.Show("Введите данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Close();
                        break;
                    }
                case "updCargo":
                    {
                        cmd = new SqlCommand($"SELECT COUNT(*) FROM Waybill JOIN Cargo ON Cargo.Id = Waybill.id_cargo WHERE Cargo.Id = '{GlobalVar.id}'", db.GetConnection());
                        db.OpenConnection();
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            cmd = new SqlCommand($"SELECT COUNT(*) FROM Cargo WHERE name_cargo = '{TBAdd1.Text}' AND Cargo.Id <> '{GlobalVar.id}'", db.GetConnection());
                            if (cmd.ExecuteScalar().ToString() != "0")
                            {
                                db.CloseConnection();
                                MessageBox.Show("Данный груз уже существует!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                if (TBAdd1.Text.Trim() != string.Empty)
                                {
                                    cmd = new SqlCommand("SELECT TypeOfCargo.name_tс, TypeOfCargo.Id FROM TypeOfCargo", db.GetConnection());
                                    adp = new SqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    adp.Fill(dt);
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (CBAdd1.Text == dt.Rows[i][0].ToString())
                                            GlobalVar.id2 = dt.Rows[i][1].ToString();
                                    }

                                    cmd = new SqlCommand(@"UPDATE Cargo SET name_cargo = @Name, id_type = @IdType where Cargo.Id = @Id", db.GetConnection());
                                    cmd.Parameters.AddWithValue("Name", TBAdd1.Text);
                                    cmd.Parameters.AddWithValue("IdType", Convert.ToInt32(GlobalVar.id2));
                                    cmd.Parameters.AddWithValue("Id", Convert.ToInt32(GlobalVar.id));
                                    cmd.ExecuteNonQuery();
                                    db.CloseConnection();
                                    MessageBox.Show("Запись была успешно изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                                else
                                {
                                    db.CloseConnection();
                                    MessageBox.Show("Введите данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            db.CloseConnection();
                            MessageBox.Show("Перестаньте использовать данный тип груза в другой таблице, чтобы изменить его!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    }
                case "addDrivers":
                    {
                        try
                        {
                            if (TBAdd1.Text.Trim() != string.Empty && TBAdd3.Text.Trim() != string.Empty && TBAdd2.Text.Length == 16)//Не добавлял проверку на значения, так как вдруг существуют близнецы, которые родились в один дель, с одинаковыми именами, да ешё и с 1 номера зарегестрировались по одному адресу!!!!!
                            {
                                cmd = new SqlCommand(@"INSERT INTO Drivers (name_d, phone_d, address_d) VALUES (@Name, @Phone, @Address)",db.GetConnection());
                                cmd.Parameters.AddWithValue("Name", TBAdd1.Text);
                                cmd.Parameters.AddWithValue("Phone", TBAdd2.Text);
                                cmd.Parameters.AddWithValue("Address", TBAdd3.Text);
                                db.OpenConnection();
                                cmd.ExecuteNonQuery();
                                db.CloseConnection();
                            }
                            else
                            {
                                MessageBox.Show("Проверьте введённые данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Close();
                        break;
                    }
                case "updDrivers":
                    {
                        try
                        {
                            cmd = new SqlCommand($"SELECT COUNT(*) From Drivers JOIN Cars ON Drivers.Id = Cars.id_driver WHERE Drivers.Id = {GlobalVar.id}", db.GetConnection());
                            db.OpenConnection();
                            if (cmd.ExecuteScalar().ToString() == "0")
                            {
                                if (TBAdd1.Text.Trim() != string.Empty && TBAdd3.Text.Trim() != string.Empty && TBAdd2.Text.Length == 16)
                                {
                                    cmd = new SqlCommand(@"UPDATE Drivers SET name_d = @Name, phone_d = @Phone, address_d = @Address WHERE Drivers.Id = @Id", db.GetConnection());
                                    cmd.Parameters.AddWithValue("Name", TBAdd1.Text);
                                    cmd.Parameters.AddWithValue("Phone", TBAdd2.Text);
                                    cmd.Parameters.AddWithValue("Address", TBAdd3.Text);
                                    cmd.Parameters.AddWithValue("Id", GlobalVar.id);
                                    cmd.ExecuteNonQuery();
                                    db.CloseConnection();
                                    MessageBox.Show("Запись была успешно изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    db.CloseConnection();
                                    MessageBox.Show("Проверьте введённые данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                db.CloseConnection();
                                MessageBox.Show("Данный водитель используется в перевозках! Уберите его из таблицы автомобилей, чтобы изменить.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Close();
                        break;
                    }
                case "addCustomers":
                    {
                        try
                        {
                            if (TBAdd1.Text.Trim() != string.Empty && TBAdd3.Text.Trim() != string.Empty && TBAdd2.Text.Length == 16)//Не добавлял проверку на значения, так как вдруг существуют близнецы, которые родились в один дель, с одинаковыми именами, да ешё и с 1 номера зарегестрировались по одному адресу!!!!!
                            {
                                cmd = new SqlCommand(@"INSERT INTO Customers (customerName, customerPhone, customerAddress) VALUES (@Name, @Phone, @Address)", db.GetConnection());
                                cmd.Parameters.AddWithValue("Name", TBAdd1.Text);
                                cmd.Parameters.AddWithValue("Phone", TBAdd2.Text);
                                cmd.Parameters.AddWithValue("Address", TBAdd3.Text);
                                db.OpenConnection();
                                cmd.ExecuteNonQuery();
                                db.CloseConnection();
                            }
                            else
                            {
                                MessageBox.Show("Проверьте введённые данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Close();
                        break;
                    }
                case "updCustomers":
                    {
                        try
                        {
                            cmd = new SqlCommand($"SELECT COUNT(*) From Customers JOIN Waybill ON Waybill.id_customer = Customers.Id WHERE Customers.Id = {GlobalVar.id}", db.GetConnection());
                            db.OpenConnection();
                            if (cmd.ExecuteScalar().ToString() == "0")
                            {
                                if (TBAdd1.Text.Trim() != string.Empty && TBAdd3.Text.Trim() != string.Empty && TBAdd2.Text.Length == 16)
                                {
                                    cmd = new SqlCommand(@"UPDATE Customers SET customerName = @Name, customerPhone = @Phone, customerAddress = @Address WHERE Customers.Id = @Id", db.GetConnection());
                                    cmd.Parameters.AddWithValue("Name", TBAdd1.Text);
                                    cmd.Parameters.AddWithValue("Phone", TBAdd2.Text);
                                    cmd.Parameters.AddWithValue("Address", TBAdd3.Text);
                                    cmd.Parameters.AddWithValue("Id", GlobalVar.id);
                                    cmd.ExecuteNonQuery();
                                    db.CloseConnection();
                                    MessageBox.Show("Запись была успешно изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    db.CloseConnection();
                                    MessageBox.Show("Проверьте введённые данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                db.CloseConnection();
                                MessageBox.Show("Данный клиент уже заказал груз! Уберите его из таблицы путевых листов, чтобы изменить.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Close();
                        break;
                    }
                case "addTOCars":
                    {
                        try
                        {
                            cmd = new SqlCommand($"SELECT COUNT(*) FROM TypeOfCars WHERE TypeOfCars.name_tCar =  '{TBAdd1.Text}'", db.GetConnection());
                            db.OpenConnection();
                            if (cmd.ExecuteScalar().ToString() != "0")
                            {
                                db.CloseConnection();
                                MessageBox.Show("Данный тип автомобиля уже существует!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else if (TBAdd1.Text.Trim() != string.Empty)
                                {
                                cmd = new SqlCommand(@"INSERT INTO TypeOfCars (name_tCar) VALUES (@Name)", db.GetConnection());
                                cmd.Parameters.AddWithValue("Name", TBAdd1.Text);
                                cmd.ExecuteNonQuery();
                                db.CloseConnection();
                                MessageBox.Show("Запись была успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    db.CloseConnection();
                                    MessageBox.Show("Введите данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Close();
                        break;
                    }
                case "updTOCars":
                    {
                        cmd = new SqlCommand($"SELECT Count(*) From TypeOfCars JOIN Cars ON TypeOfCars.Id = Cars.id_typeCar WHERE TypeOfCars.Id = '{GlobalVar.id}'", db.GetConnection());
                        db.OpenConnection();
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            cmd = new SqlCommand($"SELECT COUNT(*) FROM TypeOfCars WHERE TypeOfCars.name_tCar =  '{TBAdd1.Text}'", db.GetConnection());
                            if (cmd.ExecuteScalar().ToString() == "0")
                            {
                                if (TBAdd1.Text.Trim() != string.Empty)
                                {
                                    cmd = new SqlCommand(@"UPDATE TypeOfCars SET name_tCar = @Name Where Id = @Id", db.GetConnection());
                                    cmd.Parameters.AddWithValue("Name", TBAdd1.Text);
                                    cmd.Parameters.AddWithValue("Id", GlobalVar.id);
                                    cmd.ExecuteNonQuery();
                                    db.CloseConnection();
                                    MessageBox.Show("Запись была успешно изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    db.CloseConnection();
                                    MessageBox.Show("Введите данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                this.Close();
                            }
                            else
                            {
                                db.CloseConnection();
                                MessageBox.Show("Данный тип автомобиля уже существует!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            db.CloseConnection();
                            MessageBox.Show("Перестаньте использовать данный тип автомобиля в таблице автомобилей, чтобы изменить его!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    }
                case "addCars":
                    {
                        if(TBAdd2.Text != string.Empty && TBAdd3.Text != string.Empty)
                        {
                            cmd = new SqlCommand($"SELECT COUNT(*) FROM Cars WHERE number_car = '{TBAdd3.Text}'", db.GetConnection());
                            db.OpenConnection();
                            if (cmd.ExecuteScalar().ToString() == "0")
                            {
                                cmd = new SqlCommand("SELECT Drivers.Id, Drivers.name_d FROM Drivers", db.GetConnection());
                                adp = new SqlDataAdapter(cmd);
                                dt = new DataTable();
                                adp.Fill(dt);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][1].ToString() == CBAdd1.Text)
                                        GlobalVar.tb1 = dt.Rows[i][0].ToString();
                                }

                                cmd = new SqlCommand("SELECT TypeOfCars.Id, TypeOfCars.name_tCar FROM TypeOfCars", db.GetConnection());
                                adp = new SqlDataAdapter(cmd);
                                dt = new DataTable();
                                adp.Fill(dt);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][1].ToString() == CBAdd2.Text)
                                        GlobalVar.tb2 = dt.Rows[i][0].ToString();
                                }

                                cmd = new SqlCommand("INSERT INTO Cars (id_driver, id_typeCar, carrying, number_car) VALUES (@Driver, @Type, @Carrying, @Number)", db.GetConnection());
                                cmd.Parameters.AddWithValue("Driver", GlobalVar.tb1);
                                cmd.Parameters.AddWithValue("Type", GlobalVar.tb2);
                                cmd.Parameters.AddWithValue("Carrying", TBAdd2.Text);
                                cmd.Parameters.AddWithValue("Number", TBAdd3.Text);
                                cmd.ExecuteNonQuery();
                                db.CloseConnection();
                                this.Close();
                                MessageBox.Show("Запись была успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                db.CloseConnection();
                                MessageBox.Show("Автомобиль с таким номером уже существует!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    }
                case "updCars":
                    {
                        if (TBAdd2.Text != string.Empty)
                        {
                            cmd = new SqlCommand($"SELECT COUNT(*) FROM Cars JOIN Waybill ON Cars.Id = Waybill.id_car WHERE Cars.Id = {GlobalVar.id}", db.GetConnection());
                            db.OpenConnection();
                            if (cmd.ExecuteScalar().ToString() == "0")
                            {
                                cmd = new SqlCommand($"SELECT COUNT(*) FROM Cars WHERE number_car = {TBAdd3.Text} AND Cars.Id <> {GlobalVar.id}", db.GetConnection());
                                if (cmd.ExecuteScalar().ToString() == "0")
                                {
                                    cmd = new SqlCommand("SELECT Drivers.Id, Drivers.name_d FROM Drivers", db.GetConnection());
                                    adp = new SqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    adp.Fill(dt);
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (dt.Rows[i][1].ToString() == CBAdd1.Text)
                                            GlobalVar.tb1 = dt.Rows[i][0].ToString();
                                    }

                                    cmd = new SqlCommand("SELECT TypeOfCars.Id, TypeOfCars.name_tCar FROM TypeOfCars", db.GetConnection());
                                    adp = new SqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    adp.Fill(dt);
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (dt.Rows[i][1].ToString() == CBAdd2.Text)
                                            GlobalVar.tb2 = dt.Rows[i][0].ToString();
                                    }

                                    cmd = new SqlCommand("UPDATE Cars SET id_driver = @Driver, id_typeCar = @Type, carrying = @Carrying, number_car = @Number WHERE Cars.Id = @Id", db.GetConnection());
                                    cmd.Parameters.AddWithValue("Driver", GlobalVar.tb1);
                                    cmd.Parameters.AddWithValue("Type", GlobalVar.tb2);
                                    cmd.Parameters.AddWithValue("Carrying", TBAdd2.Text);
                                    cmd.Parameters.AddWithValue("Number", TBAdd3.Text);
                                    cmd.Parameters.AddWithValue("Id", GlobalVar.id);
                                    cmd.ExecuteNonQuery();
                                    db.CloseConnection();
                                    this.Close();
                                    MessageBox.Show("Запись была успешно изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    db.CloseConnection();
                                    MessageBox.Show("Автомобиль с таким номером уже существует!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                db.CloseConnection();
                                MessageBox.Show("Данный автомобиль используется в путевом листе! Удалите его из той таблицы, чтобы изменить", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    }
                case "addWaybill":
                    {
                        try
                        {
                            if (TBAdd1.Text != string.Empty && TBAdd2.Text != string.Empty && TBAdd3.Text != string.Empty && TBAdd4.Text != string.Empty && TBAdd5.Text != string.Empty && TBAdd6.Text.Length > 6)
                            {
                                cmd = new SqlCommand($"SELECT COUNT(*) FROM Waybill WHERE Waybill.numberOfWaybill = '{TBAdd5.Text}'", db.GetConnection());
                                db.OpenConnection();
                                if (cmd.ExecuteScalar().ToString() != "0")
                                {
                                    db.CloseConnection();
                                    MessageBox.Show("Номер путевого листа уже занят!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                else
                                {
                                    cmd = new SqlCommand("SELECT Cars.carrying, Cars.number_car FROM Cars", db.GetConnection());
                                    adp = new SqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    adp.Fill(dt);
                                    int weightCargo = Convert.ToInt32(TBAdd4.Text);
                                    int carrying = 0;
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (dt.Rows[i][1].ToString() == CBAdd1.Text)
                                            carrying = Convert.ToInt32(dt.Rows[i][0].ToString());
                                    }
                                    if (carrying >= weightCargo)
                                    {
                                        cmd = new SqlCommand("SELECT Customers.Id, Customers.customerName FROM Customers", db.GetConnection());
                                        adp = new SqlDataAdapter(cmd);
                                        dt = new DataTable();
                                        adp.Fill(dt);
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            if (dt.Rows[i][1].ToString() == CBAdd2.Text)
                                                GlobalVar.tb1 = dt.Rows[i][0].ToString();
                                        }

                                        cmd = new SqlCommand("SELECT Cargo.Id, Cargo.name_cargo FROM Cargo", db.GetConnection());
                                        adp = new SqlDataAdapter(cmd);
                                        dt = new DataTable();
                                        adp.Fill(dt);
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            if (dt.Rows[i][1].ToString() == CBAdd3.Text)
                                                GlobalVar.tb2 = dt.Rows[i][0].ToString();
                                        }

                                        cmd = new SqlCommand("SELECT Cars.Id, Cars.number_car FROM Cars", db.GetConnection());
                                        adp = new SqlDataAdapter(cmd);
                                        dt = new DataTable();
                                        adp.Fill(dt);
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            if (dt.Rows[i][1].ToString() == CBAdd1.Text)
                                                GlobalVar.tb3 = dt.Rows[i][0].ToString();
                                        }

                                        cmd = new SqlCommand("INSERT INTO Waybill (id_car, id_customer, id_cargo, numberOfWaybill, finalAddress, kilometrage, costkm, weight_cargo, dataACT) " +
                                                            "VALUES (@IdCar, @IdCustomer, @IdCargo, @NumberWay, @Address, @Kilometrag, @Costcm, @Weight, @Data)", db.GetConnection());
                                        cmd.Parameters.AddWithValue("IdCar", GlobalVar.tb3);
                                        cmd.Parameters.AddWithValue("IdCustomer", GlobalVar.tb1);
                                        cmd.Parameters.AddWithValue("IdCargo", GlobalVar.tb2);
                                        cmd.Parameters.AddWithValue("NumberWay", TBAdd5.Text);
                                        cmd.Parameters.AddWithValue("Address", TBAdd1.Text);
                                        cmd.Parameters.AddWithValue("Kilometrag", TBAdd2.Text);
                                        cmd.Parameters.AddWithValue("Costcm", TBAdd3.Text);
                                        cmd.Parameters.AddWithValue("Weight", TBAdd4.Text);
                                        cmd.Parameters.AddWithValue("Data", DateTime.Parse(TBAdd6.Text));
                                        cmd.ExecuteNonQuery();
                                        db.CloseConnection();
                                        MessageBox.Show("Запись была успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        db.CloseConnection();
                                        MessageBox.Show("Вес груза больше грузоподъёмности авто! Выберите другое авто.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Введите данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Close();
                        break;
                    }
                case "updWaybill":
                    {
                        if (TBAdd1.Text != string.Empty && TBAdd2.Text != string.Empty && TBAdd3.Text != string.Empty && TBAdd4.Text != string.Empty && TBAdd5.Text != string.Empty && TBAdd6.Text != string.Empty)
                        {
                            cmd = new SqlCommand($"SELECT COUNT(*) FROM Waybill WHERE Waybill.numberOfWaybill = {TBAdd5.Text} AND Waybill.Id <> {GlobalVar.id}", db.GetConnection());
                            db.OpenConnection();
                            if (cmd.ExecuteScalar().ToString() == "0")
                            {
                                cmd = new SqlCommand("SELECT Cars.carrying, Cars.number_car FROM Cars", db.GetConnection());
                                adp = new SqlDataAdapter(cmd);
                                dt = new DataTable();
                                adp.Fill(dt);
                                int weightCargo = Convert.ToInt32(TBAdd4.Text);
                                int carrying = 0;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][1].ToString() == CBAdd1.Text)
                                        carrying = Convert.ToInt32(dt.Rows[i][0].ToString());
                                }
                                if (carrying >= weightCargo)
                                {
                                    cmd = new SqlCommand("SELECT Customers.Id, Customers.customerName FROM Customers", db.GetConnection());
                                    adp = new SqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    adp.Fill(dt);
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (dt.Rows[i][1].ToString() == CBAdd2.Text)
                                            GlobalVar.tb7 = dt.Rows[i][0].ToString();
                                    }

                                    cmd = new SqlCommand("SELECT Cargo.Id, Cargo.name_cargo FROM Cargo", db.GetConnection());
                                    adp = new SqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    adp.Fill(dt);
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (dt.Rows[i][1].ToString() == CBAdd3.Text)
                                            GlobalVar.tb8 = dt.Rows[i][0].ToString();
                                    }

                                    cmd = new SqlCommand("SELECT Cars.Id, Cars.number_car FROM Cars", db.GetConnection());
                                    adp = new SqlDataAdapter(cmd);
                                    dt = new DataTable();
                                    adp.Fill(dt);
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (dt.Rows[i][1].ToString() == CBAdd1.Text)
                                            GlobalVar.id2 = dt.Rows[i][0].ToString();
                                    }

                                    cmd = new SqlCommand("UPDATE Waybill SET id_car = @IdCar, id_customer = @IdCustomer, id_cargo = @IdCargo, " +
                                                        "numberOfWaybill = @NumberWay, finalAddress = @Address, kilometrage = @Kilometrag, costkm = @Costcm, " +
                                                        "weight_cargo = @Weight, dataACT = @Data WHERE Id = @Id", db.GetConnection());
                                    cmd.Parameters.AddWithValue("IdCar", GlobalVar.id2);
                                    cmd.Parameters.AddWithValue("IdCustomer", GlobalVar.tb7);
                                    cmd.Parameters.AddWithValue("IdCargo", GlobalVar.tb8);
                                    cmd.Parameters.AddWithValue("NumberWay", TBAdd5.Text);
                                    cmd.Parameters.AddWithValue("Address", TBAdd1.Text);
                                    cmd.Parameters.AddWithValue("Kilometrag", TBAdd2.Text);
                                    cmd.Parameters.AddWithValue("Costcm", TBAdd3.Text);
                                    cmd.Parameters.AddWithValue("Weight", TBAdd4.Text);
                                    cmd.Parameters.AddWithValue("Data", DateTime.Parse(TBAdd6.Text));
                                    cmd.Parameters.AddWithValue("Id", GlobalVar.id);
                                    cmd.ExecuteNonQuery();
                                    db.CloseConnection();
                                    this.Close();
                                    MessageBox.Show("Запись была успешно изменена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    db.OpenConnection();
                                    MessageBox.Show("Вес груза больше грузоподъёмности авто! Выберите другое авто.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                db.OpenConnection();
                                MessageBox.Show("Номер путевого листа уже занят!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Введите данные!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    }
            }
        }
        private void btshow0_Click(object sender, EventArgs e)
        {
            FormShowInfo form = new FormShowInfo("Drivers");
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

        private void btshow1_Click(object sender, EventArgs e)
        {
            FormShowInfo form = new FormShowInfo("Cars");
            int i = 0;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "FormShowInfo")
                {
                    i++;
                    if(i == 3)
                    {
                        f.Close();
                        i = 2;
                        break;
                    }
                }
                
            }
            form.Show();
        }

        private void btshow2_Click(object sender, EventArgs e)
        {
            FormShowInfo form = new FormShowInfo("Customers");
            int i = 0;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "FormShowInfo")
                {
                    i++;
                    if (i == 3)
                    {
                        f.Close();
                        i = 2;
                        break;
                    }
                }

            }
            form.Show();
        }

        private void btshow3_Click(object sender, EventArgs e)
        {
            FormShowInfo form = new FormShowInfo("Cargo");
            int i = 0;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "FormShowInfo")
                {
                    i++;
                    if (i == 3)
                    {
                        f.Close();
                        i = 2;
                        break;
                    }
                }

            }
            form.Show();
        }
    }
}
