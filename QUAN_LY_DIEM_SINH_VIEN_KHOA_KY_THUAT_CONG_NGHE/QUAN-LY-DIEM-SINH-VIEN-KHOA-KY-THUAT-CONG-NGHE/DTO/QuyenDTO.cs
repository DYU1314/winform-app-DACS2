using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO
{
    class QuyenDTO
    {
        public QuyenDTO(int id, string quyen)
        {
            this.Id = id;
            this.Quyen = quyen;
        }

        public QuyenDTO(DataRow rows)
        {
            this.Id = (int)rows["ID"];
            this.Quyen = rows["QUYEN"].ToString();
        }

        private int id;
        private string quyen;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Quyen
        {
            get { return quyen; }
            set { quyen = value; }
        }
    }
}
