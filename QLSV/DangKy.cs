using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLSV
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }

        // Phương thức được gọi khi form DangKy load
        private void DangKy_Load(object sender, EventArgs e)
        {
            // Có thể thêm mã khởi tạo nếu cần thiết khi form tải
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem các ô có bị bỏ trống không
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtTenDN.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhauLai.Text) ||
                string.IsNullOrWhiteSpace(txtKey.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ tất cả các trường.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra độ dài họ tên (phải lớn hơn 4 ký tự)
            if (txtHoTen.Text.Length <= 4)
            {
                MessageBox.Show("Họ tên phải lớn hơn 4 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtMatKhau.Text.Length <= 6)
            {
                MessageBox.Show("Mật khẩu mới phải có độ dài lớn hơn 6 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Kiểm tra tên đăng nhập không được để trống
            if (string.IsNullOrWhiteSpace(txtTenDN.Text))
            {
                MessageBox.Show("Tên đăng nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra email có đuôi là @gmail.com
            if (!txtEmail.Text.EndsWith("@gmail.com"))
            {
                MessageBox.Show("Email phải có đuôi là @gmail.com.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra số điện thoại phải đủ 10 ký tự và phải là số
            if (txtSoDienThoai.Text.Length != 10 || !long.TryParse(txtSoDienThoai.Text, out _))
            {
                MessageBox.Show("Số điện thoại phải đủ 10 ký tự và chỉ bao gồm số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra key
            if (txtKey.Text != "13102004")
            {
                MessageBox.Show("Key không đúng! Vui lòng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra xem mật khẩu có khớp nhau không
            if (txtMatKhau.Text != txtMatKhauLai.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tiến hành đăng ký sau khi kiểm tra các điều kiện thành công
            string connectionString = @"Data Source=MAYBANDANG04;Initial Catalog=quanlysv;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO TaiKhoan (HoTen, TenDangNhap, Email, SoDienThoai, MatKhau, Keypass) " +
                                   "VALUES (@HoTen, @TenDN, @Email, @SoDienThoai, @MatKhau, @Keypass)";
                    SqlCommand cmd = new SqlCommand(query, connection);

                    // Thêm các tham số
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@TenDN", txtTenDN.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
                    cmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);
                    cmd.Parameters.AddWithValue("@Keypass", txtKey.Text);

                    cmd.ExecuteNonQuery();  // Thực thi truy vấn
                    MessageBox.Show("Đăng ký thành công!");
                    this.Hide();

                    Login loginForm = new Login();  // Mở lại form đăng nhập
                    loginForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đăng ký: " + ex.Message);
                }
            }
        }

        // Phương thức xử lý sự kiện Click cho nút Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form đăng ký
            Login loginForm = new Login(); // Mở lại form đăng nhập
            loginForm.Show();
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form đăng ký
            Login loginForm = new Login(); // Mở lại form đăng nhập
            loginForm.Show();
        }
    }
}
