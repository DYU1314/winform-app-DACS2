using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DAO
{
    class HocKiDAO
    {
        private static HocKiDAO instance;

        public static HocKiDAO Instance
        {
            get { if (instance == null) instance = new HocKiDAO(); return instance; }
            private set { instance = value; }
        }

        private HocKiDAO() { }

        public List<HocKiDTO> GetListHocKi()
        {
            List<HocKiDTO> list = new List<HocKiDTO>();
            string query = "SELECT * FROM dbo.HOCKI";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                HocKiDTO q = new HocKiDTO(item);
                list.Add(q);
            }
            return list;
        }

        public DataTable LoadListDanhSachHocKi()
        {
            return DataProvider.Instance.ExecuteQuery("EXECUTE dbo.USP_LoadDanhSachHocKi");
        }

        public int GetCountByMaHocKi(string mahocki)
        {
            return (int)DataProvider.Instance.ExecuteScalar("EXECUTE dbo.USP_GetCountByMaHocKi @maHocki ", new object[] { mahocki });
        }

        public int GetCountByIdKhoaHoc(int idkhoahoc)
        {
            return (int)DataProvider.Instance.ExecuteScalar("EXECUTE dbo.USP_GetCountHocKiByIdKhoaHoc @idKhoaHoc ", new object[] { idkhoahoc });
        }

        public HocKiDTO GetHocKiByMaHocKi(string maHocKi)
        {
            HocKiDTO q = null;
            string query = "SELECT * FROM dbo.HOCKI WHERE MAHOCKI = N'" + maHocKi + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                q = new HocKiDTO(item);
                return q;
            }
            return q;
        }

        public bool InsertHocKi(string maHocKi, string tenHocKi, string tgBatDau, string tgKetThuc, int idKhoaHoc)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("INSERT INTO dbo.HOCKI( MAHOCKI , TENHOCKI , TGBATDAU , TGKETTHUC , IDKHOAHOC ) VALUES ( N'{0}' , N'{1}' , N'{2}' , N'{3}' , {4} )", maHocKi, tenHocKi, tgBatDau, tgKetThuc, idKhoaHoc));
            return result > 0;
        }

        public bool UpdateHocKi(string maHocKi, string tenHocKi, string tgbd, string tgkt, int idKhoaHoc)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("UPDATE dbo.HOCKI SET TENHOCKI = N'{0}' , TGBATDAU = N'{1}' , TGKETTHUC = N'{2}' , IDKHOAHOC = {3} WHERE MAHOCKI = N'{4}'", tenHocKi, tgbd, tgkt, idKhoaHoc, maHocKi));
            return result > 0;
        }

        public bool DeleteHocKi(string maHocKi)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("DELETE dbo.HOCKI WHERE MAHOCKI = N'{0}'", maHocKi));
            return result > 0;
        }

        public DataTable timKiemCoMaHK(string maHK, int idKhoaHoc)
        {
            string query = string.Format("EXEC USP_TimKiemCoMaHK @maHK = N'{0}' , @idKhoaHoc = N'{1}'", maHK, idKhoaHoc);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timKiemKhongMaHocKi(int idKhoaHoc)
        {
            string query = string.Format("EXEC USP_TimKiemKhongMaHK @idKhoaHoc = N'{0}' ", idKhoaHoc);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable getLopByIdKhoaHoc1(int idKhoaHoc)
        {
            string query = string.Format("SELECT MAHOCKI FROM dbo.HOCKI WHERE IDKHOAHOC = {0}", idKhoaHoc);
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
