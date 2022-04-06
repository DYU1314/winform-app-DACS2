using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DAO;
using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE
{
    public partial class frmQuanLy : Form
    {
        public string mssv = frmQuanLyDiem._mssv;

        BindingSource KhoaHoc = new BindingSource();
        BindingSource Lop = new BindingSource();
        BindingSource HocKi = new BindingSource();
        BindingSource HocPhan = new BindingSource();
        BindingSource SinhVien = new BindingSource();

        public frmQuanLy()
        {
            InitializeComponent();

            LoadData();
        }

        #region MeThods

        private void LoadData()
        {
            LoadKhoaHoc();
            LoadLop();
            LoadHocPhan();
            LoadHocKi();
            LoadSinhVien();
            EditSinhVien();
        }

        #region Khóa học 

        private void LoadKhoaHoc()
        {
            dgvShowKhoaHoc.DataSource = KhoaHoc;
            errorRong.Clear();
            errorTrung.Clear();
            LoadListKhoaHoc();
            BindingKhoaHoc();
            EnableControlKhoaHoc(false);
            txtTKMKhoaHoc.Enabled = true;
            txtTKTenKH.Enabled = true;
            txtTKNamBD.Enabled = true;
            btnThemKH.Enabled = true;
            dgvShowKhoaHoc.Enabled = true;
            btnTimKiemKhoaHoc.Enabled = true;

            grpThongTinKH.Text = "Thông tin";
            lblCanhBaoKH.Visible = false;
        }

        private void EnableControlKhoaHoc(Boolean e)
        {
            btnThemKH.Enabled = e;
            btnCapNhapKH.Enabled = e;
            btnXoaKH.Enabled = e;
            btnBoQuaKhoaHoc.Enabled = e;
            btnLuuKhoaHoc.Enabled = e;

            txtTKMKhoaHoc.Enabled = e;
            txtTKTenKH.Enabled = e;
            txtTKNamBD.Enabled = e;
            txtMaKhoaHoc.Enabled = e;
            txtTenKhoaHoc.Enabled = e;
            txtNamBatDau.Enabled = e;
            txtNamKetThuc.Enabled = e;
            nudTGHToiThieu.Enabled = e;
            nudTGHToiDa.Enabled = e;
            nudTGHTieuChuan.Enabled = e;
        }

        private void clearTextboxKhoaHoc()
        {
            txtTKMKhoaHoc.Clear();
            txtTKTenKH.Clear();
            txtTKNamBD.Clear();
            txtMaKhoaHoc.Clear();
            txtTenKhoaHoc.Clear();
            txtNamBatDau.Clear();
            txtNamKetThuc.Clear();
        }

        private void LoadListKhoaHoc()
        {
            KhoaHoc.DataSource = KhoaHocDAO.Instance.LoadListKhoaHoc();
            dgvShowKhoaHoc.Columns["ID Khóa Học"].Visible = false;
        }

        private void BindingKhoaHoc()
        {
            try
            {
                txtMaKhoaHoc.DataBindings.Add(new Binding("Text", dgvShowKhoaHoc.DataSource, "Mã Khóa Học", true, DataSourceUpdateMode.Never));
                txtTenKhoaHoc.DataBindings.Add(new Binding("Text", dgvShowKhoaHoc.DataSource, "Tên Khóa Học", true, DataSourceUpdateMode.Never));
                txtNamBatDau.DataBindings.Add(new Binding("Text", dgvShowKhoaHoc.DataSource, "Năm Bắt Đầu", true, DataSourceUpdateMode.Never));
                txtNamKetThuc.DataBindings.Add(new Binding("Text", dgvShowKhoaHoc.DataSource, "Năm Kết Thúc", true, DataSourceUpdateMode.Never));
                nudTGHToiThieu.DataBindings.Add(new Binding("Value", dgvShowKhoaHoc.DataSource, "TGH Tối Thiểu", true, DataSourceUpdateMode.Never));
                nudTGHToiDa.DataBindings.Add(new Binding("Value", dgvShowKhoaHoc.DataSource, "TGH Tối Đa", true, DataSourceUpdateMode.Never));
                nudTGHTieuChuan.DataBindings.Add(new Binding("Value", dgvShowKhoaHoc.DataSource, "TGH Quy Định", true, DataSourceUpdateMode.Never));
            }
            catch { }
        }

        private int StringToInt(string e)
        {
            int x = 0;
            for (int i = 0; i < e.Length; i++)
            {
                int temp = e[i] - '0';
                if (temp != 0)
                {
                    x += temp * (int)Math.Pow(10, (e.Length - (i + 1)));
                }
            }
            return x;
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

        private Boolean checkNamNu(string chuoicankiemtra)
        {
            string db = "NAM NỮ nam nữ";
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

        private Boolean checkThongTinKhoahoc()
        {
            Boolean check = true;
            if (txtMaKhoaHoc.Text.Trim() == "")
            {
                errorRong.SetError(txtMaKhoaHoc, "Dữ liệu không được trống!!!");
                check = false;
                txtMaKhoaHoc.Focus();
                return check;
            }
            else { errorRong.Clear(); }

            if (txtTenKhoaHoc.Text.Trim() == "")
            {
                errorRong.SetError(txtTenKhoaHoc, "Dữ liệu không được trống!!!");
                check = false;
                txtTenKhoaHoc.Focus();
                return check;
            }
            else { errorRong.Clear(); }

            if (txtNamBatDau.Text.Trim() == "")
            {
                errorRong.SetError(txtNamBatDau, "Dữ liệu không được trống!!!");
                check = false;
                txtNamBatDau.Focus();
                return check;
            }
            else { errorRong.Clear(); }

            if (txtNamKetThuc.Text.Trim() == "")
            {
                errorRong.SetError(txtNamKetThuc, "Dữ liệu không được trống!!!");
                check = false;
                txtNamKetThuc.Focus();
                return check;
            }
            else { errorRong.Clear(); }

            return check;
        }

        private void XoaKhoaHoc(string maKH)
        {
            if (KhoaHocDAO.Instance.XoaKhoaHoc(maKH))
            {
                MessageBox.Show("Xóa thành công!!!!");
            }
            else
            {
                MessageBox.Show("Xóa thất bại!!!");
            }
            return;
        }

        private void timKiemKhoaHoc(string mkh, string tenkh, int namBD)
        {
            if (mkh == "" && tenkh == "" && namBD == 0)
            {
                MessageBox.Show("Bạn chưa nhập thông tin tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadKhoaHoc();
                return;
            }
            else
            {
                if (mkh == "")
                {
                    if (tenkh == "")
                    {
                        if (namBD != 0)
                        {
                            KhoaHoc.DataSource = KhoaHocDAO.Instance.timKiemNamBD(namBD);
                            dgvShowKhoaHoc.Columns["ID Khóa Học"].Visible = false;
                            return;
                        }
                    }
                    else
                    {
                        if (namBD == 0)
                        {
                            KhoaHoc.DataSource = KhoaHocDAO.Instance.timKiemTenKH(tenkh);
                            dgvShowKhoaHoc.Columns["ID Khóa Học"].Visible = false;
                            return;
                        }
                        else
                        {
                            KhoaHoc.DataSource = KhoaHocDAO.Instance.timKiemTenvsNamBD(tenkh, namBD);
                            dgvShowKhoaHoc.Columns["ID Khóa Học"].Visible = false;
                            return;
                        }
                    }
                }
                else
                {
                    if (tenkh == "")
                    {
                        if (namBD != 0)
                        {
                            KhoaHoc.DataSource = KhoaHocDAO.Instance.timKiemMaKHvsNamBD(mkh, namBD);
                            dgvShowKhoaHoc.Columns["ID Khóa Học"].Visible = false;
                            return;
                        }
                        else
                        {
                            KhoaHoc.DataSource = KhoaHocDAO.Instance.timKiemMKH(mkh);
                            dgvShowKhoaHoc.Columns["ID Khóa Học"].Visible = false;
                            return;
                        }
                    }
                    else
                    {
                        if (namBD != 0)
                        {
                            KhoaHoc.DataSource = KhoaHocDAO.Instance.timKiemTatCa(mkh, tenkh, namBD);
                            dgvShowKhoaHoc.Columns["ID Khóa Học"].Visible = false;
                            return;
                        }
                        else
                        {
                            KhoaHoc.DataSource = KhoaHocDAO.Instance.timKiemMaKHvsTen(mkh, tenkh);
                            dgvShowKhoaHoc.Columns["ID Khóa Học"].Visible = false;
                            return;
                        }
                    }
                }
            }
        }

        private void capnhatKhoaHoc(string maKhoahoc, string tenKH, int namKT, int tghToiThieu, int tghToiDa, int tghTieuChuan)
        {
            if (KhoaHocDAO.Instance.UpdateKhoaHoc(maKhoahoc, tenKH, namKT, tghToiThieu, tghToiDa, tghTieuChuan))
            {
                MessageBox.Show("Cập nhật thành công.!!!", "Thông báo cập nhật", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.!!!", "Thông báo cập nhật", MessageBoxButtons.OK);
            }
        }

        private void ThemKhoaHoc(string maKH, string tenKH, int namBD, int namKT, int tghToiThieu, int tghToiDa, int tghTieuChuan)
        {
            if (KhoaHocDAO.Instance.InsertKhoaHoc(maKH, tenKH, namBD, namKT, tghToiThieu, tghToiDa, tghTieuChuan))
            {
                MessageBox.Show("Thêm dữ liêu thành công", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Thêm dữ liêu thất bại", "Thông báo", MessageBoxButtons.OK);
            }
        }
        #endregion Khóa học

        #region Lớp

        private void LoadLop()
        {
            grpThongTinLop.Text = "Thông tin";
            dgvShowLop.DataSource = Lop;
            LoadListDanhSachLop();
            LoadComboBoxTKKhoaHocLop(cboKHLop);
            LoadComboBoxTKKhoaHocLop(cboTKKhoaHocLop);
            BindingLop();

            EnableControlLop(false);
            txtTKMaLop.Enabled = true;
            cboTKKhoaHocLop.Enabled = true;
            btnTKLop.Enabled = true;
            btnThemLop.Enabled = true;
            dgvShowLop.Enabled = true;
        }

        private void EnableControlLop(Boolean e)
        {
            txtTKMaLop.Enabled = e;
            cboTKKhoaHocLop.Enabled = e;
            txtMaLop.Enabled = e;
            txtTenLop.Enabled = e;
            txtNKNamBD.Enabled = e;
            txtNKNamKT.Enabled = e;
            cboKHLop.Enabled = e;

            btnTKLop.Enabled = e;
            btnThemLop.Enabled = e;
            btnCapNhatLop.Enabled = e;
            btnXoaLop.Enabled = e;
            btnBoQuaLop.Enabled = e;
            btnLuuLop.Enabled = e;
        }

        private void LoadListDanhSachLop()
        {
            Lop.DataSource = LopDAO.Instance.LoadListLop();
            dgvShowLop.Columns["ID Lớp"].Visible = false;
            dgvShowLop.Columns["ID Khóa Học"].Visible = false;
        }

        private void BindingLop()
        {
            try
            {
                txtMaLop.DataBindings.Add(new Binding("Text", dgvShowLop.DataSource, "Mã Lớp", true, DataSourceUpdateMode.Never));
                txtTenLop.DataBindings.Add(new Binding("Text", dgvShowLop.DataSource, "Tên Lớp", true, DataSourceUpdateMode.Never));
                txtNKNamBD.DataBindings.Add(new Binding("Text", dgvShowLop.DataSource, "Năm Bắt Đầu", true, DataSourceUpdateMode.Never));
                txtNKNamKT.DataBindings.Add(new Binding("Text", dgvShowLop.DataSource, "Năm Kết Thúc", true, DataSourceUpdateMode.Never));
            }
            catch { }
        }

        private void LoadComboBoxTKKhoaHocLop(ComboBox cbo)
        {
            try
            {
                cbo.DataSource = KhoaHocDAO.Instance.GetListKhoaHoc();
                cbo.DisplayMember = "MAKHOAHOC";
            }
            catch { }
        }

        private Boolean checkDuLieuLop()
        {
            Boolean check = true;

            if (txtMaLop.Text.Trim() == "")
            {
                errorRong.SetError(txtMaLop, "Dữ liêu không được trống");
                txtMaLop.Focus();
                return check = false;
            }
            else { errorRong.Clear(); }

            if (txtTenLop.Text.Trim() == "")
            {
                errorRong.SetError(txtTenLop, "Dữ liêu không được trống");
                txtMaLop.Focus();
                return check = false;
            }
            else { errorRong.Clear(); }

            return check;
        }

        private void themLopHoc(string maLop, string tenLop, int idKhoaHoc)
        {
            if (LopDAO.Instance.InsertLop(maLop, tenLop, idKhoaHoc))
            {
                MessageBox.Show("Thêm dữ liệu thành công");
                return;
            }
            else
            {
                MessageBox.Show("Thêm dữ liệu thất bại");
                return;
            }
        }

        private void capNhatLop(string malop, string tenLop, int idKhoaHoc)
        {
            if (LopDAO.Instance.UpdatetLop(malop, tenLop, idKhoaHoc))
            {
                MessageBox.Show("Cập nhật thành công dữ liệu thành công");
                return;
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại dữ liệu thất bại");
                return;
            }
        }

        private void xoaLop(string maLop)
        {
            if (LopDAO.Instance.DeletaLop(maLop))
            {
                MessageBox.Show("Xóa lớp thành công!!!");
            }
            else
            {
                MessageBox.Show("Xóa lớp thất bại!!!");
            }
        }

        private void timkiemLop(string malop, string makhoahoc)
        {
            if (malop == "")
            {
                Lop.DataSource = LopDAO.Instance.timKiemKhongMaLop(makhoahoc);
                dgvShowLop.Columns["ID Lớp"].Visible = false;
                dgvShowLop.Columns["ID Khóa Học"].Visible = false;
                return;
            }
            else
            {
                Lop.DataSource = LopDAO.Instance.timKiemCoMaLop(malop, makhoahoc);
                dgvShowLop.Columns["ID Lớp"].Visible = false;
                dgvShowLop.Columns["ID Khóa Học"].Visible = false;
                return;
            }
        }
        #endregion Lớp

        #region Học Phần 

        private void LoadHocPhan()
        {
            dgvShowHocPhan.DataSource = HocPhan;

            LoadListDanhSachHocPhan();
            LoadComboBoxKhoaHocHocPhan(cboKhoaHocHP);
            LoadComboBoxKhoaHocHocPhan(cboTKKhoaHocHP);
            LoadComboBoxHocKiHocPhan(cboHocKiHocPhan);
            LoadComboBoxHocKiHocPhan(cboTimKiemHocKiHocPhan);
            BindingHocPhan();

            EnableControlHocPhan(false);
            txtTKMaHocPhan.Enabled = true;
            cboTimKiemHocKiHocPhan.Enabled = true;
            cboTKKhoaHocHP.Enabled = true;
            btnThemHP.Enabled = true;
            btnTimKiemHP.Enabled = true;
            grpThongTinHocPhan.Text = "Thông tin";
            lblCanhBaoHocPhan.Visible = false;
            txtTKMaHocPhan.Clear();
            txtMaHocPhan.Clear();
            txtTenHocPhan.Clear();
            dgvShowHocPhan.Enabled = true;
        }

        private void LoadListDanhSachHocPhan()
        {
            HocPhan.DataSource = HocPhanDAO.Instance.LoadDanhSachHocPhan();
            dgvShowHocPhan.Columns["ID Học Phần"].Visible = false;
            dgvShowHocPhan.Columns["ID Học Kì"].Visible = false;
            dgvShowHocPhan.Columns["ID Khóa Học"].Visible = false;
        }

        private void LoadComboBoxKhoaHocHocPhan(ComboBox cbo)
        {
            try
            {
                cbo.DataSource = KhoaHocDAO.Instance.GetListKhoaHoc();
                cbo.DisplayMember = "MAKHOAHOC";
            }
            catch { }
        }

        private void LoadComboBoxHocKiHocPhan(ComboBox cbo)
        {
            try
            {
                cbo.DataSource = HocKiDAO.Instance.GetListHocKi();
                cbo.DisplayMember = "MAHOCKI";
            }
            catch { }
        }

        private void timKiemHocPhan(string maHocPhan, string maHocKi, string maKhoaHoc)
        {
            if (maHocPhan == "")
            {
                HocPhan.DataSource = HocPhanDAO.Instance.timKiemCoMaHocPhan(maHocPhan, maHocKi, maKhoaHoc);
                dgvShowHocPhan.Columns["ID Học Phần"].Visible = false;
                dgvShowHocPhan.Columns["ID Học Kì"].Visible = false;
                dgvShowHocPhan.Columns["ID Khóa Học"].Visible = false;
                return;
            }
            else
            {
                HocPhan.DataSource = HocPhanDAO.Instance.timKiemKhongMaHocPhan(maHocPhan, maHocKi, maKhoaHoc);
                dgvShowHocPhan.Columns["ID Học Phần"].Visible = false;
                dgvShowHocPhan.Columns["ID Học Kì"].Visible = false;
                dgvShowHocPhan.Columns["ID Khóa Học"].Visible = false;
                return;
            }
        }

        private Boolean checkDuHocPhan()
        {
            Boolean check = true;

            if (txtMaHocPhan.Text.Trim() == "")
            {
                errorRong.SetError(txtMaHocPhan, "Dữ liêu không được trống");
                txtMaHocPhan.Focus();
                return check = false;
            }
            else { errorRong.Clear(); }

            if (txtTenHocPhan.Text.Trim() == "")
            {
                errorRong.SetError(txtTenHocPhan, "Dữ liêu không được trống");
                txtTenHocPhan.Focus();
                return check = false;
            }
            else { errorRong.Clear(); }

            return check;
        }

        private void BindingHocPhan()
        {
            try
            {
                txtMaHocPhan.DataBindings.Add(new Binding("Text", dgvShowHocPhan.DataSource, "Mã Học Phần", true, DataSourceUpdateMode.Never));
                txtTenHocPhan.DataBindings.Add(new Binding("Text", dgvShowHocPhan.DataSource, "Tên Học Phần", true, DataSourceUpdateMode.Never));
                dtpTGBatDauHP.DataBindings.Add(new Binding("Text", dgvShowHocPhan.DataSource, "Thời Gian Bắt Đầu Học", true, DataSourceUpdateMode.Never));
                dtpTGKetThucHP.DataBindings.Add(new Binding("Text", dgvShowHocPhan.DataSource, "Thời Gian Kết Thúc", true, DataSourceUpdateMode.Never));
            }
            catch { }
        }

        private void ThemHocPhan(string mahp, string tenhp, int idhk, int idkh)
        {
            if (HocPhanDAO.Instance.InsertHocPhan(mahp, tenhp, idhk, idkh))
            {
                MessageBox.Show("Thêm dữ liệu thành công!!!");
            }
            else
            {
                MessageBox.Show("Thêm dữ liệu thất bại!!!");
            }
        }

        private void CapNhatHocPhan(string mahp, string tenhp, int idhk, int idkh)
        {
            if (HocPhanDAO.Instance.UpdatetHocPhan(mahp, tenhp, idhk, idkh))
            {
                MessageBox.Show("Cập nhât thành công!!!");
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!!!");
            }
        }

        private void XoaHocPhan(string mahp)
        {
            if (HocPhanDAO.Instance.DeletaHocPhan(mahp))
            {
                MessageBox.Show("Xóa dữ liệu học kì thành công");
            }
            else
            {
                MessageBox.Show("Xóa dữ liệu học kì thất bại");
            }
        }

        private void EnableControlHocPhan(Boolean e)
        {
            txtTKMaHocPhan.Enabled = e;
            cboTimKiemHocKiHocPhan.Enabled = e;
            cboTKKhoaHocHP.Enabled = e;
            txtMaHocPhan.Enabled = e;
            txtTenHocPhan.Enabled = e;
            dtpTGBatDauHP.Enabled = e;
            dtpTGKetThucHP.Enabled = e;
            cboHocKiHocPhan.Enabled = e;
            cboKhoaHocHP.Enabled = e;

            btnTimKiemHP.Enabled = e;
            btnThemHP.Enabled = e;
            btnCapNhatHP.Enabled = e;
            btnXoaHP.Enabled = e;
            btnBoQuaHP.Enabled = e;
            btnLuuHP.Enabled = e;
        }

        #endregion Học Phần

        #region Học Kì

        private void LoadHocKi()
        {
            dgvShowHocKi.DataSource = HocKi;

            LoadListDanhSachHocKi();
            LoadComboBoxHocKi(cboKhoaHocHK);
            LoadComboBoxHocKi(cboTKKhoaHocHK);
            BindingHocKi();
            EnableControlHocKi(false);
            btnThemHocKi.Enabled = true;
            btnTimKiemHK.Enabled = true;
            txtTKMaHocKi.Enabled = true;
            cboTKKhoaHocHK.Enabled = true;
            dgvShowHocKi.Enabled = true;
        }

        private void LoadListDanhSachHocKi()
        {
            HocKi.DataSource = HocKiDAO.Instance.LoadListDanhSachHocKi();
            dgvShowHocKi.Columns["ID Khóa Học"].Visible = false;
            dgvShowHocKi.Columns["ID Học Kì"].Visible = false;
        }

        private void LoadComboBoxHocKi(ComboBox cbo)
        {
            try
            {
                cbo.DataSource = KhoaHocDAO.Instance.GetListKhoaHoc();
                cbo.DisplayMember = "MAKHOAHOC";
            }
            catch { }
        }

        private void BindingHocKi()
        {
            try
            {
                txtMaHocKi.DataBindings.Add(new Binding("Text", dgvShowHocKi.DataSource, "Mã Học Kì", true, DataSourceUpdateMode.Never));
                txtTenHocKi.DataBindings.Add(new Binding("Text", dgvShowHocKi.DataSource, "Tên Học kì", true, DataSourceUpdateMode.Never));
                dtpTGBatDauHK.DataBindings.Add(new Binding("Text", dgvShowHocKi.DataSource, "Thời Gian Bắt Đầu", true, DataSourceUpdateMode.Never));
                dtpTGKetThucHK.DataBindings.Add(new Binding("Text", dgvShowHocKi.DataSource, "Thời Gian Kết Thúc", true, DataSourceUpdateMode.Never));
            }
            catch { }
        }

        private void ClearTextboxHocKi()
        {
            txtTKMaHocKi.Clear();
            cboTKKhoaHocHK.Text = "";
            txtMaHocKi.Clear();
            txtTenHocKi.Clear();
            cboKHLop.Text = "";
        }

        private void EnableControlHocKi(Boolean e)
        {
            txtTKMaHocKi.Enabled = e;
            cboTKKhoaHocHK.Enabled = e;
            txtMaHocKi.Enabled = e;
            txtTenHocKi.Enabled = e;
            dtpTGBatDauHK.Enabled = e;
            dtpTGKetThucHK.Enabled = e;
            cboKhoaHocHK.Enabled = e;

            btnTimKiemHK.Enabled = e;
            btnThemHocKi.Enabled = e;
            btnXoaHocKi.Enabled = e;
            btnBoQuaHocKi.Enabled = e;
            btnCapNhatHocKi.Enabled = e;
            btnLuuHocKi.Enabled = e;
        }

        private void ThemHocKi(string mahk, string tenhk, string tgbd, string tgkt, int idkh)
        {
            if (HocKiDAO.Instance.InsertHocKi(mahk, tenhk, tgbd, tgkt, idkh))
            {
                MessageBox.Show("Thêm dữ liệu học kì thành công");
            }
            else
            {
                MessageBox.Show("Thêm dữ liệu học kì thất bại");
            }
        }

        private void capNhatHocKi(string mahk, string tenhk, string tgbd, string tgkt,int idKhoahoc)
        {
            if (HocKiDAO.Instance.GetCountByMaHocKi(mahk) < 1)
            {
                MessageBox.Show("Thông tin cập nhật không tồn tại", "Lỗi thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadHocKi();
                return;
            }
            else
            {
                if (HocKiDAO.Instance.UpdateHocKi(mahk, tenhk, tgbd, tgkt,idKhoahoc))
                {
                    MessageBox.Show("Cập nhật dữ liệu học kì thành công");
                }
                else
                {
                    MessageBox.Show("Cập nhật dữ liệu học kì thất bại");
                }
            }
        }

        private void XoaHocKi(string mahk)
        {
            if (HocKiDAO.Instance.DeleteHocKi(mahk))
            {
                MessageBox.Show("Xóa dữ liệu học kì thành công");
            }
            else
            {
                MessageBox.Show("Xóa dữ liệu học kì thất bại");
            }
        }

        private Boolean checkDuLieuHocKi()
        {
            Boolean check = true;

            if (txtMaHocKi.Text.Trim() == "")
            {
                errorRong.SetError(txtMaHocKi, "Dữ liệu không được trống");
                txtMaHocKi.Focus();
                return check = false;
            }
            else { errorRong.Clear(); }

            if (txtTenHocKi.Text.Trim() == "")
            {
                errorRong.SetError(txtTenHocKi, "Dữ liệu không được trống");
                txtMaHocKi.Focus();
                return check = false;
            }
            else { errorRong.Clear(); }

            return check;
        }

        private void timkiemhocki(string mahk, int idkhoahoc)
        {
            if (mahk == "")
            {
                HocKi.DataSource = HocKiDAO.Instance.timKiemKhongMaHocKi(idkhoahoc);
                dgvShowHocKi.Columns["ID Khóa Học"].Visible = false;
                dgvShowHocKi.Columns["ID Học Kì"].Visible = false;
                return;
            }
            else
            {
                HocKi.DataSource = HocKiDAO.Instance.timKiemCoMaHK(mahk, idkhoahoc);
                dgvShowHocKi.Columns["ID Khóa Học"].Visible = false;
                dgvShowHocKi.Columns["ID Học Kì"].Visible = false;
                return;
            }
        }

        #endregion Học Kì

        #region Sinh Viên

        private void LoadSinhVien()
        {
            dgvShowSinhVien.DataSource = SinhVien;

            LoadDanhSachSinhVien();
            LoadComboBoxLopSinhVien(cboLopSV);
            LoadComboBoxLopSinhVien(cboTimKiemLopSV);
            BindingSinhVien();

            EnableControlSinhVien(false);
            btnTimKiemSinhVien.Enabled = true;
            btnThemSV.Enabled = true;
            txtTimKiemMSSV.Enabled = true;
            txtTimKiemTenSV.Enabled = true;
            cboTimKiemLopSV.Enabled = true;
            dgvShowSinhVien.Enabled = true;

            lblCanhBaoSV.Visible = false;
        }

        private void LoadDanhSachSinhVien()
        {
            SinhVien.DataSource = SinhVienDAO.Instance.LoadListDanhSachSinhVien();
            dgvShowSinhVien.Columns["ID Sinh Viên"].Visible = false;
            dgvShowSinhVien.Columns["ID Lớp"].Visible = false;
        }

        private void BindingSinhVien()
        {
            try
            {
                txtMaSoSinhVien.DataBindings.Add(new Binding("Text", dgvShowSinhVien.DataSource, "Mã Số Sinh Viên", true, DataSourceUpdateMode.Never));
                txtHovsTenLotSV.DataBindings.Add(new Binding("Text", dgvShowSinhVien.DataSource, "Họ và Tên Lót", true, DataSourceUpdateMode.Never));
                txtTenSinhVien.DataBindings.Add(new Binding("Text", dgvShowSinhVien.DataSource, "Tên", true, DataSourceUpdateMode.Never));
                txtGioiTinhSV.DataBindings.Add(new Binding("Text", dgvShowSinhVien.DataSource, "Giới Tính", true, DataSourceUpdateMode.Never));
                dtpNgaySinhSV.DataBindings.Add(new Binding("Text", dgvShowSinhVien.DataSource, "Ngày Sinh", true, DataSourceUpdateMode.Never));
                txtSoDienThoaiSinhVien.DataBindings.Add(new Binding("Text", dgvShowSinhVien.DataSource, "Số Điện Thoại", true, DataSourceUpdateMode.Never));
                txtEmailSV.DataBindings.Add(new Binding("Text", dgvShowSinhVien.DataSource, "Email", true, DataSourceUpdateMode.Never));
                txtDiaChiSV.DataBindings.Add(new Binding("Text", dgvShowSinhVien.DataSource, "Địa Chỉ", true, DataSourceUpdateMode.Never));
            }
            catch { }
        }

        private void LoadComboBoxLopSinhVien(ComboBox cbo)
        {
            try
            {
                cbo.DataSource = LopDAO.Instance.GetListLop();
                cbo.DisplayMember = "MALOP";
            }
            catch { }
        }

        private void EnableControlSinhVien(Boolean e)
        {
            txtTimKiemMSSV.Enabled = e;
            txtTimKiemTenSV.Enabled = e;
            cboTimKiemLopSV.Enabled = e;
            txtMaSoSinhVien.Enabled = e;
            txtHovsTenLotSV.Enabled = e;
            txtTenSinhVien.Enabled = e;
            txtGioiTinhSV.Enabled = e;
            dtpNgaySinhSV.Enabled = e;
            txtSoDienThoaiSinhVien.Enabled = e;
            txtEmailSV.Enabled = e;
            txtDiaChiSV.Enabled = e;
            cboLopSV.Enabled = e;

            btnTimKiemSinhVien.Enabled = e;
            btnThemSV.Enabled = e;
            btnCapNhatSV.Enabled = e;
            btnXoaSV.Enabled = e;
            btnBoQuaSV.Enabled = e;
            btnLuuSV.Enabled = e;
        }

        private void clearTextboxSinhVien()
        {
            txtTimKiemMSSV.Clear();
            txtTimKiemTenSV.Clear();
            txtMaSoSinhVien.Clear();
            txtHovsTenLotSV.Clear();
            txtTenSinhVien.Clear();
            txtGioiTinhSV.Clear();
            dtpNgaySinhSV.Value = DateTime.Today;
            txtSoDienThoaiSinhVien.Clear();
            txtEmailSV.Clear();
            txtDiaChiSV.Clear();
        }

        private Boolean checkThongTinSinhVien()
        {
            Boolean check = true;

            if (txtMaSoSinhVien.Text.Trim() == "")
            {
                errorRong.SetError(txtMaSoSinhVien, "Dữ liệu không được trống!!!");
                check = false;
                txtMaSoSinhVien.Focus();
                return check;
            }
            else { errorRong.Clear(); }

            if (txtHovsTenLotSV.Text.Trim() == "")
            {
                errorRong.SetError(txtHovsTenLotSV, "Dữ liệu không được trống!!!");
                check = false;
                txtHovsTenLotSV.Focus();
                return check;
            }
            else { errorRong.Clear(); }

            if (txtTenSinhVien.Text.Trim() == "")
            {
                errorRong.SetError(txtTenSinhVien, "Dữ liệu không được trống!!!");
                check = false;
                txtTenSinhVien.Focus();
                return check;
            }
            else { errorRong.Clear(); }

            if (txtGioiTinhSV.Text.Trim() == "")
            {
                errorRong.SetError(txtGioiTinhSV, "Dữ liệu không được trống!!!");
                check = false;
                txtGioiTinhSV.Focus();
                return check;
            }
            else { errorRong.Clear(); }

            if (txtSoDienThoaiSinhVien.Text.Trim() == "")
            {
                errorRong.SetError(txtSoDienThoaiSinhVien, "Dữ liệu không được trống!!!");
                check = false;
                txtSoDienThoaiSinhVien.Focus();
                return check;
            }
            else { errorRong.Clear(); }

            if (txtEmailSV.Text.Trim() == "")
            {
                errorRong.SetError(txtEmailSV, "Dữ liệu không được trống!!!");
                check = false;
                txtEmailSV.Focus();
                return check;
            }
            else { errorRong.Clear(); }

            if (txtDiaChiSV.Text.Trim() == "")
            {
                errorRong.SetError(txtDiaChiSV, "Dữ liệu không được trống!!!");
                check = false;
                txtDiaChiSV.Focus();
                return check;
            }
            else { errorRong.Clear(); }

            return check;
        }

        private void capNhatSinhVien(string mssv, string ho, string ten, string gioitinh, string ngaysinh, string sodienthoai, string email, string diachi, int idLop)
        {
            if (SinhVienDAO.Instance.UpdatetSinhVien(mssv, ho, ten, gioitinh, ngaysinh, sodienthoai, email, diachi, idLop))
            {
                MessageBox.Show("Cập nhật thông tin sinh viên thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin sinh viên thất bại");
            }
        }

        private void ThemSinhVien(string mssv, string ho, string ten, string gioitinh, string ngaysinh, string sodienthoai, string email, string diachi, int idLop)
        {
            if (SinhVienDAO.Instance.InsertSinhVien(mssv, ho, ten, gioitinh, ngaysinh, sodienthoai, email, diachi, idLop))
            {
                MessageBox.Show("Thêm thông tin sinh viên thành công");
            }
            else
            {
                MessageBox.Show("Thêm thông tin sinh viên thất bại");
            }
        }

        private void XoaSinhVien(string maSosinhVien)
        {
            if (SinhVienDAO.Instance.DeleteSinhVien(maSosinhVien))
            {
                MessageBox.Show("Xóa thông tin sinh viên thành công");
            }
            else
            {
                MessageBox.Show("Xóa thông tin sinh viên thất bại");
            }
        }

        private void timkiemsinhvien(string mssv, string ten, string malop)
        {
            if (mssv == "")
            {
                if (ten == "")
                {
                    SinhVien.DataSource = SinhVienDAO.Instance.timkiemMaLopSV(malop);
                    dgvShowSinhVien.Columns["ID Sinh Viên"].Visible = false;
                    dgvShowSinhVien.Columns["ID Lớp"].Visible = false;
                }
                else
                {
                    SinhVien.DataSource = SinhVienDAO.Instance.timkiemTenSV(ten, malop);
                    dgvShowSinhVien.Columns["ID Sinh Viên"].Visible = false;
                    dgvShowSinhVien.Columns["ID Lớp"].Visible = false;
                }
            }
            else
            {
                if (ten == "")
                {
                    SinhVien.DataSource = SinhVienDAO.Instance.timkiemMSSV(mssv, malop);
                    dgvShowSinhVien.Columns["ID Sinh Viên"].Visible = false;
                    dgvShowSinhVien.Columns["ID Lớp"].Visible = false;
                }
                else
                {
                    SinhVien.DataSource = SinhVienDAO.Instance.timkiemTCSV(mssv, ten, malop);
                    dgvShowSinhVien.Columns["ID Sinh Viên"].Visible = false;
                    dgvShowSinhVien.Columns["ID Lớp"].Visible = false;
                }
            }
        }

        #endregion

        #endregion

        #region Event

        #region Khóa học

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            EnableControlKhoaHoc(true);
            txtTKMKhoaHoc.Enabled = false;
            txtTKTenKH.Enabled = false;
            txtTKNamBD.Enabled = false;
            txtMaKhoaHoc.Focus();
            btnTimKiemKhoaHoc.Enabled = false;
            btnThemKH.Enabled = false;
            btnCapNhapKH.Enabled = false;
            btnXoaKH.Enabled = false;
            clearTextboxKhoaHoc();
            grpThongTinKH.Text = "Thêm thông tin khóa học";
            dgvShowKhoaHoc.Enabled = false;
            nudTGHToiThieu.Value = 1;
            nudTGHToiDa.Value = 1;
            nudTGHTieuChuan.Value = 1;
            lblCanhBaoKH.Visible = true;
            this.AcceptButton = btnLuuKhoaHoc;
        }

        private void btnCapNhapKH_Click(object sender, EventArgs e)
        {
            EnableControlKhoaHoc(false);
            btnLuuKhoaHoc.Enabled = true;
            btnBoQuaKhoaHoc.Enabled = true;
            btnTimKiemKhoaHoc.Enabled = false;
            txtTenKhoaHoc.Enabled = true;
            txtNamKetThuc.Enabled = true;
            nudTGHToiThieu.Enabled = true;
            nudTGHToiDa.Enabled = true;
            nudTGHTieuChuan.Enabled = true;
            dgvShowKhoaHoc.Enabled = false;
            grpThongTinKH.Text = "Cập nhật thông tin khóa học";
            lblCanhBaoKH.Visible = true;
            txtTKMKhoaHoc.Clear();
            txtTKTenKH.Clear();
            txtTKNamBD.Clear();
            txtTenKhoaHoc.Focus();
            this.AcceptButton = btnLuuKhoaHoc;
        }

        private void dgvShowKhoaHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EnableControlKhoaHoc(false);
            txtTKMKhoaHoc.Enabled = true;
            txtTKTenKH.Enabled = true;
            txtTKNamBD.Enabled = true;
            btnTimKiemKhoaHoc.Enabled = true;
            btnCapNhapKH.Enabled = true;
            btnXoaKH.Enabled = true;
            btnThemKH.Enabled = true;
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            string mkh = txtMaKhoaHoc.Text;
            if (mkh == "")
            {
                MessageBox.Show("Bạn chưa chọn dữ liệu để xóa.!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadKhoaHoc();
                return;
            }
            else
            {
                if (KhoaHocDAO.Instance.GetCountByMaKhoaHoc(mkh) < 1)
                {
                    MessageBox.Show("Dữ liệu bạn muốn xóa không có trong danh sách");
                    LoadKhoaHoc();
                    return;
                }
                else
                {
                    ///////////////////////////////////
                    int id = (int)dgvShowKhoaHoc.SelectedCells[0].OwningRow.Cells["ID Khóa Học"].Value;

                    if (LopDAO.Instance.GetCountLopByIDKhoaHoc(id) > 0 || HocKiDAO.Instance.GetCountByIdKhoaHoc(id) > 0)
                    {
                        MessageBox.Show("Bạn không thể xóa khóa học này vì còn tồn tại lớp hoặc học kì ở khóa học", "Lỗi thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoadKhoaHoc();
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("Bạn có chắc muốn xóa!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            LoadKhoaHoc();
                            return;
                        }
                        else
                        {
                            XoaKhoaHoc(mkh);
                            LoadKhoaHoc();
                            return;
                        }
                    }
                }
            }
        }

        private void btnLuuKhoaHoc_Click(object sender, EventArgs e)
        {
            string mkh = txtMaKhoaHoc.Text;
            string tkh = txtTenKhoaHoc.Text;
            int namBD = StringToInt(txtNamBatDau.Text);
            int namKT = StringToInt(txtNamKetThuc.Text);
            int tghtoithieu = StringToInt(nudTGHToiThieu.Value.ToString());
            int tghtoida = StringToInt(nudTGHToiDa.Value.ToString());
            int tghtieuchuan = StringToInt(nudTGHTieuChuan.Value.ToString());
            Boolean check = checkThongTinKhoahoc();
            if (check == false)
            {
                MessageBox.Show("Bạn chưa nhập đử thông tin.\nVui lòng nhập đủn thông tin trước khi lưu!!!", "Lỗi nhập thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (checkKTDB(mkh) == false)
                {
                    MessageBox.Show("Mã khóa học không được có ký tự đặt biệt.\nVui lòng kiểm tra lại!!!", "Lỗi nhập thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaKhoaHoc.Focus();
                    return;
                }
                else
                {
                    if (txtNamBatDau.TextLength != 4 || txtNamKetThuc.TextLength != 4)
                    {
                        MessageBox.Show("Thời gian bắt đầu thời gian kết thúc không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        // Thêm ////////////////////////////
                        if (btnLuuKhoaHoc.Enabled == true && grpThongTinKH.Text == "Thêm thông tin khóa học")
                        {
                            if (namKT <= namBD || (namKT - namBD) > tghtieuchuan)
                            {
                                MessageBox.Show("Lỗi thông tin thời gian nhập.\nVui lòng kiểm tra lại!!!", "Lỗi nhập thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtNamBatDau.Focus();
                                return;
                            }
                            else
                            {
                                if (KhoaHocDAO.Instance.GetCountByMaKhoaHoc(mkh) > 0)
                                {
                                    MessageBox.Show("Mã khóa học đã có trong danh sách.\nVui lòng kiểm tra lại!!!", "Lỗi nhập thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    errorTrung.SetError(txtMaKhoaHoc, "Dữ liệu không được trùng!!!");
                                    txtMaKhoaHoc.Focus();
                                    return;
                                }
                                else
                                {
                                    ThemKhoaHoc(mkh, tkh, namBD, namKT, tghtoithieu, tghtoida, tghtieuchuan);
                                    LoadData();
                                    txtTKMKhoaHoc.Focus();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            // Cập nhật ////////////////////
                            if (btnLuuKhoaHoc.Enabled == true && grpThongTinKH.Text == "Cập nhật thông tin khóa học")
                            {
                                if (namKT <= namBD || (namKT - namBD) > tghtieuchuan)
                                {
                                    MessageBox.Show("Lỗi thông tin thời gian nhập.\nVui lòng kiểm tra lại!!!", "Lỗi nhập thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtNamBatDau.Focus();
                                    return;
                                }
                                else
                                {
                                    if (KhoaHocDAO.Instance.GetCountByMaKhoaHoc(mkh) < 1)
                                    {
                                        MessageBox.Show("Dữ liệu cập nhật không tồn tại.");
                                        return;
                                    }
                                    else
                                    {
                                        if (mkh == "")
                                        {
                                            MessageBox.Show("Bạn chưa chọn dữ liệu muốn cập nhật");
                                            LoadKhoaHoc();
                                            return;
                                        }
                                        else
                                        {
                                            capnhatKhoaHoc(mkh, tkh, namKT, tghtoithieu, tghtoida, tghtieuchuan);
                                            errorRong.Clear();
                                            errorTrung.Clear();
                                            LoadData();
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

        private void txtNamBatDau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnTimKiemKhoaHoc_Click(object sender, EventArgs e)
        {
            string mkh = txtTKMKhoaHoc.Text;
            string tenkh = txtTKTenKH.Text;
            int namBD = StringToInt(txtTKNamBD.Text);
            btnBoQuaKhoaHoc.Enabled = true;
            timKiemKhoaHoc(mkh, tenkh, namBD);
            txtTKMKhoaHoc.Clear();
            txtTKTenKH.Clear();
            txtTKNamBD.Clear();
            btnCapNhapKH.Enabled = false;
            btnXoaKH.Enabled = false;
            return;
        }

        private void btnBoQuaKhoaHoc_Click(object sender, EventArgs e)
        {
            LoadData();
            txtTKMKhoaHoc.Focus();
            return;
        }

        #endregion

        #region Lớp

        private void txtMaLop_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvShowLop.SelectedCells.Count > 0)
                {
                    int id = (int)dgvShowLop.SelectedCells[0].OwningRow.Cells["ID Khóa Học"].Value;
                    KhoaHocDTO kh = KhoaHocDAO.Instance.GetKhoaHocById(id);
                    cboKHLop.SelectedItem = kh;
                    int index = -1;
                    int i = 0;
                    foreach (KhoaHocDTO item in cboKHLop.Items)
                    {
                        if (item.Id == kh.Id)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cboKHLop.SelectedIndex = index;
                }
            }
            catch { }
        }

        private void btnThemLop_Click(object sender, EventArgs e)
        {
            grpThongTinLop.Text = "Thêm thông tin lớp";
            EnableControlLop(false);
            txtNKNamBD.Clear();
            txtNKNamKT.Clear();
            txtMaLop.Enabled = true;
            txtTenLop.Enabled = true;
            txtMaLop.Clear();
            txtTenLop.Clear();
            cboKHLop.Enabled = true;
            btnLuuLop.Enabled = true;
            btnBoQuaLop.Enabled = true;
            dgvShowLop.Enabled = false;
            txtMaLop.Focus();
            this.AcceptButton = btnLuuLop;
        }

        private void btnBoQuaLop_Click(object sender, EventArgs e)
        {
            LoadData();
            txtTKMaLop.Focus();
            return;
        }

        private void btnLuuLop_Click(object sender, EventArgs e)
        {
            string malop = txtMaLop.Text;
            string tenlop = txtTenLop.Text;
            KhoaHocDTO kh = KhoaHocDAO.Instance.GetKhoaHocByMaKhoaHoc(cboKHLop.Text);
            int idkhoahoc = kh.Id;

            Boolean check = checkDuLieuLop();

            if (check == false)
            {
                MessageBox.Show("Dữ liệu của bạn không được để trống.\n Vui lòng kiểm tra lại", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (checkKTDB(malop) == false)
                {
                    MessageBox.Show("Mã lớp có chứa kí tự đặt biệt vui lòng kiểm tra lại", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaLop.Clear();
                    txtMaLop.Focus();
                    return;
                }
                else
                {
                    // Thêm Lớp
                    if (btnLuuLop.Enabled == true && grpThongTinLop.Text == "Thêm thông tin lớp")
                    {
                        if (LopDAO.Instance.GetCountByMaLop(malop) > 0)
                        {
                            MessageBox.Show("Mã lớp đã tồn tại.\n Vui lòng kiểm tra lại!!!", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            errorTrung.SetError(txtMaLop, "Lỗi trùng dữ liệu.");
                            txtMaLop.Clear();
                            txtMaLop.Focus();
                            return;
                        }
                        else
                        {
                            themLopHoc(malop, tenlop, idkhoahoc);
                            LoadData();
                            return;
                        }
                    }
                    else
                    {
                        // Cập nhật Lớp
                        if (btnLuuLop.Enabled == true && grpThongTinLop.Text == "Cập nhật thông tin lớp")
                        {
                            if (LopDAO.Instance.GetCountByMaLop(malop) < 1)
                            {
                                MessageBox.Show("Lớp mà bạn muốn cập nhật không tồn tại.");
                                return;
                            }
                            else
                            {
                                if (malop == "")
                                {
                                    MessageBox.Show("Bạn chưa chọn dữ liệu để cập nhật!!!");
                                    LoadLop();
                                    return;
                                }
                                else
                                {
                                    capNhatLop(malop, tenlop, idkhoahoc);
                                    LoadData();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnXoaLop_Click(object sender, EventArgs e)
        {
            string malop = txtMaLop.Text;
            if (txtMaLop.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn dữ liệu muốn xóa.\nVui lòng kiểm tra lại!!!", "Lỗi  khi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadLop();
                return;
            }
            else
            {
                if (LopDAO.Instance.GetCountByMaLop(malop) < 1)
                {
                    MessageBox.Show("Dữ liệu bạn muốn xóa không có trong danh sách");
                    LoadLop();
                    return;
                }
                else
                {
                    if (MessageBox.Show("Bạn có chắc muốn xóa dữ liệu này!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        LoadLop();
                        return;
                    }
                    else
                    {
                        xoaLop(malop);
                        LoadData();
                        return;
                    }
                }
            }
        }

        private void btnTKLop_Click(object sender, EventArgs e)
        {
            string malop = txtTKMaLop.Text;
            string maKhoaHoc = cboTKKhoaHocLop.Text;
            timkiemLop(malop, maKhoaHoc);
            btnBoQuaLop.Enabled = true;
            txtTKMaLop.Clear();
            btnCapNhatLop.Enabled = false;
            btnXoaLop.Enabled = false;
            return;
        }

        private void dgvShowLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EnableControlLop(false);
            txtTKMaLop.Enabled = true;
            cboTKKhoaHocLop.Enabled = true;
            btnCapNhatLop.Enabled = true;
            btnXoaLop.Enabled = true;
            btnThemLop.Enabled = true;
        }

        private void btnCapNhatLop_Click(object sender, EventArgs e)
        {
            EnableControlLop(false);
            txtNKNamBD.Clear();
            txtNKNamKT.Clear();
            txtTenLop.Enabled = true;
            dtpTGBatDauHK.Enabled = true;
            dtpTGKetThucHK.Enabled = true;
            btnBoQuaLop.Enabled = true;
            btnLuuLop.Enabled = true;
            btnTKLop.Enabled = true;
            grpThongTinLop.Text = "Cập nhật thông tin lớp";
            dgvShowLop.Enabled = false;
            txtTenLop.Focus();
            this.AcceptButton = btnLuuLop;
        }

        #endregion

        #region Học Phần

        private void btnLuuHP_Click(object sender, EventArgs e)
        {
            string mahp = txtMaHocPhan.Text;
            string tenhp = txtTenHocPhan.Text;
            KhoaHocDTO kh = KhoaHocDAO.Instance.GetKhoaHocByMaKhoaHoc(cboKhoaHocHP.Text);
            HocKiDTO hk = HocKiDAO.Instance.GetHocKiByMaHocKi(cboHocKiHocPhan.Text);
            int idkh = kh.Id;
            int idhk = hk.Id;

            Boolean check = checkDuHocPhan();

            if (check == false)
            {
                MessageBox.Show("Dữ liệu của bạn không được để trống.\nVui lòng kiểm tra lại.!!!", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (checkKTDB(mahp) == false)
                {
                    MessageBox.Show("Mã học phần của bạn không được chưa ký tự đặt biệt", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaHocPhan.Clear();
                    txtMaHocPhan.Focus();
                    return;
                }
                else
                {
                    // Thêm thông tin học phần
                    if (btnLuuHP.Enabled == true && grpThongTinHocPhan.Text == "Thêm thông tin học phần")
                    {
                        if (kh.Id != hk.Idkhoahoc)
                        {
                            MessageBox.Show("Học kì bạn chọn không nằm trong khóa học bạn chọn.\nVui lòng kiểm tra lại", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            if (HocPhanDAO.Instance.GetCountByMaHocPhan(mahp) > 0)
                            {
                                MessageBox.Show("Mã học phần của bạn đã có trong danh sách.\nVui lòng kiểm tra lại", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtMaHocPhan.Focus();
                                return;
                            }
                            else
                            {
                                ThemHocPhan(mahp, tenhp, idhk, idkh);
                                LoadData();
                                return;
                            }
                        }
                    }
                    else
                    {
                        // Cập nhật thông tin học phần
                        if (btnLuuHP.Enabled == true && grpThongTinHocPhan.Text == "Cập nhật thông tin học phần")
                        {
                            if (mahp == "")
                            {
                                MessageBox.Show("Bạn chưa chọn dữ liệu cập nhật");
                                LoadHocPhan();
                                return;
                            }
                            else
                            {
                                if (kh.Id != hk.Idkhoahoc)
                                {
                                    MessageBox.Show("Học kì bạn chọn không nằm trong khóa học bạn chọn.\nVui lòng kiểm tra lại", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                else
                                {
                                    if (HocPhanDAO.Instance.GetCountByMaHocPhan(mahp) < 1)
                                    {
                                        MessageBox.Show("Dữ liệu muốn cập nhật không có trong danh sách.");
                                        LoadHocPhan();
                                    }
                                    else
                                    {
                                        CapNhatHocPhan(mahp, tenhp, idhk, idkh);
                                        LoadData();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnXoaHP_Click(object sender, EventArgs e)
        {
            string mahp = txtMaHocPhan.Text;

            if (mahp == "")
            {
                MessageBox.Show("Bạn chưa chon dữ liệu để xóa.");
                LoadHocPhan();
                return;
            }
            else
            {
                if (HocPhanDAO.Instance.GetCountByMaHocPhan(mahp) < 1)
                {
                    MessageBox.Show("Dữ liệu muốn xóa không có trong danh sách");
                    LoadHocPhan();
                    return;
                }
                else
                {
                    if (MessageBox.Show("Bạn có chắc muốn xóa.?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        LoadHocPhan();
                        return;
                    }
                    else
                    {
                        XoaHocPhan(mahp);
                        LoadData();
                        return;
                    }
                }
            }
        }

        private void btnThemHP_Click(object sender, EventArgs e)
        {
            EnableControlHocPhan(false);
            btnBoQuaHP.Enabled = true;
            btnLuuHP.Enabled = true;
            txtMaHocPhan.Enabled = true;
            txtTenHocPhan.Enabled = true;
            dtpTGBatDauHP.Value = DateTime.Today;
            dtpTGKetThucHP.Value = DateTime.Today;
            cboHocKiHocPhan.Enabled = true;
            cboKhoaHocHP.Enabled = true;

            txtTKMaHocPhan.Clear();
            txtMaHocPhan.Clear();
            txtTenHocPhan.Clear();
            txtMaHocPhan.Focus();
            dgvShowHocPhan.Enabled = false;
            this.AcceptButton = btnLuuHP;
            grpThongTinHocPhan.Text = "Thêm thông tin học phần";
        }

        private void btnBoQuaHP_Click(object sender, EventArgs e)
        {
            LoadData();
            return;
        }

        private void btnCapNhatHP_Click(object sender, EventArgs e)
        {
            EnableControlHocPhan(false);
            btnBoQuaHP.Enabled = true;
            btnLuuHP.Enabled = true;
            txtTenHocPhan.Enabled = true;
            dtpTGBatDauHP.Value = DateTime.Today;
            dtpTGKetThucHP.Value = DateTime.Today;
            cboHocKiHocPhan.Enabled = true;
            cboKhoaHocHP.Enabled = true;

            txtTKMaHocPhan.Clear();
            txtTenHocPhan.Focus();
            dgvShowHocPhan.Enabled = false;
            this.AcceptButton = btnLuuHP;
            grpThongTinHocPhan.Text = "Cập nhật thông tin học phần";
        }

        private void dgvShowHocPhan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnCapNhatHP.Enabled = true;
            btnXoaHP.Enabled = true;
        }

        private void txtMaHocPhan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvShowHocPhan.SelectedCells.Count > 0)
                {
                    string idkh = dgvShowHocPhan.SelectedCells[0].OwningRow.Cells["Mã Khóa Học"].Value.ToString();
                    KhoaHocDTO kh = KhoaHocDAO.Instance.GetKhoaHocByMaKhoaHoc(idkh);

                    string mahk = dgvShowHocPhan.SelectedCells[0].OwningRow.Cells["Mã Học Kì"].Value.ToString();
                    HocKiDTO hk = HocKiDAO.Instance.GetHocKiByMaHocKi(mahk);

                    cboKhoaHocHP.SelectedItem = kh;
                    int index = -1;
                    int i = 0;
                    foreach (KhoaHocDTO item in cboKhoaHocHP.Items)
                    {
                        if (item.Id == kh.Id)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cboKhoaHocHP.SelectedIndex = index;

                    cboHocKiHocPhan.SelectedItem = hk;
                    int Index = -1;
                    int I = 0;
                    foreach (HocKiDTO item in cboHocKiHocPhan.Items)
                    {
                        if (item.Id == hk.Id)
                        {
                            Index = I;
                            break;
                        }
                        I++;
                    }
                    cboHocKiHocPhan.SelectedIndex = Index;
                }
            }
            catch { }
        }

        private void btnTimKiemHP_Click(object sender, EventArgs e)
        {
            string maHocPhan = txtTKMaHocPhan.Text;
            string maHocKi = cboTimKiemHocKiHocPhan.Text;
            string maKhoaHoc = cboTKKhoaHocHP.Text;
            btnBoQuaHP.Enabled = true;
            timKiemHocPhan(maHocPhan, maHocKi, maKhoaHoc);
            txtTKMaHocPhan.Clear();
            btnCapNhatHP.Enabled = false;
            btnXoaHP.Enabled = false;
            return;
        }

        #endregion

        #region Học Kì

        private void btnThemHocKi_Click(object sender, EventArgs e)
        {
            EnableControlHocKi(false);
            ClearTextboxHocKi();
            btnLuuHocKi.Enabled = true;
            btnBoQuaHocKi.Enabled = true;
            txtMaHocKi.Enabled = true;
            txtTenHocKi.Enabled = true;
            dtpTGBatDauHK.Enabled = true;
            dtpTGKetThucHK.Enabled = true;
            cboKhoaHocHK.Enabled = true;
            btnThemHocKi.Enabled = false;
            grpThongTinHocKi.Text = "Thêm thông tin học kì";
            txtMaHocKi.Focus();
            this.AcceptButton = btnLuuHocKi;
            dgvShowHocKi.Enabled = false;
        }

        private void btnLuuHocKi_Click(object sender, EventArgs e)
        {
            string mahk = txtMaHocKi.Text;
            string tenhk = txtTenHocKi.Text;
            string tghbd = dtpTGBatDauHK.Value.Date.ToString("MM/dd/yyyy");
            string tghkt = dtpTGKetThucHK.Value.Date.ToString("MM/dd/yyyy");
            KhoaHocDTO kh = KhoaHocDAO.Instance.GetKhoaHocByMaKhoaHoc(cboKhoaHocHK.Text);

            Boolean check = checkDuLieuHocKi();

            if (check == false)
            {
                MessageBox.Show("Dữ liệu nhập không được trống.\nVui lòng kiểm tra lại.", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (checkKTDB(mahk) == false)
                {
                    MessageBox.Show("Mã khóa học của bạn có chứa kí tự đặt biệt.\nVui lòng kiểm tra lại!!!", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaHocKi.Clear();
                    txtMaHocKi.Focus();
                    return;
                }
                else
                {
                    if (dtpTGKetThucHK.Value < dtpTGBatDauHK.Value)
                    {
                        MessageBox.Show("Lỗi thông tin thời gian.\nVui lòng kiểm tra lại!!!", "Lối nhập thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dtpTGBatDauHK.Focus();
                        return;
                    }
                    else
                    {
                        // Thêm /////////////////////////
                        if (btnLuuHocKi.Enabled = true && grpThongTinHocKi.Text == "Thêm thông tin học kì")
                        {
                            if (HocKiDAO.Instance.GetCountByMaHocKi(mahk) > 0)
                            {
                                MessageBox.Show("Mã khóa học đã có trong danh sách.\nVui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                LoadHocKi();
                                return;
                            }
                            else
                            {
                                ThemHocKi(mahk, tenhk, tghbd, tghkt, kh.Id);
                                LoadData();
                                return;
                            }
                        }
                        else
                        {
                            // Cập nhật /////////////////////////
                            if (btnLuuHocKi.Enabled = true && grpThongTinHocKi.Text == "Cập nhật thông tin học kì")
                            {
                                if (HocKiDAO.Instance.GetCountByMaHocKi(mahk) < 1)
                                {
                                    MessageBox.Show("Dữ liệu muốn cập nhật không tồn tại.");
                                    return;
                                }
                                else
                                {
                                    if (mahk == "")
                                    {
                                        MessageBox.Show("Bạn chưa chọn dữ liệu cập nhật.");
                                        LoadHocKi();
                                        return;
                                    }
                                    else
                                    {
                                        capNhatHocKi(mahk, tenhk, tghbd, tghkt,kh.Id);
                                        LoadData();
                                        return;
                                    }
                                }
                            }
                        }
                        return;
                    }
                }
            }
        }

        private void btnBoQuaHocKi_Click(object sender, EventArgs e)
        {
            LoadHocKi();
            txtTKMaHocKi.Focus();
            return;
        }

        private void btnCapNhatHocKi_Click(object sender, EventArgs e)
        {
            EnableControlHocKi(false);
            cboKhoaHocHK.Enabled = true;
            btnThemHocKi.Enabled = false;
            btnLuuHocKi.Enabled = true;
            btnBoQuaHocKi.Enabled = true;
            txtTenHocKi.Enabled = true;
            dtpTGBatDauHK.Enabled = true;
            dtpTGKetThucHK.Enabled = true;
            grpThongTinHocKi.Text = "Cập nhật thông tin học kì";
            txtTenHocKi.Focus();
            this.AcceptButton = btnLuuHocKi;
            dgvShowHocKi.Enabled = false;
        }

        private void dgvShowHocKi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EnableControlHocKi(false);
            txtTKMaHocKi.Enabled = true;
            cboTKKhoaHocHK.Enabled = true;
            btnCapNhatHocKi.Enabled = true;
            btnXoaHocKi.Enabled = true;
            btnThemHocKi.Enabled = false;
            btnTimKiemHK.Enabled = true;
            btnThemHocKi.Enabled = true;
        }

        private void btnXoaHocKi_Click(object sender, EventArgs e)
        {
            string mahocki = txtMaHocKi.Text;
            if (mahocki == "")
            {
                MessageBox.Show("Bạn chưa chọn dữ liệu để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadHocKi();
                return;
            }
            else
            {
                if (HocKiDAO.Instance.GetCountByMaHocKi(mahocki) < 1)
                {
                    MessageBox.Show("Dữ liệu bạn muốn xóa không có trong danh sách");
                    LoadHocKi();
                    return;
                }
                else
                {
                    int id = (int)dgvShowHocKi.SelectedCells[0].OwningRow.Cells["ID Học kì"].Value;

                    if (HocPhanDAO.Instance.GetCountByIdHocKi(id) > 0)
                    {
                        MessageBox.Show("Bạn không thể xóa khi có học phần còn trong học kì này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("Bạn có chứa muốn xóa dữ liệu này", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            LoadHocKi();
                            return;
                        }
                        else
                        {
                            XoaHocKi(mahocki);
                            LoadData();
                            return;
                        }
                    }
                }
            }
        }

        private void btnTimKiemHK_Click(object sender, EventArgs e)
        {
            string makh = txtTKMaHocKi.Text;
            KhoaHocDTO kh = KhoaHocDAO.Instance.GetKhoaHocByMaKhoaHoc(cboTKKhoaHocHK.Text);
            int idkhoahoc = kh.Id;
            btnBoQuaHocKi.Enabled = true;
            timkiemhocki(makh, idkhoahoc);
            btnCapNhatHocKi.Enabled = false;
            btnXoaHocKi.Enabled = false;
            return;
        }

        private void txtMaHocKi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvShowHocKi.SelectedCells.Count > 0)
                {
                    int id = (int)dgvShowHocKi.SelectedCells[0].OwningRow.Cells["ID Khóa Học"].Value;
                    KhoaHocDTO kh = KhoaHocDAO.Instance.GetKhoaHocById(id);
                    cboKhoaHocHK.SelectedItem = kh;
                    int index = -1;
                    int i = 0;
                    foreach (KhoaHocDTO item in cboKHLop.Items)
                    {
                        if (item.Id == kh.Id)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cboKhoaHocHK.SelectedIndex = index;
                }
            }
            catch { }
        }

        #endregion

        #region Sinh Viên

        private void txtMaSoSinhVien_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvShowSinhVien.SelectedCells.Count > 0)
                {
                    int id = (int)dgvShowSinhVien.SelectedCells[0].OwningRow.Cells["ID Lớp"].Value;
                    LopDTO lop = LopDAO.Instance.GetLopById(id);
                    cboLopSV.SelectedItem = lop;
                    int index = -1;
                    int i = 0;
                    foreach (LopDTO item in cboLopSV.Items)
                    {
                        if (item.Id == lop.Id)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cboLopSV.SelectedIndex = index;
                }
            }
            catch { }
        }

        private void btnLuuSV_Click(object sender, EventArgs e)
        {
            string mssv = txtMaSoSinhVien.Text;
            string ho = txtHovsTenLotSV.Text;
            string ten = txtTenSinhVien.Text;
            string gt = txtGioiTinhSV.Text;
            string ngaysinh = dtpNgaySinhSV.Value.ToString("MM/dd/yyyy");
            string sodienthoai = txtSoDienThoaiSinhVien.Text;
            string email = txtEmailSV.Text;
            string diachi = txtDiaChiSV.Text;
            LopDTO lop = LopDAO.Instance.GetLopByMaLop(cboLopSV.Text);

            Boolean check = checkThongTinSinhVien();

            if (check == false)
            {
                MessageBox.Show("Dữ liệu nhập không được trống.\nVui lòng kiểm tra lại.", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (checkKTDB(mssv) == false)
                {
                    MessageBox.Show("Mã số sinh viên có chứa kí tự đặt biệt.\nVui lòng kiểm tra lại!!!", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaSoSinhVien.Clear();
                    txtMaSoSinhVien.Focus();
                    return;
                }
                else
                {
                    if (checkNamNu(gt) == false)
                    {
                        MessageBox.Show("Kí tự giới tính không hợp lệ", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtGioiTinhSV.Clear();
                        txtGioiTinhSV.Focus();
                        return;
                    }
                    else
                    {
                        if (dtpNgaySinhSV.Value >= DateTime.Today || (DateTime.Today.Year - dtpNgaySinhSV.Value.Year) < 18)
                        {
                            MessageBox.Show("Ngày sinh không hợp lệ", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dtpNgaySinhSV.Value = DateTime.Today;
                            dtpNgaySinhSV.Focus();
                            return;
                        }
                        else
                        {
                            if (txtSoDienThoaiSinhVien.Text != "")
                            {
                                if (txtSoDienThoaiSinhVien.TextLength != 10 && txtSoDienThoaiSinhVien.TextLength != 11)
                                {
                                    MessageBox.Show("Số điện thoại không hợp lệ", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtSoDienThoaiSinhVien.Clear();
                                    txtSoDienThoaiSinhVien.Focus();
                                    return;
                                }
                                else
                                {
                                    //Thêm thông tin sinh viên
                                    if (btnLuuSV.Enabled == true && grpThongTinSinhVien.Text == "Thêm thông tin sinh viên")
                                    {
                                        if (SinhVienDAO.Instance.GetCountByMaSinhVien(mssv) > 0)
                                        {
                                            MessageBox.Show("Mã số sinh viên đã tồn tại trong danh sách vui lòng kiểm tra lại.\nVui lòng kiểm tra lại!!!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            errorTrung.SetError(txtMaSoSinhVien, "Dữ liệu không được trùng");
                                            txtMaSoSinhVien.Focus();
                                            return;
                                        }
                                        else
                                        {
                                            ThemSinhVien(mssv, ho, ten, gt, ngaysinh, sodienthoai, email, diachi, lop.Id);
                                            LoadData();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        //Cập nhật thông tin sinh viên
                                        if (btnLuuSV.Enabled == true && grpThongTinSinhVien.Text == "Cập nhật thông tin sinh viên")
                                        {
                                            if (mssv == "")
                                            {
                                                MessageBox.Show("Bạn chưa chọn sinh viên muốn cập nhật");
                                                LoadSinhVien();
                                                return;
                                            }
                                            else
                                            {
                                                if (SinhVienDAO.Instance.GetCountByMaSinhVien(mssv) < 1)
                                                {
                                                    MessageBox.Show("Sinh viên không tồn tại trong danh sách");
                                                    LoadSinhVien();
                                                    return;
                                                }
                                                else
                                                {
                                                    capNhatSinhVien(mssv, ho, ten, gt, ngaysinh, sodienthoai, email, diachi, lop.Id);
                                                    LoadData();
                                                    return;
                                                }
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

        private void btnXoaSV_Click(object sender, EventArgs e)
        {
            string mssv = txtMaSoSinhVien.Text;
            if (mssv == "")
            {
                MessageBox.Show("Bạn chưa chọn dữ liệu để xóa");
                return;
            }
            else
            {
                XoaSinhVien(mssv);
                LoadData();
                return;
            }
        }

        private void btnTimKiemSinhVien_Click(object sender, EventArgs e)
        {
            string mssv = txtTimKiemMSSV.Text;
            string ten = txtTimKiemTenSV.Text;
            string malop = cboTimKiemLopSV.Text;
            timkiemsinhvien(mssv, ten, malop);
            btnBoQuaSV.Enabled = true;
            txtTimKiemMSSV.Clear();
            txtTimKiemTenSV.Clear();
            txtTimKiemMSSV.Focus();
        }

        private void btnThemSV_Click(object sender, EventArgs e)
        {
            clearTextboxSinhVien();
            txtMaSoSinhVien.Focus();
            EnableControlSinhVien(true);
            txtTimKiemMSSV.Enabled = false;
            txtTimKiemTenSV.Enabled = false;
            cboTimKiemLopSV.Enabled = false;
            btnTimKiemSinhVien.Enabled = false;
            btnCapNhatSV.Enabled = false;
            btnXoaSV.Enabled = false;
            btnThemSV.Enabled = false;
            lblCanhBaoSV.Visible = true;
            grpThongTinSinhVien.Text = "Thêm thông tin sinh viên";
            txtMaSoSinhVien.Focus();
            this.AcceptButton = btnLuuSV;
            dgvShowSinhVien.Enabled = false;
        }

        private void btnBoQuaSV_Click(object sender, EventArgs e)
        {
            LoadSinhVien();
            txtTimKiemMSSV.Focus();
            return;
        }

        private void btnCapNhatSV_Click(object sender, EventArgs e)
        {
            txtMaSoSinhVien.Focus();
            EnableControlSinhVien(true);
            txtMaSoSinhVien.Enabled = false;
            txtTimKiemMSSV.Enabled = false;
            txtTimKiemTenSV.Enabled = false;
            cboTimKiemLopSV.Enabled = false;
            btnTimKiemSinhVien.Enabled = false;
            btnCapNhatSV.Enabled = false;
            btnXoaSV.Enabled = false;
            btnCapNhatSV.Enabled = false;
            lblCanhBaoSV.Visible = true;
            grpThongTinSinhVien.Text = "Cập nhật thông tin sinh viên";
            txtMaSoSinhVien.Focus();
            this.AcceptButton = btnLuuSV;
            dgvShowSinhVien.Enabled = false;
        }

        private void dgvShowSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EnableControlSinhVien(false);
            txtTimKiemMSSV.Enabled = true;
            txtTimKiemTenSV.Enabled = true;
            cboTimKiemLopSV.Enabled = true;
            btnTimKiemSinhVien.Enabled = true;
            btnThemSV.Enabled = true;
            btnCapNhatSV.Enabled = true;
            btnXoaSV.Enabled = true;
        }

        #endregion

        private void tabQuanLy_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (btnLuuKhoaHoc.Enabled == true && (tabQuanLy.SelectedTab == tabLop || tabQuanLy.SelectedTab == tabHocPhan || tabQuanLy.SelectedTab == tabHocKi || tabQuanLy.SelectedTab == tabSinhVien))
            {
                tabQuanLy.SelectedTab = tabKhoaHoc;
                MessageBox.Show("Bạn chưa hoàn thành xong công việc.\nHãy hoàn thành hoặc bỏ qua công việc hiện tại để rời khỏi tab!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (btnLuuLop.Enabled == true && (tabQuanLy.SelectedTab == tabKhoaHoc || tabQuanLy.SelectedTab == tabHocPhan || tabQuanLy.SelectedTab == tabHocKi || tabQuanLy.SelectedTab == tabSinhVien))
                {
                    tabQuanLy.SelectedTab = tabLop;
                    MessageBox.Show("Bạn chưa hoàn thành xong công việc.\nHãy hoàn thành hoặc bỏ qua công việc hiện tại để rời khỏi tab!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }
                else
                {
                    if (btnLuuHP.Enabled == true && (tabQuanLy.SelectedTab == tabKhoaHoc || tabQuanLy.SelectedTab == tabLop || tabQuanLy.SelectedTab == tabHocKi || tabQuanLy.SelectedTab == tabSinhVien))
                    {
                        tabQuanLy.SelectedTab = tabHocPhan;
                        MessageBox.Show("Bạn chưa hoàn thành xong công việc.\nHãy hoàn thành hoặc bỏ qua công việc hiện tại để rời khỏi tab!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;

                    }
                    else
                    {
                        if (btnLuuHocKi.Enabled == true && (tabQuanLy.SelectedTab == tabKhoaHoc || tabQuanLy.SelectedTab == tabLop || tabQuanLy.SelectedTab == tabHocPhan || tabQuanLy.SelectedTab == tabSinhVien))
                        {
                            tabQuanLy.SelectedTab = tabHocKi;
                            MessageBox.Show("Bạn chưa hoàn thành xong công việc.\nHãy hoàn thành hoặc bỏ qua công việc hiện tại để rời khỏi tab!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;

                        }
                        else
                        {
                            if (btnLuuSV.Enabled == true && (tabQuanLy.SelectedTab == tabKhoaHoc || tabQuanLy.SelectedTab == tabLop || tabQuanLy.SelectedTab == tabHocPhan || tabQuanLy.SelectedTab == tabHocKi))
                            {
                                tabQuanLy.SelectedTab = tabSinhVien;
                                MessageBox.Show("Bạn chưa hoàn thành xong công việc.\nHãy hoàn thành hoặc bỏ qua công việc hiện tại để rời khỏi tab!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;

                            }
                        }
                    }
                }
            }
        }

        #endregion

        private void EditSinhVien()
        {
            if (mssv != "")
            {
                tabQuanLy.SelectedTab = tabSinhVien;
                btnCapNhatSV_Click(new object[] { }, null);
                btnThemSV.Enabled = false;
                dgvShowSinhVien.DataSource = SinhVienDAO.Instance.GetSinhVienbyMSSV(mssv);
                return;
            }
            else
            {
                LoadSinhVien();
            }
        }
    }
}