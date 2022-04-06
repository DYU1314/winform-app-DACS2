namespace QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE
{
    partial class frmReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.rp1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.db_QLDSVKKTCNDataSet = new QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.db_QLDSVKKTCNDataSet();
            this.USP_LoadDanhSachSinhVienReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.USP_LoadDanhSachSinhVienReportTableAdapter = new QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.db_QLDSVKKTCNDataSetTableAdapters.USP_LoadDanhSachSinhVienReportTableAdapter();
            this.rp2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.USP_DanhSachSinhVienVaDiemHocPhanReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.USP_DanhSachSinhVienVaDiemHocPhanReportTableAdapter = new QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.db_QLDSVKKTCNDataSetTableAdapters.USP_DanhSachSinhVienVaDiemHocPhanReportTableAdapter();
            this.rp3 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopTableAdapter = new QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.db_QLDSVKKTCNDataSetTableAdapters.USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.db_QLDSVKKTCNDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USP_LoadDanhSachSinhVienReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USP_DanhSachSinhVienVaDiemHocPhanReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // rp1
            // 
            this.rp1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.USP_LoadDanhSachSinhVienReportBindingSource;
            this.rp1.LocalReport.DataSources.Add(reportDataSource1);
            this.rp1.LocalReport.ReportEmbeddedResource = "QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.Report1.rdlc";
            this.rp1.Location = new System.Drawing.Point(101, 0);
            this.rp1.Name = "rp1";
            this.rp1.Size = new System.Drawing.Size(1115, 815);
            this.rp1.TabIndex = 0;
            // 
            // db_QLDSVKKTCNDataSet
            // 
            this.db_QLDSVKKTCNDataSet.DataSetName = "db_QLDSVKKTCNDataSet";
            this.db_QLDSVKKTCNDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // USP_LoadDanhSachSinhVienReportBindingSource
            // 
            this.USP_LoadDanhSachSinhVienReportBindingSource.DataMember = "USP_LoadDanhSachSinhVienReport";
            this.USP_LoadDanhSachSinhVienReportBindingSource.DataSource = this.db_QLDSVKKTCNDataSet;
            // 
            // USP_LoadDanhSachSinhVienReportTableAdapter
            // 
            this.USP_LoadDanhSachSinhVienReportTableAdapter.ClearBeforeFill = true;
            // 
            // rp2
            // 
            this.rp2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.USP_DanhSachSinhVienVaDiemHocPhanReportBindingSource;
            this.rp2.LocalReport.DataSources.Add(reportDataSource2);
            this.rp2.LocalReport.ReportEmbeddedResource = "QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.Report2.rdlc";
            this.rp2.Location = new System.Drawing.Point(101, 0);
            this.rp2.Name = "rp2";
            this.rp2.Size = new System.Drawing.Size(1115, 815);
            this.rp2.TabIndex = 1;
            // 
            // USP_DanhSachSinhVienVaDiemHocPhanReportBindingSource
            // 
            this.USP_DanhSachSinhVienVaDiemHocPhanReportBindingSource.DataMember = "USP_DanhSachSinhVienVaDiemHocPhanReport";
            this.USP_DanhSachSinhVienVaDiemHocPhanReportBindingSource.DataSource = this.db_QLDSVKKTCNDataSet;
            // 
            // USP_DanhSachSinhVienVaDiemHocPhanReportTableAdapter
            // 
            this.USP_DanhSachSinhVienVaDiemHocPhanReportTableAdapter.ClearBeforeFill = true;
            // 
            // rp3
            // 
            this.rp3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopBindingSource;
            this.rp3.LocalReport.DataSources.Add(reportDataSource3);
            this.rp3.LocalReport.ReportEmbeddedResource = "QUAN_LY_DIEM_SINH_VIEN_KHOA_KY_THUAT_CONG_NGHE.Report3.rdlc";
            this.rp3.Location = new System.Drawing.Point(101, 0);
            this.rp3.Name = "rp3";
            this.rp3.Size = new System.Drawing.Size(1115, 815);
            this.rp3.TabIndex = 2;
            // 
            // USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopBindingSource
            // 
            this.USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopBindingSource.DataMember = "USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLop";
            this.USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopBindingSource.DataSource = this.db_QLDSVKKTCNDataSet;
            // 
            // USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopTableAdapter
            // 
            this.USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopTableAdapter.ClearBeforeFill = true;
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1327, 815);
            this.Controls.Add(this.rp3);
            this.Controls.Add(this.rp2);
            this.Controls.Add(this.rp1);
            this.Name = "frmReport";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.db_QLDSVKKTCNDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USP_LoadDanhSachSinhVienReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USP_DanhSachSinhVienVaDiemHocPhanReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rp1;
        private System.Windows.Forms.BindingSource USP_LoadDanhSachSinhVienReportBindingSource;
        private db_QLDSVKKTCNDataSet db_QLDSVKKTCNDataSet;
        private db_QLDSVKKTCNDataSetTableAdapters.USP_LoadDanhSachSinhVienReportTableAdapter USP_LoadDanhSachSinhVienReportTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer rp2;
        private System.Windows.Forms.BindingSource USP_DanhSachSinhVienVaDiemHocPhanReportBindingSource;
        private db_QLDSVKKTCNDataSetTableAdapters.USP_DanhSachSinhVienVaDiemHocPhanReportTableAdapter USP_DanhSachSinhVienVaDiemHocPhanReportTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer rp3;
        private System.Windows.Forms.BindingSource USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopBindingSource;
        private db_QLDSVKKTCNDataSetTableAdapters.USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopTableAdapter USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopTableAdapter;
    }
}