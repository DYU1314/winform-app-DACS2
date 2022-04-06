using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO
{
    class LopDTO
    {
        public LopDTO(int id, string malop, string tenlop, int idkhoahoc)
        {
            this.Id = id;
            this.Malop = malop;
            this.Tenlop = tenlop;
            this.Idkhoahoc = idkhoahoc;
        }

        public LopDTO(DataRow rows)
        {
            this.Id = (int)rows["ID"];
            this.Malop = rows["MALOP"].ToString();
            this.Tenlop = rows["TENLOP"].ToString();
            this.Idkhoahoc = (int)rows["IDKHOAHOC"];
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string malop;

        public string Malop
        {
            get { return malop; }
            set { malop = value; }
        }

        private string tenlop;

        public string Tenlop
        {
            get { return tenlop; }
            set { tenlop = value; }
        }

        private int idkhoahoc;

        public int Idkhoahoc
        {
            get { return idkhoahoc; }
            set { idkhoahoc = value; }
        }
    }
}
