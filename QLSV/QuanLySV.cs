using QLSV;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq; // Để sử dụng LINQ
using System.Windows.Forms;

namespace QuanLySV
{
    public partial class Form2 : Form
    {
        int rowindex = -1;
        string connectionString = @"Data Source=MAYBANDANG04;Initial Catalog=quanlysv;Integrated Security=True";
        private string selectedImagePath;
        private bool _isLoadingData = false;

        public Form2()
        {
            InitializeComponent();
            mtxtMaSV.ReadOnly = false; // Đặt mã sinh viên có thể nhập
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadKhoa();
            btnXoa.Enabled = false;
            btnCapNhat.Enabled = false;
            btnThem.Enabled = true;
            LoadData();

            // Thiết lập Mask cho MaskedTextBox để chỉ cho phép nhập 10 chữ số cho Mã sinh viên
            mtxtMaSV.Mask = "0000000000"; // Đặt Mask để chỉ cho phép nhập 10 chữ số
        }

        private void LoadKhoa()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT TenKhoa FROM Khoa", connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                cbKhoa.DataSource = dataTable;
                cbKhoa.DisplayMember = "TenKhoa";
                cbKhoa.ValueMember = "TenKhoa";
            }
        }

        private void LoadData()
{
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        dgvDanhSach.Columns.Clear(); // Xóa tất cả các cột cũ
        dgvDanhSach.DataSource = null;

        // Thêm các cột vào DataGridView
        dgvDanhSach.Columns.Add("MaSinhVien", "Mã sinh viên");
        dgvDanhSach.Columns.Add("HoTen", "Họ tên");
        dgvDanhSach.Columns.Add("NgaySinh", "Ngày sinh");
        dgvDanhSach.Columns.Add("GioiTinh", "Giới tính");
        dgvDanhSach.Columns.Add("DiemTB", "Điểm TB");
        dgvDanhSach.Columns.Add("Khoa", "Khoa");

        // Thêm cột "Hinh" để hiển thị tên tệp hình ảnh
        dgvDanhSach.Columns.Add("Hinh", "Hình");

        // Truy vấn lấy dữ liệu sinh viên
        SqlCommand cmd = new SqlCommand("SELECT MaSinhVien, HoTen, NgaySinh, GioiTinh, DiemTB, Khoa, HinhAnh FROM SinhVien", connection);
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            int rowIndex = dgvDanhSach.Rows.Add();
            dgvDanhSach.Rows[rowIndex].Cells["MaSinhVien"].Value = reader["MaSinhVien"];
            dgvDanhSach.Rows[rowIndex].Cells["HoTen"].Value = reader["HoTen"];
            dgvDanhSach.Rows[rowIndex].Cells["NgaySinh"].Value = Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy");
dgvDanhSach.Rows[rowIndex].Cells["GioiTinh"].Value = reader["GioiTinh"];
            dgvDanhSach.Rows[rowIndex].Cells["DiemTB"].Value = reader["DiemTB"];
            dgvDanhSach.Rows[rowIndex].Cells["Khoa"].Value = reader["Khoa"];

            // Hiển thị tên tệp hình ảnh
            string hinhAnh = reader["HinhAnh"] != DBNull.Value ? Path.GetFileName(reader["HinhAnh"].ToString()) : "";
            dgvDanhSach.Rows[rowIndex].Cells["Hinh"].Value = hinhAnh;
        }
    }
}



        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex == -1 || dgvDanhSach.Rows[e.RowIndex].IsNewRow)
            {
                ResetData();
                btnThem.Enabled = true;
                btnCapNhat.Enabled = false;
                btnXoa.Enabled = false;

                // Đặt mã sinh viên có thể nhập khi thêm sinh viên mới
                mtxtMaSV.ReadOnly = false;
            }
            else
            {
                rowindex = e.RowIndex;
                mtxtMaSV.Text = dgvDanhSach.Rows[rowindex].Cells["MaSinhVien"].Value.ToString();
                mtxtMaSV.ReadOnly = true; // Đặt mã sinh viên không thể sửa đổi khi chọn sinh viên

                txtHoTen.Text = dgvDanhSach.Rows[rowindex].Cells["HoTen"].Value.ToString();
                txtDiemTB.Text = dgvDanhSach.Rows[rowindex].Cells["DiemTB"].Value.ToString();
                cbKhoa.Text = dgvDanhSach.Rows[rowindex].Cells["Khoa"].Value.ToString();

                string ngaySinhString = dgvDanhSach.Rows[rowindex].Cells["NgaySinh"].Value.ToString();
                DateTime ngaySinh;
                if (DateTime.TryParse(ngaySinhString, out ngaySinh))
                {
                    dtpNgaySinh.Value = ngaySinh;
                }

                string gioiTinh = dgvDanhSach.Rows[rowindex].Cells["GioiTinh"].Value.ToString();
                if (gioiTinh == "Nam")
                {
                    rbNam.Checked = true;
                }
                else
                {
                    rbNu.Checked = true;
                }

                // Hiển thị hình ảnh từ cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT HinhAnh FROM SinhVien WHERE MaSinhVien = @MaSV";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaSV", mtxtMaSV.Text);
                    byte[] imageBytes = (byte[])cmd.ExecuteScalar();

                    if (imageBytes != null)
                    {
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            pictureBoxStudent.Image = Image.FromStream(ms);
                        }
                    }
else
                    {
                        pictureBoxStudent.Image = null;
                    }
                }

                btnThem.Enabled = false;
                btnCapNhat.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            double diemtb;
            try
            {
                if (string.IsNullOrWhiteSpace(txtDiemTB.Text) || !double.TryParse(txtDiemTB.Text, out diemtb) || diemtb < 0 || diemtb > 10)
                {
                    throw new Exception("Điểm trung bình phải là số thực từ 0 đến 10 và không được để trống.");
                }

                // Kiểm tra xem có chọn giới tính hay không
                if (!rbNam.Checked && !rbNu.Checked)
                {
                    throw new Exception("Vui lòng chọn giới tính.");
                }
                string gioiTinh = rbNam.Checked ? "Nam" : "Nữ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO SinhVien (MaSinhVien, HoTen, NgaySinh, GioiTinh, DiemTB, Khoa, HinhAnh, TinhTrangTotNghiep) " +
                                   "VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @DiemTB, @Khoa, @HinhAnh, @TinhTrangTotNghiep)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaSV", mtxtMaSV.Text);
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@DiemTB", diemtb);
                    cmd.Parameters.AddWithValue("@Khoa", cbKhoa.Text);
                    cmd.Parameters.AddWithValue("@TinhTrangTotNghiep", checkBox1.Checked);

                    if (pictureBoxStudent.Image != null)
                    {
                        byte[] imageBytes = ImageToByteArray(pictureBoxStudent.Image);
                        cmd.Parameters.AddWithValue("@HinhAnh", imageBytes);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@HinhAnh", DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Thêm sinh viên thành công!");
                LoadData();
                ResetData();
                btnXoa.Enabled = false;
                btnCapNhat.Enabled = false;
                btnThem.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void buttonChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = openFileDialog.FileName;

                using (var fs = new FileStream(selectedImagePath, FileMode.Open, FileAccess.Read))
                {
                    var img = Image.FromStream(fs);
                    pictureBoxStudent.Image = new Bitmap(img);
                }
            }
        }


        // Hàm chọn ảnh và hiển thị lên PictureBox
    




        private bool checkMaSV(string masv)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM SinhVien WHERE MaSinhVien = @MaSV", connection);
                cmd.Parameters.AddWithValue("@MaSV", masv);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count == 0;
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            double diemtb;
            try
            {
                // Kiểm tra điểm trung bình
                if (string.IsNullOrWhiteSpace(txtDiemTB.Text) || !double.TryParse(txtDiemTB.Text, out diemtb) || diemtb < 0 || diemtb > 10)
                {
                    throw new Exception("Điểm trung bình phải là số thực từ 0 đến 10 và không được để trống.");
                }

                // Các kiểm tra khác...

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query;
                    SqlCommand cmd;

                    if (selectedImagePath != null && pictureBoxStudent.Image != null)
                    {
                        // Trường hợp có ảnh mới được chọn, cập nhật cả hình ảnh
                        query = "UPDATE SinhVien SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, DiemTB=@DiemTB, Khoa=@Khoa, HinhAnh=@HinhAnh WHERE MaSinhVien=@MaSV";
                        cmd = new SqlCommand(query, connection);
                        byte[] imageBytes = ImageToByteArray(pictureBoxStudent.Image);
                        cmd.Parameters.AddWithValue("@HinhAnh", imageBytes);
                    }
                    else
                    {
                        // Trường hợp không có ảnh mới, giữ nguyên ảnh cũ
                        query = "UPDATE SinhVien SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, DiemTB=@DiemTB, Khoa=@Khoa WHERE MaSinhVien=@MaSV";
                        cmd = new SqlCommand(query, connection);
                    }

                    cmd.Parameters.AddWithValue("@MaSV", mtxtMaSV.Text);
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", rbNam.Checked ? "Nam" : "Nữ");
                    cmd.Parameters.AddWithValue("@DiemTB", diemtb);
                    cmd.Parameters.AddWithValue("@Khoa", cbKhoa.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Cập nhật sinh viên thành công!");
                LoadData();
                ResetData();
                btnThem.Enabled = true;
                btnCapNhat.Enabled = false;
                btnXoa.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }




        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (rowindex == -1 || rowindex >= dgvDanhSach.Rows.Count - 1)
                {
                    throw new Exception("Chưa chọn sinh viên cần xóa");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM SinhVien WHERE MaSinhVien = @MaSV";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaSV", mtxtMaSV.Text);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Xóa sinh viên thành công!");
                LoadData();
                ResetData();
                btnXoa.Enabled = false;
                btnCapNhat.Enabled = false;
                btnThem.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void ResetData()
        {
            mtxtMaSV.Clear();
            txtHoTen.Clear();
            txtDiemTB.Clear();
            rbNam.Checked = false;
            rbNu.Checked = false;
            pictureBoxStudent.Image = null;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn thoát không?",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    



        private void mtxtMaSV_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số (0-9) và phím xóa
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn chặn việc nhập ký tự không hợp lệ
            }
        }

        private void txtDiemTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số (0-9) và phím xóa
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Ngăn chặn việc nhập ký tự không hợp lệ
            }
        }

       
    

        // Hàm xử lý khi nhấn nút Hủy lọc
      

        // Hàm kiểm tra xem một chuỗi có phải chỉ là số hay không
        private bool IsAllDigits(string str)
        {
            return str.All(char.IsDigit); // Sử dụng LINQ để kiểm tra tất cả ký tự là số
        }

        // Sự kiện khi nhấn vào ô Mã sinh viên
        private void mtxtMaSV_Enter(object sender, EventArgs e)
        {
            // Đưa con trỏ chuột về đầu ô mỗi khi ô "Mã sinh viên" được focus
            mtxtMaSV.SelectionStart = 0; // Đưa con trỏ chuột về đầu ô
        }

        private void mtxtMaSV_Click(object sender, EventArgs e)
        {
            // Bạn có thể bỏ qua sự kiện này nếu chỉ cần sự kiện Enter
            mtxtMaSV.SelectionStart = 0; // Đưa con trỏ chuột về đầu ô
        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://hitu.edu.vn");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu dữ liệu đang được tải thì không thực hiện cập nhật vào cơ sở dữ liệu
            if (_isLoadingData)
            {
                return;
            }

            if (checkBox1.Checked)
            {
                checkBox1.Text = "Đã Tốt Nghiệp"; // Khi được tích chọn
            }
            else
            {
                checkBox1.Text = "Chưa Tốt Nghiệp"; // Khi không được tích chọn
            }

            // Cập nhật tình trạng tốt nghiệp vào cơ sở dữ liệu
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE SinhVien SET TinhTrangTotNghiep = @TinhTrangTotNghiep WHERE MaSinhVien = @MaSV";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@TinhTrangTotNghiep", checkBox1.Checked);
                    cmd.Parameters.AddWithValue("@MaSV", mtxtMaSV.Text); // Sử dụng Mã sinh viên hiện tại
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật tình trạng tốt nghiệp: " + ex.Message, "Thông báo");
            }
        }

        private string tenDangNhap; // Giả sử biến này lưu tên đăng nhập của người dùng

        private void btntk_Click(object sender, EventArgs e)
        {
            // Mở Form TaiKhoan và truyền tên đăng nhập và mật khẩu từ Login
            TaiKhoan taiKhoanForm = new TaiKhoan(Login.LoggedInUserName, Login.LoggedInPassword);
            taiKhoanForm.Show();
            this.Hide();
        }

        private void txtDiemTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxStudent_Click(object sender, EventArgs e)
        {

        }

   
    }
}