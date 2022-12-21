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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;

namespace Учет_учеников
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int Id = 0;
        public Main()
        {

            InitializeComponent();
        }
        private void DisplayData()
        {

            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Ocenki", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataView dv = this.schoolDataSet.Ocenki.DefaultView;
            dv.RowFilter = "FIO LIKE '" + textBox2.Text + "%'";
            dataGrid.DataSource = dv;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Id != 0)
            {
                cmd = new SqlCommand("delete Ocenki where Id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Данные успешно удалены!");
                DisplayData();
               // Clear();
            }
            else
            {
                MessageBox.Show("Пожалуйста выделите поле для удаления!");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != "" && textBox3.Text != "" && textBox4.Text != "" && comboBox1.Text != "")
            {
                cmd = new SqlCommand("insert into Ocenki(FIO,Discip,Prepod,Ocenka) values(@fio,@discip,@prepod,@ocenka)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@fio", textBox1.Text);
                cmd.Parameters.AddWithValue("@discip", textBox3.Text);
                cmd.Parameters.AddWithValue("@prepod", textBox4.Text);
                cmd.Parameters.AddWithValue("@ocenka", comboBox1.Text);
                //cmd.Parameters.AddWithValue("@sotr", SotrZ.Text);


                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Данные успешно добавлены");
                DisplayData();
                Clear();
            }
            else
            {
                MessageBox.Show("Пожалуйста заполните все поля!");
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != "" && textBox3.Text != "" && textBox4.Text != "" && comboBox1.Text != "")
            {
                cmd = new SqlCommand("update Ocenki set FIO=@fio,Discip=@discip,Prepod=@prepod,Ocenka=@ocenka where Id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@fio", textBox1.Text);
                cmd.Parameters.AddWithValue("@discip", textBox3.Text);
                cmd.Parameters.AddWithValue("@prepod", textBox4.Text);
                cmd.Parameters.AddWithValue("@ocenka", comboBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Данные успешно обновлены");
                con.Close();
                DisplayData();
                Clear();
            }
            else
            {
                MessageBox.Show("Пожалуйста выделете поле для редактирования!");
            }
        }
    }
}
