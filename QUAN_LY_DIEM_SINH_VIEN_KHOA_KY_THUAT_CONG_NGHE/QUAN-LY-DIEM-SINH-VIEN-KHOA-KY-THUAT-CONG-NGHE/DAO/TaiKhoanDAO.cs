using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DAO
{
    class TaiKhoanDAO
    {

        private static TaiKhoanDAO instance;

        public static TaiKhoanDAO Instance
        {
            get { if (instance == null) instance = new TaiKhoanDAO(); return instance; }
            private set { instance = value; }
        }

        private TaiKhoanDAO() { }

        public string MaHoa(string chuoi)
        {
            string hasPass = "admin";
            byte[] data = UTF8Encoding.UTF8.GetBytes(chuoi);

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hasPass));

                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    hasPass = Convert.ToBase64String(results, 0, results.Length);
                }
            }
            return hasPass;
        }

        public string GiaiMa(string chuoi)
        {
            string hash = "admin";
            byte[] data = Convert.FromBase64String(chuoi);

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));

                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    chuoi = UTF8Encoding.UTF8.GetString(results);
                }
            }
            return chuoi;
        }

        public DataTable GetTaiKhoan()
        {
            return DataProvider.Instance.ExecuteQuery("EXEC dbo.USP_LoadTaiKhoan");
        }

        public DataTable TKCoHoTen(string hoten, int quyen)
        {
            string query = string.Format("EXEC USP_TimKiemCoHoTen @quyen = N'{0}' , @hoTen = N'{1}'", quyen, hoten);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable timKiemQuyen(string hoten, int quyen)
        {
            string query = string.Format("EXEC USP_TimKiemQuyen @quyen = N'{0}'",quyen);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public TaiKhoan GetTaiKhoanByTenTaiKhoan(string userName)
        {
            string query = "EXECUTE dbo.USP_GetTaiKhoanByTenTaiKhoan  @userName ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { userName });
            foreach (DataRow item in data.Rows)
            {
                return new TaiKhoan(item);
            }
            return null;
        }

        public bool DangNhap(string userName, string passWord)
        {
            string hasPass = MaHoa(passWord);

            string query = "EXECUTE dbo.USP_Login @userName , @passWord ";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, hasPass });
            return result.Rows.Count > 0;
        }

        public bool CapNhatThongTinTaiKhoan(string tentaikhoan, string hoten, string tenhienthi, string sodienthoai, string email, string diachi)
        {
            string query = string.Format("UPDATE dbo.TAIKHOAN SET HOTEN = N'{0}' , TENHIENTHI = N'{1}' , SODIENTHOAI = N'{2}' , EMAIL = N'{3}' , DIACHI = N'{4}' WHERE TENTAIKHOAN = N'{5}'", hoten, tenhienthi, sodienthoai, email, diachi, tentaikhoan);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool CapNhatMatKhauTaiKhoan(string userName, string passWord)
        {

            string hasPass = TaiKhoanDAO.Instance.MaHoa(passWord);

            string query = string.Format("UPDATE dbo.TAIKHOAN SET MATKHAU = N'{0}' WHERE TENTAIKHOAN = N'{1}'", hasPass, userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public int GetCountByTenTaiKhoan(string userName)
        {
            return (int)DataProvider.Instance.ExecuteScalar("EXECUTE dbo.USP_GetCountByTenTaiKhoan @userName ", new object[] { userName });
        }

        public bool InsertTaiKhoan(string ttk, string mk, string ht, string tht, string sdt, string email, string dc, int q)
        {
            string hasPass = MaHoa(mk);

            string query = string.Format("INSERT INTO dbo.TAIKHOAN ( TENTAIKHOAN , MATKHAU , HOTEN , TENHIENTHI , SODIENTHOAI , EMAIL , DIACHI , IDQUYEN ) VALUES ( N'{0}' , N'{1}' , N'{2}' , N'{3}' , N'{4}' , N'{5}' , N'{6}' , N'{7}' )", ttk, hasPass, ht, tht, sdt, email, dc, q);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool CapNhatMatKhauvsQuyen(string userName, string passWord, int quyen)
        {
            string hasPass = MaHoa(passWord);

            string query = string.Format("UPDATE dbo.TAIKHOAN SET MATKHAU = N'{0}' , IDQUYEN = N'{1}' WHERE TENTAIKHOAN = N'{2}'", hasPass, quyen, userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool XoaTaiKhoan(string userName)
        {
            string query = string.Format("DELETE dbo.TAIKHOAN WHERE TENTAIKHOAN = N'{0}'", userName);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}