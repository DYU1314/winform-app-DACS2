using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DAO
{
    class SinhVienDAO
    {

        private static SinhVienDAO instance;

        public static SinhVienDAO Instance
        {
            get { if (instance == null) instance = new SinhVienDAO(); return instance; }
            private set { instance = value; }
        }

        private SinhVienDAO() { }

        public DataTable LoadListDanhSachSinhVien()
        {
            return DataProvider.Instance.ExecuteQuery("EXECUTE dbo.USP_LoadDanhSachSinhVien");
        }

        public DataTable LoadListDanhSachSinhVienQLD(string maLop)
        {
            string query = "EXECUTE dbo.USP_LoadDanhSachSinhVienQLD @maLop ";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maLop });
        }

        public int GetCountByMaSinhVien(string maoSinhVien)
        {
            return (int)DataProvider.Instance.ExecuteScalar("EXECUTE dbo.USP_GetCountSinhVienByMaSoSinhVien @maoSinhVien ", new object[] { maoSinhVien });
        }

        public bool InsertSinhVien(string mssv, string ho, string ten, string gioitinh, string ngaysinh, string sodienthoai, string email, string diachi, int idLop)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("INSERT INTO dbo.SINHVIEN ( MSSV , HO , TEN , GIOITINH , NGAYSINH , SODIENTHOAI , EMAIL , DIACHI , IDLOP ) VALUES ( N'{0}' , N'{1}' , N'{2}' , N'{3}' , N'{4}' , N'{5}' , N'{6}' , N'{7}' , N'{8}' )", mssv, ho, ten, gioitinh, ngaysinh, sodienthoai, email, diachi, idLop));
            return result > 0;
        }

        public bool UpdatetSinhVien(string mssv, string ho, string ten, string gioitinh, string ngaysinh, string sodienthoai, string email, string diachi, int idLop)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("UPDATE dbo.SINHVIEN SET HO = N'{0}' , TEN = N'{1}' , GIOITINH = N'{2}' , NGAYSINH = N'{3}' , SODIENTHOAI = N'{4}' , EMAIL = N'{5}' , DIACHI = N'{6}' , IDLOP = {7} WHERE MSSV = N'{8}'", ho, ten, gioitinh, ngaysinh, sodienthoai, email, diachi, idLop, mssv));
            return result > 0;
        }

        public SinhVienDTO GetSVbymssv(string mssv)
        {
            SinhVienDTO q = null;
            string query = "SELECT * FROM dbo.SINHVIEN WHERE MSSV = N'" + mssv + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                q = new SinhVienDTO(item);
                return q;
            }

            return q;
        }

        public bool DeleteSinhVien(string mssv)
        {
            SinhVienDTO sinhvien = SinhVienDAO.instance.GetSVbymssv(mssv);
            int id = sinhvien.Id;

            KQHocPhanDAO.Instance.XoaKQHocPhanCuaSinhVien(id);
            KQHocPhanDAO.Instance.XoaKetQuaCuoiKiCuaSinhVien(id);

            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("DELETE dbo.SINHVIEN WHERE MSSV = N'{0}' ", mssv));
            return result > 0;
        }

        public DataTable timkiemMSSV(string mssv, string malop)
        {
            string query = string.Format("EXEC USP_TimKiemMSSV @mssv = N'{0}' , @malop = N'{1}' ", mssv, malop);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timkiemTenSV(string ten, string malop)
        {
            string query = string.Format("EXEC USP_TimKiemTenSV @ten = N'{0}' , @malop = N'{1}' ", ten, malop);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timkiemMaLopSV(string malop)
        {
            string query = string.Format("EXEC USP_TimKiemMaLopSV @malop = N'{0}' ", malop);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timkiemTCSV(string mssv, string ten, string malop)
        {
            string query = string.Format("EXEC USP_TimKiemtcSV @mssv = N'{0}' @ten = N'{1}' , @malop = N'{2}' ", mssv, ten, malop);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetSinhVienbyMSSV(string mssv)
        {
            string query = "SELECT * FROM dbo.SINHVIEN WHERE MSSV = N'" + mssv + "'";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public SinhVienDTO GetSinhVienbyMSSVDiem(string mssv)
        {
            SinhVienDTO q = null;
            string query = string.Format(string.Format("SELECT * FROM dbo.SINHVIEN WHERE MSSV = N'" + mssv + "'"));
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                q = new SinhVienDTO(item);
                return q;
            }

            return q;
        }

        public DataTable ReportSinhVien(string malop)
        {
            string query = "USP_LoadDanhSachSinhVienReport @maLop";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { malop });
        }

        public DataTable timkiemMSSVQLD(string maSoSV)
        {
            string query = string.Format("EXEC USP_TimKiemMaSoSVQLD @maSo = N'{0}' ", maSoSV);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timkiemTenSVQLD(string ten)
        {
            string query = string.Format("EXEC USP_TimKiemTenSVQLD @ten = N'{0}' ", ten);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timkiemMaSovsTenSVQLD(string maSoSV, string ten)
        {
            string query = string.Format("EXEC USP_TimKiemMaSovsTenSVQLD @maSo = N'{0}' , @ten = N'{1}' ", maSoSV, ten);
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
