using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DAO;
using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE
{
    public partial class frmTaiKhoan : Form
    {
        public string TenTaiKhoan = frmMain.TenTaiKhoan;

        BindingSource HeThong = new BindingSource();

        public frmTaiKhoan()
        {
            InitializeComponent();
            LoadData();
        }

        #region Methods

        private void LoadData()
        {
            LoadTaiKhoan();

            LoadHeThong();

            LoadXTM();

            setQuyen();


        }

        private void LoadXTM()
        {
            pnl2.Visible = false;
            pnl1.Visible = true;
            txtUserName.Clear();
            txtPasswd.Clear();
            btnLayKhuonMat.Enabled = false;
            btnLuuAXT.Enabled = false;
        }

        private void setQuyen()
        {
            TaiKhoan qUser = TaiKhoanDAO.Instance.GetTaiKhoanByTenTaiKhoan(TenTaiKhoan);
            if (qUser.Idquyen > 3)
            {
                tabTaiKhoan.TabPages.Remove(tabHeThong);
            }
        }

        public void LoadTaiKhoan()
        {
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnCapNhat.Enabled = true;

            TaiKhoan tk = TaiKhoanDAO.Instance.GetTaiKhoanByTenTaiKhoan(TenTaiKhoan);
            QuyenDTO q = QuyenDAO.Instance.GetQuyenByID(tk.Idquyen);
            //string hasPass = DataProvider.Instance.GiaiMa(tk.Matkhau);
            txtTenTKhoan.Text = tk.Tentaikhoan;
            txtMKhau.Text = tk.Matkhau;
            txtHTen.Text = tk.Hoten;
            txtTHienThi.Text = tk.Tenhienthi;
            txtSDienThoai.Text = tk.Sodienthoai;
            txtEmailofUser.Text = tk.Email;
            txtDChi.Text = tk.Diachi;
            if (q.Quyen == "SupperAdmin")
            {
                txtQuyenUser.Text = "";
            }
            else
            {
                txtQuyenUser.Text = q.Quyen;
            }
            grpThongTinTK.Text = "Thông tin cái nhân";

            ReadOnlyControlTaiKhoan(true);

            if (txtTKHoTen.Text.Length > 0)
            {
                errorRong.Clear();
            }
            this.txtTenTKhoan.Focus();
        }

        private void ReadOnlyControlTaiKhoan(Boolean e)
        {
            txtHTen.ReadOnly = e;
            txtTHienThi.ReadOnly = e;
            txtSDienThoai.ReadOnly = e;
            txtEmailofUser.ReadOnly = e;
            txtDChi.ReadOnly = e;
            txtQuyenUser.ReadOnly = e;
        }

        private Boolean CheckThongtTinCapNhat()
        {
            Boolean check = true;

            if (txtHTen.Text.Trim() == "")
            {
                errorRong.SetError(txtHTen, "Họ tên của bạn không được trống!!!");
                check = false;
                return check;
            }
            else
            {
                check = true;
                errorRong.Clear();
            }
            return check;
        }

        private void LoadHeThong()
        {
            dgvShowTaiKhoan.DataSource = HeThong;

            LoadDanhSachTaiKhoan();
            BindingHeThong();
            LoadComboBoxQuyen(cboQuyen);
            LoadComboBoxQuyen(cboTKQuyen);

            EnableControl(false);
            btnTimKiem.Enabled = true;
            btnThem.Enabled = true;
            EnableTextBox(false);
            cboTKQuyen.Enabled = true;
            txtTKHoTen.Enabled = true;
            lblCanhBao.Text = "";
            grpThongTin.Text = "Thông tin";
            dgvShowTaiKhoan.Enabled = true;
        }

        private void LoadComboBoxQuyen(ComboBox cbo)
        {
            try
            {
                cbo.DataSource = QuyenDAO.Instance.GetListQuyen();
                cbo.DisplayMember = "QUYEN";

                string quyen = dgvShowTaiKhoan.SelectedCells[0].OwningRow.Cells["Quyền"].Value.ToString();
                cboQuyen.Text = quyen;
            }
            catch { }
        }

        private void LoadDanhSachTaiKhoan()
        {
            HeThong.DataSource = TaiKhoanDAO.Instance.GetTaiKhoan();
            dgvShowTaiKhoan.Columns["ID Tài Khoản"].Visible = false;
            dgvShowTaiKhoan.Columns["Mật Khẩu"].Visible = false;
            dgvShowTaiKhoan.Columns["ID Quyền"].Visible = false;
        }

        private void BindingHeThong()
        {
            try
            {
                txtTenTaiKhoan.DataBindings.Add(new Binding("Text", dgvShowTaiKhoan.DataSource, "Tên Tài Khoản", true, DataSourceUpdateMode.Never));
                txtMatKhau.DataBindings.Add(new Binding("Text", dgvShowTaiKhoan.DataSource, "Mật Khẩu", true, DataSourceUpdateMode.Never));
                txtHoTenTK.DataBindings.Add(new Binding("Text", dgvShowTaiKhoan.DataSource, "Họ Tên", true, DataSourceUpdateMode.Never));
                txtTenHienThi.DataBindings.Add(new Binding("Text", dgvShowTaiKhoan.DataSource, "Tên Hiển Thị", true, DataSourceUpdateMode.Never));
                txtSoDienThoai.DataBindings.Add(new Binding("Text", dgvShowTaiKhoan.DataSource, "Số Điện Thoại", true, DataSourceUpdateMode.Never));
                txtEmail.DataBindings.Add(new Binding("Text", dgvShowTaiKhoan.DataSource, "Email", true, DataSourceUpdateMode.Never));
                txtDiaChi.DataBindings.Add(new Binding("Text", dgvShowTaiKhoan.DataSource, "Địa Chỉ", true, DataSourceUpdateMode.Never));
            }
            catch { }
        }

        private void EnableControl(Boolean e)
        {
            btnThem.Enabled = e;
            btnCapNhatUser.Enabled = e;
            btnXoa.Enabled = e;
            btnLuuHeThong.Enabled = e;
            btnTimKiem.Enabled = e;
            btnBQua.Enabled = e;
        }

        private void EnableTextBox(Boolean e)
        {
            txtTKHoTen.Enabled = e;
            cboTKQuyen.Enabled = e;
            cboQuyen.Enabled = e;
            txtTenTaiKhoan.Enabled = e;
            txtMatKhau.Enabled = e;
            txtHoTenTK.Enabled = e;
            txtTenHienThi.Enabled = e;
            txtSoDienThoai.Enabled = e;
            txtEmail.Enabled = e;
            txtDiaChi.Enabled = e;
        }

        private void ClearTextBox()
        {
            txtTKHoTen.Clear();
            txtTenTaiKhoan.Clear();
            txtMatKhau.Clear();
            txtHoTenTK.Clear();
            txtTenHienThi.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
        }

        private Boolean checkDuLieuVao()
        {
            Boolean check = true;

            if (txtTenTaiKhoan.Text.Trim() == "")
            {
                errorRong.SetError(txtTenTaiKhoan, "Dữ liệu không được trống");
                txtTenTaiKhoan.Focus();
                check = false;
                return check;
            }
            else { errorRong.Clear(); }

            if (txtMatKhau.Text.Trim() == "")
            {
                errorRong.SetError(txtMatKhau, "Dữ liệu không được trống");
                txtMatKhau.Focus();
                check = false;
                return check;
            }
            else { errorRong.Clear(); }

            if (txtHoTenTK.Text.Trim() == "")
            {
                errorRong.SetError(txtHoTenTK, "Dữ liệu không được trống");
                txtHoTenTK.Focus();
                check = false;
                return check;
            }
            else { errorRong.Clear(); }

            return check;
        }

        private void themTaiKhoan(string userName, string passWord, string hoTen, string tenHienThi, string soDienThoai, string Email, string diachi, int Quyen)
        {
            if (TaiKhoanDAO.Instance.InsertTaiKhoan(userName, passWord, hoTen, tenHienThi, soDienThoai, Email, diachi, Quyen))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản không thành công.\nVui lòng kiểm tra lại");
            }
        }

        private int checkQuyen(string q)
        {
            int intQuyen = 0;
            int sQuyen = 0;
            int i = 2;
            if (cboQuyen.Items.Count > 0)
            {
                q = cboQuyen.Text;
                QuyenDTO quyen = QuyenDAO.Instance.GetIdByQuyen(q);

                foreach (QuyenDTO item in cboQuyen.Items)
                {
                    if (item.Quyen == quyen.Quyen)
                    {
                        sQuyen = i;
                        break;
                    }
                    i++;
                }
            }
            intQuyen = sQuyen;
            return intQuyen;
        }

        private int checkQuyentk(string q)
        {
            int intQuyen = 0;
            int sQuyen = 0;
            int i = 2;
            if (cboQuyen.Items.Count > 0)
            {
                q = cboTKQuyen.Text;
                QuyenDTO quyen = QuyenDAO.Instance.GetIdByQuyen(q);

                foreach (QuyenDTO item in cboQuyen.Items)
                {
                    if (item.Quyen == quyen.Quyen)
                    {
                        sQuyen = i;
                        break;
                    }
                    i++;
                }
            }
            intQuyen = sQuyen;
            return intQuyen;
        }

        private Boolean checkKTDB(string chuoicankiemtra)
        {
            string db = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            string chuoidung = db;
            foreach (char kitu1 in chuoicankiemtra)
            {
                bool check = false;
                foreach (char kitu2 in chuoidung)
                {
                    if (kitu1 == kitu2) check = true;
                }
                if (check == false) return false;
            }
            return true;
        }

        private Boolean checkKTHoa(string chuoicankiemtra)
        {
            string hoa = "1234567890qwertyuiopasdfghjklzxcvbnm~!@#$%^&*)(_+{}|:" + "<>?`-=[];',./âăôơ " + "\"";
            string chuoidung = hoa;
            foreach (char kitu1 in chuoicankiemtra)
            {
                bool check = false;
                foreach (char kitu2 in chuoidung)
                {
                    if (kitu1 == kitu2) check = true;
                }
                if (check == false) return false;
            }
            return true;
        }

        private Boolean checkKTSo(string chuoicankiemtra)
        {
            string so = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM~!@#$%^&*)(_+{}|:" + "<>?`-=[];',./âăôơ " + "\"";
            string chuoidung = so;
            foreach (char kitu1 in chuoicankiemtra)
            {
                bool check = false;
                foreach (char kitu2 in chuoidung)
                {
                    if (kitu1 == kitu2) check = true;
                }
                if (check == false) return false;
            }
            return true;
        }

        private Boolean checkKTThuong(string chuoicankiemtra)
        {
            string ct = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM~!@#$%^&*)(_+{}|:" + "<>?`-=[];',./âăôơ " + "\"";
            string chuoidung = ct;
            foreach (char kitu1 in chuoicankiemtra)
            {
                bool check = false;
                foreach (char kitu2 in chuoidung)
                {
                    if (kitu1 == kitu2) check = true;
                }
                if (check == false) return false;
            }
            return true;
        }

        private void CapNhat(string userName, string passWord, int quyen)
        {
            if (TaiKhoanDAO.Instance.CapNhatMatKhauvsQuyen(userName, passWord, quyen))
            {
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Cập nhậtb không thành công.\nVui lòng kiểm tra lại");
            }
        }

        private void xoaTaiKhoan(string userName)
        {
            if (dgvShowTaiKhoan.SelectedRows.Count < 1)
            {
                MessageBox.Show("Bạn không thể xóa tài khoản cuối cùng của hệ thông", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData();
                return;
            }
            else
            {
                if (TaiKhoanDAO.Instance.XoaTaiKhoan(userName))
                {
                    MessageBox.Show("Xóa tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Xóa tài khoản thất bại");
                }
            }
        }

        private void timKiem(string hoten, int quyen)
        {
            try
            {
                if (hoten != "")
                {
                    HeThong.DataSource = TaiKhoanDAO.Instance.TKCoHoTen(hoten, quyen);
                    dgvShowTaiKhoan.Columns["ID Tài Khoản"].Visible = false;
                    dgvShowTaiKhoan.Columns["Mật Khẩu"].Visible = false;
                    dgvShowTaiKhoan.Columns["ID Quyền"].Visible = false;
                    return;
                }
                else
                {
                    HeThong.DataSource = TaiKhoanDAO.Instance.timKiemQuyen(hoten, quyen);
                    dgvShowTaiKhoan.Columns["ID Tài Khoản"].Visible = false;
                    dgvShowTaiKhoan.Columns["Mật Khẩu"].Visible = false;
                    dgvShowTaiKhoan.Columns["ID Quyền"].Visible = false;
                    return;
                }
            }
            catch { }
        }

        #endregion Medthods

        #region Event

        private void tabTaiKhoan_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (btnLuu.Enabled == true && tabTaiKhoan.SelectedTab == tabHeThong)
            {
                tabTaiKhoan.SelectedTab = tabpThongTinTaiKhoan;
                MessageBox.Show("Bạn không thể chuyển tab khác ngày bây giờ.\nBạn hãy hoàn thành (lưu) hoặc bỏ qua mới có thể chuyển tab được!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (btnLuuHeThong.Enabled == true && tabTaiKhoan.SelectedTab == tabpThongTinTaiKhoan)
            {
                tabTaiKhoan.SelectedTab = tabHeThong;
                MessageBox.Show("Bạn không thể chuyển tab khác ngày bây giờ.\nBạn hãy hoàn thành (lưu) hoặc bỏ qua mới có thể chuyển tab được!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        #region Tài Khoản

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {

            frmDoiMatKhau f = new frmDoiMatKhau();
            f.ShowDialog();
            LoadData();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnDoiMatKhau.Enabled = false;
            this.AcceptButton = btnLuu;
            ReadOnlyControlTaiKhoan(false);
            txtQuyenUser.ReadOnly = true;
            txtHTen.Focus();
            btnCapNhat.Enabled = false;
            grpThongTinTK.Text = "Cập nhật thông tin cái nhân";
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            LoadTaiKhoan();
            btnCapNhat.Enabled = true;
            errorRong.Clear();
        }

        private void CapNhatThongTinTaiKhoan(string tentaikhoan, string hoten, string tenhienthi, string sodienthoai, string email, string diachi)
        {
            TaiKhoan tk = TaiKhoanDAO.Instance.GetTaiKhoanByTenTaiKhoan(tentaikhoan);
            string userName = tk.Tentaikhoan;
            if (TaiKhoanDAO.Instance.CapNhatThongTinTaiKhoan(userName, hoten, tenhienthi, sodienthoai, email, diachi))
            {
                MessageBox.Show("Cập nhật thông tin thành công!!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật!!!/nVui lòng kiểm tra lại thông tin của bạn", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string userName = txtTenTKhoan.Text;
            string ht = txtHTen.Text;
            string tht = txtTHienThi.Text;
            string sdt = txtSDienThoai.Text;
            string email = txtEmailofUser.Text;
            string dc = txtDChi.Text;

            Boolean check = CheckThongtTinCapNhat();

            if (check == true)
            {
                if (txtSDienThoai.Text != "")
                {
                    if (txtSDienThoai.TextLength != 10 && txtSDienThoai.TextLength != 11)
                    {
                        MessageBox.Show("Số điện thoại không hợp lệ", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSDienThoai.Clear();
                        txtSDienThoai.Focus();
                        return;
                    }
                    else
                    {
                        if (btnCapNhat.Enabled == false && btnBoQua.Enabled == true && btnLuu.Enabled == true)
                        {
                            if (TaiKhoanDAO.Instance.GetCountByTenTaiKhoan(userName) > 0)
                            {
                                CapNhatThongTinTaiKhoan(userName, ht, tht, sdt, email, dc);
                                btnLuu.Enabled = false;
                                btnDoiMatKhau.Enabled = true;
                                LoadTaiKhoan();
                            }
                        }
                    }
                }
                else
                {
                    if (btnCapNhat.Enabled == false && btnBoQua.Enabled == true && btnLuu.Enabled == true)
                    {
                        if (TaiKhoanDAO.Instance.GetCountByTenTaiKhoan(userName) > 0)
                        {
                            CapNhatThongTinTaiKhoan(userName, ht, tht, sdt, email, dc);
                            btnLuu.Enabled = false;
                            btnDoiMatKhau.Enabled = true;
                            LoadTaiKhoan();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.\nVui lòng kiểm tra lại!!!", "Lỗi nhập thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHTen.Focus();
                return;
            }
            LoadTaiKhoan();
        }

        #endregion Tài khoản 

        #region Hệ Thống

        private void txtTenTaiKhoan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvShowTaiKhoan.SelectedRows.Count > 0)
                {
                    int id = (int)dgvShowTaiKhoan.SelectedCells[0].OwningRow.Cells["ID Quyền"].Value;
                    QuyenDTO tk = QuyenDAO.Instance.GetQuyenByID(id);
                    cboQuyen.SelectedItem = tk;
                    int index = -1;
                    int i = 0;
                    foreach (QuyenDTO item in cboQuyen.Items)
                    {
                        if (item.Id == tk.Id)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cboQuyen.SelectedIndex = index;
                }
            }
            catch { }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            EnableControl(false);
            EnableTextBox(true);
            btnLuuHeThong.Enabled = true;
            btnBQua.Enabled = true;
            ClearTextBox();
            txtTKHoTen.Enabled = false;
            cboTKQuyen.Enabled = false;
            txtTKHoTen.TabStop = false;
            cboTKQuyen.TabStop = false;
            txtTenTaiKhoan.Focus();
            grpThongTin.Text = "Thêm tài khoản người dùng";
            this.AcceptButton = btnLuuHeThong;
            lblCanhBao.Text = "Mật khẩu của bạn phải từ 6 đến 18 ký tự bao gồm chữ cái hoa, thường, ký tự đặt biệt và số";
            dgvShowTaiKhoan.Enabled = false;
        }

        private void btnBQua_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvShowTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnCapNhatUser.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnCapNhatUser_Click(object sender, EventArgs e)
        {
            string gm = "";
            try
            {
                if (dgvShowTaiKhoan.SelectedRows.Count > 0)
                {
                    string userName = dgvShowTaiKhoan.SelectedCells[0].OwningRow.Cells["Tên Tài Khoản"].Value.ToString();
                    TaiKhoan tk = TaiKhoanDAO.Instance.GetTaiKhoanByTenTaiKhoan(userName);
                    gm = TaiKhoanDAO.Instance.GiaiMa(tk.Matkhau);
                }
            }
            catch { }

            EnableControl(false);
            EnableTextBox(false);
            btnLuuHeThong.Enabled = true;
            btnBQua.Enabled = true;
            txtMatKhau.Enabled = true;
            txtMatKhau.Text = gm;
            txtTKHoTen.Clear();
            cboQuyen.Enabled = true;
            lblCanhBao.Text = "Cảnh báo: bạn chỉ có thể cập nhật lại mật khẩu và quyền. Nên cân nhắc.!!!";
            grpThongTin.Text = "Cập nhật tài khoản người dùng";
            this.AcceptButton = btnLuuHeThong;
            dgvShowTaiKhoan.Enabled = false;
        }

        private void txtTKHoTen_TextChanged(object sender, EventArgs e)
        {
            if (txtTKHoTen.Text.Length > 0)
            {
                btnTimKiem.Enabled = true;
            }
        }

        private void cboTKQuyen_TextChanged(object sender, EventArgs e)
        {
            if (cboTKQuyen.Text.Length > 0)
            {
                btnTimKiem.Enabled = true;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            lblCanhBao.Text = "";
            string hoten = txtTKHoTen.Text;
            int quyen = checkQuyentk(cboTKQuyen.Text);
            btnBoQua.Enabled = true;
            timKiem(hoten, quyen);
            txtTKHoTen.Clear();
        }

        private void btnLuuHeThong_Click(object sender, EventArgs e)
        {
            string userName = txtTenTaiKhoan.Text;
            string passWord = txtMatKhau.Text;
            string hoTen = txtHoTenTK.Text;
            string tenHienThi = txtTenHienThi.Text;
            string soDienThoai = txtSoDienThoai.Text;
            string Email = txtEmail.Text;
            string diaChi = txtDiaChi.Text;

            Boolean check = checkDuLieuVao();

            // thêm
            if (check == false)
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.\nVui lòng nhập lại thông tin và kiểm tra thông tin đầy đủ!!!", "Lỗi nhập thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (checkKTDB(userName) == false)
                {
                    MessageBox.Show("Tên tài khoản của bạn có chứa ký tự đặt biệt!!!.\nVui lòng kiểm tra lại.", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenTaiKhoan.Clear();
                    txtTenTaiKhoan.Focus();
                    return;
                }
                else
                {
                    if (passWord.Length < 6 || passWord.Length > 18)
                    {
                        MessageBox.Show("Mật khẩu của bạn phải từ 6 đến 18 ký tự\nVui lòng kiểm tra lại.", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMatKhau.Clear();
                        txtMatKhau.Focus();
                        return;
                    }
                    else
                    {
                        if (checkKTDB(passWord) == true)
                        {
                            MessageBox.Show("Mật khẩu của bạn phải có ký tự đặt biệt.\nVui lòng kiểm tra lại.", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtMatKhau.Clear();
                            txtMatKhau.Focus();
                            return;
                        }
                        else
                        {
                            if (checkKTHoa(passWord) == true)
                            {
                                MessageBox.Show("Mật khẩu của bạn phải có ký tự ký tự viết hoa.\nVui lòng kiểm tra lại.", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtMatKhau.Clear();
                                txtMatKhau.Focus();
                                return;
                            }
                            else
                            {
                                if (checkKTSo(passWord) == true)
                                {
                                    MessageBox.Show("Mật khẩu của bạn phải có ký tự ký tự số.\nVui lòng kiểm tra lại.", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtMatKhau.Clear();
                                    txtMatKhau.Focus();
                                    return;
                                }
                                else
                                {
                                    if (checkKTThuong(passWord))
                                    {
                                        MessageBox.Show("Mật khẩu của bạn phải có ký tự ký tự viết thường.\nVui lòng kiểm tra lại.", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtMatKhau.Clear();
                                        txtMatKhau.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        if (txtSoDienThoai.Text != "")
                                        {
                                            if (txtSoDienThoai.TextLength != 10 && txtSoDienThoai.TextLength != 11)
                                            {
                                                MessageBox.Show("Số điện thoại không hợp lệ", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                txtSDienThoai.Clear();
                                                txtSDienThoai.Focus();
                                                return;
                                            }
                                            else
                                            {

                                                TaiKhoan user = TaiKhoanDAO.Instance.GetTaiKhoanByTenTaiKhoan(TenTaiKhoan);

                                                QuyenDTO Quyen = QuyenDAO.Instance.GetQuyenByID(user.Idquyen);

                                                QuyenDTO qCbo = QuyenDAO.Instance.GetIdByQuyen(cboQuyen.Text);

                                                //Thêm
                                                if (btnLuuHeThong.Enabled == true && grpThongTin.Text == "Thêm tài khoản người dùng")
                                                {
                                                    if (TaiKhoanDAO.Instance.GetCountByTenTaiKhoan(userName) > 0)
                                                    {
                                                        MessageBox.Show("Tên tài khoản đã có.\nVui lòng kiểm tra lại!!!", "Lỗi nhập thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        errorTrung.SetError(txtTenTKhoan, "Trùng tên tài khoản");
                                                        txtTenTKhoan.Clear();
                                                        txtTenTKhoan.Focus();
                                                        return;
                                                    }
                                                    else
                                                    {


                                                        if (user.Idquyen == 1)
                                                        {
                                                            themTaiKhoan(userName, passWord, hoTen, tenHienThi, soDienThoai, Email, diaChi, qCbo.Id);
                                                            errorRong.Clear();
                                                            errorTrung.Clear();
                                                            LoadData();
                                                            return;
                                                        }
                                                        else
                                                        {
                                                            if (user.Idquyen == 2)
                                                            {
                                                                if (user.Tentaikhoan == "ADMIN")
                                                                {
                                                                    themTaiKhoan(userName, passWord, hoTen, tenHienThi, soDienThoai, Email, diaChi, qCbo.Id);
                                                                    errorRong.Clear();
                                                                    errorTrung.Clear();
                                                                    LoadData();
                                                                    return;
                                                                }
                                                                else
                                                                {
                                                                    if (qCbo.Id == 3)
                                                                    {
                                                                        themTaiKhoan(userName, passWord, hoTen, tenHienThi, soDienThoai, Email, diaChi, qCbo.Id);
                                                                        errorRong.Clear();
                                                                        errorTrung.Clear();
                                                                        LoadData();
                                                                        return;
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("Tài khoản đăng nhập của bạn không đươc phép thêm tài khoản mới có quyền Admin hoặc Giáo Viên.\nVui lòng kiểm tra lại.", "Lỗi bảo mật hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        cboQuyen.Text = "";
                                                                        return;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (user.Idquyen == 3)
                                                                {
                                                                    if (qCbo.Id == 1 || qCbo.Id == 2 || qCbo.Id == 3)
                                                                    {
                                                                        MessageBox.Show("Bạn không được phép thêm tài khoản có quyền Admin hoặc tương đương quyền của bạn.\nVui lòng kiểm tra lại.", "Lỗi bảo mật hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        cboQuyen.Text = "";
                                                                        return;
                                                                    }
                                                                    else
                                                                    {
                                                                        themTaiKhoan(userName, passWord, hoTen, tenHienThi, soDienThoai, Email, diaChi, qCbo.Id);
                                                                        errorRong.Clear();
                                                                        errorTrung.Clear();
                                                                        LoadData();
                                                                        return;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (user.Idquyen == 4)
                                                                    {
                                                                        MessageBox.Show("Bạn không được phép thêm tài khoản trong hệ thống này.\nVui lòng kiểm tra lại.", "Lỗi bảo mật hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        cboQuyen.Text = "";
                                                                        return;
                                                                    }
                                                                    return;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }


                                                //Cập nhật
                                                else
                                                {
                                                    int idQdgv = (int)dgvShowTaiKhoan.SelectedCells[0].OwningRow.Cells["ID Quyền"].Value;

                                                    if (TaiKhoanDAO.Instance.GetCountByTenTaiKhoan(userName) != 1)
                                                    {
                                                        MessageBox.Show("Tên tài khoản không tồn tại.\nVui lòng kiểm tra lại!!!", "Lỗi cập nhật thông tin thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        if (user.Idquyen == 1)
                                                        {
                                                            CapNhat(userName, passWord, qCbo.Id);
                                                            errorRong.Clear();
                                                            errorTrung.Clear();
                                                            LoadData();
                                                            return;
                                                        }
                                                        else
                                                        {
                                                            if (user.Idquyen == 2)
                                                            {
                                                                if (user.Tentaikhoan == "ADMIN")
                                                                {
                                                                    CapNhat(userName, passWord, qCbo.Id);
                                                                    errorRong.Clear();
                                                                    errorTrung.Clear();
                                                                    LoadData();
                                                                    return;
                                                                }
                                                                else
                                                                {
                                                                    if (idQdgv == 1 || idQdgv == 2 || qCbo.Id == 2)
                                                                    {
                                                                        MessageBox.Show("Bạn không đươc phếp cập nhật tài khoản có quyền Admin khác.\nHoặc cập nhật tài khoản khác lên quyền Admin.", "Lỗi bảo mật hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        cboQuyen.Text = "";
                                                                        return;
                                                                    }
                                                                    else
                                                                    {
                                                                        CapNhat(userName, passWord, qCbo.Id);
                                                                        errorRong.Clear();
                                                                        errorTrung.Clear();
                                                                        LoadData();
                                                                        return;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (user.Idquyen == 3)
                                                                {
                                                                    if (idQdgv == 1 || idQdgv == 2 || idQdgv == 3)
                                                                    {
                                                                        MessageBox.Show("Bạn không được phép cập nhật tài khoản có quyền Admin hoặc tương đương với bạn.\nVui lòng kiểm tra lại.", "Lỗi bảo mật hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        cboQuyen.Text = "";
                                                                        return;
                                                                    }
                                                                    else
                                                                    {
                                                                        CapNhat(userName, passWord, qCbo.Id);
                                                                        errorRong.Clear();
                                                                        errorTrung.Clear();
                                                                        LoadData();
                                                                        return;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (user.Idquyen == 4)
                                                                    {
                                                                        MessageBox.Show("Bạn không được phép cập nhật tài khoản trong hệ thống này.\nVui lòng kiểm tra lại.", "Lỗi bảo mật hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        cboQuyen.Text = "";
                                                                        return;
                                                                    }
                                                                }
                                                                return;
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }

                                        else
                                        {

                                            TaiKhoan user = TaiKhoanDAO.Instance.GetTaiKhoanByTenTaiKhoan(TenTaiKhoan);

                                            QuyenDTO Quyen = QuyenDAO.Instance.GetQuyenByID(user.Idquyen);

                                            QuyenDTO qCbo = QuyenDAO.Instance.GetIdByQuyen(cboQuyen.Text);

                                            //Thêm
                                            if (btnLuuHeThong.Enabled == true && grpThongTin.Text == "Thêm tài khoản người dùng")
                                            {
                                                if (TaiKhoanDAO.Instance.GetCountByTenTaiKhoan(userName) > 0)
                                                {
                                                    MessageBox.Show("Tên tài khoản đã có.\nVui lòng kiểm tra lại!!!", "Lỗi nhập thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    errorTrung.SetError(txtTenTKhoan, "Trùng tên tài khoản");
                                                    txtTenTKhoan.Clear();
                                                    txtTenTKhoan.Focus();
                                                    return;
                                                }
                                                else
                                                {


                                                    if (user.Idquyen == 1)
                                                    {
                                                        themTaiKhoan(userName, passWord, hoTen, tenHienThi, soDienThoai, Email, diaChi, qCbo.Id);
                                                        errorRong.Clear();
                                                        errorTrung.Clear();
                                                        LoadData();
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        if (user.Idquyen == 2)
                                                        {
                                                            if (user.Tentaikhoan == "ADMIN")
                                                            {
                                                                themTaiKhoan(userName, passWord, hoTen, tenHienThi, soDienThoai, Email, diaChi, qCbo.Id);
                                                                errorRong.Clear();
                                                                errorTrung.Clear();
                                                                LoadData();
                                                                return;
                                                            }
                                                            else
                                                            {
                                                                if (qCbo.Id == 3)
                                                                {
                                                                    themTaiKhoan(userName, passWord, hoTen, tenHienThi, soDienThoai, Email, diaChi, qCbo.Id);
                                                                    errorRong.Clear();
                                                                    errorTrung.Clear();
                                                                    LoadData();
                                                                    return;
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("Tài khoản đăng nhập của bạn không đươc phép thêm tài khoản mới có quyền Admin hoặc Giáo Viên.\nVui lòng kiểm tra lại.", "Lỗi bảo mật hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    cboQuyen.Text = "";
                                                                    return;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (user.Idquyen == 3)
                                                            {
                                                                if (qCbo.Id == 1 || qCbo.Id == 2 || qCbo.Id == 3)
                                                                {
                                                                    MessageBox.Show("Bạn không được phép thêm tài khoản có quyền Admin hoặc tương đương quyền của bạn.\nVui lòng kiểm tra lại.", "Lỗi bảo mật hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    cboQuyen.Text = "";
                                                                    return;
                                                                }
                                                                else
                                                                {
                                                                    themTaiKhoan(userName, passWord, hoTen, tenHienThi, soDienThoai, Email, diaChi, qCbo.Id);
                                                                    errorRong.Clear();
                                                                    errorTrung.Clear();
                                                                    LoadData();
                                                                    return;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (user.Idquyen == 4)
                                                                {
                                                                    MessageBox.Show("Bạn không được phép thêm tài khoản trong hệ thống này.\nVui lòng kiểm tra lại.", "Lỗi bảo mật hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    cboQuyen.Text = "";
                                                                    return;
                                                                }
                                                                return;
                                                            }
                                                        }
                                                    }
                                                }
                                            }


                                            //Cập nhật
                                            else
                                            {
                                                int idQdgv = (int)dgvShowTaiKhoan.SelectedCells[0].OwningRow.Cells["ID Quyền"].Value;

                                                if (TaiKhoanDAO.Instance.GetCountByTenTaiKhoan(userName) != 1)
                                                {
                                                    MessageBox.Show("Tên tài khoản không tồn tại.\nVui lòng kiểm tra lại!!!", "Lỗi cập nhật thông tin thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    return;
                                                }
                                                else
                                                {
                                                    if (user.Idquyen == 1)
                                                    {
                                                        CapNhat(userName, passWord, qCbo.Id);
                                                        errorRong.Clear();
                                                        errorTrung.Clear();
                                                        LoadData();
                                                        return;
                                                    }
                                                    else
                                                    {
                                                        if (user.Idquyen == 2)
                                                        {
                                                            if (user.Tentaikhoan == "ADMIN")
                                                            {
                                                                CapNhat(userName, passWord, qCbo.Id);
                                                                errorRong.Clear();
                                                                errorTrung.Clear();
                                                                LoadData();
                                                                return;
                                                            }
                                                            else
                                                            {
                                                                if (idQdgv == 1 || idQdgv == 2 || qCbo.Id == 2)
                                                                {
                                                                    MessageBox.Show("Bạn không đươc phếp cập nhật tài khoản có quyền Admin khác.\nHoặc cập nhật tài khoản khác lên quyền Admin.", "Lỗi bảo mật hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    cboQuyen.Text = "";
                                                                    return;
                                                                }
                                                                else
                                                                {
                                                                    CapNhat(userName, passWord, qCbo.Id);
                                                                    errorRong.Clear();
                                                                    errorTrung.Clear();
                                                                    LoadData();
                                                                    return;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (user.Idquyen == 3)
                                                            {
                                                                if (idQdgv == 1 || idQdgv == 2 || idQdgv == 3)
                                                                {
                                                                    MessageBox.Show("Bạn không được phép cập nhật tài khoản có quyền Admin hoặc tương đương với bạn.\nVui lòng kiểm tra lại.", "Lỗi bảo mật hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    cboQuyen.Text = "";
                                                                    return;
                                                                }
                                                                else
                                                                {
                                                                    CapNhat(userName, passWord, qCbo.Id);
                                                                    errorRong.Clear();
                                                                    errorTrung.Clear();
                                                                    LoadData();
                                                                    return;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (user.Idquyen == 4)
                                                                {
                                                                    MessageBox.Show("Bạn không được phép cập nhật tài khoản trong hệ thống này.\nVui lòng kiểm tra lại.", "Lỗi bảo mật hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    cboQuyen.Text = "";
                                                                    return;
                                                                }
                                                            }
                                                            return;
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            TaiKhoan user = TaiKhoanDAO.Instance.GetTaiKhoanByTenTaiKhoan(TenTaiKhoan);

            QuyenDTO Quyen = QuyenDAO.Instance.GetQuyenByID(user.Idquyen);

            int idQdgv = (int)dgvShowTaiKhoan.SelectedCells[0].OwningRow.Cells["ID Quyền"].Value;

            string userName = dgvShowTaiKhoan.SelectedCells[0].OwningRow.Cells["Tên Tài Khoản"].Value.ToString();

            if (TaiKhoanDAO.Instance.GetCountByTenTaiKhoan(userName) < 1)
            {
                MessageBox.Show("Bạn chưa chọn tài khoản để xóa !", "Lỗi xóa tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (userName.Equals(TenTaiKhoan))
                {
                    MessageBox.Show("Bạn không được phép xóa tài khoản đăng nhập hiện tại !", "Lỗi xóa tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadData();
                    return;
                }
                else
                {

                    if (user.Idquyen == 1)
                    {
                        xoaTaiKhoan(userName);
                        LoadData();
                        return;
                    }
                    else
                    {
                        if (user.Idquyen == 2)
                        {
                            if (user.Tentaikhoan == "ADMIN")
                            {
                                xoaTaiKhoan(userName);
                                LoadData();
                                return;
                            }
                            else
                            {
                                if (idQdgv == 1 || idQdgv == 2)
                                {
                                    MessageBox.Show("Bạn không được phép xóa tài khoản có quyền tương đương với bạn", "Lỗi xóa tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    LoadData();
                                    return;
                                }
                                else
                                {
                                    xoaTaiKhoan(userName);
                                    LoadData();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (user.Idquyen == 3)
                            {

                                if (idQdgv == 1 || idQdgv == 2 || idQdgv == 3)
                                {
                                    MessageBox.Show("Bạn không được phép xóa tài khoản có quyền cao hơn hoặc tương đương với bạn", "Lỗi xóa tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    LoadData();
                                    return;
                                }
                                else
                                {
                                    xoaTaiKhoan(userName);
                                    LoadData();
                                    return;
                                }
                            }
                            else
                            {
                                if (user.Idquyen == 4)
                                {
                                    MessageBox.Show("Bạn không được phép xóa tài khoản trong hệ thống này", "Lỗi xóa tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    LoadData();
                                    return;
                                }
                                return;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Variables
        private Capture videoCapture = null;
        private Image<Bgr, Byte> currentFrame = null;
        Mat frame = new Mat();
        private bool facesDetectionEnabled = false;
        CascadeClassifier faceCasacdeClassifier = new CascadeClassifier(@"C:\haarcascade\haarcascade_frontalface_alt.xml");
        Image<Bgr, Byte> faceResult = null;
        List<Image<Gray, Byte>> TrainedFaces = new List<Image<Gray, byte>>();
        List<int> PersonsLabes = new List<int>();

        bool EnableSaveImage = false;
        private bool isTrained = false;
        EigenFaceRecognizer recognizer;
        List<string> PersonsNames = new List<string>();

        #endregion

        private bool DangNhap(string userName, string passWord)
        {
            return TaiKhoanDAO.Instance.DangNhap(userName, passWord);
        }


        private void btnOKXT_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string passWord = txtPasswd.Text;

            if (userName == "" || passWord == "")
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu trống!!!\nVui lòng kiểm tra lại", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenTaiKhoan.Clear();
                txtMatKhau.Clear();
                txtTenTaiKhoan.Focus();
            }
            else
            {
                if (DangNhap(userName, passWord))
                {
                    pnl1.Visible = false;
                    pnl2.Visible = true;
                }
            }
        }

        private void txtPasswd_TextChanged(object sender, EventArgs e)
        {
            if (txtPasswd.TextLength > 0)
            {
                this.AcceptButton = btnOKXT;
            }
        }

        private void tabTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabTaiKhoan.SelectedTab == tabFace)
            {
                this.txtUserName.Focus();
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            btnLayKhuonMat.Enabled = true;
            if (videoCapture != null) videoCapture.Dispose();
            videoCapture = new Capture();
            Application.Idle += ProcessFrame;
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            if (videoCapture != null && videoCapture.Ptr != IntPtr.Zero)
            {
                videoCapture.Retrieve(frame, 0);
                currentFrame = frame.ToImage<Bgr, Byte>().Resize(picCapture.Width, picCapture.Height, Inter.Cubic);
                if (facesDetectionEnabled)
                {
                    Mat grayImage = new Mat();
                    CvInvoke.CvtColor(currentFrame, grayImage, ColorConversion.Bgr2Gray);
                    CvInvoke.EqualizeHist(grayImage, grayImage);

                    Rectangle[] faces = faceCasacdeClassifier.DetectMultiScale(grayImage, 1.1, 5, Size.Empty, Size.Empty);
                    if (faces.Length > 0)
                    {

                        foreach (var face in faces)
                        {
                            Image<Bgr, Byte> resultImage = currentFrame.Convert<Bgr, Byte>();
                            resultImage.ROI = face;
                            picDetected.SizeMode = PictureBoxSizeMode.StretchImage;
                            picDetected.Image = resultImage.Bitmap;
                            if (EnableSaveImage)
                            {
                                string path = Directory.GetCurrentDirectory() + @"\TrainedImages";
                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);
                                Task.Factory.StartNew(() =>
                                {
                                    for (int i = 0; i < 5; i++)
                                    {
                                        resultImage.Resize(200, 200, Inter.Cubic).Save(path + @"\" + txtUserName.Text + "_" + DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss") + ".jpg");
                                        Thread.Sleep(1000);
                                    }
                                });
                                if (MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                {
                                    MessageBox.Show("Vui lòng đợi vài giây......", "Thông báo");
                                    Thread.Sleep(5000);
                                    videoCapture.Dispose();
                                    picDetected.Image = null;
                                    picXNTC.Image = null;
                                    LoadXTM();
                                }
                            }
                            EnableSaveImage = false;



                            if (btnLuuAXT.InvokeRequired)
                            {
                                btnLuuAXT.Invoke(new ThreadStart(delegate
                                {
                                    btnLuuAXT.Enabled = true;
                                }));
                            }
                        }
                    }
                }
                picCapture.Image = currentFrame.Bitmap;
            }
            if (currentFrame != null)
                currentFrame.Dispose();
        }

        private void btnLayKhuonMat_Click(object sender, EventArgs e)
        {
            btnLuuAXT.Enabled = true;
            facesDetectionEnabled = true;
        }

        private void btnLuuAXT_Click(object sender, EventArgs e)
        {
            EnableSaveImage = true;
        }
    }
}