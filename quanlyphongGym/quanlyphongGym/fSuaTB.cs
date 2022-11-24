using quanlyphongGym.BUS;
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
    public partial class fSuaTB : Form
    {
        private ThietBiCTL thietBiCTL = new ThietBiCTL();
        private ThietBiDTO tb = new ThietBiDTO();
        private DataGridViewRow curRow;
        private bool isAnotherImage = false;
        private string imgLoc;

        public fSuaTB(DataGridViewRow curRow)
        {
            InitializeComponent();
            this.curRow = curRow;

            // row data to form
            txtTenTB.Text = curRow.Cells["tentb"].Value.ToString();
            cmbLoaiTB.Text = curRow.Cells["loaitb"].Value.ToString();
            txtSoLuongTB.Text = curRow.Cells["soluongtb"].Value.ToString();
            txtHangSXTB.Text = curRow.Cells["hangsxtb"].Value.ToString();
            cmbTinhTrangTB.Text = curRow.Cells["tinhtrangtb"].Value.ToString();
            txtSoLuongHuTB.Text = curRow.Cells["soluonghu"].Value.ToString();
            txtGhiChuTB.Text = curRow.Cells["ghichu"].Value.ToString();

            Byte[] data = new Byte[0];
            data = (Byte[])(curRow.Cells["hinhanh"].Value);
            MemoryStream mem = new MemoryStream(data);
            picBoxTB.Image = Image.FromStream(mem);
        }

        private Byte[] ImageToByteArray(string imgLocation)
        {
            Byte[] img = null;
            FileStream fs = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            img = br.ReadBytes((int)fs.Length);

            return img;
        }

        private void LayThongTinThietBi()
        {
            tb.ID_TB = curRow.Cells["id_tb"].Value.ToString();
            tb.Ten = txtTenTB.Text;
            tb.Loai = cmbLoaiTB.Text;

            int result = 0;
            bool sl = int.TryParse(txtSoLuongTB.Text, out result);
            tb.SoLuong = result;
            bool slh = int.TryParse(txtSoLuongHuTB.Text, out result);
            tb.SoLuongHu = result;

            tb.HangSX = txtHangSXTB.Text;
            tb.TinhTrang = cmbTinhTrangTB.Text;
            tb.GhiChu = txtGhiChuTB.Text;
            if (!isAnotherImage)
                tb.HinhAnh = (Byte[])curRow.Cells["hinhanh"].Value;
            else
                tb.HinhAnh = ImageToByteArray(imgLoc);
        }

        private void txtSoLuongTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cmbTinhTrangTB_TextChanged(object sender, EventArgs e)
        {
            if (cmbTinhTrangTB.Text == "Hư")
                txtSoLuongHuTB.Enabled = true;
            else
            {
                txtSoLuongHuTB.Enabled = false;
                txtSoLuongHuTB.Text = "0";
            }
        }

        private void picBoxTB_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Chọn ảnh đại diện";
                dlg.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imgLoc = dlg.FileName;
                    picBoxTB.ImageLocation = imgLoc;
                    isAnotherImage = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLuuSP_Click(object sender, EventArgs e)
        {
            try
            {
                LayThongTinThietBi();
                thietBiCTL.ThietBi = tb;
                thietBiCTL.update();

                MessageBox.Show("Lưu THÀNH CÔNG!", "Thông báo");
            }
            catch (Exception)
            {
                MessageBox.Show("Lưu THẤT BẠI!", "Thông báo");
            }
        }

        private void txtSoLuongTB_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
