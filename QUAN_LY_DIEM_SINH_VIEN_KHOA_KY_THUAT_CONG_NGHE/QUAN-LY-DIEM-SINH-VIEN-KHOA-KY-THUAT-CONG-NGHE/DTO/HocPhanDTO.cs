using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO
{
    class HocPhanDTO
    {
        public HocPhanDTO(int id, string mahocphan, string tenhocphan, int idhocki, int idkhoahoc)
        {
            this.Id = id;
            this.Mahocphan = mahocphan;
            this.Tenhocphan = tenhocphan;
            this.Idhocki = idhocki;
            this.Idkhoahoc = idkhoahoc;
        }

        public HocPhanDTO(DataRow row)
        {
            this.Id = (int)row["ID"];
            this.Mahocphan = row["MAHOCPHAN"].ToString();
            this.Tenhocphan = row["TENHOCPHAN"].ToString();
            this.Idhocki = (int)row["IDHOCKI"];
            this.Idkhoahoc = (int)row["IDKHOAHOC"];
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string mahocphan;

        public string Mahocphan
        {
            get { return mahocphan; }
            set { mahocphan = value; }
        }
        private string tenhocphan;

        public string Tenhocphan
        {
            get { return tenhocphan; }
            set { tenhocphan = value; }
        }
        private int idhocki;

        public int Idhocki
        {
            get { return idhocki; }
            set { idhocki = value; }
        }
        private int idkhoahoc;

        public int Idkhoahoc
        {
            get { return idkhoahoc; }
            set { idkhoahoc = value; }
        }
    }
}
