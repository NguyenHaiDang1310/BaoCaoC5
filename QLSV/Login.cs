using QuanLySV;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLSV
{
    public partial class Login : Form
    {
        public static string LoggedInUserName;  // Biến để lưu tên đăng nhập của người dùng
        public static string LoggedInPassword;  // Biến để lưu mật khẩu của người dùng

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Bạn có thể thêm mã khởi tạo nếu cần khi form load
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDN.Text) || string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.");
                return;
            }

            string connectionString = @"Data Source=MAYBANDANG04;Initial Catalog=quanlysv;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(1) FROM TaiKhoan WHERE TenDangNhap=@TenDN AND MatKhau=@MatKhau";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TenDN", txtTenDN.Text);
                    cmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 1)
                    {
                        LoggedInUserName = txtTenDN.Text;
                        LoggedInPassword = txtMatKhau.Text;

                        MessageBox.Show("Đăng nhập thành công!");
                        this.Hide();

                        Form2 qlsvForm = new Form2();
                        qlsvForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangKy dkForm = new DangKy();
            dkForm.Show();
        }

   

    }
}
