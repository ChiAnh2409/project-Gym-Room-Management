using quanlyphongGym.BUS;
using quanlyphongGym.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlyphongGym
{
    public partial class fThemHV : Form
    {
        private HoiVienCTL hoiVienCTL = new HoiVienCTL();
        private HoiVienDTO hv = new HoiVienDTO();
        private int iLastRowID;
        private string imgLoc;

        public fThemHV()
        {
            InitializeComponent();
        }
        public fThemHV(string lastRowID)
        {
            InitializeComponent();
            cmbGoiTapHV.SelectedIndex = 0;
            cmbGioiTinhHV.SelectedIndex = 0;
            
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnLuuSP_Click(object sender, EventArgs e)
        {
            try
            {
                LayThongTinHoiVien();
                hoiVienCTL.HoiVien = hv;
                hoiVienCTL.insert();

                MessageBox.Show("Thêm THÀNH CÔNG!", "Thông báo");
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn chưa thêm ảnh!", "Thông báo");
            }
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
        private void LayThongTinHoiVien()
        {
            hv.HoTen = txtTenHV.Text;
            hv.SDT = txtSdtHV.Text;
            hv.GioiTinh = cmbGioiTinhHV.Text;
            hv.GoiTap = cmbGoiTapHV.Text;
            iLastRowID++;
            hv.ID_HV = "KH00" + iLastRowID.ToString();

            if (picBoxHV.Image != null)
                hv.HinhAnh = ImageToByteArray(imgLoc);
        }
        private void btnXoaHetHV_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
