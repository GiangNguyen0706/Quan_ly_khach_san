using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace Đồ_án_1___QLKS
{
    public partial class QuanLyThuePhong : Form
    {
        SqlConnection con;
        public QuanLyThuePhong()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=DESKTOP-49A7EKC;Initial Catalog=QLKS;Integrated Security=True");
            con.Open();
        }
        public void loadKhachHang()
        {
            SqlCommand cmd = new SqlCommand("select HoTenKH from KhachHang", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbKH.DataSource = dt;
            cbKH.DisplayMember = "HoTenKH";
            cbKH.ValueMember = "HoTenKH";
        }
        public void loadKhachHang2()
        {
            SqlCommand cmd = new SqlCommand("select HoTenKH from KhachHang", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbTen.DataSource = dt;
            cbTen.DisplayMember = "HoTenKH";
            cbTen.ValueMember = "HoTenKH";
        }
        public void loadChonPhong2()
        {
            SqlCommand cmd = new SqlCommand("select * from Phong where TrangThai=N'Trống'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbPhong.DataSource = dt;
            cbPhong.DisplayMember = "TenPhong";
            cbPhong.ValueMember = "TenPhong";
        }
        public void loadChonPhong()
        {
            SqlCommand cmd = new SqlCommand("select * from Phong where TrangThai=N'Đã đặt'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbChonPhong.DataSource = dt;
            cbChonPhong.DisplayMember = "TenPhong";
            cbChonPhong.ValueMember = "TenPhong";
        }
        public void loadChonDV()
        {
            SqlCommand cmd = new SqlCommand("select TenDV from DichVu", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            libChonDV.DataSource = dt;
            libChonDV.DisplayMember = "TenDV";
            libChonDV.ValueMember = "TenDV";
        }
        public void loadChonDV2()
        {
            SqlCommand cmd = new SqlCommand("select TenDV from DichVu", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            libDV.DataSource = dt;
            libDV.DisplayMember = "TenDV";
            libDV.ValueMember = "TenDV";
        }
        public void loadHoaDon()
        {
            SqlCommand cmd = new SqlCommand("select * from DatPhong", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvHoaDon.DataSource = dt;
        }
        //public void loadNhanDatPhong()
        //{
        //    SqlCommand cmd = new SqlCommand("select * from DatPhong", con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    dgvNhanDatPhong.DataSource = dt;
        //}
        private void QuanLyThuePhong_Load(object sender, EventArgs e)
        {
            loadKhachHang();
            loadKhachHang2();
            loadChonPhong();
            loadChonPhong2();
            loadChonDV();
            loadChonDV2();
            loadHoaDon();
            loaitienphong2();
            loaitienphong();
        }

        public void loadPhong()
        {
            SqlCommand cmd = new SqlCommand("select * from Phong", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvHoaDon.DataSource = dt;
        }
        private void setDaDat()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update Phong set TrangThai = @trangthai where TenPhong = @tenphong", con);
                cmd.Parameters.AddWithValue("@trangthai", "Đã đặt");
                cmd.Parameters.AddWithValue("@tenphong", cbChonPhong.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("");
            }
            loadPhong();
        }
        private void setDaDat2()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update Phong set TrangThai = @trangthai where TenPhong = @tenphong", con);
                cmd.Parameters.AddWithValue("@trangthai", "Đã đặt");
                cmd.Parameters.AddWithValue("@tenphong", cbPhong.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("");
            }
            loadPhong();
        }
        private void setTrong()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update Phong set TrangThai = @trangthai where TenPhong = @tenphong", con);
                cmd.Parameters.AddWithValue("@trangthai", "Trống");
                cmd.Parameters.AddWithValue("@tenphong", cbChonPhong.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
                loadPhong();
            }
            catch (Exception)
            {
                MessageBox.Show("");
            }
            loadPhong();
        }
        private void btLapHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert into DatPhong(MaDat,TenPhong,KhachHangDat,NgayDat,TenDV,KhoangThoiGian) values (@madat,@tenphong,@khdat,@ngaydat,@tendv,@tg)", con);
                cmd.Parameters.AddWithValue("@madat", txtMaHD.Text);
                cmd.Parameters.AddWithValue("@tenphong", cbChonPhong.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@khdat", cbKH.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@ngaydat", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@tendv", libChonDV.Text);
                cmd.Parameters.AddWithValue("@tg", txtTG.Text);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Lập hóa đơn thành công");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Lập hóa đơn thất bại");
            }
            setDaDat();
            loadChonPhong();
            loadHoaDon();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                DangNhap f = new DangNhap();
                f.ShowDialog();
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvHoaDon.CurrentRow.Index;
            if (chon >= 0)
            {
                SqlCommand cmd = new SqlCommand(
                    "update DatPhong set TenPhong=@tenphong,KhachHangDat=@khachhangdat,NgayDat=@ngaydat,TenDV=@tendv,KhoangThoiGian=@tg where MaDat = @madatcu", con);

                cmd.Parameters.AddWithValue("@tenphong", cbChonPhong.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@khachhangdat", cbKH.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@ngaydat", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@tendv", libChonDV.Text);
                cmd.Parameters.AddWithValue("@tg", txtTG.Text);
                cmd.Parameters.AddWithValue("@madatcu", dgvHoaDon.Rows[chon].Cells["MaDat"].Value.ToString());

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Sửa thành công");
                }
                else MessageBox.Show("Sửa thất bại");
                loadHoaDon();
            }
        }
        public void loaitienphong()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = string.Format(@"select TenLoaiPhong from Phong where TenPhong = N'" +cbChonPhong.Text+ "'", con);
            SqlDataAdapter dt = new SqlDataAdapter();
            dt.SelectCommand = cmd;
            DataTable dt1 = new DataTable();
            dt.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                string ma = dt1.Rows[0]["TenLoaiPhong"].ToString();
                cmd.CommandText = string.Format(@"select Gia from LoaiPhong where TenLoaiPhong = N'" +ma+ "'", con);
                SqlDataAdapter dt2 = new SqlDataAdapter();
                dt2.SelectCommand = cmd;
                DataTable db = new DataTable();
                dt2.Fill(db);
                if(db.Rows.Count >0)
                {
                    txtTienPhong.Text = db.Rows[0]["Gia"].ToString();
                }
            }
        }
        public void loaitienphong2()
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = string.Format(@"select GiaDV from DichVu where TenDV = N'"+libChonDV.Text+"'", con);
            SqlDataAdapter dt = new SqlDataAdapter();
            dt.SelectCommand = cmd;
            DataTable dt1 = new DataTable();
            dt.Fill(dt1);
            if(dt1.Rows.Count > 0)
            {
                txtTienDV.Text = dt1.Rows[0]["GiaDV"].ToString();
            }
        }

        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            int chon = -1;
            chon = dgvHoaDon.CurrentRow.Index;
            
            if (chon >= 0)
            {
                txtMaHD.Text = dgvHoaDon.Rows[chon].Cells["MaDat"].Value.ToString().Trim();
                cbChonPhong.SelectedValue = dgvHoaDon.Rows[chon].Cells["TenPhong"].Value;
                cbKH.SelectedValue = dgvHoaDon.Rows[chon].Cells["KhachHangDat"].Value;
                dateTimePicker1.Value = DateTime.Parse(dgvHoaDon.Rows[chon].Cells["NgayDat"].Value.ToString());
                libChonDV.Text = dgvHoaDon.Rows[chon].Cells["TenDV"].Value.ToString();
                txtTG.Text = dgvHoaDon.Rows[chon].Cells["KhoangThoiGian"].Value.ToString().Trim();
                //txtTong.Text = (Int32.Parse(txtTG.Text) * (Int32.Parse(txtTienPhong.Text)));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert into DatPhong(MaDat,TenPhong,KhachHangDat,NgayDat,TenDV,KhoangThoiGian) values (@madat,@tenphong,@khdat,@ngaydat,@tendv,@tg)", con);
                cmd.Parameters.AddWithValue("@madat", txtMa.Text);
                cmd.Parameters.AddWithValue("@tenphong", cbPhong.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@khdat", cbTen.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@ngaydat", dateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@tendv", libDV.Text);
                cmd.Parameters.AddWithValue("@tg", txtktg.Text);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Nhận đặt phòng thành công");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Nhận đặt phòng thất bại");
            }
            setDaDat2();
            loadChonPhong2();
            loadHoaDon();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string excelFilePath = null;
            SqlCommand cmd = new SqlCommand("select * from Phong where TrangThai=N'Đã đặt'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            da.Fill(tbl);
            var excelApp = new Excel.Application();
            excelApp.Workbooks.Add();

            // single worksheet
            Excel._Worksheet workSheet = excelApp.ActiveSheet;
            workSheet.Cells[1, 3] = "DANH SÁCH";
            // column headings
            for (var i = 0; i < tbl.Columns.Count; i++)
            {

                workSheet.Cells[2, i + 1] = tbl.Columns[i].ColumnName;
            }

            // rows
            for (var i = 0; i < tbl.Rows.Count; i++)
            {
                // to do: format datetime values before printing
                for (var j = 0; j < tbl.Columns.Count; j++)
                {
                    if (j == 3)
                    { workSheet.Cells[i + 3, j + 1] = Convert.ToDateTime(tbl.Rows[i][j]).ToString("MM/dd/yyyy"); }
                    else
                    { workSheet.Cells[i + 3, j + 1] = tbl.Rows[i][j]; }
                }
            }

            if (!string.IsNullOrEmpty(excelFilePath))
            {
                try
                {
                    workSheet.SaveAs(excelFilePath);
                    excelApp.Quit();
                    MessageBox.Show("Excel file saved!");
                }
                catch (Exception ex)
                {
                    throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                        + ex.Message);
                }
            }
            else
            {
                excelApp.Visible = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            QuanLyDanhMuc Obj = new QuanLyDanhMuc();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            BaoCaoThongKe Obj = new BaoCaoThongKe();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            QuanLyHeThong Obj = new QuanLyHeThong();
            Obj.Show();
            this.Hide();
        }

        private void libChonDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            loaitienphong2();
        }

        private void cbChonPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            loaitienphong();
            if (txtTienPhong.Text != "" && txtTienDV.Text != "")
            {
                double tong = double.Parse(txtTienPhong.Text) + double.Parse(txtTienDV.Text);
                txtTong.Text = tong + "";
            }
        }
    }
}
