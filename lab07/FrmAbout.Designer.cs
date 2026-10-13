namespace ShopManagerMini.Forms;

partial class FrmAbout
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
        lblTenApp = new Label();
        lblPhienBan = new Label();
        lblMoTa = new Label();
        lblSinhVien = new Label();
        lblLop = new Label();
        btnDong = new Button();
        SuspendLayout();
        // 
        // lblTenApp
        // 
        lblTenApp.Dock = DockStyle.Top;
        lblTenApp.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTenApp.Location = new Point(0, 0);
        lblTenApp.Name = "lblTenApp";
        lblTenApp.Size = new Size(420, 60);
        lblTenApp.Text = "ShopManagerMini";
        lblTenApp.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblPhienBan
        // 
        lblPhienBan.Location = new Point(30, 65);
        lblPhienBan.Name = "lblPhienBan";
        lblPhienBan.Size = new Size(360, 23);
        lblPhienBan.Text = "Phiên bản: 1.0";
        // 
        // lblMoTa
        // 
        lblMoTa.Location = new Point(30, 95);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(360, 45);
        lblMoTa.Text = "Ứng dụng quản lý cửa hàng mini nhiều Form (SDI + MDI), dữ liệu lưu trong bộ nhớ.";
        // 
        // lblSinhVien
        // 
        lblSinhVien.Location = new Point(30, 150);
        lblSinhVien.Name = "lblSinhVien";
        lblSinhVien.Size = new Size(360, 23);
        lblSinhVien.Text = "Sinh viên:";
        // 
        // lblLop
        // 
        lblLop.Location = new Point(30, 178);
        lblLop.Name = "lblLop";
        lblLop.Size = new Size(360, 23);
        lblLop.Text = "Lớp:";
        // 
        // btnDong
        // 
        btnDong.DialogResult = DialogResult.OK;
        btnDong.Location = new Point(155, 225);
        btnDong.Name = "btnDong";
        btnDong.Size = new Size(110, 34);
        btnDong.TabIndex = 0;
        btnDong.Text = "Đóng";
        btnDong.UseVisualStyleBackColor = true;
        // 
        // FrmAbout
        // 
        AcceptButton = btnDong;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnDong;
        ClientSize = new Size(420, 280);
        Controls.Add(btnDong);
        Controls.Add(lblLop);
        Controls.Add(lblSinhVien);
        Controls.Add(lblMoTa);
        Controls.Add(lblPhienBan);
        Controls.Add(lblTenApp);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmAbout";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Giới thiệu";
        ResumeLayout(false);
    }

    #endregion

    private Label lblTenApp = null!;
    private Label lblPhienBan = null!;
    private Label lblMoTa = null!;
    private Label lblSinhVien = null!;
    private Label lblLop = null!;
    private Button btnDong = null!;
}
