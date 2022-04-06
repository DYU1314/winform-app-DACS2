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
    public partial class frmReport : Form
    {
        private string nhanbiet = frmQuanLyDiem._NhanBiet;
        private string nhanbiet1 = frmXemDiem._nhanbiet;

        private string malop = frmQuanLyDiem._maLop;

        private int malop1 = frmXemDiem._malop;
        private int mahocphan = frmXemDiem._mahocphan;
        private int mahocki = frmXemDiem._hocki;

        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            if (nhanbiet == "indsSinhVien" && malop != "")
            {
                this.rp1.BringToFront();
                this.rp2.SendToBack();
                this.rp3.SendToBack();
                //Load danh sách thông tin sinh viên
                //TODO: This line of code loads data into the 'db_QLDSVKKTCNDataSet.USP_LoadDanhSachSinhVienReport' table. You can move, or remove it, as needed.
                this.USP_LoadDanhSachSinhVienReportTableAdapter.Fill(this.db_QLDSVKKTCNDataSet.USP_LoadDanhSachSinhVienReport, malop);
                this.rp1.RefreshReport();
                return;
            }
            else
            {
                if (nhanbiet1 == "indiemhocphan" && malop1 != 0 && mahocphan != 0)
                {
                    this.rp1.SendToBack();
                    this.rp2.BringToFront();
                    this.rp3.SendToBack();
                    //Danh sách điểm học phần của lớp
                    // TODO: This line of code loads data into the 'db_QLDSVKKTCNDataSet.USP_DanhSachSinhVienVaDiemHocPhanReport' table. You can move, or remove it, as needed.
                    this.USP_DanhSachSinhVienVaDiemHocPhanReportTableAdapter.Fill(this.db_QLDSVKKTCNDataSet.USP_DanhSachSinhVienVaDiemHocPhanReport, malop1, mahocphan);
                    this.rp2.RefreshReport();
                    return;
                }
                else
                {
                    if (nhanbiet1 == "indiemhocki" && malop1 != 0 && mahocki != 0)
                    {
                        this.rp1.SendToBack();
                        this.rp2.SendToBack();
                        this.rp3.BringToFront();
                        //Điêm trung bình học kì của lớp
                        // TODO: This line of code loads data into the 'db_QLDSVKKTCNDataSet.USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLop' table. You can move, or remove it, as needed.
                        this.USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopTableAdapter.Fill(this.db_QLDSVKKTCNDataSet.USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLop, mahocki, malop1);
                        this.rp3.RefreshReport();
                        return;
                    }
                }
            }
        }
    }
}
