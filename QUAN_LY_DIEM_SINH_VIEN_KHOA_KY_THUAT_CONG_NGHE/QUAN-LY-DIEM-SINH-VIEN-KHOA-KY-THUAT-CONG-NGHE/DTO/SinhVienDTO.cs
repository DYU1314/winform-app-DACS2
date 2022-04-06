using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO
{
    class SinhVienDTO
    {
        public SinhVienDTO(int id, string mssv, string ho, string ten, string gioitinh, DateTime? ngaysinh, string sodienthoai, string email, string diachi, int idlop)
        {
            this.Id = id;
            this.Mssv = mssv;
            this.Ho = ho;
            this.Ten = ten;
            this.Gioitinh = gioitinh;
            this.Ngaysinh = ngaysinh;
            this.Sodienthoai = sodienthoai;
            this.Email = email;
            this.Diachi = diachi;
            this.Idlop = idlop;
        }

        public SinhVienDTO(DataRow row)
        {
            this.Id = (int)row["ID"];
            this.Mssv = row["MSSV"].ToString();
            this.Ho = row["HO"].ToString();
            this.Ten = row["TEN"].ToString();
            this.Gioitinh = row["GIOITINH"].ToString();

            var ngaysinh = row["NGAYSINH"];
            if (ngaysinh.ToString() != "")
                this.Ngaysinh = (DateTime?)ngaysinh;

            this.Sodienthoai = row["SODIENTHOAI"].ToString();
            this.Email = row["EMAIL"].ToString();
            this.Diachi = row["DIACHI"].ToString();
            this.Idlop = (int)row["IDLOP"];
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string mssv;

        public string Mssv
        {
            get { return mssv; }
            set { mssv = value; }
        }
        private string ho;

        public string Ho
        {
            get { return ho; }
            set { ho = value; }
        }
        private string ten;

        public string Ten
        {
            get { return ten; }
            set { ten = value; }
        }
        private string gioitinh;

        public string Gioitinh
        {
            get { return gioitinh; }
            set { gioitinh = value; }
        }
        private DateTime? ngaysinh;

        public DateTime? Ngaysinh
        {
            get { return ngaysinh; }
            set { ngaysinh = value; }
        }
        private string sodienthoai;

        public string Sodienthoai
        {
            get { return sodienthoai; }
            set { sodienthoai = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string diachi;

        public string Diachi
        {
            get { return diachi; }
            set { diachi = value; }
        }
        private int idlop;

        public int Idlop
        {
            get { return idlop; }
            set { idlop = value; }
        }
    }
}
