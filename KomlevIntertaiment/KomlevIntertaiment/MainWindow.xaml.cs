using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace KomlevIntertaiment
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    internal class Clients
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
        public Clients(int Id, string FirstName, string LastName, string Gender, string Address, string City, string Phone, string Email, string Status, string CreatedDate)
        { this.Id = Id; this.FirstName = FirstName; this.LastName = LastName; this.Gender = Gender; this.Address = Address; this.City = City; this.Phone = Phone; this.Email = Email; this.Status = Status; this.CreatedDate = CreatedDate; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MySqlConnection connection = new MySqlConnection(Config.DBConnection);
            connection.Open();
            List<Clients> users = new List<Clients>();
            MySqlCommand command = new MySqlCommand(Config.GetAllClients, connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                while (reader.Read())
                {
                    users.Add(new Clients(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetString(8),
                        reader.GetDateTime(9).ToString()
                    ));
                }
            }
            reader.Close();
            this.DataGridOrder.ItemsSource = users;
            connection.Close();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstNameTextBox.Text.Length < 3)
            {
                MessageBox.Show("Имя клиента не может быть короче 3 символов");
            }
            else if (LastNameTextBox.Text.Length < 3)
            {
                MessageBox.Show("Фамилия клиента не может быть короче 3 символов");
            }
            else if (GenderTextBox.Text == "")
            {
                MessageBox.Show("Гендер не может быть короче 3 символов");
            }
            else if (AdressTextBox.Text == "")
            {
                MessageBox.Show("Адрес не может быть короче 3 символов");
            }
            else if (CityTextBox.Text == "")
            {
                MessageBox.Show("Город не может быть короче 3 символов");
            }
            else if (PhoneTextBox.Text == "")
            {
                MessageBox.Show("Телефон не может быть короче 3 символов");
            }
            else if (EmailTextBox.Text == "")
            {
                MessageBox.Show("Email не может быть короче 3 символов");
            }
            else if (StatusTextBox.Text == "")
            {
                MessageBox.Show("Статус не может быть короче 3 символов");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(Config.DBConnection);
                connection.Open();

                MySqlCommand command = new MySqlCommand(Config.CreateClients(FirstNameTextBox.Text, LastNameTextBox.Text, GenderTextBox.Text, AdressTextBox.Text, CityTextBox.Text, PhoneTextBox.Text, EmailTextBox.Text, StatusTextBox.Text), connection);
                FirstNameTextBox.Text = "";
                LastNameTextBox.Text = "";
                GenderTextBox.Text = "";
                AdressTextBox.Text = "";
                CityTextBox.Text = "";
                PhoneTextBox.Text = "";
                EmailTextBox.Text = "";
                StatusTextBox.Text = "";
                command.ExecuteNonQuery();
                MessageBox.Show("Клиент успешно зарегестрирован");
                List<Clients> users = new List<Clients>();

                command = new MySqlCommand(Config.GetAllClients, connection);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    while (reader.Read())
                    {
                        users.Add(new Clients(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.GetString(5),
                            reader.GetString(6),
                            reader.GetString(7),
                            reader.GetString(8),
                            reader.GetDateTime(9).ToString()
                        ));
                    }
                }
                reader.Close();
                this.DataGridOrder.ItemsSource = users;

                connection.Close();
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(Config.DBConnection);
            connection.Open();
            MySqlCommand command = new MySqlCommand(Config.CheckClientByID(ClientIDTextBox.Text, ColumnNameTextBox.Text), connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                command = new MySqlCommand(Config.EditClientByID(ClientIDTextBox.Text, ColumnNameTextBox.Text, NewParameterTextBox.Text), connection);
                MessageBox.Show("Параметр у пользователя успешно изменен");
                FirstNameTextBox.Text = "";
                LastNameTextBox.Text = "";
                GenderTextBox.Text = "";
                AdressTextBox.Text = "";
                CityTextBox.Text = "";
                PhoneTextBox.Text = "";
                EmailTextBox.Text = "";
                StatusTextBox.Text = "";
                ClientIDTextBox.Text = "";
                ColumnNameTextBox.Text = "";
                NewParameterTextBox.Text = "";
                command.ExecuteNonQuery();
                List<Clients> users = new List<Clients>();

                command = new MySqlCommand(Config.GetAllClients, connection);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    while (reader.Read())
                    {
                        users.Add(new Clients(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.GetString(5),
                            reader.GetString(6),
                            reader.GetString(7),
                            reader.GetString(8),
                            reader.GetDateTime(9).ToString()
                        ));
                    }
                }
                reader.Close();
                this.DataGridOrder.ItemsSource = users;
            }
            else
            {
                MessageBox.Show("Клиента с таким ID не существует");
            }
            connection.Close();
        }

    }
}
