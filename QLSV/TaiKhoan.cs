using QuanLySV;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLSV
{
    public partial class TaiKhoan : Form
    {
        private string tenDangNhap;
        private string matKhau;

        public TaiKhoan(string tenDangNhap, string matKhau) // Constructor với 2 tham số
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
            this.matKhau = matKhau;
        }

        private void TaiKhoan_Load(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = tenDangNhap;
            txtMatKhau.Text = matKhau;
            LoadThongTinTaiKhoan();
        }

        private void LoadThongTinTaiKhoan()
        {
            string connectionString = @"Data Source=MAYBANDANG04;Initial Catalog=quanlysv;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT HoTen, Email, SoDienThoai FROM TaiKhoan WHERE TenDangNhap = @TenDN AND MatKhau = @MatKhau";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TenDN", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtHoTen.Text = reader["HoTen"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtSoDienThoai.Text = reader["SoDienThoai"].ToString();

                        txtHoTen.ReadOnly = false;
                        txtEmail.ReadOnly = false;
                        txtSoDienThoai.ReadOnly = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy thông tin tài khoản: " + ex.Message);
                }
            }
        }

        private void btnCapNhatThongTin_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin người dùng
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) || txtHoTen.Text.Length <= 4)
            {
                MessageBox.Show("Họ tên không được để trống và phải lớn hơn 4 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.EndsWith("@gmail.com"))
            {
                MessageBox.Show("Email không được để trống và phải có đuôi là @gmail.com.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text) || txtSoDienThoai.Text.Length != 10 || !long.TryParse(txtSoDienThoai.Text, out _))
            {
                MessageBox.Show("Số điện thoại phải đủ 10 ký tự và chỉ bao gồm số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Nếu tất cả các điều kiện đều đúng, tiếp tục cập nhật thông tin
            string connectionString = @"Data Source=MAYBANDANG04;Initial Catalog=quanlysv;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE TaiKhoan SET HoTen = @HoTen, Email = @Email, SoDienThoai = @SoDienThoai WHERE TenDangNhap = @TenDN";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TenDN", tenDangNhap);
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật thông tin: " + ex.Message);
                }
            }
        }


        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            Form2 quanLySVForm = new Form2();
            quanLySVForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoiMatKhau doiMatKhauForm = new DoiMatKhau(tenDangNhap);
            doiMatKhauForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login.LoggedInUserName = null;
            Login.LoggedInPassword = null;

            this.Hide();
            Login loginForm = new Login();
            loginForm.Show();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                txtMatKhau.PasswordChar = '\0';
            }
            else
            {
                txtMatKhau.PasswordChar = '*';
            }
        }
    }
}
