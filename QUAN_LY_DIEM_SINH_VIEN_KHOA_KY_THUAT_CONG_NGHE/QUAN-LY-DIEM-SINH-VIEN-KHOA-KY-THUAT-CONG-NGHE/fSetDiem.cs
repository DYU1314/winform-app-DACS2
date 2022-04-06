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
    public partial class frmSetDiem : Form
    {
        private string maLop = frmQuanLyDiem._maLop;
        private string makhoahoc = frmQuanLyDiem._maKhoaHoc;
        public string nhanbiet = frmQuanLyDiem._NhanBiet;

        BindingSource HocPhan = new BindingSource();
        BindingSource SinhVienvsKQ = new BindingSource();

        public frmSetDiem()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgvShowHocPhan.DataSource = HocPhan;
            dgvSetDiemSV.DataSource = SinhVienvsKQ;
            pnlAn.Enabled = true;
            pnlAn.BringToFront();
            pnlCongViec.Enabled = false;
            pnlCongViec.SendToBack();

            LoadcomboBoxHocKi(cboXNHocKi);
            LoadListHocPhan();
            bindingHocPhan();
            txtDiemChuyenCan.Enabled = false;
            txtDiemGiuaKi.Enabled = false;
            txtDiemThiCuoiKi.Enabled = false;

            btnChinhSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void bindingHocPhan()
        {
            try
            {
                if (makhoahoc == "")
                {
                    return;
                }
                else
                {
                    txtXNMaHocPhan.DataBindings.Add(new Binding("Text", dgvShowHocPhan.DataSource, "Mã Học Phần", true, DataSourceUpdateMode.Never));
                }

            }
            catch { }
        }

        private void LoadListHocPhan()
        {
            if (makhoahoc == "")
            {
                return;
            }
            else
            {
                KhoaHocDTO khoa = KhoaHocDAO.Instance.GetKhoaHocByMaKhoaHoc(makhoahoc);

                HocPhan.DataSource = HocPhanDAO.Instance.LoadDanhSachHocPhanTheoKhoa(khoa.Id);
                dgvShowHocPhan.Columns["ID Học Phần"].Visible = false;
                dgvShowHocPhan.Columns["ID Học Kì"].Visible = false;
                dgvShowHocPhan.Columns["ID Khóa Học"].Visible = false;
                dgvShowHocPhan.Columns["Mã Khóa Học"].Visible = false;
                dgvShowHocPhan.Columns["Mã Học Kì"].Visible = false;
            }
        }

        private void LoadcomboBoxHocKi(ComboBox cbo)
        {
            if (makhoahoc == "")
            {
                return;
            }
            else
            {
                KhoaHocDTO khoa = KhoaHocDAO.Instance.GetKhoaHocByMaKhoaHoc(makhoahoc);
                cbo.DataSource = HocKiDAO.Instance.getLopByIdKhoaHoc1(khoa.Id);
                cbo.DisplayMember = "MAHOCKI";
            }
        }

        private Boolean CheckXN()
        {
            Boolean check = true;

            if (txtXNMaHocPhan.Text.Trim() == "")
            {
                errorRong.SetError(txtXNMaHocPhan, "Dữ liệu không được trống");
                txtXNMaHocPhan.Focus();
                return check = false;
            }
            else
            {
                errorRong.Clear();
            }

            return check;
        }

        private void btnOkXN_Click(object sender, EventArgs e)
        {
            string xnmHocPhan = txtXNMaHocPhan.Text;
            Boolean check = CheckXN();
            if (check == false)
            {
                MessageBox.Show("Xác nhận không thành công.\nVui lòng kiểm tra lại Mã học phần", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (maLop == "")
                {
                    MessageBox.Show("Bạn chưa chọn lớp trước khi xác nhận.\nVui lòng chọn lớp trước khi tiến hành bước xác nhận", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    this.Close();
                    return;
                }
                else
                {
                    dgvSetDiemSV.DataSource = SinhVienvsKQ;
                    LopDTO lop = LopDAO.Instance.GetLopByMaLop(maLop);
                    int idLop = lop.Id;
                    string maHocPhan = txtXNMaHocPhan.Text;
                    HocPhanDTO HocPhan = HocPhanDAO.Instance.GetDanhSachHocPhanByMaHocPhan(maHocPhan);
                    int idHocPhan = HocPhan.Id;
                    if (nhanbiet == "Nhap")
                    {
                        SinhVienvsKQ.DataSource = KQHocPhanDAO.Instance.LoadDanhSachSanhVienCuaLopvsDiemHocPhan(idLop, idHocPhan);
                        dgvSetDiemSV.Columns["ID Lớp"].Visible = false;
                        dgvSetDiemSV.Columns["ID Học Phần"].Visible = false;
                        dgvSetDiemSV.Columns["ID Sinh Viên"].Visible = false;
                        dgvSetDiemSV.Columns["ID KQ Học Phần"].Visible = false;
                        lblDanhSachSVLop.Text = "Danh Sach Sinh Viên Lớp" + lop.Malop;

                        pnlAn.Enabled = false;
                        pnlAn.SendToBack();
                        pnlCongViec.Enabled = true;
                        pnlCongViec.BringToFront();
                    }
                    else
                    {
                        if (nhanbiet == "CapNhat")
                        {
                            SinhVienvsKQ.DataSource = KQHocPhanDAO.Instance.LoadDanhSachSanhVienCuaLopDaCoDiemHocPhan(idLop, idHocPhan);
                            dgvSetDiemSV.Columns["ID Lớp"].Visible = false;
                            dgvSetDiemSV.Columns["ID Học Phần"].Visible = false;
                            dgvSetDiemSV.Columns["ID Sinh Viên"].Visible = false;
                            dgvSetDiemSV.Columns["ID KQ Học Phần"].Visible = false;
                            lblDanhSachSVLop.Text = "Danh Sach Sinh Viên Lớp" + lop.Malop;

                            pnlAn.Enabled = false;
                            pnlAn.SendToBack();
                            pnlCongViec.Enabled = true;
                            pnlCongViec.BringToFront();
                        }
                    }
                }
            }
        }

        private void cboXNHocKi_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvShowHocPhan.DataSource = HocPhan;
            string maHK = cboXNHocKi.Text;
            try
            {
                if (maHK != "System.Data.DataRowView")
                {
                    maHK = cboXNHocKi.Text;
                    HocKiDTO HK = HocKiDAO.Instance.GetHocKiByMaHocKi(maHK);
                    HocPhan.DataSource = HocPhanDAO.Instance.LoadDanhSachHocPhanTheoIDHocKi(HK.Id);
                    dgvShowHocPhan.Columns["ID Học Phần"].Visible = false;
                    dgvShowHocPhan.Columns["ID Học Kì"].Visible = false;
                    dgvShowHocPhan.Columns["ID Khóa Học"].Visible = false;
                    dgvShowHocPhan.Columns["Mã Khóa Học"].Visible = false;
                    dgvShowHocPhan.Columns["Mã Học Kì"].Visible = false;
                }
            }
            catch { }
        }

        private void dgvSetDiemSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMSSV.Text = dgvSetDiemSV[6, e.RowIndex].Value.ToString();
                txtHo.Text = dgvSetDiemSV[7, e.RowIndex].Value.ToString();
                txtTen.Text = dgvSetDiemSV[8, e.RowIndex].Value.ToString();
                txtDiemChuyenCan.Text = dgvSetDiemSV[9, e.RowIndex].Value.ToString();
                txtDiemGiuaKi.Text = dgvSetDiemSV[10, e.RowIndex].Value.ToString();
                txtDiemThiCuoiKi.Text = dgvSetDiemSV[11, e.RowIndex].Value.ToString();
                txtDTBHeSo10.Text = dgvSetDiemSV[12, e.RowIndex].Value.ToString();
                txtDTBHeSo4.Text = dgvSetDiemSV[13, e.RowIndex].Value.ToString();
                txtDTBChu.Text = dgvSetDiemSV[14, e.RowIndex].Value.ToString();
            }
            catch { }
        }

        private void rbtnChuaCoDiem_CheckedChanged(object sender, EventArgs e)
        {
            dgvSetDiemSV.DataSource = SinhVienvsKQ;
            LopDTO lop = LopDAO.Instance.GetLopByMaLop(maLop);
            int idLop = lop.Id;
            string maHocPhan = txtXNMaHocPhan.Text;
            HocPhanDTO HocPhan = HocPhanDAO.Instance.GetDanhSachHocPhanByMaHocPhan(maHocPhan);
            int idHocPhan = HocPhan.Id;

            SinhVienvsKQ.DataSource = KQHocPhanDAO.Instance.LoadDanhSachSanhVienCuaLopChuaCoDiemHocPhan(idLop, idHocPhan);
            dgvSetDiemSV.Columns["ID Lớp"].Visible = false;
            dgvSetDiemSV.Columns["ID Học Phần"].Visible = false;
            dgvSetDiemSV.Columns["ID Sinh Viên"].Visible = false;
            dgvSetDiemSV.Columns["ID KQ Học Phần"].Visible = false;
            lblDanhSachSVLop.Text = "Danh Sach Sinh Viên Lớp" + lop.Malop;

            pnlAn.Enabled = false;
            pnlAn.SendToBack();
            pnlCongViec.Enabled = true;
            pnlCongViec.BringToFront();
        }

        private void rbtnDaCoDiem_CheckedChanged(object sender, EventArgs e)
        {
            dgvSetDiemSV.DataSource = SinhVienvsKQ;
            LopDTO lop = LopDAO.Instance.GetLopByMaLop(maLop);
            int idLop = lop.Id;
            string maHocPhan = txtXNMaHocPhan.Text;
            HocPhanDTO HocPhan = HocPhanDAO.Instance.GetDanhSachHocPhanByMaHocPhan(maHocPhan);
            int idHocPhan = HocPhan.Id;

            SinhVienvsKQ.DataSource = KQHocPhanDAO.Instance.LoadDanhSachSanhVienCuaLopDaCoDiemHocPhan(idLop, idHocPhan);
            dgvSetDiemSV.Columns["ID Lớp"].Visible = false;
            dgvSetDiemSV.Columns["ID Học Phần"].Visible = false;
            dgvSetDiemSV.Columns["ID Sinh Viên"].Visible = false;
            dgvSetDiemSV.Columns["ID KQ Học Phần"].Visible = false;

            pnlAn.Enabled = false;
            pnlAn.SendToBack();
            pnlCongViec.Enabled = true;
            pnlCongViec.BringToFront();
        }

        private void rbtnTatCaSinhVien_CheckedChanged(object sender, EventArgs e)
        {
            dgvSetDiemSV.DataSource = SinhVienvsKQ;
            LopDTO lop = LopDAO.Instance.GetLopByMaLop(maLop);
            int idLop = lop.Id;
            string maHocPhan = txtXNMaHocPhan.Text;
            HocPhanDTO HocPhan = HocPhanDAO.Instance.GetDanhSachHocPhanByMaHocPhan(maHocPhan);
            int idHocPhan = HocPhan.Id;

            SinhVienvsKQ.DataSource = KQHocPhanDAO.Instance.LoadDanhSachSanhVienCuaLopvsDiemHocPhan(idLop, idHocPhan);
            dgvSetDiemSV.Columns["ID Lớp"].Visible = false;
            dgvSetDiemSV.Columns["ID Học Phần"].Visible = false;
            dgvSetDiemSV.Columns["ID Sinh Viên"].Visible = false;
            dgvSetDiemSV.Columns["ID KQ Học Phần"].Visible = false;

            pnlAn.Enabled = false;
            pnlAn.SendToBack();
            pnlCongViec.Enabled = true;
            pnlCongViec.BringToFront();
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            dgvSetDiemSV.Enabled = false;
            btnChinhSua.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;

            txtDiemChuyenCan.Enabled = true;
            txtDiemChuyenCan.Focus();
            txtDiemGiuaKi.Enabled = true;
            txtDiemThiCuoiKi.Enabled = true;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            dgvSetDiemSV.DataSource = SinhVienvsKQ;
            LopDTO lop = LopDAO.Instance.GetLopByMaLop(maLop);
            int idLop = lop.Id;
            string maHocPhan = txtXNMaHocPhan.Text;
            HocPhanDTO HocPhan = HocPhanDAO.Instance.GetDanhSachHocPhanByMaHocPhan(maHocPhan);
            int idHocPhan = HocPhan.Id;

            SinhVienvsKQ.DataSource = KQHocPhanDAO.Instance.LoadDanhSachSanhVienCuaLopvsDiemHocPhan(idLop, idHocPhan);
            dgvSetDiemSV.Columns["ID Lớp"].Visible = false;
            dgvSetDiemSV.Columns["ID Học Phần"].Visible = false;
            dgvSetDiemSV.Columns["ID Sinh Viên"].Visible = false;
            dgvSetDiemSV.Columns["ID KQ Học Phần"].Visible = false;

            pnlAn.Enabled = false;
            pnlAn.SendToBack();
            pnlCongViec.Enabled = true;
            pnlCongViec.BringToFront();

            dgvSetDiemSV.Enabled = true;
            btnChinhSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;

            txtDiemChuyenCan.Enabled = false;
            txtDiemGiuaKi.Enabled = false;
            txtDiemThiCuoiKi.Enabled = false;
        }

        private int trarasinhvien(string mssv)
        {
            int idsinhvien = 0;
            try
            {
                if (mssv != "")
                {
                    SinhVienDTO sinhvien = SinhVienDAO.Instance.GetSinhVienbyMSSVDiem(mssv);
                    return idsinhvien = sinhvien.Id;
                }
            }
            catch { }
            return idsinhvien;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maHocPhan = txtXNMaHocPhan.Text;
            HocPhanDTO HocPhan = HocPhanDAO.Instance.GetDanhSachHocPhanByMaHocPhan(maHocPhan);
            int idHocPhan = HocPhan.Id;
            string mSSV = txtMSSV.Text;

            int idsinhvien = trarasinhvien(mSSV);

            string dcc = txtDiemChuyenCan.Text;
            string dgk = txtDiemGiuaKi.Text;
            string dck = txtDiemThiCuoiKi.Text;

            Boolean check = checkrong();


            if (mSSV == "")
            {
                MessageBox.Show("Bạn chưa chọn sinh viên.\nVui lòng chọn sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlAn.Enabled = false;
                pnlAn.SendToBack();
                pnlCongViec.Enabled = true;
                pnlCongViec.BringToFront();

                dgvSetDiemSV.Enabled = true;
                btnChinhSua.Enabled = true;
                btnBoQua.Enabled = false;
                btnLuu.Enabled = false;

                txtDiemChuyenCan.Enabled = false;
                txtDiemGiuaKi.Enabled = false;
                txtDiemThiCuoiKi.Enabled = false;
                return;
            }
            else
            {
                if (check == false)
                {
                    MessageBox.Show("Bạn chưa nhập đủ thông tin");
                    return;
                }
                else
                {
                    if (checkchiem(dcc) == false || checkchiem(dgk) == false || checkchiem(dck) == false)
                    {
                        MessageBox.Show("Điểm nhập vào không hợp lệ", "Lỗi nhập thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtDiemChuyenCan.Clear();
                        txtDiemGiuaKi.Clear();
                        txtDiemThiCuoiKi.Clear();
                        txtDiemChuyenCan.Focus();
                        return;
                    }
                    else
                    {
                        string diemtbchu = txtDTBChu.Text;

                        if (nhanbiet == "Nhap")
                        {
                            if (diemtbchu == "")
                            {
                                NhapDiem(dcc, dgk, dck, idHocPhan, idsinhvien);
                                dgvSetDiemSV.DataSource = SinhVienvsKQ;
                                LopDTO lop = LopDAO.Instance.GetLopByMaLop(maLop);
                                int idLop = lop.Id;

                                SinhVienvsKQ.DataSource = KQHocPhanDAO.Instance.LoadDanhSachSanhVienCuaLopChuaCoDiemHocPhan(idLop, idHocPhan);
                                dgvSetDiemSV.Columns["ID Lớp"].Visible = false;
                                dgvSetDiemSV.Columns["ID Học Phần"].Visible = false;
                                dgvSetDiemSV.Columns["ID Sinh Viên"].Visible = false;
                                dgvSetDiemSV.Columns["ID KQ Học Phần"].Visible = false;
                                lblDanhSachSVLop.Text = "Danh Sach Sinh Viên Lớp" + lop.Malop;

                                pnlAn.Enabled = false;
                                pnlAn.SendToBack();
                                pnlCongViec.Enabled = true;
                                pnlCongViec.BringToFront();

                                dgvSetDiemSV.Enabled = true;
                                btnChinhSua.Enabled = true;
                                btnBoQua.Enabled = false;
                                btnLuu.Enabled = false;

                                txtDiemChuyenCan.Enabled = false;
                                txtDiemGiuaKi.Enabled = false;
                                txtDiemThiCuoiKi.Enabled = false;
                            }
                            else
                            {
                                MessageBox.Show("Bạn không thể thêm khi sinh viên đã có điểm");
                                return;
                            }
                        }
                        else
                        {
                            if (nhanbiet == "CapNhat")
                            {
                                if (diemtbchu != "")
                                {
                                    CapNhat(dcc, dgk, dck, idHocPhan, idsinhvien);
                                    dgvSetDiemSV.DataSource = SinhVienvsKQ;
                                    LopDTO lop = LopDAO.Instance.GetLopByMaLop(maLop);
                                    int idLop = lop.Id;

                                    SinhVienvsKQ.DataSource = KQHocPhanDAO.Instance.LoadDanhSachSanhVienCuaLopDaCoDiemHocPhan(idLop, idHocPhan);
                                    dgvSetDiemSV.Columns["ID Lớp"].Visible = false;
                                    dgvSetDiemSV.Columns["ID Học Phần"].Visible = false;
                                    dgvSetDiemSV.Columns["ID Sinh Viên"].Visible = false;
                                    dgvSetDiemSV.Columns["ID KQ Học Phần"].Visible = false;
                                    lblDanhSachSVLop.Text = "Danh Sach Sinh Viên Lớp" + lop.Malop;

                                    pnlAn.Enabled = false;
                                    pnlAn.SendToBack();
                                    pnlCongViec.Enabled = true;
                                    pnlCongViec.BringToFront();

                                    dgvSetDiemSV.Enabled = true;
                                    btnChinhSua.Enabled = true;
                                    btnBoQua.Enabled = false;
                                    btnLuu.Enabled = false;

                                    txtDiemChuyenCan.Enabled = false;
                                    txtDiemGiuaKi.Enabled = false;
                                    txtDiemThiCuoiKi.Enabled = false;
                                }
                                else
                                {
                                    MessageBox.Show("Bạn không thể cập nhật khi sinh viên chưa có điểm");
                                    return;
                                }
                            }
                            return;
                        }
                    }
                }
            }
        }

        private Boolean checkchiem(string chuoicankiemtra)
        {
            string db = "0123456789,.";
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

        private Boolean checkrong()
        {
            Boolean check = true;

            if (txtDiemChuyenCan.Text.Trim() == "")
            {
                errorRong.SetError(txtDiemChuyenCan, "Dữ liệu không được trống");
                txtDiemChuyenCan.Focus();
                return check = false;
            }
            else { errorRong.Clear(); }

            if (txtDiemGiuaKi.Text.Trim() == "")
            {
                errorRong.SetError(txtDiemGiuaKi, "Dữ liệu không được trống");
                txtDiemGiuaKi.Focus();
                return check = false;
            }
            else { errorRong.Clear(); }

            if (txtDiemThiCuoiKi.Text.Trim() == "")
            {
                errorRong.SetError(txtDiemThiCuoiKi, "Dữ liệu không được trống");
                txtDiemThiCuoiKi.Focus();
                return check = false;
            }
            else { errorRong.Clear(); }

            return check;
        }

        private void NhapDiem(string dcc, string dgk, string dck, int idHP, int idSV)
        {
            if (KQHocPhanDAO.Instance.InsertKQHocPhan(dcc, dgk, dck, idHP, idSV))
            {
                MessageBox.Show("Nhập điểm thành công");
            }
            else
            {
                MessageBox.Show("Nhập điểm thất bại");
            }
        }

        private void CapNhat(string dcc, string dgk, string dck, int idHP, int idSV)
        {
            if (KQHocPhanDAO.Instance.UpdateKQHocPhan(dcc, dgk, dck, idHP, idSV))
            {
                MessageBox.Show("Cập nhật điểm thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật điểm thất bại");
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            dgvShowHocPhan.DataSource = HocPhan;
            pnlAn.Enabled = true;
            pnlAn.BringToFront();
            pnlCongViec.Enabled = false;
            pnlCongViec.SendToBack();
        }
    }
}
