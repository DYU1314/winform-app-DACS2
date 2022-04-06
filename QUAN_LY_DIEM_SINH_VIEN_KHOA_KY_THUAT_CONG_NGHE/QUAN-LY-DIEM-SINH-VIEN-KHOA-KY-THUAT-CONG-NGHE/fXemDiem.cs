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
    public partial class frmXemDiem : Form
    {
        public static string _nhanbiet = "";

        public static int _mahocphan = 0;
        public static int _hocki = 0;
        public static int _malop = 0;
        public static string _mssv = "";

        BindingSource bangdiem = new BindingSource();

        public frmXemDiem()
        {
            InitializeComponent();
            loaddata();
        }

        private void loaddata()
        {
            EnableControl(false);
            btnXemDiemTheoDanhSachLop.Enabled = true;
            btnXemDiemCuaSinhVien.Enabled = true;
            btnXemDiemTheoDanhSachHocKi.Enabled = true;
        }

        private void EnableControl(Boolean e)
        {
            txtMaHocPhan.Enabled = e;
            txtMaHocKi.Enabled = e;
            txtMaLop.Enabled = e;
            txtMaSSV.Enabled = e;

            btnXemDiemTheoDanhSachLop.Enabled = e;
            btnXemDiemCuaSinhVien.Enabled = e;
            btnXemDiemTheoDanhSachHocKi.Enabled = e;

            btnInPhieuDiemHocki.Enabled = e;
            btnInPhieuDiemHocPhan.Enabled = e;

            btnXem.Enabled = e;
            btnCancel.Enabled = e;

            grpLoadDanhSachSinhVien.Enabled = e;
        }

        private Boolean checkxemdiemtheodanhsachlop()
        {
            Boolean check = true;

            if (txtMaHocPhan.Text.Trim() == "")
            {
                MessageBox.Show("Bạn không được để tróng mã học phần");
                txtMaHocPhan.Focus();
                return check = false;
            }
            if (txtMaLop.Text.Trim() == "")
            {
                MessageBox.Show("Bạn không được để tróng mã lớp");
                txtMaLop.Focus();
                return check = false;
            }

            return check;
        }

        private void btnXemDiemTheoDanhSachLop_Click(object sender, EventArgs e)
        {
            txtMaHocKi.Enabled = false;
            txtMaSSV.Enabled = false;
            txtMaHocPhan.Enabled = true;
            txtMaLop.Enabled = true;

            btnInPhieuDiemHocki.Enabled = false;

            clearTextbox();

            dgvBangDiem.DataSource = null;

            btnXemDiemTheoDanhSachLop.Enabled = false;
            btnXemDiemCuaSinhVien.Enabled = true;
            btnXemDiemTheoDanhSachHocKi.Enabled = true;

            btnXem.Enabled = true;
            btnCancel.Enabled = true;

            txtMaHocPhan.Focus();
            this.AcceptButton = btnXem;
        }

        private Boolean checkdiemtheomasosinhvien()
        {
            Boolean check = true;

            if (txtMaSSV.Text.Trim() == "")
            {
                MessageBox.Show("Bạn cần nhập mã số sinh viên");
                return check = false;
            }

            return check;
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            string mahocphan = txtMaHocPhan.Text;
            string mahocki = txtMaHocKi.Text;
            string malop = txtMaLop.Text;
            string mssv = txtMaSSV.Text;

            int idlop = 0;
            int idhocphan = 0;
            int idsinhvien = 0;
            int idhocki = 0;

            if (malop != "")
            {
                if (LopDAO.Instance.GetCountByMaLop(malop) > 0)
                {
                    LopDTO lop = LopDAO.Instance.GetLopByMaLop(malop);
                    idlop = lop.Id;
                }

            }

            if (mahocphan != "")
            {
                if (HocPhanDAO.Instance.GetCountByMaHocPhan(mahocphan) > 0)
                {
                    HocPhanDTO hocphan = HocPhanDAO.Instance.GetDanhSachHocPhanByMaHocPhan(mahocphan);
                    idhocphan = hocphan.Id;
                }
            }

            if (mssv != "")
            {
                if (SinhVienDAO.Instance.GetCountByMaSinhVien(mssv) > 0)
                {
                    SinhVienDTO sinhvien = SinhVienDAO.Instance.GetSinhVienbyMSSVDiem(mssv);
                    idsinhvien = sinhvien.Id;
                }
            }

            if (mahocki != "")
            {
                if (HocKiDAO.Instance.GetCountByMaHocKi(mahocki) > 0)
                {
                    HocKiDTO hocki = HocKiDAO.Instance.GetHocKiByMaHocKi(mahocki);
                    idhocki = hocki.Id;
                }
            }



            if (btnXemDiemTheoDanhSachLop.Enabled == false && txtMaHocPhan.Enabled == true && txtMaLop.Enabled == true)
            {
                Boolean check = checkxemdiemtheodanhsachlop();
                if (check == false)
                {
                    return;
                }
                else
                {
                    if (LopDAO.Instance.GetCountByMaLop(malop) < 1 || HocPhanDAO.Instance.GetCountByMaHocPhan(mahocphan) < 1)
                    {
                        MessageBox.Show("Mã học phần hoặc mã lớp không có trong danh sách hệ thống.\nVui lòng kiểm tra lại");
                        txtMaHocPhan.Focus();
                        return;
                    }
                    else
                    {
                        loaddata();
                        grpLoadDanhSachSinhVien.Enabled = true;
                        btnInPhieuDiemHocPhan.Enabled = true;
                        dgvBangDiem.DataSource = KQHocPhanDAO.Instance.LoadDanhSachSanhVienCuaLopvsDiemHocPhan(idlop, idhocphan);
                        dgvBangDiem.Columns["ID Lớp"].Visible = false;
                        dgvBangDiem.Columns["ID Học Phần"].Visible = false;
                        dgvBangDiem.Columns["ID Sinh Viên"].Visible = false;
                        dgvBangDiem.Columns["ID KQ Học Phần"].Visible = false;
                        grpLoadDanhSachSinhVien.Text = "Danh Sách Sinh Viên Lớp " + malop;
                        btnCancel.Enabled = true;
                        return;
                    }
                }
            }
            else
            {
                if (btnXemDiemCuaSinhVien.Enabled == false && txtMaSSV.Enabled == true)
                {
                    if (SinhVienDAO.Instance.GetCountByMaSinhVien(mssv) < 1)
                    {
                        MessageBox.Show("Mã số sinh viên không có trong danh sách hệ thống.\nvui lòng kiểm tra lại");
                        txtMaSSV.Focus();
                        return;
                    }
                    else
                    {
                        loaddata();
                        grpLoadDanhSachSinhVien.Enabled = false;
                        dgvBangDiem.DataSource = KQHocPhanDAO.Instance.LoadDanhSachDiemHocPhanCuaSinhVien(idsinhvien);
                        dgvBangDiem.Columns["MSSV"].Visible = false;
                        dgvBangDiem.Columns["Họ và Tên Lót"].Visible = false;
                        dgvBangDiem.Columns["Tên"].Visible = false;
                        btnCancel.Enabled = true;
                        return;
                    }
                }
                else
                {
                    if (btnXemDiemTheoDanhSachHocKi.Enabled == false && txtMaHocKi.Enabled == true && txtMaLop.Enabled == true)
                    {
                        if (HocKiDAO.Instance.GetCountByMaHocKi(mahocki) < 1 || LopDAO.Instance.GetCountByMaLop(malop) < 1)
                        {
                            MessageBox.Show("Mã học kì hoặc mã lớp của không tồn tại trong danh sách hệ thống.\nvui lòng kiểm tra lại");
                            txtMaHocKi.Focus();
                            return;
                        }
                        else
                        {
                            loaddata();
                            grpLoadDanhSachSinhVien.Enabled = false;
                            dgvBangDiem.DataSource = KQHocPhanDAO.Instance.LoadDanhSachDiemHocKiCuaTatCaSinhVienTrongLop(idhocki, idlop);
                            btnCancel.Enabled = true;
                            btnInPhieuDiemHocki.Enabled = true;
                            return;
                        }
                    }
                }
            }
        }

        private void clearTextbox()
        {
            txtMaSSV.Clear();
            txtMaHocPhan.Clear();
            txtMaHocKi.Clear();
            txtMaLop.Clear();

            txtDiemChuyenCan.Clear();
            txtDiemGiuaKi.Clear();
            txtDiemCuoiKi.Clear();
            txtDTBHeSo10.Clear();
            txtDTBHeSo4.Clear();
            txtDTBChuHP.Clear();
            txtDTBChuHK.Clear();
            txtDTBHK10.Clear();
            txtDTBHK4.Clear();
        }

        private void rbtnChuaCoDiem_CheckedChanged(object sender, EventArgs e)
        {
            string mahocphan = txtMaHocPhan.Text;
            string malop = txtMaLop.Text;

            LopDTO lop = LopDAO.Instance.GetLopByMaLop(malop);
            int idlop = lop.Id;
            HocPhanDTO hocphan = HocPhanDAO.Instance.GetDanhSachHocPhanByMaHocPhan(mahocphan);
            int idhocphan = hocphan.Id;
            Boolean check = checkxemdiemtheodanhsachlop();

            dgvBangDiem.DataSource = KQHocPhanDAO.Instance.LoadDanhSachSanhVienCuaLopChuaCoDiemHocPhan(idlop, idhocphan);
            dgvBangDiem.Columns["ID Lớp"].Visible = false;
            dgvBangDiem.Columns["ID Học Phần"].Visible = false;
            dgvBangDiem.Columns["ID Sinh Viên"].Visible = false;
            dgvBangDiem.Columns["ID KQ Học Phần"].Visible = false;
            grpLoadDanhSachSinhVien.Text = "Danh Sách Sinh Viên " + malop;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            loaddata();
            dgvBangDiem.DataSource = null;
        }

        private void btnXemDiemCuaSinhVien_Click(object sender, EventArgs e)
        {
            txtMaHocKi.Enabled = false;
            txtMaSSV.Enabled = true;
            txtMaHocPhan.Enabled = false;
            txtMaLop.Enabled = false;

            btnInPhieuDiemHocki.Enabled = false;
            btnInPhieuDiemHocPhan.Enabled = false;

            clearTextbox();

            dgvBangDiem.DataSource = null;

            btnXemDiemTheoDanhSachLop.Enabled = true;
            btnXemDiemCuaSinhVien.Enabled = false;
            btnXemDiemTheoDanhSachHocKi.Enabled = true;

            btnXem.Enabled = true;
            btnCancel.Enabled = true;

            txtMaSSV.Focus();
            this.AcceptButton = btnXem;
        }

        private void rbtnDaCoDiem_CheckedChanged(object sender, EventArgs e)
        {
            string mahocphan = txtMaHocPhan.Text;
            string malop = txtMaLop.Text;

            LopDTO lop = LopDAO.Instance.GetLopByMaLop(malop);
            int idlop = lop.Id;
            HocPhanDTO hocphan = HocPhanDAO.Instance.GetDanhSachHocPhanByMaHocPhan(mahocphan);
            int idhocphan = hocphan.Id;
            Boolean check = checkxemdiemtheodanhsachlop();

            dgvBangDiem.DataSource = KQHocPhanDAO.Instance.LoadDanhSachSanhVienCuaLopDaCoDiemHocPhan(idlop, idhocphan);
            dgvBangDiem.Columns["ID Lớp"].Visible = false;
            dgvBangDiem.Columns["ID Học Phần"].Visible = false;
            dgvBangDiem.Columns["ID Sinh Viên"].Visible = false;
            dgvBangDiem.Columns["ID KQ Học Phần"].Visible = false;
            grpLoadDanhSachSinhVien.Text = "Danh Sách Sinh Viên " + malop;
        }

        private void rbtnTatCaSinhVien_CheckedChanged(object sender, EventArgs e)
        {
            string mahocphan = txtMaHocPhan.Text;
            string malop = txtMaLop.Text;

            LopDTO lop = LopDAO.Instance.GetLopByMaLop(malop);
            int idlop = lop.Id;
            HocPhanDTO hocphan = HocPhanDAO.Instance.GetDanhSachHocPhanByMaHocPhan(mahocphan);
            int idhocphan = hocphan.Id;
            Boolean check = checkxemdiemtheodanhsachlop();

            dgvBangDiem.DataSource = KQHocPhanDAO.Instance.LoadDanhSachSanhVienCuaLopvsDiemHocPhan(idlop, idhocphan);
            dgvBangDiem.Columns["ID Lớp"].Visible = false;
            dgvBangDiem.Columns["ID Học Phần"].Visible = false;
            dgvBangDiem.Columns["ID Sinh Viên"].Visible = false;
            dgvBangDiem.Columns["ID KQ Học Phần"].Visible = false;
        }

        private void btnXemDiemTheoDanhSachHocKi_Click(object sender, EventArgs e)
        {
            txtMaHocKi.Enabled = true;
            txtMaSSV.Enabled = false;
            txtMaHocPhan.Enabled = false;
            txtMaLop.Enabled = true;

            clearTextbox();

            btnInPhieuDiemHocPhan.Enabled = false;

            dgvBangDiem.DataSource = null;

            btnXemDiemTheoDanhSachLop.Enabled = true;
            btnXemDiemCuaSinhVien.Enabled = true;
            btnXemDiemTheoDanhSachHocKi.Enabled = false;

            btnXem.Enabled = true;
            btnCancel.Enabled = true;

            txtMaHocKi.Focus();
            this.AcceptButton = btnXem;
        }

        private void dgvBangDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (txtMaHocPhan.TextLength > 0 && txtMaLop.TextLength > 0)
                {
                    txtmssvDisplay.Text = dgvBangDiem[6, e.RowIndex].Value.ToString();
                    txtHo.Text = dgvBangDiem[7, e.RowIndex].Value.ToString();
                    txtTen.Text = dgvBangDiem[8, e.RowIndex].Value.ToString();
                    txtDiemChuyenCan.Text = dgvBangDiem[9, e.RowIndex].Value.ToString();
                    txtDiemGiuaKi.Text = dgvBangDiem[10, e.RowIndex].Value.ToString();
                    txtDiemCuoiKi.Text = dgvBangDiem[11, e.RowIndex].Value.ToString();
                    txtDTBHeSo10.Text = dgvBangDiem[12, e.RowIndex].Value.ToString();
                    txtDTBHeSo4.Text = dgvBangDiem[13, e.RowIndex].Value.ToString();
                    txtDTBChuHP.Text = dgvBangDiem[14, e.RowIndex].Value.ToString();
                }
                else
                {
                    if (txtMaSSV.TextLength > 0)
                    {
                        txtmssvDisplay.Text = dgvBangDiem[1, e.RowIndex].Value.ToString();
                        txtHo.Text = dgvBangDiem[2, e.RowIndex].Value.ToString();
                        txtTen.Text = dgvBangDiem[3, e.RowIndex].Value.ToString();
                        txtDiemChuyenCan.Text = dgvBangDiem[6, e.RowIndex].Value.ToString();
                        txtDiemGiuaKi.Text = dgvBangDiem[7, e.RowIndex].Value.ToString();
                        txtDiemCuoiKi.Text = dgvBangDiem[8, e.RowIndex].Value.ToString();
                        txtDTBHeSo10.Text = dgvBangDiem[9, e.RowIndex].Value.ToString();
                        txtDTBHeSo4.Text = dgvBangDiem[10, e.RowIndex].Value.ToString();
                        txtDTBChuHP.Text = dgvBangDiem[11, e.RowIndex].Value.ToString();
                    }
                    else
                    {
                        if (txtMaHocKi.TextLength > 0 && txtMaLop.TextLength > 0)
                        {
                            txtmssvDisplay.Text = dgvBangDiem[2, e.RowIndex].Value.ToString();
                            txtHo.Text = dgvBangDiem[3, e.RowIndex].Value.ToString();
                            txtTen.Text = dgvBangDiem[4, e.RowIndex].Value.ToString();
                            txtDTBHK10.Text = dgvBangDiem[5, e.RowIndex].Value.ToString();
                            txtDTBHK4.Text = dgvBangDiem[6, e.RowIndex].Value.ToString();
                            txtDTBChuHK.Text = dgvBangDiem[7, e.RowIndex].Value.ToString();
                        }
                    }
                }
            }
            catch { }
        }

        private void btnInPhieuDiemHocPhan_Click(object sender, EventArgs e)
        {
            HocPhanDTO hocphan = HocPhanDAO.Instance.GetDanhSachHocPhanByMaHocPhan(txtMaHocPhan.Text);
            LopDTO lop = LopDAO.Instance.GetLopByMaLop(txtMaLop.Text);
            _mahocphan = hocphan.Id;
            _malop = lop.Id;
            _nhanbiet = "indiemhocphan";
            frmReport f = new frmReport();
            f.ShowDialog();
            loaddata();
            _nhanbiet = "";
            _malop = 0;
            _mahocphan = 0;
        }

        private void btnInPhieuDiemHocki_Click(object sender, EventArgs e)
        {
            LopDTO lop = LopDAO.Instance.GetLopByMaLop(txtMaLop.Text);
            HocKiDTO hocki = HocKiDAO.Instance.GetHocKiByMaHocKi(txtMaHocKi.Text);
            _hocki = hocki.Id;
            _malop = lop.Id;
            _nhanbiet = "indiemhocki";
            frmReport f = new frmReport();
            f.ShowDialog();
            _nhanbiet = "";
            _malop = 0;
            _hocki = 0;
        }

        private void txtMaHocPhan_TextChanged(object sender, EventArgs e)
        {
            if (txtMaHocPhan.TextLength > 0) this.AcceptButton = btnXem;
        }

        private void txtMaHocKi_TextChanged(object sender, EventArgs e)
        {
            if (txtMaHocKi.TextLength > 0) this.AcceptButton = btnXem;
        }

        private void txtMaLop_TextChanged(object sender, EventArgs e)
        {
            if (txtMaLop.TextLength > 0) this.AcceptButton = btnXem;
        }

        private void txtMaSSV_TextChanged(object sender, EventArgs e)
        {
            if (txtMaSSV.TextLength > 0) this.AcceptButton = btnXem;
        }
    }
}
