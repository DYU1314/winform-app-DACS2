using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO
{
    public class TaiKhoan
    {
        public TaiKhoan(int id, string tentaikhoan, string matkhau, string hoten, string tenhienthi, string sodienthoai, string email, int idquyen, string diachi)
        {
            this.Id = id;
            this.Tentaikhoan = tentaikhoan;
            this.Matkhau = matkhau;
            this.Hoten = hoten;
            this.Tenhienthi = tenhienthi;
            this.Sodienthoai = sodienthoai;
            this.Email = email;
            this.Diachi = diachi;
            this.Idquyen = idquyen;
        }

        public TaiKhoan(DataRow rows)
        {
            this.Id = (int)rows["ID"];
            this.Tentaikhoan = rows["TENTAIKHOAN"].ToString();
            this.Matkhau = rows["MATKHAU"].ToString();
            this.Hoten = rows["HOTEN"].ToString();
            this.Tenhienthi = rows["TENHIENTHI"].ToString();
            this.Sodienthoai = rows["SODIENTHOAI"].ToString();
            this.Email = rows["EMAIL"].ToString();
            this.Diachi = rows["DIACHI"].ToString();
            this.Idquyen = (int)rows["IDQUYEN"];
        }

        private int id;
        private string tentaikhoan;
        private string matkhau;
        private string hoten;
        private string tenhienthi;
        private string sodienthoai;
        private string email;
        private string diachi;
        private int idquyen;

        public int Id { get { return id; } set { id = value; } }
        public string Tentaikhoan { get { return tentaikhoan; } set { tentaikhoan = value; } }
        public string Matkhau { get { return matkhau; } set { matkhau = value; } }
        public string Hoten { get { return hoten; } set { hoten = value; } }
        public string Tenhienthi { get { return tenhienthi; } set { tenhienthi = value; } }
        public string Sodienthoai { get { return sodienthoai; } set { sodienthoai = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Diachi { get { return diachi; } set { diachi = value; } }
        public int Idquyen { get { return idquyen; } set { idquyen = value; } }
    }
}
