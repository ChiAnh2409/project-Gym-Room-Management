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
    public partial class fThemSP : Form
    {
        private SanPhamCTL sanphamCTL = new SanPhamCTL();
        private SanPhamDTO sp = new SanPhamDTO();
        private int sophukien;
        private int sovatdung;
        private string imgLoc;

        public fThemSP()
        {
            InitializeComponent();
        }
        public fThemSP(int sophukien, int sovatdung)
        {
            InitializeComponent();
            cmbLoaiSP.SelectedIndex = 0;
            cmbTinhTrangSP.SelectedIndex = 0;
            this.sophukien = sophukien;
            this.sovatdung = sovatdung;

        }
        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };
            func(Controls);
        }
        private Byte[] ImageToByteArray(string imgLocation)
        {
            Byte[] img = null;
            FileStream fs = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            img = br.ReadBytes((int)fs.Length);

            return img;
        }
        private void LayThongTinSanPham()
        {
            int result = 0;
            sp.Ten = txtTenSP.Text;
            sp.Loai = cmbLoaiSP.Text;
            //sp.SoLuong = txtSoLuongSP.Text;
            sp.HangSX = txtHangSXSP.Text;
            sp.TinhTrang = cmbTinhTrangSP.Text;
            sp.TrongLuong = txtTrongLuongSP.Text;
            sp.DonGia = txtDonGiaSP.Text;


            bool b = int.TryParse(txtSoLuongSP.Text, out result);
            sp.SoLuong = result;
            if (sp.Loai == "Phụ kiện")

            {
                sophukien++;
                sp.ID_SP = "PK00" + sophukien.ToString();
            }
            else if (sp.Loai == "Vật dụng")
            {
                sovatdung++;
                sp.ID_SP = "VD00" + sovatdung.ToString();
            }
            if (picBoxSP.Image != null)
                sp.HinhAnh = ImageToByteArray(imgLoc);
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbTinhTrangSP_TextChanged(object sender, EventArgs e)
        {

        }

        private void picBoxSP_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Chọn ảnh đại diện";
                dlg.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imgLoc = dlg.FileName;
                    picBoxSP.ImageLocation = imgLoc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaHetSP_Click_1(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void btnLuuSP_Click_1(object sender, EventArgs e)
        {
            try
            {
                LayThongTinSanPham();
                sanphamCTL.SanPham = sp;
                sanphamCTL.insert();

                MessageBox.Show("Thêm THÀNH CÔNG!", "Thông báo");
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn chưa thêm ảnh!", "Thông báo");
            }
        }

        private void txtSoLuongSP_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}

