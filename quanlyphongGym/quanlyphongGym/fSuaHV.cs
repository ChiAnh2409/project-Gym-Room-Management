using quanlyphongGym.BUS;
using quanlyphongGym.DAO;
using quanlyphongGym.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlyphongGym
{
    public partial class fSuaHV : Form
    {
        private HoiVienCTL hoiVienCTL = new HoiVienCTL();
        private HoiVienDTO hv = new HoiVienDTO();
        private DataGridViewRow curRow;
        private bool isAnotherImage = false;
        private int iLastRowID;
        private string imgLoc;

        public fSuaHV(DataGridViewRow curRow)
        {
            InitializeComponent();
            this.curRow = curRow;
            // row data to form
            txtTenHV.Text = curRow.Cells["hoten"].Value.ToString();
            cmbGioiTinhHV.Text = curRow.Cells["gioitinh"].Value.ToString();
            txtSdtHV.Text = curRow.Cells["sdt"].Value.ToString();
            cmbGoiTapHV.Text = curRow.Cells["goitap"].Value.ToString();
            Byte[] data = new Byte[0];
            data = (Byte[])(curRow.Cells["hinhanh"].Value);
            MemoryStream mem = new MemoryStream(data);
            picBoxHV.Image = Image.FromStream(mem);
        }
        private Byte[] ImageToByteArray(string imgLocation)
        {
            Byte[] img = null;
            FileStream fs = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            img = br.ReadBytes((int)fs.Length);

            return img;
        }
        public void LayThongTinHoiVien()
        {
            hv.ID_HV = curRow.Cells["id_hv"].Value.ToString();
            hv.HoTen = txtTenHV.Text;
            hv.GioiTinh = cmbGioiTinhHV.Text;
            hv.SDT = txtSdtHV.Text;
            hv.GoiTap = cmbGoiTapHV.Text;
            if (!isAnotherImage)
                hv.HinhAnh = (Byte[])curRow.Cells["hinhanh"].Value;
            else
                hv.HinhAnh = ImageToByteArray(imgLoc);
        }

        private void btnLuuHV_Click(object sender, EventArgs e)
        {
            LayThongTinHoiVien();
            hoiVienCTL.HoiVien = hv;
            hoiVienCTL.update();

            MessageBox.Show("Lưu THÀNH CÔNG!", "Thông báo");
        }

        private void cmbGoiTapHV_TextChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            switch (cmbGoiTapHV.Text)
            {
                case "1 tháng":
                    dt = dt.AddMonths(1);
                    break;
                case "3 tháng":
                    dt = dt.AddMonths(3);
                    break;
                case "VIP":
                    dt = dt.AddMonths(13);
                    break;
                case "Thường":
                    dt = dt.AddMonths(7);
                    break;
            }
        }

        private void txtSdtHV_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void picBoxHV_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Chọn ảnh đại diện";
                dlg.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imgLoc = dlg.FileName;
                    picBoxHV.ImageLocation = imgLoc;
                    isAnotherImage = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
