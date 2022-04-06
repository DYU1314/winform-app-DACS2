using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO
{
    class KhoaHocDTO
    {
        public KhoaHocDTO(int id, string makhoahoc, string tenkhoahoc, int nambatdau, int namketthuc, int tghtoithieu, int tghtieuchuan, int tghtoida)
        {
            this.Id = id;
            this.Makhoahoc = makhoahoc;
            this.Tenkhoahoc = tenkhoahoc;
            this.Nambatdau = nambatdau;
            this.Namketthuc = namketthuc;
            this.Tghtoithieu = tghtoithieu;
            this.Tghtieuchuan = tghtieuchuan;
            this.Tghtoida = tghtoida;
        }

        public KhoaHocDTO(DataRow rows)
        {
            this.Id = (int)rows["ID"];
            this.Makhoahoc = rows["MAKHOAHOC"].ToString();
            this.Tenkhoahoc = rows["TENKHOAHOC"].ToString();
            this.Nambatdau = (int)rows["NAMBATDAU"];
            this.Namketthuc = (int)rows["NAMKETTHUC"];
            this.Tghtoithieu = (int)rows["THOIGIANHOCTOITHIEU"];
            this.Tghtieuchuan = (int)rows["THOIGIANHOCTIEUCHUAN"];
            this.Tghtoida = (int)rows["THOIGIANHOCTOIDA"];
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string makhoahoc;

        public string Makhoahoc
        {
            get { return makhoahoc; }
            set { makhoahoc = value; }
        }

        private string tenkhoahoc;

        public string Tenkhoahoc
        {
            get { return tenkhoahoc; }
            set { tenkhoahoc = value; }
        }

        private int nambatdau;

        public int Nambatdau
        {
            get { return nambatdau; }
            set { nambatdau = value; }
        }

        private int namketthuc;

        public int Namketthuc
        {
            get { return namketthuc; }
            set { namketthuc = value; }
        }

        private int tghtoithieu;

        public int Tghtoithieu
        {
            get { return tghtoithieu; }
            set { tghtoithieu = value; }
        }

        private int tghtieuchuan;

        public int Tghtieuchuan
        {
            get { return tghtieuchuan; }
            set { tghtieuchuan = value; }
        }

        private int tghtoida;

        public int Tghtoida
        {
            get { return tghtoida; }
            set { tghtoida = value; }
        }
    }
}
