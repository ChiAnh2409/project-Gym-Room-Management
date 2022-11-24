using quanlyphongGym.BUS;
using quanlyphongGym.DTO;
using System;
using System.Collections;
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
    public partial class FTableManager : Form
    {
        private HoiVienCTL hoiVienCTL = new HoiVienCTL();
        private ThietBiCTL thietBiCTL = new ThietBiCTL();
        private SanPhamCTL sanPhamCTL = new SanPhamCTL();
        private HoiVienDTO hoiVienDTO = new HoiVienDTO();
        private ThietBiDTO thietBiDTO = new ThietBiDTO();
        private SanPhamDTO sanPhamDTO = new SanPhamDTO();
        private ArrayList dsHoiVien;
        private ArrayList dsThietBi;
        private ArrayList dsSanPham;
        private string imgLoc;
        private bool isAnotherImage = false;
        public FTableManager()
        {
            InitializeComponent();
            tabCtrl.DrawItem += new DrawItemEventHandler(tabCtrl_DrawItem);
        }
        // Support functions
        private void loadHoiVien(string keyword = null)
        {
            if (dsHoiVien != null)
                dsHoiVien.Clear();

            dsHoiVien = hoiVienCTL.getDsHocVien(keyword);
            if (dsHoiVien.Count == 0)
                dtgvHoiVien.DataSource = null;
            dtgvHoiVien.DataSource = dsHoiVien;
            if (dtgvHoiVien.RowCount > 0)
            {
                dtgvHoiVien.Columns[0].HeaderText = "Mã học viên";
                dtgvHoiVien.Columns[1].HeaderText = "Họ tên";
                dtgvHoiVien.Columns[2].HeaderText = "giới tính";
                dtgvHoiVien.Columns[3].HeaderText = "Số điện thoại";
                dtgvHoiVien.Columns[4].HeaderText = "gói tập";
                dtgvHoiVien.Columns[5].Visible = false;
            }
            dtgvHoiVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void loadSanPham(string keyword = null)
        {
            if (dsSanPham != null)
                dsSanPham.Clear();

            dsSanPham = sanPhamCTL.getDsSanPham(keyword);
            dtgvSanPham.DataSource = dsSanPham;
            if (dtgvSanPham.RowCount > 0)
            {
                dtgvSanPham.Columns[0].HeaderText = "Mã sản phẩm";
                dtgvSanPham.Columns[1].HeaderText = "Tên";
                dtgvSanPham.Columns[2].HeaderText = "Loại";
                dtgvSanPham.Columns[3].HeaderText = "Số lượng";
                dtgvSanPham.Columns[4].HeaderText = "Hãng sản xuất";
                dtgvSanPham.Columns[5].HeaderText = "Tình trạng";
                dtgvSanPham.Columns[6].HeaderText = "Trọng lượng";
                dtgvSanPham.Columns[7].HeaderText = "Đơn giá";
                dtgvSanPham.Columns[8].Visible = false;
            }
            dtgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void loadThietBi(string keyword = null)
        {
            if (dsThietBi != null)
                dsThietBi.Clear();

            dsThietBi = thietBiCTL.getDsThietBi(keyword);
            dtgvThietBi.DataSource = dsThietBi;
            if (dtgvThietBi.RowCount > 0)
            {
                dtgvThietBi.Columns[0].HeaderText = "Mã thiết bị";
                dtgvThietBi.Columns[1].HeaderText = "Tên";
                dtgvThietBi.Columns[2].HeaderText = "Loại";
                dtgvThietBi.Columns[3].HeaderText = "Số lượng";
                dtgvThietBi.Columns[4].HeaderText = "Số lượng hư";
                dtgvThietBi.Columns[5].HeaderText = "Tình trạng";
                dtgvThietBi.Columns[6].HeaderText = "Hãng sản xuất";
                dtgvThietBi.Columns[7].HeaderText = "Ghi chú";
                dtgvThietBi.Columns[8].Visible = false;
            }
            dtgvThietBi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        public Byte[] ImageToByteArray(string imgLocation)
        {
            Byte[] img = null;
            FileStream fs = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            img = br.ReadBytes((int)fs.Length);

            return img;
        }
        //dataGridView Row
        private void getCurrentRowHVInfo()
        {
            DataGridViewRow row = dtgvHoiVien.CurrentRow;
            hoiVienDTO.ID_HV = row.Cells["id_hv"].Value.ToString();
            hoiVienDTO.HoTen = row.Cells["hoten"].Value.ToString();
            hoiVienDTO.GioiTinh = row.Cells["gioitinh"].Value.ToString();
            hoiVienDTO.SDT = row.Cells["sdt"].Value.ToString();
            hoiVienDTO.GoiTap = row.Cells["goitap"].Value.ToString();
            hoiVienDTO.HinhAnh = (Byte[])row.Cells["hinhanh"].Value;
        }
        private void getCurrentRowSPInfo()
        {
            DataGridViewRow row = dtgvSanPham.CurrentRow;
            sanPhamDTO.ID_SP = row.Cells["id_sp"].Value.ToString();
            sanPhamDTO.Ten = row.Cells["tensp"].Value.ToString();
            sanPhamDTO.Loai = row.Cells["loaisp"].Value.ToString();
            sanPhamDTO.SoLuong = Convert.ToInt32(row.Cells["soluongsp"].Value);
            sanPhamDTO.DonGia = row.Cells["dongia"].Value.ToString();
            sanPhamDTO.TrongLuong = row.Cells["trongluong"].Value.ToString();
            sanPhamDTO.HangSX = row.Cells["hangsxsp"].Value.ToString();
            sanPhamDTO.TinhTrang = row.Cells["tinhtrangsp"].Value.ToString();
            sanPhamDTO.HinhAnh = (Byte[])row.Cells["hinhanh"].Value;
        }
        private void getCurrentRowTBInfo()
        {
            DataGridViewRow row = dtgvThietBi.CurrentRow;
            thietBiDTO.ID_TB = row.Cells["id_tb"].Value.ToString();
            thietBiDTO.Ten = row.Cells["tentb"].Value.ToString();
            thietBiDTO.Loai = row.Cells["loaitb"].Value.ToString();
            thietBiDTO.SoLuong = Convert.ToInt32(row.Cells["soluongtb"].Value);
            thietBiDTO.TinhTrang = row.Cells["tinhtrangtb"].Value.ToString();
            thietBiDTO.SoLuongHu = Convert.ToInt32(row.Cells["soluonghu"].Value);
            thietBiDTO.HangSX = row.Cells["hangsxtb"].Value.ToString();
            thietBiDTO.GhiChu = row.Cells["ghichu"].Value.ToString();
            thietBiDTO.HinhAnh = (Byte[])row.Cells["hinhanh"].Value;
        }
        //dataGridView Show 
        private void getHVRowToTxtBOX(DataGridViewRow row)
        {
            txtMaHV.Text = row.Cells[0].Value.ToString();
            txtTenHV.Text = row.Cells[1].Value.ToString();
            txtGioiTinhHV.Text = row.Cells[2].Value.ToString();
            txtSdtHV.Text = row.Cells[3].Value.ToString();
            txtGoiTapHV.Text = row.Cells[4].Value.ToString(); 
            Byte[] data = new Byte[0];
            data = (Byte[])(row.Cells["hinhanh"].Value);
            MemoryStream mem = new MemoryStream(data);
            picBoxHV.Image = Image.FromStream(mem);
        }
        private void getSPRowToTxtBOX(DataGridViewRow row)
        {
            txtTenSP.Text = row.Cells[1].Value.ToString();
            txtLoaiSP.Text = row.Cells[2].Value.ToString();
            txtSoLuongSP.Text = row.Cells[3].Value.ToString();
            txtHangSXSP.Text = row.Cells[7].Value.ToString();

            string tt = row.Cells[5].Value.ToString();
            lblTinhTrangTB.Text = tt;
            if (tt == "Còn hàng")
                lblTinhTrangTB.ForeColor = Color.MediumBlue;
            else if (tt == "Hết hàng")
                lblTinhTrangTB.ForeColor = Color.Red;

            Byte[] data = new Byte[0];
            data = (Byte[])(row.Cells["hinhanh"].Value);
            MemoryStream mem = new MemoryStream(data);
            picBoxTB.Image = Image.FromStream(mem);
        }
        private void getTBRowToTxtBOX(DataGridViewRow row)
        {
            txtTenTB.Text = row.Cells[1].Value.ToString();
            txtLoaiTB.Text = row.Cells[2].Value.ToString();
            txtSoLuongTB.Text = row.Cells[3].Value.ToString();
            txtHangSXTB.Text = row.Cells[6].Value.ToString();

            string tt = row.Cells[5].Value.ToString();
            lblTinhTrangTB.Text = tt;
            if (tt == "Mới")
                lblTinhTrangTB.ForeColor = Color.MediumBlue;
            else if (tt == "Tốt")
                lblTinhTrangTB.ForeColor = Color.Green;
            else if (tt == "Hư")
                lblTinhTrangTB.ForeColor = Color.Red;

            Byte[] data = new Byte[0];
            data = (Byte[])(row.Cells["hinhanh"].Value);
            MemoryStream mem = new MemoryStream(data);
            picBoxTB.Image = Image.FromStream(mem);
        }
      

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchHV_Click(object sender, EventArgs e)
        {

        }

        private void picBoxTB_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtMaHV_TextChanged(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }
        //image list color
        private void tabCtrl_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabCtrl.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabCtrl.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {
                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.White);
                g.FillRectangle(Brushes.DarkCyan, e.Bounds);
            }
            else
            {
                _textBrush = new SolidBrush(e.ForeColor);
                //e.DrawBackground();
                //_textBrush = new SolidBrush(Color.White);
                g.FillRectangle(Brushes.LightBlue, e.Bounds);
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", (float)13.0, FontStyle.Regular, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Near;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));

            // Draw image if available
            int indent = 3;
            Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y + indent, e.Bounds.Width, e.Bounds.Height - indent);
            if (tabCtrl.TabPages[e.Index].ImageIndex >= 0)
            {
                Image img = tabCtrl.ImageList.Images[tabCtrl.TabPages[e.Index].ImageIndex];
                float _x = (rect.X + rect.Width) - img.Width - 2 * indent;
                float _y = ((rect.Height - img.Height) / 2.0f) + rect.Y;
                e.Graphics.DrawImage(img, _x, _y);
            }
        }

        private void dtgvThietBi_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtgvThietBi.CurrentRow;
            getTBRowToTxtBOX(row);
        }
        // Search boxes placeholders
        private void txtSearchTB_Enter(object sender, EventArgs e)
        {
            if (txtSearchTB.Text == "Search...")
            {
                txtSearchTB.Text = "";
                txtSearchTB.ForeColor = Color.Black;
            }
        }

        private void txtSearchTB_Leave(object sender, EventArgs e)
        {
            if (txtSearchTB.Text == "")
            {
                txtSearchTB.Text = "Search...";
                txtSearchTB.ForeColor = Color.Gray;
            }
        }

        private void txtSearchHV_Enter(object sender, EventArgs e)
        {
            if (txtSearchHV.Text == "Search...")
            {
                txtSearchHV.Text = "";
                txtSearchHV.ForeColor = Color.Black;
            }
        }

        private void txtSearchHV_Leave(object sender, EventArgs e)
        {
            if (txtSearchHV.Text == "")
            {
                txtSearchHV.Text = "Search...";
                txtSearchHV.ForeColor = Color.Gray;
            }
        }

        private void txtSearchSP_Enter(object sender, EventArgs e)
        {
            if (txtSearchSP.Text == "Search...")
            {
                txtSearchSP.Text = "";
                txtSearchSP.ForeColor = Color.Black;
            }
        }

        private void txtSearchSP_Leave(object sender, EventArgs e)
        {
            if (txtSearchSP.Text == "")
            {
                txtSearchSP.Text = "Search...";
                txtSearchSP.ForeColor = Color.Gray;
            }
        }

        private void btnThemHV_Click(object sender, EventArgs e)
        {
            fThemHV fadd = new fThemHV();
            fadd.ShowDialog();
            loadHoiVien();
        }
        //button add, delete, update Thiet Bi
        private void btnThemTB_Click(object sender, EventArgs e)
        {
            int soMay = thietBiCTL.countTBType("Máy");
            int soTa = thietBiCTL.countTBType("Tạ");
            fThemTB fadd = new fThemTB(soMay, soTa);
            fadd.ShowDialog();
            loadThietBi();
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {

        }

        private void btnXoaTB_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Chấp nhận xóa dữ liệu ?", "Thông báo", MessageBoxButtons.OKCancel)
                != System.Windows.Forms.DialogResult.OK)
                return;

            try
            {
                getCurrentRowTBInfo();
                thietBiCTL.ThietBi = thietBiDTO;
                thietBiCTL.delete();

                MessageBox.Show("Xóa THÀNH CÔNG!");
                loadThietBi();
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa THẤT BẠI!");
            }
        }

        private void btnSuaTB_Click(object sender, EventArgs e)
        {
            DataGridViewRow curRow = dtgvThietBi.CurrentRow;
            fSuaTB fEdit = new fSuaTB(curRow);
            fEdit.ShowDialog();
            loadThietBi();
        }
        //search follow name
        private void txtSearchTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                loadThietBi(txtSearchTB.Text);
        }

        private void btnSearchTB_Click(object sender, EventArgs e)
        {
            loadThietBi(txtSearchTB.Text);
        }
        // Events
        private void FTableManager_Load_1(object sender, EventArgs e)
        {
            loadThietBi();
            loadSanPham();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void dtgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgvSanPham_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtgvSanPham.CurrentRow;
            getSPRowToTxtBOX(row);
        }
        //button add, delete, update San Pham
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            int sophukien = sanPhamCTL.countSPType("Phụ kiện");
            int sovatdung = sanPhamCTL.countSPType("Vật dụng");
            fThemSP fadd = new fThemSP(sophukien, sovatdung);
            fadd.ShowDialog();
            loadSanPham();

        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Chấp nhận xóa dữ liệu ?", "Thông báo", MessageBoxButtons.OKCancel)
                != System.Windows.Forms.DialogResult.OK)
                return;

            try
            {
                getCurrentRowSPInfo();
                sanPhamCTL.SanPham = sanPhamDTO;
                sanPhamCTL.delete();

                MessageBox.Show("Xóa THÀNH CÔNG!");
                loadSanPham();
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa THẤT BẠI!");
            }
        }

        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            DataGridViewRow curRow = dtgvSanPham.CurrentRow;
            fSuaSP fEdit = new fSuaSP(curRow);
            fEdit.ShowDialog();
            loadSanPham();
        }
        //search follow name
        private void txtSearchSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                loadSanPham(txtSearchSP.Text);
        }

        private void btnSearchSP_Click(object sender, EventArgs e)
        {
            loadSanPham(txtSearchSP.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnXoaHV_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Chấp nhận xóa dữ liệu ?", "Thông báo", MessageBoxButtons.OKCancel)
                != System.Windows.Forms.DialogResult.OK)
                return;

            try
            {
                getCurrentRowHVInfo();
                hoiVienCTL.HoiVien = hoiVienDTO;
                hoiVienCTL.delete();

                MessageBox.Show("Xóa THÀNH CÔNG!");
                loadHoiVien();
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa THẤT BẠI!");
            }
        }

        private void btnSuaHV_Click(object sender, EventArgs e)
        {
            DataGridViewRow curRow = dtgvHoiVien.CurrentRow;
            fSuaHV fEdit = new fSuaHV(curRow);
            fEdit.ShowDialog();
            loadHoiVien();
        }

        private void dtgvHoiVien_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dtgvHoiVien.CurrentRow;
            getHVRowToTxtBOX(row);
        }
    }
}
