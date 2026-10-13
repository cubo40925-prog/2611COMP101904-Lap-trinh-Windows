namespace ShopManagerMini.Forms;

partial class FrmMain
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
        components = new System.ComponentModel.Container();
        menuStrip1 = new MenuStrip();
        mnuHeThong = new ToolStripMenuItem();
        mnuDangXuat = new ToolStripMenuItem();
        toolStripSeparator1 = new ToolStripSeparator();
        mnuThoat = new ToolStripMenuItem();
        mnuDanhMuc = new ToolStripMenuItem();
        mnuQuanLySanPham = new ToolStripMenuItem();
        mnuQuanLyKhachHang = new ToolStripMenuItem();
        mnuTroGiup = new ToolStripMenuItem();
        mnuGioiThieu = new ToolStripMenuItem();
        statusStrip1 = new StatusStrip();
        lblApp = new ToolStripStatusLabel();
        lblUser = new ToolStripStatusLabel();
        lblRole = new ToolStripStatusLabel();
        lblTime = new ToolStripStatusLabel();
        timerClock = new System.Windows.Forms.Timer(components);
        menuStrip1.SuspendLayout();
        statusStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { mnuHeThong, mnuDanhMuc, mnuTroGiup });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(1100, 24);
        menuStrip1.TabIndex = 0;
        // 
        // mnuHeThong
        // 
        mnuHeThong.DropDownItems.AddRange(new ToolStripItem[] { mnuDangXuat, toolStripSeparator1, mnuThoat });
        mnuHeThong.Name = "mnuHeThong";
        mnuHeThong.Text = "Hệ thống";
        // 
        // mnuDangXuat
        // 
        mnuDangXuat.Name = "mnuDangXuat";
        mnuDangXuat.Text = "Đăng xuất";
        mnuDangXuat.Click += mnuDangXuat_Click;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        // 
        // mnuThoat
        // 
        mnuThoat.Name = "mnuThoat";
        mnuThoat.ShortcutKeys = Keys.Alt | Keys.F4;
        mnuThoat.Text = "Thoát";
        mnuThoat.Click += mnuThoat_Click;
        // 
        // mnuDanhMuc
        // 
        mnuDanhMuc.DropDownItems.AddRange(new ToolStripItem[] { mnuQuanLySanPham, mnuQuanLyKhachHang });
        mnuDanhMuc.Name = "mnuDanhMuc";
        mnuDanhMuc.Text = "Danh mục";
        // 
        // mnuQuanLySanPham
        // 
        mnuQuanLySanPham.Name = "mnuQuanLySanPham";
        mnuQuanLySanPham.ShortcutKeys = Keys.Control | Keys.P;
        mnuQuanLySanPham.Text = "Quản lý sản phẩm";
        mnuQuanLySanPham.Click += mnuQuanLySanPham_Click;
        // 
        // mnuQuanLyKhachHang
        // 
        mnuQuanLyKhachHang.Name = "mnuQuanLyKhachHang";
        mnuQuanLyKhachHang.ShortcutKeys = Keys.Control | Keys.K;
        mnuQuanLyKhachHang.Text = "Quản lý khách hàng";
        mnuQuanLyKhachHang.Click += mnuQuanLyKhachHang_Click;
        // 
        // mnuTroGiup
        // 
        mnuTroGiup.DropDownItems.AddRange(new ToolStripItem[] { mnuGioiThieu });
        mnuTroGiup.Name = "mnuTroGiup";
        mnuTroGiup.Text = "Trợ giúp";
        // 
        // mnuGioiThieu
        // 
        mnuGioiThieu.Name = "mnuGioiThieu";
        mnuGioiThieu.ShortcutKeys = Keys.F1;
        mnuGioiThieu.Text = "Giới thiệu";
        mnuGioiThieu.Click += mnuGioiThieu_Click;
        // 
        // statusStrip1
        // 
        statusStrip1.Items.AddRange(new ToolStripItem[] { lblApp, lblUser, lblRole, lblTime });
        statusStrip1.Location = new Point(0, 678);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(1100, 22);
        statusStrip1.TabIndex = 1;
        // 
        // lblApp
        // 
        lblApp.Name = "lblApp";
        lblApp.Size = new Size(500, 17);
        lblApp.Spring = true;
        lblApp.Text = "ShopManagerMini";
        lblApp.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblUser
        // 
        lblUser.BorderSides = ToolStripStatusLabelBorderSides.Left;
        lblUser.Name = "lblUser";
        lblUser.Size = new Size(150, 17);
        lblUser.Text = "Người dùng:";
        // 
        // lblRole
        // 
        lblRole.BorderSides = ToolStripStatusLabelBorderSides.Left;
        lblRole.Name = "lblRole";
        lblRole.Size = new Size(100, 17);
        lblRole.Text = "Vai trò:";
        // 
        // lblTime
        // 
        lblTime.BorderSides = ToolStripStatusLabelBorderSides.Left;
        lblTime.Name = "lblTime";
        lblTime.Size = new Size(130, 17);
        lblTime.Text = "00/00/0000 00:00:00";
        // 
        // timerClock
        // 
        timerClock.Interval = 1000;
        timerClock.Tick += timerClock_Tick;
        // 
        // FrmMain
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 700);
        Controls.Add(statusStrip1);
        Controls.Add(menuStrip1);
        IsMdiContainer = true;
        MainMenuStrip = menuStrip1;
        Name = "FrmMain";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "ShopManagerMini";
        WindowState = FormWindowState.Maximized;
        FormClosing += FrmMain_FormClosing;
        Load += FrmMain_Load;
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1 = null!;
    private ToolStripMenuItem mnuHeThong = null!;
    private ToolStripMenuItem mnuDangXuat = null!;
    private ToolStripSeparator toolStripSeparator1 = null!;
    private ToolStripMenuItem mnuThoat = null!;
    private ToolStripMenuItem mnuDanhMuc = null!;
    private ToolStripMenuItem mnuQuanLySanPham = null!;
    private ToolStripMenuItem mnuQuanLyKhachHang = null!;
    private ToolStripMenuItem mnuTroGiup = null!;
    private ToolStripMenuItem mnuGioiThieu = null!;
    private StatusStrip statusStrip1 = null!;
    private ToolStripStatusLabel lblApp = null!;
    private ToolStripStatusLabel lblUser = null!;
    private ToolStripStatusLabel lblRole = null!;
    private ToolStripStatusLabel lblTime = null!;
    private System.Windows.Forms.Timer timerClock = null!;
}
