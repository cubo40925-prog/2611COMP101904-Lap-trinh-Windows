namespace StudentCardPrinter;

partial class FrmStudentCard
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
        grbInfo = new GroupBox();
        lblStudentId = new Label();
        txtStudentId = new TextBox();
        lblFullName = new Label();
        txtFullName = new TextBox();
        lblClass = new Label();
        cboClass = new ComboBox();
        lblFaculty = new Label();
        cboFaculty = new ComboBox();
        lblCourse = new Label();
        txtCourse = new TextBox();
        lblBirthDate = new Label();
        dtpBirthDate = new DateTimePicker();
        lblGender = new Label();
        cboGender = new ComboBox();
        lblAvatar = new Label();
        picAvatar = new PictureBox();
        btnChoosePhoto = new Button();
        lblPhotoHint = new Label();
        btnClear = new Button();
        btnPreviewPrint = new Button();
        btnPrint = new Button();
        btnExit = new Button();
        grbPreview = new GroupBox();
        pnlCardPreview = new BufferedPanel();
        btnCardColor = new Button();
        btnSavePng = new Button();
        printDocument1 = new System.Drawing.Printing.PrintDocument();
        printPreviewDialog1 = new PrintPreviewDialog();
        printDialog1 = new PrintDialog();
        grbInfo.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
        grbPreview.SuspendLayout();
        SuspendLayout();
        // 
        // grbInfo
        // 
        grbInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        grbInfo.Controls.Add(lblStudentId);
        grbInfo.Controls.Add(txtStudentId);
        grbInfo.Controls.Add(lblFullName);
        grbInfo.Controls.Add(txtFullName);
        grbInfo.Controls.Add(lblClass);
        grbInfo.Controls.Add(cboClass);
        grbInfo.Controls.Add(lblFaculty);
        grbInfo.Controls.Add(cboFaculty);
        grbInfo.Controls.Add(lblCourse);
        grbInfo.Controls.Add(txtCourse);
        grbInfo.Controls.Add(lblBirthDate);
        grbInfo.Controls.Add(dtpBirthDate);
        grbInfo.Controls.Add(lblGender);
        grbInfo.Controls.Add(cboGender);
        grbInfo.Controls.Add(lblAvatar);
        grbInfo.Controls.Add(picAvatar);
        grbInfo.Controls.Add(btnChoosePhoto);
        grbInfo.Controls.Add(lblPhotoHint);
        grbInfo.Location = new Point(12, 12);
        grbInfo.Name = "grbInfo";
        grbInfo.Size = new Size(450, 480);
        grbInfo.TabIndex = 0;
        grbInfo.TabStop = false;
        grbInfo.Text = "Thông tin sinh viên";
        // 
        // lblStudentId
        // 
        lblStudentId.AutoSize = true;
        lblStudentId.Location = new Point(15, 38);
        lblStudentId.Name = "lblStudentId";
        lblStudentId.Text = "Mã sinh viên (*):";
        // 
        // txtStudentId
        // 
        txtStudentId.Location = new Point(125, 35);
        txtStudentId.Name = "txtStudentId";
        txtStudentId.Size = new Size(300, 23);
        txtStudentId.TabIndex = 0;
        // 
        // lblFullName
        // 
        lblFullName.AutoSize = true;
        lblFullName.Location = new Point(15, 73);
        lblFullName.Name = "lblFullName";
        lblFullName.Text = "Họ tên (*):";
        // 
        // txtFullName
        // 
        txtFullName.Location = new Point(125, 70);
        txtFullName.Name = "txtFullName";
        txtFullName.Size = new Size(300, 23);
        txtFullName.TabIndex = 1;
        // 
        // lblClass
        // 
        lblClass.AutoSize = true;
        lblClass.Location = new Point(15, 108);
        lblClass.Name = "lblClass";
        lblClass.Text = "Lớp:";
        // 
        // cboClass
        // 
        cboClass.Location = new Point(125, 105);
        cboClass.Name = "cboClass";
        cboClass.Size = new Size(300, 23);
        cboClass.TabIndex = 2;
        // 
        // lblFaculty
        // 
        lblFaculty.AutoSize = true;
        lblFaculty.Location = new Point(15, 143);
        lblFaculty.Name = "lblFaculty";
        lblFaculty.Text = "Khoa:";
        // 
        // cboFaculty
        // 
        cboFaculty.Location = new Point(125, 140);
        cboFaculty.Name = "cboFaculty";
        cboFaculty.Size = new Size(300, 23);
        cboFaculty.TabIndex = 3;
        // 
        // lblCourse
        // 
        lblCourse.AutoSize = true;
        lblCourse.Location = new Point(15, 178);
        lblCourse.Name = "lblCourse";
        lblCourse.Text = "Khóa học:";
        // 
        // txtCourse
        // 
        txtCourse.Location = new Point(125, 175);
        txtCourse.Name = "txtCourse";
        txtCourse.PlaceholderText = "VD: K49";
        txtCourse.Size = new Size(140, 23);
        txtCourse.TabIndex = 4;
        // 
        // lblBirthDate
        // 
        lblBirthDate.AutoSize = true;
        lblBirthDate.Location = new Point(15, 213);
        lblBirthDate.Name = "lblBirthDate";
        lblBirthDate.Text = "Ngày sinh:";
        // 
        // dtpBirthDate
        // 
        dtpBirthDate.Format = DateTimePickerFormat.Short;
        dtpBirthDate.Location = new Point(125, 210);
        dtpBirthDate.Name = "dtpBirthDate";
        dtpBirthDate.Size = new Size(140, 23);
        dtpBirthDate.TabIndex = 5;
        // 
        // lblGender
        // 
        lblGender.AutoSize = true;
        lblGender.Location = new Point(15, 248);
        lblGender.Name = "lblGender";
        lblGender.Text = "Giới tính:";
        // 
        // cboGender
        // 
        cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
        cboGender.Location = new Point(125, 245);
        cboGender.Name = "cboGender";
        cboGender.Size = new Size(140, 23);
        cboGender.TabIndex = 6;
        // 
        // lblAvatar
        // 
        lblAvatar.AutoSize = true;
        lblAvatar.Location = new Point(15, 293);
        lblAvatar.Name = "lblAvatar";
        lblAvatar.Text = "Ảnh đại diện (*):";
        // 
        // picAvatar
        // 
        picAvatar.BackColor = Color.White;
        picAvatar.BorderStyle = BorderStyle.FixedSingle;
        picAvatar.Location = new Point(125, 290);
        picAvatar.Name = "picAvatar";
        picAvatar.Size = new Size(130, 170);
        picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
        picAvatar.TabIndex = 8;
        picAvatar.TabStop = false;
        // 
        // btnChoosePhoto
        // 
        btnChoosePhoto.Location = new Point(270, 290);
        btnChoosePhoto.Name = "btnChoosePhoto";
        btnChoosePhoto.Size = new Size(155, 34);
        btnChoosePhoto.TabIndex = 7;
        btnChoosePhoto.Text = "Chọn ảnh...";
        btnChoosePhoto.UseVisualStyleBackColor = true;
        // 
        // lblPhotoHint
        // 
        lblPhotoHint.ForeColor = SystemColors.GrayText;
        lblPhotoHint.Location = new Point(270, 330);
        lblPhotoHint.Name = "lblPhotoHint";
        lblPhotoHint.Size = new Size(160, 60);
        lblPhotoHint.Text = "Chấp nhận JPG, JPEG, PNG, BMP. Ảnh giữ nguyên tỉ lệ, không bị méo.";
        // 
        // btnClear
        // 
        btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnClear.Location = new Point(12, 505);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(105, 38);
        btnClear.TabIndex = 1;
        btnClear.Text = "Làm mới";
        btnClear.UseVisualStyleBackColor = true;
        // 
        // btnPreviewPrint
        // 
        btnPreviewPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnPreviewPrint.Location = new Point(124, 505);
        btnPreviewPrint.Name = "btnPreviewPrint";
        btnPreviewPrint.Size = new Size(105, 38);
        btnPreviewPrint.TabIndex = 2;
        btnPreviewPrint.Text = "Xem trước in";
        btnPreviewPrint.UseVisualStyleBackColor = true;
        // 
        // btnPrint
        // 
        btnPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnPrint.Location = new Point(236, 505);
        btnPrint.Name = "btnPrint";
        btnPrint.Size = new Size(105, 38);
        btnPrint.TabIndex = 3;
        btnPrint.Text = "In";
        btnPrint.UseVisualStyleBackColor = true;
        // 
        // btnExit
        // 
        btnExit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnExit.Location = new Point(348, 505);
        btnExit.Name = "btnExit";
        btnExit.Size = new Size(105, 38);
        btnExit.TabIndex = 4;
        btnExit.Text = "Thoát";
        btnExit.UseVisualStyleBackColor = true;
        // 
        // grbPreview
        // 
        grbPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grbPreview.Controls.Add(pnlCardPreview);
        grbPreview.Location = new Point(474, 12);
        grbPreview.Name = "grbPreview";
        grbPreview.Padding = new Padding(8);
        grbPreview.Size = new Size(594, 480);
        grbPreview.TabIndex = 5;
        grbPreview.TabStop = false;
        grbPreview.Text = "Xem trước thẻ sinh viên";
        // 
        // pnlCardPreview
        // 
        pnlCardPreview.BackColor = Color.Gainsboro;
        pnlCardPreview.Dock = DockStyle.Fill;
        pnlCardPreview.Location = new Point(8, 24);
        pnlCardPreview.Name = "pnlCardPreview";
        pnlCardPreview.Size = new Size(578, 448);
        pnlCardPreview.TabIndex = 0;
        // 
        // btnCardColor
        // 
        btnCardColor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnCardColor.Location = new Point(474, 505);
        btnCardColor.Name = "btnCardColor";
        btnCardColor.Size = new Size(120, 38);
        btnCardColor.TabIndex = 6;
        btnCardColor.Text = "Màu nền thẻ";
        btnCardColor.UseVisualStyleBackColor = true;
        // 
        // btnSavePng
        // 
        btnSavePng.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnSavePng.Location = new Point(602, 505);
        btnSavePng.Name = "btnSavePng";
        btnSavePng.Size = new Size(120, 38);
        btnSavePng.TabIndex = 7;
        btnSavePng.Text = "Lưu PNG";
        btnSavePng.UseVisualStyleBackColor = true;
        // 
        // printDocument1
        // 
        printDocument1.DocumentName = "The sinh vien";
        // 
        // printPreviewDialog1
        // 
        printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
        printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
        printPreviewDialog1.ClientSize = new Size(400, 300);
        printPreviewDialog1.Document = printDocument1;
        printPreviewDialog1.Enabled = true;
        printPreviewDialog1.Name = "printPreviewDialog1";
        printPreviewDialog1.UseAntiAlias = true;
        printPreviewDialog1.Visible = false;
        // 
        // printDialog1
        // 
        printDialog1.Document = printDocument1;
        printDialog1.UseEXDialog = true;
        // 
        // FrmStudentCard
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1080, 560);
        Controls.Add(btnSavePng);
        Controls.Add(btnCardColor);
        Controls.Add(grbPreview);
        Controls.Add(btnExit);
        Controls.Add(btnPrint);
        Controls.Add(btnPreviewPrint);
        Controls.Add(btnClear);
        Controls.Add(grbInfo);
        MinimumSize = new Size(1000, 600);
        Name = "FrmStudentCard";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Thẻ sinh viên - COMP1019 Lab 09";
        grbInfo.ResumeLayout(false);
        grbInfo.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
        grbPreview.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private GroupBox grbInfo = null!;
    private Label lblStudentId = null!;
    private TextBox txtStudentId = null!;
    private Label lblFullName = null!;
    private TextBox txtFullName = null!;
    private Label lblClass = null!;
    private ComboBox cboClass = null!;
    private Label lblFaculty = null!;
    private ComboBox cboFaculty = null!;
    private Label lblCourse = null!;
    private TextBox txtCourse = null!;
    private Label lblBirthDate = null!;
    private DateTimePicker dtpBirthDate = null!;
    private Label lblGender = null!;
    private ComboBox cboGender = null!;
    private Label lblAvatar = null!;
    private PictureBox picAvatar = null!;
    private Button btnChoosePhoto = null!;
    private Label lblPhotoHint = null!;
    private Button btnClear = null!;
    private Button btnPreviewPrint = null!;
    private Button btnPrint = null!;
    private Button btnExit = null!;
    private GroupBox grbPreview = null!;
    private BufferedPanel pnlCardPreview = null!;
    private Button btnCardColor = null!;
    private Button btnSavePng = null!;
    private System.Drawing.Printing.PrintDocument printDocument1 = null!;
    private PrintPreviewDialog printPreviewDialog1 = null!;
    private PrintDialog printDialog1 = null!;
}
