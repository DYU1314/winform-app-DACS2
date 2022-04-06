using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DAO;
using QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capture = Emgu.CV.Capture;

namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE
{
    public partial class frmMain : Form
    {
        public static string TenTaiKhoan = "";
        private string UserName = string.Empty;

        public frmMain()
        {
            InitializeComponent();
            LoadForm();
        }

        #region MeThods

        private void LoadForm()
        {
            mngQLDSV.Visible = false;
            tmgDangXuat.Visible = false;
            btntmgThoat.Visible = false;

            pnl01.Visible = false;
            pnlLogin.Visible = true;

            btnCapture.Enabled = true;
            btnLayKhuonMat.Enabled = false;
            btnOK.Enabled = false;

            picDetected.Visible = false;
            LoadQuyen();
        }

        private void LoadQuyen()
        {
            if (UserName != "")
            {
                TaiKhoan user = TaiKhoanDAO.Instance.GetTaiKhoanByTenTaiKhoan(UserName);
                if (user.Idquyen < 0 && user.Idquyen > 4)
                {
                    tmgQuanLy.Visible = false;
                    Application.Exit();
                }
                else
                {
                    if (user.Tenhienthi != "")
                    {
                        tmgTenHienThi.Text = user.Tenhienthi;
                    }
                    else
                    {
                        tmgTenHienThi.Text = "Thông tin";
                    }

                    if (user.Idquyen == 2)
                    {
                        tmgQuanLy.Visible = false;
                        tmgSinhVien.Visible = false;
                        tmgTaiKhoan.Visible = true;
                    }
                    else
                    {
                        if (user.Idquyen == 3)
                        {
                            tmgQuanLy.Visible = true;
                            tmgSinhVien.Visible = true;
                            tmgTaiKhoan.Visible = true;
                        }
                        else
                        {
                            if (user.Idquyen == 4)
                            {
                                tmgQuanLy.Visible = false;
                                tmgSinhVien.Visible = true;
                                tmgTaiKhoan.Visible = true;
                            }
                        }
                    }
                    mngQLDSV.Visible = true;
                    tmgDangXuat.Visible = true;
                    btntmgThoat.Visible = true;
                }
            }
        }

        private bool DangNhap(string userName, string passWord)
        {
            return TaiKhoanDAO.Instance.DangNhap(userName, passWord);
        }

        #endregion Methods

        #region Event

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string userName = txtTenTaiKhoan.Text;
            string passWord = txtMatKhau.Text;

            if (userName == "" || passWord == "")
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu trống!!!\nVui lòng kiểm tra lại", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenTaiKhoan.Clear();
                txtMatKhau.Clear();
                txtTenTaiKhoan.Focus();
            }
            else
            {
                if (DangNhap(userName, passWord))
                {
                    pnl01.Visible = true;
                    pnlLogin.Visible = false;
                    TaiKhoan user = TaiKhoanDAO.Instance.GetTaiKhoanByTenTaiKhoan(userName);
                    TenTaiKhoan = user.Tentaikhoan;
                    UserName = user.Tentaikhoan;
                    if (userName == "SUPPERADMIN" || userName == "ADMIN")
                    {
                        pnl01.Visible = false;
                        pnlLogin.Visible = false;
                        mngQLDSV.Visible = true;
                        tmgDangXuat.Visible = true;
                        btntmgThoat.Visible = true;
                        if (user.Idquyen < 0 && user.Idquyen > 4)
                        {
                            tmgQuanLy.Visible = false;
                            Application.Exit();
                        }
                        else
                        {
                            if (user.Tenhienthi != "")
                            {
                                tmgTenHienThi.Text = user.Tenhienthi;
                            }
                            else
                            {
                                tmgTenHienThi.Text = "Thông tin";
                            }

                            if (user.Idquyen == 1)
                            {
                                tmgQuanLy.Visible = true;
                                tmgSinhVien.Visible = true;
                                tmgTaiKhoan.Visible = true;
                            }
                            else
                            {
                                if (user.Idquyen == 2)
                                {
                                    tmgQuanLy.Visible = false;
                                    tmgSinhVien.Visible = false;
                                    tmgTaiKhoan.Visible = true;
                                }
                                else
                                {
                                    if (user.Idquyen == 3)
                                    {
                                        tmgQuanLy.Visible = true;
                                        tmgSinhVien.Visible = true;
                                        tmgTaiKhoan.Visible = true;
                                    }
                                    else
                                    {
                                        if (user.Idquyen == 4)
                                        {
                                            tmgQuanLy.Visible = false;
                                            tmgSinhVien.Visible = true;
                                            tmgTaiKhoan.Visible = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!!!\nVui lòng kiểm tra lại", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenTaiKhoan.Clear();
                    txtMatKhau.Clear();
                    txtTenTaiKhoan.Focus();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát khỏi chương trình???", "thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            else
            {
                Application.Exit();
            }
        }

        private void tmgDangXuat_Click(object sender, EventArgs e)
        {
            TenTaiKhoan = "";
            UserName = "";
            LoadForm();
            pnlLogin.Visible = true;
            txtMatKhau.Clear();
            txtTenTaiKhoan.Focus();
            TenTaiKhoan = "";
            a = "";
        }

        private void tmgTenHienThi_Click(object sender, EventArgs e)
        {
            frmTaiKhoan f = new frmTaiKhoan();
            mngQLDSV.Enabled = false;
            f.ShowDialog();
            LoadForm();
            pnlLogin.Visible = false;
            mngQLDSV.Enabled = true;
        }

        private void tmgQuanLy_Click(object sender, EventArgs e)
        {
            frmQuanLy f = new frmQuanLy();
            mngQLDSV.Enabled = false;
            f.ShowDialog();
            mngQLDSV.Enabled = true;
        }

        private void lblThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát khỏi chương trình???", "thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            else
            {
                Application.Exit();
            }
        }

        private void btntmgThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát khỏi chương trình???", "thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            else
            {
                Application.Exit();
            }
        }

        private void pnlLogin_ParentChanged(object sender, EventArgs e)
        {
            if (pnlLogin.Visible == false)
            {
                this.CancelButton = btntmgThoat;
                return;
            }
            else
            {
                this.CancelButton = btnThoat;
            }
        }

        #endregion Event

        private void tmgSinhVien_Click(object sender, EventArgs e)
        {
            frmQuanLyDiem f = new frmQuanLyDiem();
            mngQLDSV.Enabled = false;
            f.ShowDialog();
            mngQLDSV.Enabled = true;
        }

        #region Variables
        private Capture videoCapture = null;
        private Image<Bgr, Byte> currentFrame = null;
        Mat frame = new Mat();
        private bool facesDetectionEnabled = false;
        CascadeClassifier faceCasacdeClassifier = new CascadeClassifier(@"C:\haarcascade\haarcascade_frontalface_alt.xml");
        Image<Bgr, Byte> faceResult = null;
        List<Image<Gray, Byte>> TrainedFaces = new List<Image<Gray, byte>>();
        List<int> PersonsLabes = new List<int>();

        bool EnableSaveImage = false;
        private bool isTrained = false;
        EigenFaceRecognizer recognizer;
        List<string> PersonsNames = new List<string>();

        #endregion

        private void btnCapture_Click(object sender, EventArgs e)
        {
            btnLayKhuonMat.Enabled = true;
            if (videoCapture != null) videoCapture.Dispose();
            videoCapture = new Capture();
            Application.Idle += ProcessFrame;
        }

        private string a = null;

        private void ProcessFrame(object sender, EventArgs e)
        {
            if (videoCapture != null && videoCapture.Ptr != IntPtr.Zero)
            {
                videoCapture.Retrieve(frame, 0);
                currentFrame = frame.ToImage<Bgr, Byte>().Resize(picCapture.Width, picCapture.Height, Inter.Cubic);
                if (facesDetectionEnabled)
                {
                    Mat grayImage = new Mat();
                    CvInvoke.CvtColor(currentFrame, grayImage, ColorConversion.Bgr2Gray);
                    CvInvoke.EqualizeHist(grayImage, grayImage);

                    Rectangle[] faces = faceCasacdeClassifier.DetectMultiScale(grayImage, 1.1, 5, Size.Empty, Size.Empty);
                    if (faces.Length > 0)
                    {

                        foreach (var face in faces)
                        {
                            Image<Bgr, Byte> resultImage = currentFrame.Convert<Bgr, Byte>();
                            resultImage.ROI = face;
                            picDetected.SizeMode = PictureBoxSizeMode.StretchImage;
                            picDetected.Image = resultImage.Bitmap;
                            if (isTrained)
                            {
                                Image<Gray, Byte> grayFaceResult = resultImage.Convert<Gray, Byte>().Resize(200, 200, Inter.Cubic);
                                CvInvoke.EqualizeHist(grayFaceResult, grayFaceResult);
                                var result = recognizer.Predict(grayFaceResult);
                                pictureBox1.Image = grayFaceResult.Bitmap;
                                pictureBox2.Image = TrainedFaces[result.Label].Bitmap;
                                Debug.WriteLine(result.Label + ". " + result.Distance);
                                if (result.Label != -1 && result.Distance < 100000)
                                {
                                    CvInvoke.PutText(currentFrame, PersonsNames[result.Label], new Point(face.X - 2, face.Y - 2),
                                        FontFace.HersheyComplex, 1.0, new Bgr(Color.WhiteSmoke).MCvScalar);
                                    CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.WhiteSmoke).MCvScalar, 2);
                                    a = PersonsNames[result.Label];
                                }
                                else
                                {
                                    CvInvoke.PutText(currentFrame, "Unknown", new Point(face.X - 2, face.Y - 2),
                                        FontFace.HersheyComplex, 1.0, new Bgr(Color.WhiteSmoke).MCvScalar);
                                    CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Red).MCvScalar, 2);
                                    a = "Unknown";
                                }
                            }
                        }
                    }
                }
                picCapture.Image = currentFrame.Bitmap;
            }
            if (currentFrame != null)
                currentFrame.Dispose();
        }

        private bool TrainImagesFromDir()
        {
            int ImagesCount = 0;
            double Threshold = 100000;
            TrainedFaces.Clear();
            PersonsLabes.Clear();
            PersonsNames.Clear();
            try
            {
                string path = Directory.GetCurrentDirectory() + @"\TrainedImages";
                string[] files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    Image<Gray, byte> trainedImage = new Image<Gray, byte>(file).Resize(200, 200, Inter.Cubic);
                    CvInvoke.EqualizeHist(trainedImage, trainedImage);
                    TrainedFaces.Add(trainedImage);
                    PersonsLabes.Add(ImagesCount);
                    string name = file.Split('\\').Last().Split('_')[0];
                    PersonsNames.Add(name);
                    ImagesCount++;
                    Debug.WriteLine(ImagesCount + ". " + name);

                }

                if (TrainedFaces.Count() > 0)
                {
                    recognizer = new EigenFaceRecognizer(ImagesCount, Threshold);
                    recognizer.Train(TrainedFaces.ToArray(), PersonsLabes.ToArray());

                    isTrained = true;
                    return true;
                }
                else
                {
                    isTrained = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                isTrained = false;
                MessageBox.Show("Error in Train Images: " + ex.Message);
                return false;
            }

        }

        private void btnLayKhuonMat_Click(object sender, EventArgs e)
        {
            facesDetectionEnabled = true;
            TrainImagesFromDir();
            picDetected.Visible = true;
            btnOK.Enabled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (a == "Unknown" || a != TenTaiKhoan)
            {
                MessageBox.Show("Xác thực không thành công", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Error);
                videoCapture.Dispose();
                facesDetectionEnabled = false;
                picCapture.Image = null;
                picDetected.Image = null;
                TenTaiKhoan = "";
                UserName = "";
                txtMatKhau.Clear();
                LoadForm();
            }
            else
            {
                videoCapture.Dispose();
                facesDetectionEnabled = false;
                picCapture.Image = null;
                picDetected.Image = null;
                pnl01.Visible = false;

                TaiKhoan user = TaiKhoanDAO.Instance.GetTaiKhoanByTenTaiKhoan(TenTaiKhoan);
                TenTaiKhoan = user.Tentaikhoan;
                UserName = user.Tentaikhoan;
                pnlLogin.Visible = false;
                mngQLDSV.Visible = true;
                tmgDangXuat.Visible = true;
                btntmgThoat.Visible = true;
                if (user.Idquyen < 0 && user.Idquyen > 4)
                {
                    tmgQuanLy.Visible = false;
                    Application.Exit();
                }
                else
                {
                    if (user.Tenhienthi != "")
                    {
                        tmgTenHienThi.Text = user.Tenhienthi;
                    }
                    else
                    {
                        tmgTenHienThi.Text = "Thông tin";
                    }

                    if (user.Idquyen == 1)
                    {
                        tmgQuanLy.Visible = true;
                        tmgSinhVien.Visible = true;
                        tmgTaiKhoan.Visible = true;
                    }
                    else
                    {
                        if (user.Idquyen == 2)
                        {
                            tmgQuanLy.Visible = false;
                            tmgSinhVien.Visible = false;
                            tmgTaiKhoan.Visible = true;
                        }
                        else
                        {
                            if (user.Idquyen == 3)
                            {
                                tmgQuanLy.Visible = true;
                                tmgSinhVien.Visible = true;
                                tmgTaiKhoan.Visible = true;
                            }
                            else
                            {
                                if (user.Idquyen == 4)
                                {
                                    tmgQuanLy.Visible = false;
                                    tmgSinhVien.Visible = true;
                                    tmgTaiKhoan.Visible = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
