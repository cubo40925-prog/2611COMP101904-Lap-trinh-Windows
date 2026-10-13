namespace ShopManagerMini.Forms;

partial class FrmLogin
{
    private System.ComponentModel.IContainer? components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        lblTieuDe = new Label();
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblPassword = new Label();
        txtPassword = new TextBox();
        btnDangNhap = new Button();
        btnThoat = new Button();
        lblGoiY = new Label();
        SuspendLayout();
        // 
        // lblTieuDe
        // 
        lblTieuDe.Dock = DockStyle.Top;
        lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblTieuDe.Location = new Point(0, 0);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(380, 55);
        lblTieuDe.Text = "ĐĂNG NHẬP";
        lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblUsername
        // 
        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(30, 78);
        lblUsername.Name = "lblUsername";
        lblUsername.Text = "Tài khoản:";
        // 
        // txtUsername
        // 
        txtUsername.Location = new Point(120, 75);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(220, 23);
        txtUsername.TabIndex = 0;
        // 
        // lblPassword
        // 
        lblPassword.AutoSize = true;
        lblPassword.Location = new Point(30, 118);
        lblPassword.Name = "lblPassword";
        lblPassword.Text = "Mật khẩu:";
        // 
        // txtPassword
        // 
        txtPassword.Location = new Point(120, 115);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(220, 23);
        txtPassword.TabIndex = 1;
        txtPassword.UseSystemPasswordChar = true;
        // 
        // btnDangNhap
        // 
        btnDangNhap.Location = new Point(120, 160);
        btnDangNhap.Name = "btnDangNhap";
        btnDangNhap.Size = new Size(105, 34);
        btnDangNhap.TabIndex = 2;
        btnDangNhap.Text = "Đăng nhập";
        btnDangNhap.UseVisualStyleBackColor = true;
        btnDangNhap.Click += btnDangNhap_Click;
        // 
        // btnThoat
        // 
        btnThoat.DialogResult = DialogResult.Cancel;
        btnThoat.Location = new Point(235, 160);
        btnThoat.Name = "btnThoat";
        btnThoat.Size = new Size(105, 34);
        btnThoat.TabIndex = 3;
        btnThoat.Text = "Thoát";
        btnThoat.UseVisualStyleBackColor = true;
        // 
        // lblGoiY
        // 
        lblGoiY.AutoSize = true;
        lblGoiY.ForeColor = SystemColors.GrayText;
        lblGoiY.Location = new Point(30, 215);
        lblGoiY.Name = "lblGoiY";
        lblGoiY.Text = "Tài khoản mẫu: admin / 123  hoặc  user / 123";
        // 
        // FrmLogin
        // 
        AcceptButton = btnDangNhap;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnThoat;
        ClientSize = new Size(380, 250);
        Controls.Add(lblGoiY);
        Controls.Add(btnThoat);
        Controls.Add(btnDangNhap);
        Controls.Add(txtPassword);
        Controls.Add(lblPassword);
        Controls.Add(txtUsername);
        Controls.Add(lblUsername);
        Controls.Add(lblTieuDe);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmLogin";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Đăng nhập - ShopManagerMini";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblTieuDe = null!;
    private Label lblUsername = null!;
    private TextBox txtUsername = null!;
    private Label lblPassword = null!;
    private TextBox txtPassword = null!;
    private Button btnDangNhap = null!;
    private Button btnThoat = null!;
    private Label lblGoiY = null!;
}
