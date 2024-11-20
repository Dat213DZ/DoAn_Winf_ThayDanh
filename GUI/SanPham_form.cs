using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using System.IO;
using DTO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net.Http;
using System.Linq;

namespace GUI
{
    public partial class SanPham_form : Form
    {
        private Cloudinary cloudinary;
        DanhMucBLL dmBLL = new DanhMucBLL();
        SanPhamBLL spBLL = new SanPhamBLL();
        private string targetFilePath = null;
        private string temp_targetFilePath = null;
        int id = -1;
        public SanPham_form()
        {
            InitializeComponent();
            LoadCBDM();
            LoadSP();
            CloudinaryAccount();
        }
        public void CloudinaryAccount()
        {
            var account = new Account(
                "dqoqkz6uu", 
                "437322322766756", 
                "aEGAb8UafvRPY0bLGvW43IGA1UQ"  
            );
            cloudinary = new Cloudinary(account);
        }
        public void LoadCBDM()
        {
            bunifuDropdown_DanhMuc.DataSource = dmBLL.LoadDM();
            bunifuDropdown_DanhMuc.ValueMember = "ID_DanhMuc";
            bunifuDropdown_DanhMuc.DisplayMember = "TenDanhMuc";
        }
        public void LoadSP()
        {
            bunifuDataGridView_SanPham.DataSource = spBLL.LoadSP();
            bunifuDataGridView_SanPham.ReadOnly = true;
            bunifuDataGridView_SanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }
        private void bunifuButton_DanhMuc_Click(object sender, EventArgs e)
        {
            DanhMuc_form dm = new DanhMuc_form();
            dm.Show();
            this.Hide();
        }

        private void bunifuButton_ThemAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    bunifuPictureBox_SanPham.Load(filePath);
                    targetFilePath = filePath;
                }
            }
        }
        public string ThemAnh(string link)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(link),
                Folder = "my_uploads", // Thư mục trên Cloudinary
            };

            var uploadResult = cloudinary.Upload(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Lấy URL ảnh đã tải lên
                string imageUrl = uploadResult.SecureUrl.ToString();
                
                MessageBox.Show("Đăng ảnh thành công: " + imageUrl);

                bunifuPictureBox_SanPham.Load(imageUrl);
                return imageUrl;
            }
            else
            {
                MessageBox.Show("Lỗi đăng ảnh!");
            }
            return null;
        }

        public void XoaAnh(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var result = cloudinary.Destroy(deletionParams); // Gọi phương thức Delete (Destroy)

            if (result.Result != "ok")
            {
                MessageBox.Show("Lỗi xóa ảnh!!!");
            }

        }
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            
            if (targetFilePath == null)
            {
                MessageBox.Show("Lỗi ảnh!!!!");
                return;
            }    
            SanPham sp = new SanPham();
            sp.Gia = decimal.Parse(bunifuTextBox_Gia.Text);
            sp.TenSanPham = bunifuTextBox_TenSP.Text;
            sp.MoTa = bunifuTextBox_MoTa.Text;
            sp.SoLuongTon = int.Parse(bunifuTextBox_SL.Text);
            sp.HinhAnh = ThemAnh(targetFilePath);
            sp.ID_DanhMuc = int.Parse(bunifuDropdown_DanhMuc.SelectedValue.ToString());
            if (spBLL.ThemSP(sp))
            {
                MessageBox.Show("Thêm sản phẩm thành công");
                LoadSP();
            }
            else
            {
                MessageBox.Show("Thêm thất bại !!!");
            }

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (id == -1)
            {
                MessageBox.Show("Hãy chọn sản phẩm để xóa");
            }
            else
            {
                if (spBLL.XoaSP(id))
                {
                    var deletionParams = temp_targetFilePath.Split('/').Last().Split('.')[0];
                    XoaAnh(deletionParams);
                }
                else
                {
                    MessageBox.Show("Xóa xóa thất bại");
                }

            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if (temp_targetFilePath == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm trước khi sửa!!!");
                return;

            }
            SanPham sp = new SanPham();
            sp.ID_SanPham = id;
            sp.Gia = decimal.Parse(bunifuTextBox_Gia.Text);
            sp.TenSanPham = bunifuTextBox_TenSP.Text;
            sp.MoTa = bunifuTextBox_MoTa.Text;
            sp.SoLuongTon = int.Parse(bunifuTextBox_SL.Text);
            sp.HinhAnh = ThemAnh(targetFilePath);
            var deletionParams = new Uri(temp_targetFilePath).Segments.Last().Split('.')[0];
            XoaAnh(deletionParams);
            sp.ID_DanhMuc = int.Parse(bunifuDropdown_DanhMuc.SelectedValue.ToString());
            if (spBLL.SuaSP(sp))
            {
                MessageBox.Show("Sửa sản phẩm thành công");
                LoadSP();
            }
            else
            {
                MessageBox.Show("Sửa thất bại !!!");
            }
        }

        private async void bunifuDataGridView_SanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = bunifuDataGridView_SanPham.Rows[e.RowIndex];

                // Gán dữ liệu từ hàng đã chọn
                id = int.Parse(row.Cells["ID_SanPham"].Value.ToString());
                bunifuTextBox_Gia.Text = row.Cells["Gia"].Value.ToString();
                bunifuTextBox_MoTa.Text = row.Cells["MoTa"].Value.ToString();
                bunifuTextBox_SL.Text = row.Cells["SoLuongTon"].Value.ToString();
                bunifuTextBox_TenSP.Text = row.Cells["TenSanPham"].Value.ToString();
                bunifuDropdown_DanhMuc.SelectedText = row.Cells["ID_DanhMuc"].ToString();
                
                // URL ảnh lưu trên Cloudinary
                string imageUrl = row.Cells["HinhAnh"].Value.ToString();
                temp_targetFilePath = imageUrl;
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    try
                    {
                        // Tải ảnh từ URL không đồng bộ
                        using (HttpClient client = new HttpClient())
                        {
                            var response = await client.GetAsync(imageUrl);
                            if (response.IsSuccessStatusCode)
                            {
                                using (var stream = await response.Content.ReadAsStreamAsync())
                                {
                                    Image originalImage = Image.FromStream(stream);

                                    // Resize ảnh để hiển thị trong PictureBox
                                    int newWidth = 349;  // Chiều rộng mới
                                    int newHeight = 224; // Chiều cao mới
                                    Bitmap resizedImage = new Bitmap(originalImage, new System.Drawing.Size(newWidth, newHeight));

                                    // Hiển thị ảnh trong PictureBox
                                    bunifuPictureBox_SanPham.Image = resizedImage;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Không thể tải ảnh từ URL: " + imageUrl);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message);
                    }
                }
                else
                {
                    // Xử lý khi không có URL ảnh
                    bunifuPictureBox_SanPham.Image = null;
                    MessageBox.Show("Không tìm thấy URL ảnh!");
                }
            }
        }


        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            bunifuTextBox_Gia.Clear();
            bunifuTextBox_MoTa.Clear(); 
            bunifuTextBox_SL.Clear();
            bunifuTextBox_TenSP.Clear();
            bunifuPictureBox_SanPham.Image = null;
        }

        private void bunifuPictureBox_SanPham_Click(object sender, EventArgs e)
        {

        }
    }
}
