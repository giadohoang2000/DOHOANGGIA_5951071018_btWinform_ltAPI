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

namespace DOHOANGGIA_5951071018_btWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            loadDT();
        }

        public void loadDT()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H6BPC8N\SQLEXPRESS;Initial Catalog=Demo_winform;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("Select * from StudentTB", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);

            conn.Close();
            dtgvStudent.DataSource = dt;

        }

        private bool IsValidData()
        {
            if (tbHoSV.Text == string.Empty || tbTenSV.Text == string.Empty
                || tbDiaChi.Text == string.Empty || string.IsNullOrEmpty(tbSDT.Text)
                || string.IsNullOrEmpty(tbSBD.Text))
            {
                MessageBox.Show("Co cho chua nhap du lieu !", "Loi du lieu",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H6BPC8N\SQLEXPRESS;Initial Catalog=Demo_winform;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("Insert Into StudentTb VALUES " + "(@Name, @FatherName, @RollNumber, @Address, @Mobile)", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", tbTenSV.Text);
                cmd.Parameters.AddWithValue("@FatherName", tbHoSV.Text);
                cmd.Parameters.AddWithValue("@RollNumber", tbSBD.Text);
                cmd.Parameters.AddWithValue("@Address", tbDiaChi.Text);
                cmd.Parameters.AddWithValue("@Mobile", tbSDT.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                loadDT();
            }
        }

        public int StudentID;

        private void dtgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int nurrow = e.RowIndex;
            if (nurrow == -1)
            {
                return;
            }
            else
            {
                StudentID = Convert.ToInt32(dtgvStudent.Rows[0].Cells[0].Value);
                tbTenSV.Text = dtgvStudent.Rows[nurrow].Cells[1].Value.ToString();
                tbHoSV.Text = dtgvStudent.Rows[nurrow].Cells[2].Value.ToString();
                tbSBD.Text = dtgvStudent.Rows[nurrow].Cells[3].Value.ToString();
                tbDiaChi.Text = dtgvStudent.Rows[nurrow].Cells[4].Value.ToString();
                tbSDT.Text = dtgvStudent.Rows[nurrow].Cells[5].Value.ToString();
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H6BPC8N\SQLEXPRESS;Initial Catalog=Demo_winform;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("Update StudentTb Set " + " Name = @Name, FatherName = @FatherName, " +" RollNumber = @RollNumber, Address = @Adress" + "Mobile = @Mobile Where StudentID = @ID", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", tbTenSV.Text);
                cmd.Parameters.AddWithValue("@FatherName", tbHoSV.Text);
                cmd.Parameters.AddWithValue("@RollNumber", tbSBD.Text);
                cmd.Parameters.AddWithValue("@Address", tbDiaChi.Text);
                cmd.Parameters.AddWithValue("@Mobile", tbSDT.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                loadDT();
                
            }
            else
            {
                MessageBox.Show("Cap nhap bi loi !", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H6BPC8N\SQLEXPRESS;Initial Catalog=Demo_winform;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("Delete from StudentTb Where StudentID = @ID", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.StudentID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                loadDT();

            }
            else
            {
                MessageBox.Show("Cap nhap bi loi !", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
