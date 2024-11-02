namespace QLSV
{
    partial class DangKy
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.txtTenDN = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.txtMatKhauLai = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.lblTenDN = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.lblMatKhauLai = new System.Windows.Forms.Label();
            this.lblKey = new System.Windows.Forms.Label();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtHoTen
            // 
            this.txtHoTen.BackColor = System.Drawing.Color.White;
            this.txtHoTen.Location = new System.Drawing.Point(196, 6);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(291, 22);
            this.txtHoTen.TabIndex = 0;
            // 
            // txtTenDN
            // 
            this.txtTenDN.Location = new System.Drawing.Point(196, 41);
            this.txtTenDN.Name = "txtTenDN";
            this.txtTenDN.Size = new System.Drawing.Size(291, 22);
            this.txtTenDN.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(196, 80);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(291, 22);
            this.txtEmail.TabIndex = 2;
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Location = new System.Drawing.Point(196, 125);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new System.Drawing.Size(291, 22);
            this.txtSoDienThoai.TabIndex = 3;
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(196, 165);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Size = new System.Drawing.Size(291, 22);
            this.txtMatKhau.TabIndex = 4;
            // 
            // txtMatKhauLai
            // 
            this.txtMatKhauLai.Location = new System.Drawing.Point(196, 210);
            this.txtMatKhauLai.Name = "txtMatKhauLai";
            this.txtMatKhauLai.PasswordChar = '*';
            this.txtMatKhauLai.Size = new System.Drawing.Size(291, 22);
            this.txtMatKhauLai.TabIndex = 5;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(196, 248);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(291, 22);
            this.txtKey.TabIndex = 6;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(67, 9);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(55, 16);
            this.lblHoTen.TabIndex = 7;
            this.lblHoTen.Text = "Họ Tên:";
            // 
            // lblTenDN
            // 
            this.lblTenDN.AutoSize = true;
            this.lblTenDN.Location = new System.Drawing.Point(67, 47);
            this.lblTenDN.Name = "lblTenDN";
            this.lblTenDN.Size = new System.Drawing.Size(105, 16);
            this.lblTenDN.TabIndex = 8;
            this.lblTenDN.Text = "Tên Đăng Nhập:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(67, 83);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(44, 16);
            this.lblEmail.TabIndex = 9;
            this.lblEmail.Text = "Email:";
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Location = new System.Drawing.Point(67, 125);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(89, 16);
            this.lblSoDienThoai.TabIndex = 10;
            this.lblSoDienThoai.Text = "Số Điện thoại:";
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Location = new System.Drawing.Point(67, 171);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(65, 16);
            this.lblMatKhau.TabIndex = 11;
            this.lblMatKhau.Text = "Mật Khẩu:";
            // 
            // lblMatKhauLai
            // 
            this.lblMatKhauLai.AutoSize = true;
            this.lblMatKhauLai.Location = new System.Drawing.Point(67, 216);
            this.lblMatKhauLai.Name = "lblMatKhauLai";
            this.lblMatKhauLai.Size = new System.Drawing.Size(86, 16);
            this.lblMatKhauLai.TabIndex = 12;
            this.lblMatKhauLai.Text = "Nhập Lại MK:";
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(67, 254);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(33, 16);
            this.lblKey.TabIndex = 13;
            this.lblKey.Text = "Key:";
            // 
            // btnDangKy
            // 
            this.btnDangKy.Location = new System.Drawing.Point(196, 288);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(100, 28);
            this.btnDangKy.TabIndex = 14;
            this.btnDangKy.Text = "Đăng Ký";
            this.btnDangKy.UseVisualStyleBackColor = true;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(365, 288);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(100, 28);
            this.btnThoat.TabIndex = 15;
            this.btnThoat.Text = "Đăng Nhập";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click_1);
            // 
            // DangKy
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(638, 339);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.txtTenDN);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.txtMatKhauLai);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.lblHoTen);
            this.Controls.Add(this.lblTenDN);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblSoDienThoai);
            this.Controls.Add(this.lblMatKhau);
            this.Controls.Add(this.lblMatKhauLai);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.btnThoat);
            this.Name = "DangKy";
            this.Text = "Đăng Ký";
            this.Load += new System.EventHandler(this.DangKy_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.TextBox txtTenDN;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSoDienThoai;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.TextBox txtMatKhauLai;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.Label lblTenDN;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblSoDienThoai;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.Label lblMatKhauLai;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Button btnThoat;
    }
}
