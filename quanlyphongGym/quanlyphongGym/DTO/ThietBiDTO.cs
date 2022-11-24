using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlyphongGym.DTO
{
    public class ThietBiDTO
    {
        private string _id;
        private string _ten;
        private string _loai;
        private int _soluong;
        private string _tinhtrang;
        private string _ghichu;
        private int _soluonghu;
        private string _hangsx;
        private Byte[] _hinhanh;

        public string ID_TB
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Ten
        {
            get { return _ten; }
            set { _ten = value; }
        }

        public string Loai
        {
            get { return _loai; }
            set { _loai = value; }
        }

        public int SoLuong
        {
            get { return _soluong; }
            set { _soluong = value; }
        }

        public int SoLuongHu
        {
            get { return _soluonghu; }
            set { _soluonghu = value; }
        }

        public string TinhTrang
        {
            get { return _tinhtrang; }
            set { _tinhtrang = value; }
        }

        public string HangSX
        {
            get { return _hangsx; }
            set { _hangsx = value; }
        }

        public string GhiChu
        {
            get { return _ghichu; }
            set { _ghichu = value; }
        }

        public Byte[] HinhAnh
        {
            get { return _hinhanh; }
            set { _hinhanh = value; }
        }
    }
}

