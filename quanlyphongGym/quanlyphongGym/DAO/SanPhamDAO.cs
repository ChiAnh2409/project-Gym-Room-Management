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
    public partial class SanPhamDAO : DataProvider
    {
        protected override object GetDataFromDataRow(DataTable dt, int i)
        {
            SanPhamDTO sp = new SanPhamDTO();
            sp.ID_SP = dt.Rows[i]["id_sp"].ToString();
            sp.Ten = dt.Rows[i]["ten"].ToString();
            sp.Loai = dt.Rows[i]["loai"].ToString();
            sp.NgayNhap = (DateTime)dt.Rows[i]["ngaynhap"];
            sp.SoLuong = Convert.ToInt32((dt.Rows[i]["soluong"]));
            sp.DonGia = dt.Rows[i]["dongia"].ToString();
            sp.TrongLuong = dt.Rows[i]["trongluong"].ToString();
            sp.HangSX = dt.Rows[i]["hangsx"].ToString();
            sp.TinhTrang = dt.Rows[i]["tinhtrang"].ToString();
            sp.HinhAnh = (Byte[])dt.Rows[i]["hinhanh"];

            return (object)sp;
        }

        public void insert(SanPhamDTO info)
        {
            string insertCommand = "INSERT INTO SANPHAM(id_sp, ten, loai, ngaynhap, soluong, dongia, trongluong, hangsx, tinhtrang, hinhanh) VALUES('" +
                                   info.ID_SP + "', N'" +
                                   info.Ten + "', N'" +
                                   info.Loai + "', '" +
                                   info.NgayNhap.ToString("MM/dd/yyyy hh:mm:ss tt") + "', '" +
                                   info.SoLuong + "', '" +
                                   info.DonGia + "', '" +
                                   info.TrongLuong + "', N'" +
                                   info.HangSX + "', N'" +
                                   info.TinhTrang + "', @img)";

            executeNonQuery(insertCommand, info.HinhAnh);
        }

        public void update(SanPhamDTO info)
        {
            string updateCommand = "UPDATE SANPHAM " +
                                   "SET ten = N'" + info.Ten + "', " +
                                   " loai = N'" + info.Loai + "', " +
                                   " ngaynhap = '" + info.NgayNhap.ToString("MM/dd/yyyy hh:mm:ss tt") + "'," +
                                   " soluong = " + info.SoLuong + "," +
                                   " dongia = '" + info.DonGia + "', " +
                                   " trongluong = '" + info.TrongLuong + "', " +
                                   " hangsx = N'" + info.HangSX + "', " +
                                   " tinhtrang = N'" + info.TinhTrang + "', " +
                                   " hinhanh = @img" +
                                   " WHERE id_sp LIKE '%" + info.ID_SP + "%'";

            executeNonQuery(updateCommand, info.HinhAnh);
        }

        public void delete(string id)
        {
            string deleteCommand = "DELETE FROM SANPHAM WHERE id_sp LIKE '" + id + "'";
            executeNonQuery(deleteCommand);
        }
        public int countSPType(string type)
        {
            string cmd = "SELECT COUNT('id_sp') FROM SANPHAM WHERE loai LIKE N'%" + type + "%'";
            return (int)executeScalar(cmd);
        }
        public void exit()
        {
            disconnect();
        }

        public ArrayList getDsSanPham(string keyword = null)
        {
            connect();
            string cmd;
            if (keyword == null || keyword == "Search...")
                cmd = "SELECT * FROM SANPHAM";
            else
                cmd = "SELECT * FROM SANPHAM WHERE ten LIKE N'%" + keyword + "%'";
            adapter = new SqlDataAdapter(cmd, connection);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            ArrayList arr = ConvertDataSetToArrayList(dataset);

            return arr;
        }
    }
}

