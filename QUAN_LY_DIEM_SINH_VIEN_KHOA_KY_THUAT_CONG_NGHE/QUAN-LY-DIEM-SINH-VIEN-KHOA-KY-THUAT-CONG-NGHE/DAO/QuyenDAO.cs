using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DAO
{
    class QuyenDAO
    {
        private static QuyenDAO instance;

        public static QuyenDAO Instance
        {
            get { if (instance == null) instance = new QuyenDAO(); return instance; }
            private set { instance = value; }
        }

        private QuyenDAO() { }

        public List<QuyenDTO> GetListQuyen()
        {
            List<QuyenDTO> list = new List<QuyenDTO>();
            string query = "SELECT * FROM dbo.QUYEN WHERE ID != 1";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                QuyenDTO q = new QuyenDTO(item);
                list.Add(q);
            }

            return list;
        }

        public QuyenDTO GetQuyenByID(int id)
        {
            QuyenDTO q = null;
            string query = "SELECT * FROM dbo.QUYEN WHERE ID = N'" + id + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                q = new QuyenDTO(item);
                return q;
            }

            return q;
        }

        public QuyenDTO GetIdByQuyen(string quyen)
        {
            QuyenDTO tk = null;
            string query = "SELECT * FROM dbo.QUYEN WHERE QUYEN = N'" + quyen + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                tk = new QuyenDTO(item);
                return tk;
            }
            return tk;
        }
    }
}
