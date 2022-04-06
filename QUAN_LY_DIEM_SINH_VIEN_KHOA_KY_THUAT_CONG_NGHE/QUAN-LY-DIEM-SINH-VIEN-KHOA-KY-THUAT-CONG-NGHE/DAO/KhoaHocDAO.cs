using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DAO
{
    class KhoaHocDAO
    {

        private static KhoaHocDAO instance;

        public static KhoaHocDAO Instance
        {
            get { if (instance == null) instance = new KhoaHocDAO(); return instance; }
            private set { instance = value; }
        }

        private KhoaHocDAO() { }

        public List<KhoaHocDTO> GetListKhoaHoc()
        {
            List<KhoaHocDTO> list = new List<KhoaHocDTO>();
            string query = "SELECT * FROM dbo.KHOAHOC";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                KhoaHocDTO q = new KhoaHocDTO(item);
                list.Add(q);
            }

            return list;
        }

        public DataTable LoadListKhoaHoc()
        {
            return DataProvider.Instance.ExecuteQuery("EXECUTE dbo.USP_LoadDanhSachKhoaHoc");
        }

        public int GetCountByMaKhoaHoc(string maKhoaHoc)
        {
            return (int)DataProvider.Instance.ExecuteScalar("EXECUTE dbo.USP_GetCountByMaKhoaHoc @maKhoaHoc ", new object[] { maKhoaHoc });
        }

        public KhoaHocDTO GetKhoaHocById(int id)
        {
            KhoaHocDTO q = null;
            string query = "SELECT * FROM dbo.KHOAHOC WHERE ID = N'" + id + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                q = new KhoaHocDTO(item);
                return q;
            }

            return q;
        }

        public KhoaHocDTO GetKhoaHocByMaKhoaHoc(string makhoahoc)
        {
            KhoaHocDTO q = null;
            string query = "SELECT * FROM dbo.KHOAHOC WHERE MAKHOAHOC = N'" + makhoahoc + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                q = new KhoaHocDTO(item);
                return q;
            }

            return q;
        }

        public bool InsertKhoaHoc(string maKH, string tenKH, int namBD, int namKT, int tghToiThieu, int tghToiDa, int tghTieuChuan)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(String.Format("INSERT INTO dbo.KHOAHOC( MAKHOAHOC , TENKHOAHOC , NAMBATDAU , NAMKETTHUC , THOIGIANHOCTOITHIEU , THOIGIANHOCTOIDA , THOIGIANHOCTIEUCHUAN ) VALUES( N'{0}', N'{1}', {2} , {3} , {4} , {5} , {6} )", maKH, tenKH, namBD, namKT, tghToiThieu, tghToiDa, tghTieuChuan));
            return result > 0;
        }

        public bool UpdateKhoaHoc(string maKhoahoc, string tenKH, int namKT, int tghToiThieu, int tghToiDa, int tghTieuChuan)
        {
            string query = String.Format("UPDATE dbo.KHOAHOC SET TENKHOAHOC = N'{0}' , NAMKETTHUC = {1} , THOIGIANHOCTOITHIEU = {2} , THOIGIANHOCTOIDA = {3} , THOIGIANHOCTIEUCHUAN = {4} WHERE MAKHOAHOC = N'{5}' ", tenKH, namKT, tghToiThieu, tghToiDa, tghTieuChuan, maKhoahoc);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool XoaKhoaHoc(string maKhoahoc)
        {
            string query = String.Format("DELETE dbo.KHOAHOC WHERE MAKHOAHOC = N'{0}'", maKhoahoc);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public DataTable timKiemMKH(string mkh)
        {
            string query = string.Format("EXEC USP_TimKiemMKH @mkh = N'{0}'", mkh);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timKiemTenKH(string tenKH)
        {
            string query = string.Format("EXEC USP_TimKiemTenKH @tenKH = N'{0}'", tenKH);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timKiemNamBD(int namBD)
        {
            string query = string.Format("EXEC USP_TimKiemNamBDKH @namBD = N'{0}'", namBD);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timKiemMaKHvsTen(string mkh, string ten)
        {
            string query = string.Format("EXEC USP_TimKiemMKHvsTenKH @mKH = N'{0}' , @tenKH = N'{1}'", mkh, ten);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timKiemMaKHvsNamBD(string mkh, int namBD)
        {
            string query = string.Format("EXEC USP_TimKiemMKHvsNamBD @mkh = N'{0}' , @namBD = N'{1}' ", mkh, namBD);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timKiemTenvsNamBD(string ten, int namBD)
        {
            string query = string.Format("EXEC USP_TimKiemTenKHvsNamBD @tenKH = N'{0}' , @namBD = '{1}' ", ten, namBD);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timKiemTatCa(string mkh, string ten, int namBD)
        {
            string query = string.Format("EXEC USP_TimKiemKHOAHOC @mkh = N'{0}' , @tenKH = N'{1}' , @namBD = N'{2}'", mkh, ten, namBD);
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
