using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO
{
    class HocKiDTO
    {
        public HocKiDTO(int id, string mahocki, string tenhocki, DateTime? tgbatdauhoc, DateTime? tgketthuchoc, int idkhoahoc)
        {
            this.Id = id;
            this.Mahocki = mahocki;
            this.Tenhocki = tenhocki;
            this.Tgbdhoc = tgbatdauhoc;
            this.Tgkthoc = tgketthuchoc;
            this.Idkhoahoc = idkhoahoc;
        }

        public HocKiDTO(DataRow rows)
        {
            this.Id = (int)rows["ID"];
            this.Mahocki = rows["MAHOCKI"].ToString();
            this.Tenhocki = rows["TENHOCKI"].ToString();

            var tgbatdauhoc = rows["TGBATDAU"];
            if (tgbatdauhoc.ToString() != "")
                this.Tgbdhoc = (DateTime?)tgbatdauhoc;

            var tgketthuchoc = rows["TGKETTHUC"];
            if (tgketthuchoc.ToString() != "")
                this.Tgkthoc = (DateTime?)tgketthuchoc;

            this.Idkhoahoc = (int)rows["IDKHOAHOC"];
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string mahocki;

        public string Mahocki
        {
            get { return mahocki; }
            set { mahocki = value; }
        }

        private string tenhocki;

        public string Tenhocki
        {
            get { return tenhocki; }
            set { tenhocki = value; }
        }

        private DateTime? tgbathoc;

        public DateTime? Tgbdhoc
        {
            get { return tgbathoc; }
            set { tgbathoc = value; }
        }

        private DateTime? tgkthoc;

        public DateTime? Tgkthoc
        {
            get { return tgkthoc; }
            set { tgkthoc = value; }
        }

        private int idkhoahoc;

        public int Idkhoahoc
        {
            get { return idkhoahoc; }
            set { idkhoahoc = value; }
        }
    }
}
