using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class DoiMatKhau : Form
    {
        private string tenDangNhap; // Lưu tên đăng nhập của người dùng
        private DateTime LoadTime; // Khai báo biến LoadTime để lưu thời gian tải form
        public DoiMatKhau(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
        }

        private async void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            // Kiểm tra mật khẩu mới có khớp nhau không
            if (txtMatKhauMoi.Text != txtXacNhanMatKhauMoi.Text)
            {
                MessageBox.Show("Mật khẩu mới không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtMatKhauMoi.Text.Length <= 6)
            {
                MessageBox.Show("Mật khẩu mới phải có độ dài lớn hơn 6 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string connectionString = @"Data Source=MAYBANDANG04;Initial Catalog=quanlysv;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Kiểm tra mật khẩu cũ
                    string queryCheck = "SELECT COUNT(1) FROM TaiKhoan WHERE TenDangNhap=@TenDN AND MatKhau=@MatKhauCu";
                    SqlCommand cmdCheck = new SqlCommand(queryCheck, connection);
                    cmdCheck.Parameters.AddWithValue("@TenDN", tenDangNhap);
                    cmdCheck.Parameters.AddWithValue("@MatKhauCu", txtMatKhauCu.Text);

                    int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                    if (count != 1)
                    {
                        MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Cập nhật mật khẩu mới
                    string queryUpdate = "UPDATE TaiKhoan SET MatKhau=@MatKhauMoi WHERE TenDangNhap=@TenDN";
                    SqlCommand cmdUpdate = new SqlCommand(queryUpdate, connection);
                    cmdUpdate.Parameters.AddWithValue("@TenDN", tenDangNhap);
                    cmdUpdate.Parameters.AddWithValue("@MatKhauMoi", txtMatKhauMoi.Text);

                    cmdUpdate.ExecuteNonQuery();

                    // Hiển thị thông báo và đợi 3 giây trước khi quay lại form TaiKhoan
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await Task.Delay(1000); // Đợi 3 giây

                    // Quay lại form TaiKhoan
                    TaiKhoan taiKhoanForm = new TaiKhoan(tenDangNhap, txtMatKhauMoi.Text);
                    taiKhoanForm.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đổi mật khẩu: " + ex.Message);
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Quay lại form TaiKhoan
            TaiKhoan taiKhoanForm = new TaiKhoan(tenDangNhap, txtMatKhauCu.Text);
            taiKhoanForm.Show();
            this.Close();
        }

        private void DoiMatKhau_Load(object sender, EventArgs e)
        {
            this.LoadTime = DateTime.Now;
        }

        private void DoiMatKhau_Load_1(object sender, EventArgs e)
        {

        }
    }
}
