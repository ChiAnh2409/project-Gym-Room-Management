using quanlyphongGym.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlyphongGym.DAO
{
    public partial class HoiVienDAO : DataProvider
    {
        protected override object GetDataFromDataRow(DataTable dt, int i)
        {
            HoiVienDTO hv = new HoiVienDTO();
            hv.ID_HV = dt.Rows[i]["id_hv"].ToString();
            hv.HoTen = dt.Rows[i]["hoten"].ToString();
            hv.GioiTinh = dt.Rows[i]["gioitinh"].ToString();
            hv.SDT = dt.Rows[i]["sdt"].ToString();
            hv.GoiTap = dt.Rows[i]["goitap"].ToString();
            hv.HinhAnh = (Byte[])dt.Rows[i]["hinhanh"];

            return (object)hv;
        }

        public void insert(HoiVienDTO info)
        {
            string insertCommand = "INSERT INTO HOIVIEN(id_hv, hoten, gioitinh, sdt, ngayhethan, goitap, hinhanh) VALUES('" +
                                   info.ID_HV + "', N'" +
                                   info.HoTen + "', N'" +
                                   info.GioiTinh + "', '" +
                                   info.SDT + "', '" +
                                   info.GoiTap + "', @img)";

            executeNonQuery(insertCommand, info.HinhAnh);
        }

        public void update(HoiVienDTO info)
        {
            string updateCommand = "UPDATE HOIVIEN " +
                                   "SET hoten = N'" + info.HoTen + "', " +
                                   " gioitinh = N'" + info.GioiTinh + "', " +
                                   " sdt = '" + info.SDT + "', " + 
                                   " goitap = N'" + info.GoiTap + "'," +
                                   " WHERE id_hv LIKE '%" + info.ID_HV + "%'";

            executeNonQuery(updateCommand);
        }

        public void delete(string id)
        {
            string deleteCommand = "DELETE FROM HOIVIEN WHERE id_hv LIKE '%" + id + "%'";
            executeNonQuery(deleteCommand);
        }
        public int countTBType(string type)
        {
            string cmd = "SELECT COUNT('id_hv') FROM HOIVIEN WHERE gioitinh LIKE N'%" + type + "%'";
            return (int)executeScalar(cmd);
        }
        public void exit()
        {
            disconnect();
        }

        public ArrayList getDsHocVien(string keyword = null)
        {
            connect();
            string cmd;
            if (keyword == null || keyword.Length == 0)
                cmd = "SELECT * FROM HOIVIEN";
            else
                cmd = "SELECT * FROM HOIVIEN WHERE hoten LIKE N'%" + keyword + "%'";
            adapter = new SqlDataAdapter(cmd, connection);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            ArrayList arr = ConvertDataSetToArrayList(dataset);

            return arr;
        }
    }
}
