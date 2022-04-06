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
    public partial class frmDoiMatKhau : Form
    {
        public string TenTaiKhoan = frmMain.TenTaiKhoan;

        public frmDoiMatKhau()
        {
            InitializeComponent();
            LoadForm();
        }

        #region Mothods

        private void LoadForm()
        {
            ptbHien.Visible = false;
            ptbHien1.Visible = false;
            ptbAn.Visible = false;
            ptbAn1.Visible = false;
        }

        private void CapNhatMatKhau(string userName, string passWord)
        {
            TaiKhoan tk = TaiKhoanDAO.Instance.GetTaiKhoanByTenTaiKhoan(TenTaiKhoan);
            userName = tk.Tentaikhoan;
            if (TaiKhoanDAO.Instance.CapNhatMatKhauTaiKhoan(userName, passWord))
            {
                MessageBox.Show("Cập nhật mật khẩu mới thành công!!!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Lỗi cập nhật!!!/nVui lòng kiểm tra lại thông tin của bạn", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private Boolean CheckMatKhauCapNhat()
        {
            Boolean check = true;

            if (txtMatKhauCu.Text == "")
            {
                errorRong.SetError(txtMatKhauCu, "Không được trống");
                check = false;
                return check;
            }
            else { errorRong.Clear(); }

            if (txtMKMoi.Text == "")
            {
                errorRong.SetError(txtMKMoi, "Không được trống");
                check = false;
                return check;
            }
            else { errorRong.Clear(); }

            if (txtLapLaiMKM.Text == "")
            {
                errorRong.SetError(txtLapLaiMKM, "Không được trống");
                check = false;
                return check;
            }
            else { errorRong.Clear(); }

            return check;
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

        private void clearTextbox()
        {
            txtMatKhauCu.Clear();
            txtMKMoi.Clear();
            txtLapLaiMKM.Clear();
        }

        #endregion

        #region Event

        private void txtMatKhauCu_TextChanged(object sender, EventArgs e)
        {
            if (txtMatKhauCu.Text.Length > 0)
            {
                errorRong.Clear();
                errorTrung.Clear();
            }
            {
                return;
            }
        }

        private void txtMKMoi_TextChanged(object sender, EventArgs e)
        {
            if (txtMKMoi.Text.Length > 0)
            {
                ptbHien.Visible = true;
            }
            else
            {
                ptbHien.Visible = false;
                ptbAn.Visible = false;
            }
        }

        private void txtLapLaiMKM_TextChanged(object sender, EventArgs e)
        {
            if (txtLapLaiMKM.TextLength > 0)
            {
                ptbHien1.Visible = true;
            }
            else
            {
                ptbHien1.Visible = false;
                ptbAn1.Visible = false;
            }
        }

        private void ptbHien_Click(object sender, EventArgs e)
        {
            txtMKMoi.UseSystemPasswordChar = false;
            ptbAn.Visible = true;
            ptbAn.BringToFront();
            ptbHien.Visible = false;
        }

        private void ptbAn_Click(object sender, EventArgs e)
        {
            txtMKMoi.UseSystemPasswordChar = true;
            ptbHien.Visible = true;
            ptbHien.BringToFront();
            ptbAn.Visible = false;
        }

        private void ptbHien1_Click(object sender, EventArgs e)
        {
            txtLapLaiMKM.UseSystemPasswordChar = false;
            ptbAn1.Visible = true;
            ptbAn1.BringToFront();
            ptbHien1.Visible = false;
        }

        private void ptbAn1_Click(object sender, EventArgs e)
        {
            txtLapLaiMKM.UseSystemPasswordChar = true;
            ptbHien1.Visible = true;
            ptbHien1.BringToFront();
            ptbAn1.Visible = false;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            TaiKhoan tk = TaiKhoanDAO.Instance.GetTaiKhoanByTenTaiKhoan(TenTaiKhoan);
            string userName = tk.Tentaikhoan;
            string passWordHT = TaiKhoanDAO.Instance.GiaiMa(tk.Matkhau);
            string passCu = txtMatKhauCu.Text;
            string passNew = txtMKMoi.Text;
            string repeatPass = txtLapLaiMKM.Text;
            Boolean check = CheckMatKhauCapNhat();

            if (check == false)
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin.\nVui lòng kiểm tra lại!!!", "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadForm();
                clearTextbox();
                txtMatKhauCu.Focus();
                return;
            }
            else
            {
                if (passNew.Length < 6 || passNew.Length > 18)
                {
                    MessageBox.Show("Mật khẩu của bạn phải từ 6 đến 18 ký tự\nVui lòng kiểm tra lại.", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearTextbox();
                    txtMatKhauCu.Focus();
                    return;
                }
                else
                {
                    if (checkKTDB(passNew) == true)
                    {
                        MessageBox.Show("Mật khẩu của bạn phải có ký tự đặt biệt.\nVui lòng kiểm tra lại.", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearTextbox();
                        txtMatKhauCu.Focus();
                        return;
                    }
                    else
                    {
                        if (checkKTHoa(passNew) == true)
                        {
                            MessageBox.Show("Mật khẩu của bạn phải có ký tự ký tự viết hoa.\nVui lòng kiểm tra lại.", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clearTextbox();
                            txtMatKhauCu.Focus();
                            return;
                        }
                        else
                        {
                            if (checkKTSo(passNew) == true)
                            {
                                MessageBox.Show("Mật khẩu của bạn phải có ký tự ký tự số.\nVui lòng kiểm tra lại.", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                clearTextbox();
                                txtMatKhauCu.Focus();
                                return;
                            }
                            else
                            {
                                if (checkKTThuong(passNew))
                                {
                                    MessageBox.Show("Mật khẩu của bạn phải có ký tự ký tự viết thường.\nVui lòng kiểm tra lại.", "Lỗi thông tin nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    clearTextbox();
                                    txtMatKhauCu.Focus();
                                    return;
                                }
                                else
                                {
                                    if (!passNew.Equals(repeatPass))
                                    {
                                        MessageBox.Show("Mật mới không chính xác!!!/nVui lòng kiểm tra lại.", "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        LoadForm();
                                        clearTextbox();
                                        txtMatKhauCu.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        if (!passCu.Equals(passWordHT))
                                        {
                                            MessageBox.Show("Mật khẩu củ không chính xác!!!/nVui lòng kiểm tra lại.", "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            LoadForm();
                                            clearTextbox();
                                            txtMatKhauCu.Focus();
                                            return;
                                        }

                                        else
                                        {
                                            if (passNew.Equals(passWordHT))
                                            {
                                                if (MessageBox.Show("Mật khẩu mới trùng với mật khẩu hiện tại của bạn!!!/n/nNhấn OK nếu bạn muốn sử dụng mật khẩu củ.", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                                                {
                                                    this.Close();
                                                }
                                                else
                                                {
                                                    LoadForm();
                                                    clearTextbox();
                                                    txtMatKhauCu.Focus();
                                                }
                                            }
                                            else
                                            {
                                                //Cập Nhật Thành Công
                                                CapNhatMatKhau(userName, passNew);
                                                this.Close();
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
