using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DAO;
using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE
{
    public partial class frmQuanLyDiem : Form
    {
        public string userName = frmMain.TenTaiKhoan;

        public static string _mssv = "";
        public static string _maLop = "";
        public static string _maKhoaHoc = "";
        public static string _NhanBiet = "";
        BindingSource KhoaHoc = new BindingSource();
        BindingSource SinhVien = new BindingSource();
        BindingSource Lop = new BindingSource();

        public frmQuanLyDiem()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgvShowKhoaHoc.DataSource = KhoaHoc;
            LoadDSKhoaHoc();
            LoadComboBoxKhoaHoc(cboTKKhoaHoc);
            TaiKhoan tk = TaiKhoanDAO.Instance.GetTaiKhoanByTenTaiKhoan(userName);

            if (tk.Idquyen == 1)
            {
                return;
            }
            else
            {
                if (tk.Idquyen == 2)
                {
                    btnNhapDiem.Enabled = false;
                    btnCapNhatDiem.Enabled = false;
                    return;
                }
                else
                {
                    if (tk.Idquyen == 3)
                    {
                        btnNhapDiem.Enabled = false;
                        btnCapNhatDiem.Enabled = false;
                        return;
                    }
                    else
                    {
                        if (tk.Idquyen == 4)
                        {
                            btnChinhSua.Enabled = false;
                            return;
                        }
                    }
                }
            }
        }

        private void LoadDSKhoaHoc()
        {
            KhoaHoc.DataSource = KhoaHocDAO.Instance.LoadListKhoaHoc();
            dgvShowKhoaHoc.Columns["ID Khóa Học"].Visible = false;
        }

        private void LoadComboBoxKhoaHoc(ComboBox cbo)
        {
            cbo.DataSource = KhoaHocDAO.Instance.LoadListKhoaHoc();
            cbo.DisplayMember = "Mã Khóa Học";
        }

        private void BindingSinhVien()
        {
            try
            {
                if (dgvShowDanhSachSinhVien.SelectedRows.Count > 0)
                {
                    txtMSSV.DataBindings.Add(new Binding("Text", dgvShowDanhSachSinhVien.DataSource, "Mã Số Sinh Viên", true, DataSourceUpdateMode.Never));
                    txtHoSV.DataBindings.Add(new Binding("Text", dgvShowDanhSachSinhVien.DataSource, "Họ và Tên Lót", true, DataSourceUpdateMode.Never));
                    txtTenSV.DataBindings.Add(new Binding("Text", dgvShowDanhSachSinhVien.DataSource, "Tên", true, DataSourceUpdateMode.Never));
                    txtGioiTinhSV.DataBindings.Add(new Binding("Text", dgvShowDanhSachSinhVien.DataSource, "Giới Tính", true, DataSourceUpdateMode.Never));
                    txtSoDienThoaiSV.DataBindings.Add(new Binding("Text", dgvShowDanhSachSinhVien.DataSource, "Số Điện Thoại", true, DataSourceUpdateMode.Never));
                    txtEmailSV.DataBindings.Add(new Binding("Text", dgvShowDanhSachSinhVien.DataSource, "Email", true, DataSourceUpdateMode.Never));
                    txtDiaChiSV.DataBindings.Add(new Binding("Text", dgvShowDanhSachSinhVien.DataSource, "Địa Chỉ", true, DataSourceUpdateMode.Never));
                    dtpNgaySinhSV.DataBindings.Add(new Binding("Value", dgvShowDanhSachSinhVien.DataSource, "Ngày Sinh", true, DataSourceUpdateMode.Never));

                }
            }
            catch { }
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            _mssv = txtMSSV.Text;
            LoadData();
            frmQuanLy f = new frmQuanLy();
            f.ShowDialog();
            _mssv = "";
            LoadData();
        }

        private void dgvShowKhoaHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvShowDanhSachLop.DataSource = Lop;
            _maKhoaHoc = dgvShowKhoaHoc.SelectedCells[0].OwningRow.Cells["Mã Khóa Học"].Value.ToString();
            try
            {
                if (dgvShowKhoaHoc.SelectedCells.Count > 0)
                {
                    string makhoahoc = dgvShowKhoaHoc.SelectedCells[0].OwningRow.Cells["Mã Khóa Học"].Value.ToString();
                    if (makhoahoc != "")
                    {
                        Lop.DataSource = LopDAO.Instance.LoadListLopByMaKhoaHoc(makhoahoc);
                        dgvShowDanhSachLop.Columns["ID Lớp"].Visible = false;
                        dgvShowDanhSachLop.Columns["ID Khóa Học"].Visible = false;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch { }
        }

        private void dgvShowDanhSachLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvShowDanhSachSinhVien.DataSource = SinhVien;
            try
            {
                if (dgvShowDanhSachLop.SelectedCells.Count > 0)
                {
                    string malop = dgvShowDanhSachLop.SelectedCells[0].OwningRow.Cells["Mã Lớp"].Value.ToString();
                    _maLop = malop;
                    if (malop != "")
                    {
                        SinhVien.DataSource = SinhVienDAO.Instance.LoadListDanhSachSinhVienQLD(malop);
                        dgvShowDanhSachSinhVien.Columns["ID Lớp"].Visible = false;
                        dgvShowDanhSachSinhVien.Columns["ID Sinh Viên"].Visible = false;
                        BindingSinhVien();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch { }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            LoadData();
            dgvShowDanhSachLop.DataSource = null;
            dgvShowDanhSachSinhVien.DataSource = null;
            _maKhoaHoc = "";
            _maLop = "";
        }

        private void timkiemKhoaHoc(string cboKhoaHoc, string textKhoaHoc)
        {
            if (textKhoaHoc == "")
            {
                dgvShowKhoaHoc.DataSource = KhoaHocDAO.Instance.timKiemMKH(cboKhoaHoc);
                dgvShowKhoaHoc.Columns["ID Khóa Học"].Visible = false;
                return;
            }
            else
            {
                dgvShowKhoaHoc.DataSource = KhoaHocDAO.Instance.timKiemMKH(textKhoaHoc);
                dgvShowKhoaHoc.Columns["ID Khóa Học"].Visible = false;
                return;
            }
        }

        private void btnOKTKKhoaHoc_Click(object sender, EventArgs e)
        {
            string cboTKkhoaHoc = cboTKKhoaHoc.Text;
            string txttimkhoahoc = txtTKkhoaHoc.Text;
            timkiemKhoaHoc(cboTKkhoaHoc, txttimkhoahoc);
            txtTKkhoaHoc.Clear();
        }

        private void btnCancelTKKhoaHoc_Click(object sender, EventArgs e)
        {
            dgvShowKhoaHoc.DataSource = KhoaHocDAO.Instance.LoadListKhoaHoc();
            dgvShowKhoaHoc.Columns["ID Khóa Học"].Visible = false;
        }

        private void btnOKTKLop_Click(object sender, EventArgs e)
        {
            string maLop = txtTKLop.Text;
            if (maLop == "")
            {
                return;
            }
            else
            {
                dgvShowDanhSachLop.DataSource = LopDAO.Instance.timKiemTheoMaLop(maLop);
            }
            txtTKLop.Clear();
        }

        private void btnOKSVien_Click(object sender, EventArgs e)
        {
            string maSo = txtTKMSSV.Text;
            string ten = txtTKTenSV.Text;

            timKiemSinhVien(maSo, ten);
        }

        private void timKiemSinhVien(string maSo, string ten)
        {
            if (maSo == "")
            {
                if (ten == "")
                {
                    dgvShowDanhSachSinhVien.DataSource = null;
                    return;
                }
                else
                {
                    dgvShowDanhSachSinhVien.DataSource = SinhVienDAO.Instance.timkiemTenSVQLD(ten);
                    dgvShowDanhSachSinhVien.Columns["ID Lớp"].Visible = false;
                    dgvShowDanhSachSinhVien.Columns["ID Sinh Viên"].Visible = false;
                }
            }
            else
            {
                if (ten == "")
                {
                    dgvShowDanhSachSinhVien.DataSource = SinhVienDAO.Instance.timkiemMSSVQLD(maSo);
                    dgvShowDanhSachSinhVien.Columns["ID Lớp"].Visible = false;
                    dgvShowDanhSachSinhVien.Columns["ID Sinh Viên"].Visible = false;
                }
                else
                {
                    dgvShowDanhSachSinhVien.DataSource = SinhVienDAO.Instance.timkiemMaSovsTenSVQLD(maSo, ten);
                    dgvShowDanhSachSinhVien.Columns["ID Lớp"].Visible = false;
                    dgvShowDanhSachSinhVien.Columns["ID Sinh Viên"].Visible = false;
                }
            }
        }

        private void btnCancelTKSVien_Click(object sender, EventArgs e)
        {
            dgvShowDanhSachSinhVien.DataSource = null;
            return;
        }

        private void btnNhapDiem_Click(object sender, EventArgs e)
        {
            _NhanBiet = "Nhap";
            frmSetDiem f = new frmSetDiem();
            f.ShowDialog();
            LoadData();
            _NhanBiet = "";
        }

        private void btnCancelTKLop_Click(object sender, EventArgs e)
        {
            dgvShowDanhSachLop.DataSource = null;
            return;
        }

        private void btnCapNhatDiem_Click(object sender, EventArgs e)
        {
            _NhanBiet = "CapNhat";
            frmSetDiem f = new frmSetDiem();
            f.ShowDialog();
            LoadData();
            _NhanBiet = "";
        }

        private void txtTKMSSV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnInDSSV_Click(object sender, EventArgs e)
        {
            if (_maLop == "")
            {
                MessageBox.Show("Bạn chưa chọn lớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                _NhanBiet = "indsSinhVien";
                frmReport f = new frmReport();
                f.ShowDialog();
                LoadData();
                dgvShowDanhSachLop.DataSource = null;
                dgvShowDanhSachSinhVien.DataSource = null;
                txtMSSV.Clear();
                txtHoSV.Clear();
                txtTenSV.Clear();
                txtGioiTinhSV.Clear();
                dtpNgaySinhSV.Value = DateTime.Today;
                txtSoDienThoaiSV.Clear();
                txtEmailSV.Clear();
                txtDiaChiSV.Clear();
                _maKhoaHoc = "";
                _maLop = "";
                _NhanBiet = "";
            }
        }

        private void btnXemDiem_Click(object sender, EventArgs e)
        {

            frmXemDiem f = new frmXemDiem();
            f.ShowDialog();
        }
    }
}
