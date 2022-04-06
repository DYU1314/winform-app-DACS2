using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DAO
{
    class KQHocPhanDAO
    {
        private static KQHocPhanDAO instance;

        public static KQHocPhanDAO Instance
        {
            get { if (instance == null) instance = new KQHocPhanDAO(); return instance; }
            private set { instance = value; }
        }

        private KQHocPhanDAO() { }

        public DataTable LoadDanhSachSanhVienCuaLopChuaCoDiemHocPhan(int idLop, int idHocPhan)
        {
            return DataProvider.Instance.ExecuteQuery(string.Format("EXECUTE USP_DanhSachSinhVienChuaCoDiemHocPhan @idLop = {0} , @idHocPhan = {1}", idLop, idHocPhan));
        }

        public DataTable LoadDanhSachSanhVienCuaLopDaCoDiemHocPhan(int idLop, int idHocPhan)
        {
            return DataProvider.Instance.ExecuteQuery(string.Format("EXECUTE USP_DanhSachSinhVienDaCoDiemHocPhan @idLop = {0} , @idHocPhan = {1}", idLop, idHocPhan));
        }

        public DataTable LoadDanhSachSanhVienCuaLopvsDiemHocPhan(int idLop, int idHocPhan)
        {
            return DataProvider.Instance.ExecuteQuery(string.Format("EXECUTE USP_DanhSachSinhVienVaDiemHocPhan @idLop = {0} , @idHocPhan = {1}", idLop, idHocPhan));
        }

        public bool InsertKQHocPhan(string dchuyencan, string dgiuaki, string dCuoiki, int idHp, int idSV)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("INSERT INTO dbo.KQHOCPHAN ( DIEMCHUYENCAN , DIEMGIUAKI , DIEMTHICUOIKI , IDHOCPHAN , IDSINHVIEN ) VALUES ( N'{0}' , N'{1}' , N'{2}' , {3} , {4} )", dchuyencan, dgiuaki, dCuoiki, idHp, idSV));
            return result > 0;
        }

        public bool UpdateKQHocPhan(string dchuyencan, string dgiuaki, string dCuoiki, int idHp, int idSV)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("UPDATE dbo.KQHOCPHAN SET DIEMCHUYENCAN = N'{0}' , DIEMGIUAKI = N'{1}' , DIEMTHICUOIKI = N'{2}' WHERE IDHOCPHAN = N'{3}' AND IDSINHVIEN = N'{4}' ", dchuyencan, dgiuaki, dCuoiki, idHp, idSV));
            return result > 0;
        }

        public DataTable LoadDanhSachDiemHocPhanCuaSinhVien(int idsinhvien)
        {
            return DataProvider.Instance.ExecuteQuery(string.Format("EXECUTE USP_DanhSachTatCacDiemHocPhanCuaSinhVien @idSinhVien = {0} ", idsinhvien));
        }

        public DataTable LoadDanhSachDiemHocKiCuaTatCaSinhVienTrongLop(int idhocki, int idlop)
        {
            return DataProvider.Instance.ExecuteQuery(string.Format("EXECUTE USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopXem @idHocKi = {0} , @idLop = {1}", idhocki, idlop));
        }

        public bool XoaKQHocPhanCuaSinhVien(int idsinhvien)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("DELETE FROM KQHOCPHAN WHERE IDSINHVIEN = {0}", idsinhvien));
            return result > 0;
        }

        public bool XoaKetQuaCuoiKiCuaSinhVien(int idsinhvien)
        {
            int result = DataProvider.Instance.ExecuteNonQuery(string.Format("DELETE FROM KQCUOIKI WHERE IDSINHVIEN = {0}", idsinhvien));
            return result > 0;
        }
    }
}