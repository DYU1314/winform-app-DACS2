using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DAO
{
    class HocPhanDAO
    {
        private static HocPhanDAO instance;

        public static HocPhanDAO Instance
        {
            get { if (instance == null) instance = new HocPhanDAO(); return instance; }
            private set { instance = value; }
        }

        private HocPhanDAO() { }

        public DataTable LoadDanhSachHocPhan()
        {
            return DataProvider.Instance.ExecuteQuery("EXECUTE USP_LoadDanhSachHocPhan");
        }

        public DataTable LoadDanhSachHocPhanTheoKhoa(int maKhoa)
        {
            return DataProvider.Instance.ExecuteQuery(string.Format("EXECUTE USP_LoadDanhSachHocPhanTheoIDKhoa @id = N'{0}'", maKhoa));
        }

        public DataTable LoadDanhSachHocPhanTheoIDHocKi(int idHK)
        {
            return DataProvider.Instance.ExecuteQuery(string.Format("EXECUTE USP_LoadDanhSachHocPhanTheoIDHocKi @id = N'{0}'", idHK));
        }

        public HocPhanDTO GetDanhSachHocPhanByMaHocPhan(string maHocPhan)
        {
            HocPhanDTO q = null;
            string query = string.Format("SELECT * FROM dbo.HOCPHAN WHERE MAHOCPHAN = N'{0}' ", maHocPhan);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                q = new HocPhanDTO(item);
                return q;
            }

            return q;
        }

        public int GetCountByMaHocPhan(string maHocPhan)
        {
            return (int)DataProvider.Instance.ExecuteScalar("EXECUTE dbo.USP_GetCountByMaHocPhan @maHocPhan ", new object[] { maHocPhan });
        }

        public int GetCountByIdHocKi(int idHocKi)
        {
            return (int)DataProvider.Instance.ExecuteScalar("EXECUTE dbo.USP_GetCountHocPhanByIdHocKi @idHocKi ", new object[] { idHocKi });
        }

        public bool InsertHocPhan(string maHocPhan, string tenHocPhan, int idhocKi, int idKhoahoc)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("INSERT INTO dbo.HOCPHAN ( MAHOCPHAN , TENHOCPHAN , IDHOCKI , IDKHOAHOC ) VALUES ( N'{0}' , N'{1}' , {2} , {3})", maHocPhan, tenHocPhan, idhocKi, idKhoahoc));
            return result > 0;
        }

        public bool UpdatetHocPhan(string maHocPhan, string tenHocPhan, int idhocKi, int idKhoahoc)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("UPDATE dbo.HOCPHAN SET TENHOCPHAN = N'{0}' , IDHOCKI = {1} , IDKHOAHOC = {2} WHERE MAHOCPHAN = N'{3}'", tenHocPhan, idhocKi, idKhoahoc, maHocPhan));
            return result > 0;
        }

        public bool DeletaHocPhan(string maHocPhan)
        {
            string query = String.Format("DELETE dbo.HOCPHAN WHERE MAHOCPHAN = N'{0}'", maHocPhan);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public DataTable timKiemCoMaHocPhan(string maHocPhan, string idHocKi, string idKhoaHoc)
        {
            string query = string.Format("EXEC USP_TimKiemHocPhanCoMa @maHocPhan = N'{0}' , @maHocKi = N'{1}' , @maKhoaHoc = N'{2}'", maHocPhan, idHocKi, idKhoaHoc);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timKiemKhongMaHocPhan(string maHocPhan, string idHocKi, string idKhoaHoc)
        {
            string query = string.Format("EXEC USP_TimKiemHocPhanKhongMa @maHocKi = N'{0}' , @maKhoaHoc = N'{1}'", idHocKi, idKhoaHoc);
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
