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
    public partial class ThietBiDAO : DataProvider
    {
        protected override object GetDataFromDataRow(DataTable dt, int i)
        {
            ThietBiDTO tb = new ThietBiDTO();
            tb.ID_TB = dt.Rows[i]["id_tb"].ToString();
            tb.Ten = dt.Rows[i]["ten"].ToString();
            tb.Loai = dt.Rows[i]["loai"].ToString();
            tb.SoLuong = Convert.ToInt32(dt.Rows[i]["soluong"]);
            tb.HangSX = dt.Rows[i]["hangsx"].ToString();
            tb.TinhTrang = dt.Rows[i]["tinhtrang"].ToString();
            tb.SoLuongHu = Convert.ToInt32(dt.Rows[i]["soluonghu"]);
            tb.GhiChu = dt.Rows[i]["ghichu"].ToString();
            tb.HinhAnh = (Byte[])dt.Rows[i]["hinhanh"];

            return (object)tb;
        }

        public void insert(ThietBiDTO info)
        {
            string insertCommand = "INSERT INTO THIETBI(id_tb, ten, loai, soluong, hangsx, tinhtrang, soluonghu, ghichu, hinhanh) VALUES('" +
                                   info.ID_TB + "', N'" +
                                   info.Ten + "', N'" +
                                   info.Loai + "', '" +
                                   info.SoLuong + "', N'" +
                                   info.HangSX + "', N'" +
                                   info.TinhTrang + "', '" +
                                   info.SoLuongHu + "', N'" +
                                   info.GhiChu + "', @img)";

            executeNonQuery(insertCommand, info.HinhAnh);
        }

        public void update(ThietBiDTO info)
        {
            string updateCommand = "UPDATE THIETBI " +
                                   "SET ten = N'" + info.Ten + "', " +
                                   " loai = N'" + info.Loai + "', " +
                                   " soluong = " + info.SoLuong + "," +
                                   " hangsx = N'" + info.HangSX + "'," +
                                   " tinhtrang = N'" + info.TinhTrang + "'," +
                                   " soluonghu = " + info.SoLuongHu + "," +
                                   " ghichu = N'" + info.GhiChu + "'" +
                                   " WHERE id_tb LIKE '%" + info.ID_TB + "%'";

            executeNonQuery(updateCommand);
        }

        public void delete(string id)
        {
            string deleteCommand = "DELETE FROM THIETBI WHERE id_tb LIKE '%" + id + "%'";
            executeNonQuery(deleteCommand);
        }

        public int countTBType(string type)
        {
            string cmd = "SELECT COUNT('id_tb') FROM THIETBI WHERE loai LIKE N'%" + type + "%'";
            return (int)executeScalar(cmd);
        }

        public void exit()
        {
            disconnect();
        }

        public ArrayList getDsThietBi(string keyword = null)
        {
            connect();
            string cmd;
            if (keyword == null || keyword.Length == 0)
                cmd = "SELECT * FROM THIETBI";
            else
                cmd = "SELECT * FROM THIETBI WHERE ten LIKE N'%" + keyword + "%'";
            adapter = new SqlDataAdapter(cmd, connection);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            ArrayList arr = ConvertDataSetToArrayList(dataset);

            return arr;
        }
    }
}

