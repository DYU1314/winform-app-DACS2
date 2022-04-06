using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DAO
{
    class LopDAO
    {
        private static LopDAO instance;

        public static LopDAO Instance
        {
            get { if (instance == null) instance = new LopDAO(); return instance; }
            private set { instance = value; }
        }

        private LopDAO() { }

        public DataTable LoadListLop()
        {
            return DataProvider.Instance.ExecuteQuery("EXECUTE dbo.USP_LoadListLop");
        }

        public DataTable LoadListLopByMaKhoaHoc(string maKhoaHoc)
        {
            string query = "EXECUTE dbo.USP_LoadListLopByMaKhoaHoc @maKhoaHoc";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maKhoaHoc });
        }

        public List<LopDTO> GetListLop()
        {
            List<LopDTO> list = new List<LopDTO>();
            string query = "SELECT * FROM dbo.LOP";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                LopDTO q = new LopDTO(item);
                list.Add(q);
            }
            return list;
        }

        public bool InsertLop(string maLop, string tenLop, int idKhoahoc)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("INSERT INTO dbo.LOP ( MALOP , TENLOP , IDKHOAHOC ) VALUES ( N'{0}' , N'{1}' , {2} )", maLop, tenLop, idKhoahoc));
            return result > 0;
        }

        public bool UpdatetLop(string malop, string tenLop, int idKhoahoc)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("UPDATE dbo.LOP SET TENLOP = N'{0}' , IDKHOAHOC = {1} WHERE MALOP = N'{2}'", tenLop, idKhoahoc, malop));
            return result > 0;
        }

        public bool DeletaLop(string maLop)
        {
            string query = String.Format("DELETE dbo.LOP WHERE MALOP = N'{0}'", maLop);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public DataTable timKiemCoMaLop(string maLop, string idKhoaHoc)
        {
            string query = string.Format("EXEC USP_TimKiemCoMaLop @maLop = N'{0}' , @maKhoaHoc = N'{1}'", maLop, idKhoaHoc);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timKiemTheoMaLop(string maLop)
        {
            string query = string.Format("EXEC USP_TimKiemLopTheoMaLop @maLop = N'{0}' ", maLop);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timKiemKhongMaLop(string maKhoaHoc)
        {
            string query = string.Format("EXEC USP_TimKiemLopTheoKhoaHoc @maKhoaHoc = N'{0}' ", maKhoaHoc);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public int GetCountByMaLop(string maLop)
        {
            return (int)DataProvider.Instance.ExecuteScalar("EXECUTE dbo.USP_GetCountByMaLop @maLop ", new object[] { maLop });
        }

        public int GetCountLopByIDKhoaHoc(int idkhoahoc)
        {
            return (int)DataProvider.Instance.ExecuteScalar("EXECUTE USP_GetCountLopByIdKhoaHoc @idKhoaHoc ", new object[] { idkhoahoc });
        }

        public LopDTO GetLopById(int id)
        {
            LopDTO q = null;
            string query = "SELECT * FROM dbo.LOP WHERE ID = N'" + id + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                q = new LopDTO(item);
                return q;
            }

            return q;
        }

        public LopDTO GetLopByMaLop(string maLop)
        {
            LopDTO q = null;
            string query = "SELECT * FROM dbo.LOP WHERE MALOP = N'" + maLop + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                q = new LopDTO(item);
                return q;
            }
            return q;
        }
    }
}
