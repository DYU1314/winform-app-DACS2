USE [master]
GO
/****** Object:  Database [db_QLDSVKKTCN]    Script Date: 14/12/2020 15:16:08 ******/
CREATE DATABASE [db_QLDSVKKTCN]
GO
USE [db_QLDSVKKTCN]
GO
/****** Object:  UserDefinedFunction [dbo].[fuConvertToUnsign1]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
--------------------------------------------------------------------------------
CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END

GO
/****** Object:  Table [dbo].[HOCKI]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOCKI](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MAHOCKI] [nvarchar](10) NOT NULL,
	[TENHOCKI] [nvarchar](30) NOT NULL DEFAULT ('HK1'),
	[TGBATDAU] [date] NOT NULL,
	[TGKETTHUC] [date] NOT NULL,
	[IDKHOAHOC] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HOCPHAN]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOCPHAN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MAHOCPHAN] [nvarchar](10) NOT NULL,
	[TENHOCPHAN] [nvarchar](100) NOT NULL,
	[IDHOCKI] [int] NULL,
	[IDKHOAHOC] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KHOAHOC]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHOAHOC](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MAKHOAHOC] [nvarchar](10) NOT NULL,
	[TENKHOAHOC] [nvarchar](100) NULL,
	[NAMBATDAU] [int] NOT NULL,
	[NAMKETTHUC] [int] NOT NULL,
	[THOIGIANHOCTOITHIEU] [int] NOT NULL DEFAULT ((1)),
	[THOIGIANHOCTIEUCHUAN] [int] NOT NULL DEFAULT ((1)),
	[THOIGIANHOCTOIDA] [int] NOT NULL DEFAULT ((1)),
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KQCUOIKI]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KQCUOIKI](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DTBHESO10] [float] NULL DEFAULT ((0.0)),
	[DTBHESO4] [float] NULL DEFAULT ((0.0)),
	[DTBCHU] [char](3) NULL DEFAULT ('F'),
	[IDHOCKI] [int] NULL,
	[IDSINHVIEN] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KQHOCPHAN]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KQHOCPHAN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DIEMCHUYENCAN] [float] NULL DEFAULT ((0.0)),
	[DIEMGIUAKI] [float] NULL DEFAULT ((0.0)),
	[DIEMTHICUOIKI] [float] NULL DEFAULT ((0.0)),
	[DTBHOCPHANHESO10] [float] NULL DEFAULT ((0.0)),
	[DTBHOCPHANHESO4] [float] NULL DEFAULT ((0.0)),
	[DTBHOCPHANCHU] [char](2) NULL,
	[IDHOCPHAN] [int] NULL,
	[IDKQCUOIKI] [int] NULL,
	[IDSINHVIEN] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LOP]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MALOP] [nvarchar](10) NOT NULL,
	[TENLOP] [nvarchar](100) NULL,
	[IDKHOAHOC] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QUYEN]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QUYEN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QUYEN] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SINHVIEN]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SINHVIEN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MSSV] [nvarchar](10) NOT NULL,
	[HO] [nvarchar](30) NOT NULL,
	[TEN] [nvarchar](30) NOT NULL,
	[GIOITINH] [nvarchar](5) NULL,
	[NGAYSINH] [date] NULL,
	[SODIENTHOAI] [nvarchar](11) NULL,
	[EMAIL] [nvarchar](50) NULL,
	[DIACHI] [nvarchar](200) NULL,
	[IDLOP] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TAIKHOAN]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAIKHOAN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TENTAIKHOAN] [nvarchar](30) NOT NULL,
	[MATKHAU] [nvarchar](max) NOT NULL,
	[HOTEN] [nvarchar](50) NOT NULL,
	[TENHIENTHI] [nvarchar](50) NULL,
	[SODIENTHOAI] [nvarchar](10) NULL,
	[EMAIL] [nvarchar](50) NULL,
	[DIACHI] [nvarchar](200) NULL,
	[IDQUYEN] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[HOCKI] ON 

INSERT [dbo].[HOCKI] ([ID], [MAHOCKI], [TENHOCKI], [TGBATDAU], [TGKETTHUC], [IDKHOAHOC]) VALUES (1, N'HK1', N'Học kì 1', CAST(N'2017-08-01' AS Date), CAST(N'2017-12-30' AS Date), 1)
INSERT [dbo].[HOCKI] ([ID], [MAHOCKI], [TENHOCKI], [TGBATDAU], [TGKETTHUC], [IDKHOAHOC]) VALUES (2, N'HK2', N'Học kì 2', CAST(N'2018-01-01' AS Date), CAST(N'2018-05-30' AS Date), 1)
SET IDENTITY_INSERT [dbo].[HOCKI] OFF
SET IDENTITY_INSERT [dbo].[HOCPHAN] ON 

INSERT [dbo].[HOCPHAN] ([ID], [MAHOCPHAN], [TENHOCPHAN], [IDHOCKI], [IDKHOAHOC]) VALUES (1, N'HP01', N'Học phần 1', 1, 1)
INSERT [dbo].[HOCPHAN] ([ID], [MAHOCPHAN], [TENHOCPHAN], [IDHOCKI], [IDKHOAHOC]) VALUES (2, N'HP02', N'Học phần 2', 1, 1)
INSERT [dbo].[HOCPHAN] ([ID], [MAHOCPHAN], [TENHOCPHAN], [IDHOCKI], [IDKHOAHOC]) VALUES (3, N'HP03', N'Học phần 3', 2, 1)
INSERT [dbo].[HOCPHAN] ([ID], [MAHOCPHAN], [TENHOCPHAN], [IDHOCKI], [IDKHOAHOC]) VALUES (4, N'HP04', N'Học phần 4', 2, 1)
SET IDENTITY_INSERT [dbo].[HOCPHAN] OFF
SET IDENTITY_INSERT [dbo].[KHOAHOC] ON 

INSERT [dbo].[KHOAHOC] ([ID], [MAKHOAHOC], [TENKHOAHOC], [NAMBATDAU], [NAMKETTHUC], [THOIGIANHOCTOITHIEU], [THOIGIANHOCTIEUCHUAN], [THOIGIANHOCTOIDA]) VALUES (1, N'K5', N'Khóa 2017', 2017, 2021, 3, 8, 4)
INSERT [dbo].[KHOAHOC] ([ID], [MAKHOAHOC], [TENKHOAHOC], [NAMBATDAU], [NAMKETTHUC], [THOIGIANHOCTOITHIEU], [THOIGIANHOCTIEUCHUAN], [THOIGIANHOCTOIDA]) VALUES (2, N'K6', N'Khóa 2018', 2018, 2022, 3, 8, 4)
SET IDENTITY_INSERT [dbo].[KHOAHOC] OFF
SET IDENTITY_INSERT [dbo].[KQCUOIKI] ON 

INSERT [dbo].[KQCUOIKI] ([ID], [DTBHESO10], [DTBHESO4], [DTBCHU], [IDHOCKI], [IDSINHVIEN]) VALUES (1, 7, 2.8000000000000003, N'B+ ', 1, 1)
INSERT [dbo].[KQCUOIKI] ([ID], [DTBHESO10], [DTBHESO4], [DTBCHU], [IDHOCKI], [IDSINHVIEN]) VALUES (2, 0, 0, N'F  ', 2, 1)
INSERT [dbo].[KQCUOIKI] ([ID], [DTBHESO10], [DTBHESO4], [DTBCHU], [IDHOCKI], [IDSINHVIEN]) VALUES (3, 8, 3.2, N'A  ', 1, 2)
INSERT [dbo].[KQCUOIKI] ([ID], [DTBHESO10], [DTBHESO4], [DTBCHU], [IDHOCKI], [IDSINHVIEN]) VALUES (4, 0, 0, N'F  ', 2, 2)
SET IDENTITY_INSERT [dbo].[KQCUOIKI] OFF
SET IDENTITY_INSERT [dbo].[KQHOCPHAN] ON 

INSERT [dbo].[KQHOCPHAN] ([ID], [DIEMCHUYENCAN], [DIEMGIUAKI], [DIEMTHICUOIKI], [DTBHOCPHANHESO10], [DTBHOCPHANHESO4], [DTBHOCPHANCHU], [IDHOCPHAN], [IDKQCUOIKI], [IDSINHVIEN]) VALUES (1, 6, 7, 8, 7, 2.8000000000000003, N'B+', 1, 1, 1)
INSERT [dbo].[KQHOCPHAN] ([ID], [DIEMCHUYENCAN], [DIEMGIUAKI], [DIEMTHICUOIKI], [DTBHOCPHANHESO10], [DTBHOCPHANHESO4], [DTBHOCPHANCHU], [IDHOCPHAN], [IDKQCUOIKI], [IDSINHVIEN]) VALUES (2, 7, 8, 9, 8, 3.2, N'A ', 2, 3, 2)
SET IDENTITY_INSERT [dbo].[KQHOCPHAN] OFF
SET IDENTITY_INSERT [dbo].[LOP] ON 

INSERT [dbo].[LOP] ([ID], [MALOP], [TENLOP], [IDKHOAHOC]) VALUES (1, N'DH17TIN01', N'Đại học Khóa 17 Công nghệ thông tin 01', 1)
INSERT [dbo].[LOP] ([ID], [MALOP], [TENLOP], [IDKHOAHOC]) VALUES (2, N'DH17TIN02', N'Đại học Khóa 2017 Công nghệ thông tin 02', 1)
INSERT [dbo].[LOP] ([ID], [MALOP], [TENLOP], [IDKHOAHOC]) VALUES (3, N'DH16TIN01', N'lớp1', 2)
SET IDENTITY_INSERT [dbo].[LOP] OFF
SET IDENTITY_INSERT [dbo].[QUYEN] ON 

INSERT [dbo].[QUYEN] ([ID], [QUYEN]) VALUES (1, N'SupperAdmin')
INSERT [dbo].[QUYEN] ([ID], [QUYEN]) VALUES (2, N'Admin')
INSERT [dbo].[QUYEN] ([ID], [QUYEN]) VALUES (3, N'Quản Trị')
INSERT [dbo].[QUYEN] ([ID], [QUYEN]) VALUES (4, N'Giáo Viên')
SET IDENTITY_INSERT [dbo].[QUYEN] OFF
SET IDENTITY_INSERT [dbo].[SINHVIEN] ON 

INSERT [dbo].[SINHVIEN] ([ID], [MSSV], [HO], [TEN], [GIOITINH], [NGAYSINH], [SODIENTHOAI], [EMAIL], [DIACHI], [IDLOP]) VALUES (1, N'177799', N'Lê Hoàng', N'Duy', N'Nam', CAST(N'1998-12-30' AS Date), N'0334102197', N'lhduy12cb34@gmail.com', N'Mỹ Hiệp, Cao Lãnh, Đồng Tháp', 1)
INSERT [dbo].[SINHVIEN] ([ID], [MSSV], [HO], [TEN], [GIOITINH], [NGAYSINH], [SODIENTHOAI], [EMAIL], [DIACHI], [IDLOP]) VALUES (2, N'176293', N'Phạm Trung', N'Đĩnh', N'Nam', CAST(N'1999-01-01' AS Date), N'0123456789', N'dinh123@gmail.com', N'Kiên Giang', 1)
SET IDENTITY_INSERT [dbo].[SINHVIEN] OFF
SET IDENTITY_INSERT [dbo].[TAIKHOAN] ON 

INSERT [dbo].[TAIKHOAN] ([ID], [TENTAIKHOAN], [MATKHAU], [HOTEN], [TENHIENTHI], [SODIENTHOAI], [EMAIL], [DIACHI], [IDQUYEN]) VALUES (1, N'SUPPERADMIN', N'C8qiotBAbGg=', N'Lê Hoàng Duy', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[TAIKHOAN] ([ID], [TENTAIKHOAN], [MATKHAU], [HOTEN], [TENHIENTHI], [SODIENTHOAI], [EMAIL], [DIACHI], [IDQUYEN]) VALUES (2, N'ADMIN', N'C8qiotBAbGg=', N'ADMIN', NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TAIKHOAN] ([ID], [TENTAIKHOAN], [MATKHAU], [HOTEN], [TENHIENTHI], [SODIENTHOAI], [EMAIL], [DIACHI], [IDQUYEN]) VALUES (3, N'QUANTRI', N'SMqn65TuxEs=', N'Quản Trị', NULL, NULL, NULL, NULL, 3)
INSERT [dbo].[TAIKHOAN] ([ID], [TENTAIKHOAN], [MATKHAU], [HOTEN], [TENHIENTHI], [SODIENTHOAI], [EMAIL], [DIACHI], [IDQUYEN]) VALUES (4, N'GIAOVIEN', N'KywbOlLM3kcEs0sepAH66g==', N'Giáo Viên', NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[TAIKHOAN] ([ID], [TENTAIKHOAN], [MATKHAU], [HOTEN], [TENHIENTHI], [SODIENTHOAI], [EMAIL], [DIACHI], [IDQUYEN]) VALUES (5, N'MINH', N'g8dBNuKLgx0Es0sepAH66g==', N'MINH', N'', N'', N'', N'', 4)
INSERT [dbo].[TAIKHOAN] ([ID], [TENTAIKHOAN], [MATKHAU], [HOTEN], [TENHIENTHI], [SODIENTHOAI], [EMAIL], [DIACHI], [IDQUYEN]) VALUES (6, N'AD', N'1fJ85JgoBePyc3XMXP3dcA==', N'KHÔI', N'', N'', N'', N'', 4)
SET IDENTITY_INSERT [dbo].[TAIKHOAN] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__HOCKI__32C918B7D305711C]    Script Date: 14/12/2020 15:16:08 ******/
ALTER TABLE [dbo].[HOCKI] ADD UNIQUE NONCLUSTERED 
(
	[MAHOCKI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__HOCPHAN__93ACA538AF16D040]    Script Date: 14/12/2020 15:16:08 ******/
ALTER TABLE [dbo].[HOCPHAN] ADD UNIQUE NONCLUSTERED 
(
	[MAHOCPHAN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__KHOAHOC__9E20788CD5712991]    Script Date: 14/12/2020 15:16:08 ******/
ALTER TABLE [dbo].[KHOAHOC] ADD UNIQUE NONCLUSTERED 
(
	[MAKHOAHOC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__LOP__7A3DE2109376447A]    Script Date: 14/12/2020 15:16:08 ******/
ALTER TABLE [dbo].[LOP] ADD UNIQUE NONCLUSTERED 
(
	[MALOP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__SINHVIEN__6CB3B7F85345F25C]    Script Date: 14/12/2020 15:16:08 ******/
ALTER TABLE [dbo].[SINHVIEN] ADD UNIQUE NONCLUSTERED 
(
	[MSSV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__TAIKHOAN__DE94905AA03C84FE]    Script Date: 14/12/2020 15:16:08 ******/
ALTER TABLE [dbo].[TAIKHOAN] ADD UNIQUE NONCLUSTERED 
(
	[TENTAIKHOAN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[HOCKI]  WITH CHECK ADD FOREIGN KEY([IDKHOAHOC])
REFERENCES [dbo].[KHOAHOC] ([ID])
GO
ALTER TABLE [dbo].[HOCPHAN]  WITH CHECK ADD FOREIGN KEY([IDHOCKI])
REFERENCES [dbo].[HOCKI] ([ID])
GO
ALTER TABLE [dbo].[HOCPHAN]  WITH CHECK ADD FOREIGN KEY([IDKHOAHOC])
REFERENCES [dbo].[KHOAHOC] ([ID])
GO
ALTER TABLE [dbo].[KQCUOIKI]  WITH CHECK ADD FOREIGN KEY([IDHOCKI])
REFERENCES [dbo].[HOCKI] ([ID])
GO
ALTER TABLE [dbo].[KQCUOIKI]  WITH CHECK ADD FOREIGN KEY([IDSINHVIEN])
REFERENCES [dbo].[SINHVIEN] ([ID])
GO
ALTER TABLE [dbo].[KQHOCPHAN]  WITH CHECK ADD FOREIGN KEY([IDHOCPHAN])
REFERENCES [dbo].[HOCPHAN] ([ID])
GO
ALTER TABLE [dbo].[KQHOCPHAN]  WITH CHECK ADD FOREIGN KEY([IDKQCUOIKI])
REFERENCES [dbo].[KQCUOIKI] ([ID])
GO
ALTER TABLE [dbo].[KQHOCPHAN]  WITH CHECK ADD FOREIGN KEY([IDSINHVIEN])
REFERENCES [dbo].[SINHVIEN] ([ID])
GO
ALTER TABLE [dbo].[LOP]  WITH CHECK ADD FOREIGN KEY([IDKHOAHOC])
REFERENCES [dbo].[KHOAHOC] ([ID])
GO
ALTER TABLE [dbo].[SINHVIEN]  WITH CHECK ADD FOREIGN KEY([IDLOP])
REFERENCES [dbo].[LOP] ([ID])
GO
ALTER TABLE [dbo].[TAIKHOAN]  WITH CHECK ADD FOREIGN KEY([IDQUYEN])
REFERENCES [dbo].[QUYEN] ([ID])
GO
ALTER TABLE [dbo].[KQCUOIKI]  WITH CHECK ADD CHECK  (([DTBHESO10]>=(0) AND [DTBHESO10]<=(10)))
GO
ALTER TABLE [dbo].[KQCUOIKI]  WITH CHECK ADD CHECK  (([DTBHESO4]>=(0) AND [DTBHESO4]<=(4)))
GO
ALTER TABLE [dbo].[KQHOCPHAN]  WITH CHECK ADD CHECK  (([DIEMCHUYENCAN]>=(0) AND [DIEMCHUYENCAN]<=(10)))
GO
ALTER TABLE [dbo].[KQHOCPHAN]  WITH CHECK ADD CHECK  (([DIEMGIUAKI]>=(0) AND [DIEMGIUAKI]<=(10)))
GO
ALTER TABLE [dbo].[KQHOCPHAN]  WITH CHECK ADD CHECK  (([DIEMTHICUOIKI]>=(0) AND [DIEMTHICUOIKI]<=(10)))
GO
ALTER TABLE [dbo].[KQHOCPHAN]  WITH CHECK ADD CHECK  (([DTBHOCPHANHESO10]>=(0) AND [DTBHOCPHANHESO10]<=(10)))
GO
ALTER TABLE [dbo].[KQHOCPHAN]  WITH CHECK ADD CHECK  (([DTBHOCPHANHESO4]>=(0) AND [DTBHOCPHANHESO4]<=(4)))
GO
ALTER TABLE [dbo].[QUYEN]  WITH CHECK ADD CHECK  (([QUYEN]=N'SupperAdmin' OR [QUYEN]=N'Admin' OR [QUYEN]=N'Quản trị' OR [QUYEN]=N'Giáo Viên'))
GO
/****** Object:  StoredProcedure [dbo].[USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLop]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROC [dbo].[USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLop]
@idHocKi INT
, @idLop INT
AS
BEGIN
	SELECT
	MALOP
	, MAHOCKI
	, MSSV
	, HO
	, TEN
	, DTBHESO10
	, DTBHESO4
	, DTBCHU
	FROM dbo.KQCUOIKI
	JOIN dbo.SINHVIEN ON SINHVIEN.ID = KQCUOIKI.IDSINHVIEN
	JOIN dbo.HOCKI ON HOCKI.ID = KQCUOIKI.IDHOCKI
	LEFT JOIN dbo.LOP ON LOP.ID = SINHVIEN.IDLOP
	WHERE LOP.ID = @idLop AND HOCKI.ID = @idHocKi
	ORDER BY TEN ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopXem]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROC [dbo].[USP_DanhSachDiemTrungBinhHocKiCuaSinhVienTrongLopXem]
@idHocKi INT
, @idLop INT
AS
BEGIN
	SELECT
	MALOP AS N'Mã Lớp'
	, MAHOCKI AS N'Mã Học Kì'
	, MSSV
	, HO AS N'Họ và Tên Lót'
	, TEN AS N'Tên'
	, DTBHESO10 AS N'DTB Hệ Số 10'
	, DTBHESO4 AS N'DTB Hệ Số 4'
	, DTBCHU AS N'DTB Chữ'
	FROM dbo.KQCUOIKI
	JOIN dbo.SINHVIEN ON SINHVIEN.ID = KQCUOIKI.IDSINHVIEN
	JOIN dbo.HOCKI ON HOCKI.ID = KQCUOIKI.IDHOCKI
	LEFT JOIN dbo.LOP ON LOP.ID = SINHVIEN.IDLOP
	WHERE LOP.ID = @idLop AND HOCKI.ID = @idHocKi
	ORDER BY TEN ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_DanhSachSinhVienChuaCoDiemHocPhan]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_DanhSachSinhVienChuaCoDiemHocPhan]
@idLop INT
, @idHocPhan INT
AS
BEGIN
	SELECT
	LOP.ID AS N'ID Lớp'
	, HOCPHAN.ID AS N'ID Học Phần'
	, SINHVIEN.ID AS N'ID Sinh Viên'
	, KQHOCPHAN.ID AS N'ID KQ Học Phần'
	, MALOP AS N'Mã Lớp'
	, MAHOCPHAN AS N'Mã Học Phần'
	, MSSV AS N'Mã Số Sinh Viên'
	, HO AS N'Họ và Tên Lót'
	, TEN AS N'Tên'
	, DIEMCHUYENCAN AS N'Điểm Chuyển Cần'
	, DIEMGIUAKI AS N'Điểm Giữa Kì'
	, DIEMTHICUOIKI AS N'Điểm Thi Cuối Kì'
	, DTBHOCPHANHESO10 AS N'DTB Hệ Số 10'
	, DTBHOCPHANHESO4 AS N'DTBHệ Số 4'
	, DTBHOCPHANCHU AS N'DTB Chữ'
	FROM dbo.LOP
	JOIN dbo.HOCPHAN ON HOCPHAN.IDKHOAHOC = LOP.IDKHOAHOC
	RIGHT JOIN dbo.SINHVIEN ON SINHVIEN.IDLOP = LOP.ID
	LEFT JOIN dbo.KQHOCPHAN ON KQHOCPHAN.IDHOCPHAN = HOCPHAN.ID AND KQHOCPHAN.IDSINHVIEN = SINHVIEN.ID
	WHERE LOP.ID = @idLop AND HOCPHAN.ID = @idHocPhan
	EXCEPT
	SELECT
	LOP.ID AS N'ID Lớp'
	, HOCPHAN.ID AS N'ID Học Phần'
	, SINHVIEN.ID AS N'ID Sinh Viên'
	, KQHOCPHAN.ID AS N'ID KQ Học Phần'
	, MALOP AS N'Mã Lớp'
	, MAHOCPHAN AS N'Mã Học Phần'
	, MSSV AS N'Mã Số Sinh Viên'
	, HO AS N'Họ và Tên Lót'
	, TEN AS N'Tên'
	, DIEMCHUYENCAN AS N'Điểm Chuyển Cần'
	, DIEMGIUAKI AS N'Điểm Giữa Kì'
	, DIEMTHICUOIKI AS N'Điểm Thi Cuối Kì'
	, DTBHOCPHANHESO10 AS N'DTB Hệ Số 10'
	, DTBHOCPHANHESO4 AS N'DTBHệ Số 4'
	, DTBHOCPHANCHU AS N'DTB Chữ'
	FROM dbo.LOP
	JOIN dbo.HOCPHAN ON HOCPHAN.IDKHOAHOC = LOP.IDKHOAHOC
	RIGHT JOIN dbo.SINHVIEN ON SINHVIEN.IDLOP = LOP.ID
	RIGHT JOIN dbo.KQHOCPHAN ON KQHOCPHAN.IDHOCPHAN = HOCPHAN.ID AND KQHOCPHAN.IDSINHVIEN = SINHVIEN.ID
	WHERE LOP.ID = @idLop AND HOCPHAN.ID = @idHocPhan
END

GO
/****** Object:  StoredProcedure [dbo].[USP_DanhSachSinhVienDaCoDiemHocPhan]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_DanhSachSinhVienDaCoDiemHocPhan]
@idLop INT
, @idHocPhan INT
AS
BEGIN
	SELECT
	LOP.ID AS N'ID Lớp'
	, HOCPHAN.ID AS N'ID Học Phần'
	, SINHVIEN.ID AS N'ID Sinh Viên'
	, KQHOCPHAN.ID AS N'ID KQ Học Phần'
	, MALOP AS N'Mã Lớp'
	, MAHOCPHAN AS N'Mã Học Phần'
	, MSSV AS N'Mã Số Sinh Viên'
	, HO AS N'Họ và Tên Lót'
	, TEN AS N'Tên'
	, DIEMCHUYENCAN AS N'Điểm Chuyển Cần'
	, DIEMGIUAKI AS N'Điểm Giữa Kì'
	, DIEMTHICUOIKI AS N'Điểm Thi Cuối Kì'
	, DTBHOCPHANHESO10 AS N'DTB Hệ Số 10'
	, DTBHOCPHANHESO4 AS N'DTBHệ Số 4'
	, DTBHOCPHANCHU AS N'DTB Chữ'
	FROM dbo.LOP
	JOIN dbo.HOCPHAN ON HOCPHAN.IDKHOAHOC = LOP.IDKHOAHOC
	RIGHT JOIN dbo.SINHVIEN ON SINHVIEN.IDLOP = LOP.ID
	RIGHT JOIN dbo.KQHOCPHAN ON KQHOCPHAN.IDHOCPHAN = HOCPHAN.ID AND KQHOCPHAN.IDSINHVIEN = SINHVIEN.ID
	WHERE LOP.ID = @idLop AND HOCPHAN.ID = @idHocPhan
END

GO
/****** Object:  StoredProcedure [dbo].[USP_DanhSachSinhVienVaDiemHocPhan]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_DanhSachSinhVienVaDiemHocPhan]
@idLop INT
, @idHocPhan INT
AS
BEGIN
	SELECT
	LOP.ID AS N'ID Lớp'
	, HOCPHAN.ID AS N'ID Học Phần'
	, SINHVIEN.ID AS N'ID Sinh Viên'
	, KQHOCPHAN.ID AS N'ID KQ Học Phần'
	, MALOP AS N'Mã Lớp'
	, MAHOCPHAN AS N'Mã Học Phần'
	, MSSV AS N'Mã Số Sinh Viên'
	, HO AS N'Họ và Tên Lót'
	, TEN AS N'Tên'
	, DIEMCHUYENCAN AS N'Điểm Chuyển Cần'
	, DIEMGIUAKI AS N'Điểm Giữa Kì'
	, DIEMTHICUOIKI AS N'Điểm Thi Cuối Kì'
	, DTBHOCPHANHESO10 AS N'DTB Hệ Số 10'
	, DTBHOCPHANHESO4 AS N'DTBHệ Số 4'
	, DTBHOCPHANCHU AS N'DTB Chữ'
	FROM dbo.LOP
	JOIN dbo.HOCPHAN ON HOCPHAN.IDKHOAHOC = LOP.IDKHOAHOC
	RIGHT JOIN dbo.SINHVIEN ON SINHVIEN.IDLOP = LOP.ID
	LEFT JOIN dbo.KQHOCPHAN ON KQHOCPHAN.IDHOCPHAN = HOCPHAN.ID AND KQHOCPHAN.IDSINHVIEN = SINHVIEN.ID
	WHERE LOP.ID = @idLop AND HOCPHAN.ID = @idHocPhan
END

GO
/****** Object:  StoredProcedure [dbo].[USP_DanhSachSinhVienVaDiemHocPhanReport]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_DanhSachSinhVienVaDiemHocPhanReport]
@idLop INT
, @idHocPhan INT
AS
BEGIN
	SELECT
	LOP.ID
	, HOCPHAN.ID
	, SINHVIEN.ID
	, KQHOCPHAN.ID
	, MALOP
	, MAHOCPHAN
	, MSSV
	, HO
	, TEN
	, DIEMCHUYENCAN
	, DIEMGIUAKI
	, DIEMTHICUOIKI
	, DTBHOCPHANHESO10
	, DTBHOCPHANHESO4
	, DTBHOCPHANCHU
	FROM dbo.LOP
	JOIN dbo.HOCPHAN ON HOCPHAN.IDKHOAHOC = LOP.IDKHOAHOC
	RIGHT JOIN dbo.SINHVIEN ON SINHVIEN.IDLOP = LOP.ID
	LEFT JOIN dbo.KQHOCPHAN ON KQHOCPHAN.IDHOCPHAN = HOCPHAN.ID AND KQHOCPHAN.IDSINHVIEN = SINHVIEN.ID
	WHERE LOP.ID = @idLop AND HOCPHAN.ID = @idHocPhan
END

GO
/****** Object:  StoredProcedure [dbo].[USP_DanhSachTatCacDiemHocPhanCuaSinhVien]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROC [dbo].[USP_DanhSachTatCacDiemHocPhanCuaSinhVien]
@idSinhVien INT
AS
BEGIN
	SELECT 
	MAHOCKI AS N'Mã Học Kì'
	, MSSV
	, HO AS N'Họ và Tên Lót'
	, TEN AS N'Tên'
	, MAHOCPHAN AS N'Mã Học Phần'
	, TENHOCPHAN AS N'Tên Học Phần'
	, DIEMCHUYENCAN AS N'Điểm Chuyên Cần'
	, DIEMGIUAKI AS N'Điểm Giữa Kì'
	, DIEMTHICUOIKI AS N'Điểm Cuối Kì'
	, DTBHOCPHANHESO10 AS N'DTB Hệ Số 10'
	, DTBHOCPHANHESO4 AS N'DTB Hệ Số 4'
	, DTBHOCPHANCHU AS N'DTB Chữ'
	 FROM dbo.KQHOCPHAN
	 JOIN dbo.SINHVIEN ON SINHVIEN.ID = KQHOCPHAN.IDSINHVIEN
	 LEFT JOIN dbo.HOCPHAN ON HOCPHAN.ID = KQHOCPHAN.IDHOCPHAN
	 LEFT JOIN dbo.HOCKI ON HOCKI.ID = HOCPHAN.IDHOCKI
	 WHERE SINHVIEN.ID = @idSinhVien
	 ORDER BY MAHOCKI ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_DanhSachTatCacDiemHocPhanCuaSinhVienReport]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROC [dbo].[USP_DanhSachTatCacDiemHocPhanCuaSinhVienReport]
@idSinhVien INT
AS
BEGIN
	SELECT 
	MAHOCKI
	, MSSV
	, (SELECT HO + ' ' + TEN AS HOTEN FROM dbo.SINHVIEN WHERE ID = @idSinhVien) AS HOTEN
	, MAHOCPHAN
	, TENHOCPHAN
	, DIEMCHUYENCAN
	, DIEMGIUAKI
	, DIEMTHICUOIKI
	, DTBHOCPHANHESO10
	, DTBHOCPHANHESO4
	, DTBHOCPHANCHU
	 FROM dbo.KQHOCPHAN
	 JOIN dbo.SINHVIEN ON SINHVIEN.ID = KQHOCPHAN.IDSINHVIEN
	 LEFT JOIN dbo.HOCPHAN ON HOCPHAN.ID = KQHOCPHAN.IDHOCPHAN
	 LEFT JOIN dbo.HOCKI ON HOCKI.ID = HOCPHAN.IDHOCKI
	 WHERE SINHVIEN.ID = @idSinhVien
	 ORDER BY MAHOCKI ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetCountByMaHocKi]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_GetCountByMaHocKi]
	@maHocKi NVARCHAR(100) 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT COUNT(*)
	FROM dbo.HOCKI
	WHERE MAHOCKI = @maHocKi
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetCountByMaHocPhan]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_GetCountByMaHocPhan]
	@maHocPhan NVARCHAR(100) 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT COUNT(*)
	FROM dbo.HOCPHAN
	WHERE MAHOCPHAN = @maHocPhan
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetCountByMaKhoaHoc]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_GetCountByMaKhoaHoc]
@maKhoaHoc NVARCHAR(10)
AS
BEGIN
	SELECT COUNT(*)
	FROM dbo.KHOAHOC
	WHERE MAKHOAHOC = @maKhoaHoc
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetCountByMaLop]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_GetCountByMaLop]
@maLop NVARCHAR(10)
AS
BEGIN
	SELECT COUNT(*)
	FROM dbo.LOP
	WHERE MALOP = @maLop
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetCountByTenTaiKhoan]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_GetCountByTenTaiKhoan]
	@tenTaiKhoan NVARCHAR(30) 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT COUNT(*)
	FROM dbo.TAIKHOAN
	WHERE TENTAIKHOAN = @tenTaiKhoan
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetCountHocKiByIdKhoaHoc]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_GetCountHocKiByIdKhoaHoc]
@idKhoaHoc NVARCHAR(10)
AS
BEGIN
	SELECT COUNT(*)
	FROM dbo.HOCKI
	WHERE IDKHOAHOC = @idKhoaHoc
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetCountHocPhanByIdHocKi]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_GetCountHocPhanByIdHocKi]
	@idHocKi NVARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT COUNT(*)
	FROM dbo.HOCPHAN
	WHERE IDHOCKI = @idHocKi
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetCountLopByIdKhoaHoc]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_GetCountLopByIdKhoaHoc]
@idKhoaHoc NVARCHAR(10)
AS
BEGIN
	SELECT COUNT(*)
	FROM dbo.LOP
	WHERE IDKHOAHOC = @idKhoaHoc
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetCountSinhVienByMaSoSinhVien]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_GetCountSinhVienByMaSoSinhVien]
	@maoSinhVien NVARCHAR(10) 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT COUNT(*)
	FROM dbo.SINHVIEN
	WHERE MSSV = @maoSinhVien
END

GO
/****** Object:  StoredProcedure [dbo].[USP_GetTaiKhoanByTenTaiKhoan]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_GetTaiKhoanByTenTaiKhoan]
	@userName NVARCHAR(30)
AS
BEGIN
	SELECT * FROM dbo.TAIKHOAN WHERE TENTAIKHOAN = @userName
END

GO
/****** Object:  StoredProcedure [dbo].[USP_LoadDanhSachHocKi]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_LoadDanhSachHocKi]
AS
BEGIN
	SELECT
	HOCKI.ID AS N'ID Học Kì'
	, MAHOCKI AS N'Mã Học Kì'
	, TENHOCKI AS N'Tên Học Kì'
	, TGBATDAU AS N'Thời Gian Bắt Đầu'
	, TGKETTHUC AS N'Thời Gian Kết Thúc'
	, IDKHOAHOC AS N'ID Khóa Học'
	, MAKHOAHOC AS N'Mã Khóa Học'
	FROM dbo.HOCKI , dbo.KHOAHOC
	WHERE IDKHOAHOC = KHOAHOC.ID
	ORDER BY [Mã Học Kì] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_LoadDanhSachHocPhan]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_LoadDanhSachHocPhan]
AS
BEGIN
	SELECT
	HOCPHAN.ID AS N'ID Học Phần'
	, MAHOCPHAN AS N'Mã Học Phần'
	, TENHOCPHAN AS N'Tên Học Phần'
	, TGBATDAU AS N'Thời Gian Bắt Đầu Học'
	, TGKETTHUC AS N'Thời Gian Kết Thúc'
	, IDHOCKI AS N'ID Học Kì'
	, HOCPHAN.IDKHOAHOC AS N'ID Khóa Học'
	, MAHOCKI AS N'Mã Học Kì'
	, MAKHOAHOC AS N'Mã Khóa Học'
	FROM dbo.HOCPHAN , dbo.HOCKI , dbo.KHOAHOC
	WHERE HOCPHAN.IDKHOAHOC = KHOAHOC.ID AND IDHOCKI =HOCKI.ID
	ORDER BY [Mã Học Phần] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_LoadDanhSachHocPhanTheoIDHocKi]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_LoadDanhSachHocPhanTheoIDHocKi]
@id INT
AS
BEGIN
	SELECT
	HOCPHAN.ID AS N'ID Học Phần'
	, MAHOCPHAN AS N'Mã Học Phần'
	, TENHOCPHAN AS N'Tên Học Phần'
	, TGBATDAU AS N'Thời Gian Bắt Đầu Học'
	, TGKETTHUC AS N'Thời Gian Kết Thúc'
	, IDHOCKI AS N'ID Học Kì'
	, HOCPHAN.IDKHOAHOC AS N'ID Khóa Học'
	, MAHOCKI AS N'Mã Học Kì'
	, MAKHOAHOC AS N'Mã Khóa Học'
	FROM dbo.HOCPHAN , dbo.HOCKI , dbo.KHOAHOC
	WHERE HOCPHAN.IDKHOAHOC = KHOAHOC.ID AND IDHOCKI =HOCKI.ID AND HOCPHAN.IDHOCKI = @id
	ORDER BY [Mã Học Phần] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_LoadDanhSachHocPhanTheoIDKhoa]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_LoadDanhSachHocPhanTheoIDKhoa]
@id INT
AS
BEGIN
	SELECT
	HOCPHAN.ID AS N'ID Học Phần'
	, MAHOCPHAN AS N'Mã Học Phần'
	, TENHOCPHAN AS N'Tên Học Phần'
	, TGBATDAU AS N'Thời Gian Bắt Đầu Học'
	, TGKETTHUC AS N'Thời Gian Kết Thúc'
	, IDHOCKI AS N'ID Học Kì'
	, HOCPHAN.IDKHOAHOC AS N'ID Khóa Học'
	, MAHOCKI AS N'Mã Học Kì'
	, MAKHOAHOC AS N'Mã Khóa Học'
	FROM dbo.HOCPHAN , dbo.HOCKI , dbo.KHOAHOC
	WHERE HOCPHAN.IDKHOAHOC = KHOAHOC.ID AND IDHOCKI =HOCKI.ID AND HOCPHAN.IDKHOAHOC = @id
	ORDER BY [Mã Học Phần] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_LoadDanhSachKhoaHoc]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_LoadDanhSachKhoaHoc]
AS
BEGIN
	SELECT
	ID AS N'ID Khóa Học'
	, MAKHOAHOC AS N'Mã Khóa Học'
	, TENKHOAHOC AS N'Tên Khóa Học'
	, NAMBATDAU AS N'Năm Bắt Đầu'
	, NAMKETTHUC AS N'Năm Kết Thúc'
	, THOIGIANHOCTOITHIEU AS N'TGH Tối Thiểu'
	, THOIGIANHOCTOIDA AS N'TGH Tối Đa'
	, THOIGIANHOCTIEUCHUAN AS N'TGH Quy Định'
	FROM dbo.KHOAHOC
	ORDER BY [Mã Khóa Học] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_LoadDanhSachSinhVien]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_LoadDanhSachSinhVien]
AS
BEGIN
	SELECT
	MALOP AS N'Mã Lớp'
	, SINHVIEN.ID AS N'ID Sinh Viên'
	, SINHVIEN.MSSV AS N'Mã Số Sinh Viên'
	, SINHVIEN.HO AS N'Họ và Tên Lót'
	, SINHVIEN.TEN AS N'Tên'
	, SINHVIEN.GIOITINH AS N'Giới Tính'
	, SINHVIEN.NGAYSINH AS N'Ngày Sinh'
	, SINHVIEN.SODIENTHOAI AS N'Số Điện Thoại'
	, SINHVIEN.EMAIL AS N'Email'
	, SINHVIEN.DIACHI AS N'Địa Chỉ'
	, IDLOP AS N'ID Lớp'
	FROM dbo.SINHVIEN,dbo.LOP
	WHERE IDLOP = LOP.ID
	ORDER BY [Tên] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_LoadDanhSachSinhVienQLD]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_LoadDanhSachSinhVienQLD]
@maLop NVARCHAR(10)
AS
BEGIN
	SELECT
	MALOP AS N'Mã Lớp'
	, SINHVIEN.ID AS N'ID Sinh Viên'
	, SINHVIEN.MSSV AS N'Mã Số Sinh Viên'
	, SINHVIEN.HO AS N'Họ và Tên Lót'
	, SINHVIEN.TEN AS N'Tên'
	, SINHVIEN.GIOITINH AS N'Giới Tính'
	, SINHVIEN.NGAYSINH AS N'Ngày Sinh'
	, SINHVIEN.SODIENTHOAI AS N'Số Điện Thoại'
	, SINHVIEN.EMAIL AS N'Email'
	, SINHVIEN.DIACHI AS N'Địa Chỉ'
	, IDLOP AS N'ID Lớp'
	FROM dbo.SINHVIEN,dbo.LOP
	WHERE IDLOP = LOP.ID AND MALOP  = @maLop
	ORDER BY [Tên] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_LoadDanhSachSinhVienReport]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_LoadDanhSachSinhVienReport]
@maLop NVARCHAR(10)
AS
BEGIN
	SELECT MSSV ,HO , TEN , GIOITINH , NGAYSINH , SODIENTHOAI , EMAIL, DIACHI ,MALOP
	FROM dbo.SINHVIEN,dbo.LOP
	WHERE IDLOP = LOP.ID AND MALOP  = @maLop
	ORDER BY [TEN] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_LoadListLop]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_LoadListLop]
AS
BEGIN
	SELECT
	MAKHOAHOC AS N'Mã Khóa Học'
	, LOP.ID AS N'ID Lớp'
	, MALOP AS N'Mã Lớp'
	, TENLOP AS N'Tên Lớp'
	, NAMBATDAU AS N'Năm Bắt Đầu'
	, NAMKETTHUC AS N'Năm Kết Thúc'
	, IDKHOAHOC AS N'ID Khóa Học'
	FROM dbo.LOP , dbo.KHOAHOC
	WHERE IDKHOAHOC = KHOAHOC.ID
	ORDER BY [Mã Khóa Học] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_LoadListLopByMaKhoaHoc]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_LoadListLopByMaKhoaHoc]
@maKhoaHoc NVARCHAR(10)
AS
BEGIN
	SELECT
	MAKHOAHOC AS N'Mã Khóa Học'
	, LOP.ID AS N'ID Lớp'
	, MALOP AS N'Mã Lớp'
	, TENLOP AS N'Tên Lớp'
	, NAMBATDAU AS N'Năm Bắt Đầu'
	, NAMKETTHUC AS N'Năm Kết Thúc'
	, IDKHOAHOC AS N'ID Khóa Học'
	FROM dbo.LOP , dbo.KHOAHOC
	WHERE IDKHOAHOC = KHOAHOC.ID AND MAKHOAHOC = @maKhoaHoc
	ORDER BY [Mã Khóa Học] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_LoadTaiKhoan]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_LoadTaiKhoan]
AS
BEGIN
	SELECT
	TAIKHOAN.ID AS N'ID Tài Khoản'
	, TENTAIKHOAN AS N'Tên Tài Khoản'
	, MATKHAU AS N'Mật Khẩu'
	, HOTEN AS N'Họ Tên'
	, TENHIENTHI AS N'Tên Hiển Thị'
	, SODIENTHOAI AS N'Số Điện Thoại'
	, EMAIL AS N'Email'
	, DIACHI AS N'Địa Chỉ'
	, IDQUYEN AS N'ID Quyền'
	, QUYEN AS N'Quyền'
	FROM dbo.TAIKHOAN, dbo.QUYEN
	WHERE IDQUYEN != 1 AND IDQUYEN = QUYEN.ID AND TENTAIKHOAN != N'ADMIN'
	ORDER BY [Tên Tài Khoản] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_Login]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_Login]
	@userName NVARCHAR(30)
	, @passWord NVARCHAR(MAX)
AS
BEGIN
	SELECT *
	FROM dbo.TAIKHOAN
	WHERE TENTAIKHOAN = @userName AND MATKHAU = @passWord
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemCoHoTen]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemCoHoTen]
@hoTen NVARCHAR(50)
,@quyen INT
AS
BEGIN
	SELECT
	TAIKHOAN.ID AS N'ID Tài Khoản'
	, TENTAIKHOAN AS N'Tên Tài Khoản'
	, MATKHAU AS N'Mật Khẩu'
	, HOTEN AS N'Họ Tên'
	, TENHIENTHI AS N'Tên Hiển Thị'
	, SODIENTHOAI AS N'Số Điện Thoại'
	, EMAIL AS N'Email'
	, DIACHI AS N'Địa Chỉ'
	, IDQUYEN AS N'ID Quyền'
	, QUYEN AS N'Quyền'
	FROM dbo.TAIKHOAN ,dbo.QUYEN
	WHERE IDQUYEN = QUYEN.ID AND (IDQUYEN = @quyen OR dbo.fuConvertToUnsign1(HOTEN) LIKE N'%' + dbo.fuConvertToUnsign1(@hoTen) + '%') AND IDQUYEN != 1 AND TENTAIKHOAN != N'ADMIN'
	ORDER BY [Tên Tài Khoản] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemCoMaHK]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemCoMaHK]
@maHK NVARCHAR(10)
,@idKhoaHoc INT
AS
BEGIN
	SELECT
	HOCKI.ID AS N'ID Học Kì'
	, MAHOCKI AS N'Mã Học Kì'
	, TENHOCKI AS N'Tên Học Kì'
	, TGBATDAU AS N'Thời Gian Bắt Đầu'
	, TGKETTHUC AS N'Thời Gian Kết Thúc'
	, IDKHOAHOC AS N'ID Khóa Học'
	, MAKHOAHOC AS N'Mã Khóa Học'
	FROM dbo.HOCKI , dbo.KHOAHOC
	WHERE KHOAHOC.ID = IDKHOAHOC AND (dbo.fuConvertToUnsign1(MAHOCKI) LIKE N'%' + dbo.fuConvertToUnsign1(@maHK) + '%') AND (dbo.fuConvertToUnsign1(IDKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@idKhoaHoc) + '%')
	ORDER BY [Mã Học Kì] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemCoMaLop]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemCoMaLop]
@maLop NVARCHAR(10)
,@maKhoaHoc NVARCHAR(10)
AS
BEGIN
	SELECT
	LOP.ID AS N'ID Lớp'
	, MALOP AS N'Mã Lớp'
	, TENLOP AS N'Tên Lớp'
	, NAMBATDAU AS N'Năm Bắt Đầu'
	, NAMKETTHUC AS N'Năm Kết Thúc'
	, MAKHOAHOC AS N'Mã Khóa Học'
	, IDKHOAHOC AS N'ID Khóa Học'
	FROM dbo.LOP , dbo.KHOAHOC
	WHERE KHOAHOC.ID = IDKHOAHOC AND (dbo.fuConvertToUnsign1(MALOP) LIKE N'%' + dbo.fuConvertToUnsign1(@maLop) + '%') AND (dbo.fuConvertToUnsign1(MAKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@maKhoaHoc) + '%')
ORDER BY [Mã Lớp] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemHocPhanCoMa]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemHocPhanCoMa]
@maHocPhan NVARCHAR(10)
, @maHocKi NVARCHAR(10)
, @maKhoaHoc NVARCHAR(10)
AS
BEGIN
	SELECT
	HOCPHAN.ID AS N'ID Học Phần'
	, MAHOCPHAN AS N'Mã Học Phần'
	, TENHOCPHAN AS N'Tên Học Phần'
	, TGBATDAU AS N'Thời Gian Bắt Đầu Học'
	, TGKETTHUC AS N'Thời Gian Kết Thúc'
	, IDHOCKI AS N'ID Học Kì'
	, HOCPHAN.IDKHOAHOC AS N'ID Khóa Học'
	, MAHOCKI AS N'Mã Học Kì'
	, MAKHOAHOC AS N'Mã Khóa Học'
	FROM dbo.HOCPHAN , dbo.HOCKI , dbo.KHOAHOC
	WHERE HOCPHAN.IDKHOAHOC = KHOAHOC.ID AND IDHOCKI =HOCKI.ID AND (dbo.fuConvertToUnsign1(MAHOCPHAN) LIKE N'%' + dbo.fuConvertToUnsign1(@maHocPhan) + '%') AND (dbo.fuConvertToUnsign1(MAHOCKI) LIKE N'%' + dbo.fuConvertToUnsign1(@maHocKi) + '%') AND (dbo.fuConvertToUnsign1(MAKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@maKhoaHoc) + '%')
	ORDER BY [Mã Học Phần] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemHocPhanKhongMa]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemHocPhanKhongMa]
@maHocKi NVARCHAR(10)
, @maKhoaHoc NVARCHAR(10)
AS
BEGIN
	SELECT
	HOCPHAN.ID AS N'ID Học Phần'
	, MAHOCPHAN AS N'Mã Học Phần'
	, TENHOCPHAN AS N'Tên Học Phần'
	, TGBATDAU AS N'Thời Gian Bắt Đầu Học'
	, TGKETTHUC AS N'Thời Gian Kết Thúc'
	, IDHOCKI AS N'ID Học Kì'
	, HOCPHAN.IDKHOAHOC AS N'ID Khóa Học'
	, MAHOCKI AS N'Mã Học Kì'
	, MAKHOAHOC AS N'Mã Khóa Học'
	FROM dbo.HOCPHAN , dbo.HOCKI , dbo.KHOAHOC
	WHERE HOCPHAN.IDKHOAHOC = KHOAHOC.ID AND IDHOCKI =HOCKI.ID AND (dbo.fuConvertToUnsign1(MAHOCKI) LIKE N'%' + dbo.fuConvertToUnsign1(@maHocKi) + '%') AND (dbo.fuConvertToUnsign1(MAKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@maKhoaHoc) + '%')
	ORDER BY [Mã Học Phần] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemKHOAHOC]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemKHOAHOC]
@mKH NVARCHAR(10)
,@tenKH NVARCHAR(100)
,@namBD INT
AS
BEGIN
	SELECT
	ID AS N'ID Khóa Học'
	, MAKHOAHOC AS N'Mã Khóa Học'
	, TENKHOAHOC AS N'Tên Khóa Học'
	, NAMBATDAU AS N'Năm Bắt Đầu'
	, NAMKETTHUC AS N'Năm Kết Thúc'
	, THOIGIANHOCTOITHIEU AS N'TGH Tối Thiểu'
	, THOIGIANHOCTOIDA AS N'TGH Tối Đa'
	, THOIGIANHOCTIEUCHUAN AS N'TGH Quy Định'
	FROM dbo.KHOAHOC
	WHERE (dbo.fuConvertToUnsign1(MAKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@mkh) + '%') OR (dbo.fuConvertToUnsign1(TENKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@tenKH) + '%') OR (dbo.fuConvertToUnsign1(NAMBATDAU) LIKE N'%' + dbo.fuConvertToUnsign1(@namBD) + '%')
	ORDER BY [Mã Khóa Học] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemKhongMaHK]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemKhongMaHK]
@idKhoaHoc INT
AS
BEGIN
	SELECT
	HOCKI.ID AS N'ID Học Kì'
	, MAHOCKI AS N'Mã Học Kì'
	, TENHOCKI AS N'Tên Học Kì'
	, TGBATDAU AS N'Thời Gian Bắt Đầu'
	, TGKETTHUC AS N'Thời Gian Kết Thúc'
	, IDKHOAHOC AS N'ID Khóa Học'
	, MAKHOAHOC AS N'Mã Khóa Học'
	FROM dbo.HOCKI , dbo.KHOAHOC
	WHERE KHOAHOC.ID = IDKHOAHOC AND (dbo.fuConvertToUnsign1(IDKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@idKhoaHoc) + '%')
	ORDER BY [Mã Học Kì] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemLopTheoKhoaHoc]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemLopTheoKhoaHoc]
@maKhoaHoc NVARCHAR(10)
AS
BEGIN
	SELECT
	LOP.ID AS N'ID Lớp'
	, MALOP AS N'Mã Lớp'
	, TENLOP AS N'Tên Lớp'
	, NAMBATDAU AS N'Năm Bắt Đầu'
	, NAMKETTHUC AS N'Năm Kết Thúc'
	, MAKHOAHOC AS N'Mã Khóa Học'
	, IDKHOAHOC AS N'ID Khóa Học'
	FROM dbo.LOP , dbo.KHOAHOC
	WHERE KHOAHOC.ID = IDKHOAHOC AND (dbo.fuConvertToUnsign1(MAKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@maKhoaHoc) + '%')
	ORDER BY [Mã Lớp] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemLopTheoMaLop]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemLopTheoMaLop]
@maLop NVARCHAR(10)
AS
BEGIN
	SELECT
	LOP.ID AS N'ID Lớp'
	, MAKHOAHOC AS N'Mã Khóa Học'
	, MALOP AS N'Mã Lớp'
	, TENLOP AS N'Tên Lớp'
	, NAMBATDAU AS N'Năm Bắt Đầu'
	, NAMKETTHUC AS N'Năm Kết Thúc'
	, IDKHOAHOC AS N'ID Khóa Học'
	FROM dbo.LOP , dbo.KHOAHOC
	WHERE KHOAHOC.ID = IDKHOAHOC AND (dbo.fuConvertToUnsign1(MALOP) LIKE N'%' + dbo.fuConvertToUnsign1(@maLop) + '%')
ORDER BY [Mã Lớp] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemMaLopSV]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemMaLopSV]
@malop NVARCHAR(10)
AS
BEGIN
	SELECT
	MALOP AS N'Mã Lớp'
	, SINHVIEN.ID AS N'ID Sinh Viên'
	, SINHVIEN.MSSV AS N'Mã Số Sinh Viên'
	, SINHVIEN.HO AS N'Họ và Tên Lót'
	, SINHVIEN.TEN AS N'Tên'
	, SINHVIEN.GIOITINH AS N'Giới Tính'
	, SINHVIEN.NGAYSINH AS N'Ngày Sinh'
	, SINHVIEN.SODIENTHOAI AS N'Số Điện Thoại'
	, SINHVIEN.EMAIL AS N'Email'
	, SINHVIEN.DIACHI AS N'Địa Chỉ'
	, IDLOP AS N'ID Lớp'
	FROM dbo.SINHVIEN,dbo.LOP
	WHERE IDLOP = LOP.ID AND (dbo.fuConvertToUnsign1(MALOP) LIKE N'%' + dbo.fuConvertToUnsign1(@malop) + '%')
	ORDER BY [Tên] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemMaSoSVQLD]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemMaSoSVQLD]
@maSo NVARCHAR(30)
AS
BEGIN
	SELECT
	MALOP AS N'Mã Lớp'
	, SINHVIEN.ID AS N'ID Sinh Viên'
	, SINHVIEN.MSSV AS N'Mã Số Sinh Viên'
	, SINHVIEN.HO AS N'Họ và Tên Lót'
	, SINHVIEN.TEN AS N'Tên'
	, SINHVIEN.GIOITINH AS N'Giới Tính'
	, SINHVIEN.NGAYSINH AS N'Ngày Sinh'
	, SINHVIEN.SODIENTHOAI AS N'Số Điện Thoại'
	, SINHVIEN.EMAIL AS N'Email'
	, SINHVIEN.DIACHI AS N'Địa Chỉ'
	, IDLOP AS N'ID Lớp'
	FROM dbo.SINHVIEN,dbo.LOP
	WHERE IDLOP = LOP.ID AND (dbo.fuConvertToUnsign1(MSSV) LIKE N'%' + dbo.fuConvertToUnsign1(@maSo) + '%')
	ORDER BY [Tên] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemMaSovsTenSVQLD]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemMaSovsTenSVQLD]
@maSo NVARCHAR(30)
,@ten NVARCHAR(30)
AS
BEGIN
	SELECT
	MALOP AS N'Mã Lớp'
	, SINHVIEN.ID AS N'ID Sinh Viên'
	, SINHVIEN.MSSV AS N'Mã Số Sinh Viên'
	, SINHVIEN.HO AS N'Họ và Tên Lót'
	, SINHVIEN.TEN AS N'Tên'
	, SINHVIEN.GIOITINH AS N'Giới Tính'
	, SINHVIEN.NGAYSINH AS N'Ngày Sinh'
	, SINHVIEN.SODIENTHOAI AS N'Số Điện Thoại'
	, SINHVIEN.EMAIL AS N'Email'
	, SINHVIEN.DIACHI AS N'Địa Chỉ'
	, IDLOP AS N'ID Lớp'
	FROM dbo.SINHVIEN,dbo.LOP
	WHERE IDLOP = LOP.ID AND ((dbo.fuConvertToUnsign1(MSSV) LIKE N'%' + dbo.fuConvertToUnsign1(@maSo) + '%') AND (dbo.fuConvertToUnsign1(MSSV) LIKE N'%' + dbo.fuConvertToUnsign1(@maSo) + '%'))
	ORDER BY [Tên] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemMKH]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemMKH]
@mKH NVARCHAR(10)
AS
BEGIN
	SELECT
	ID AS N'ID Khóa Học'
	, MAKHOAHOC AS N'Mã Khóa Học'
	, TENKHOAHOC AS N'Tên Khóa Học'
	, NAMBATDAU AS N'Năm Bắt Đầu'
	, NAMKETTHUC AS N'Năm Kết Thúc'
	, THOIGIANHOCTOITHIEU AS N'TGH Tối Thiểu'
	, THOIGIANHOCTOIDA AS N'TGH Tối Đa'
	, THOIGIANHOCTIEUCHUAN AS N'TGH Quy Định'
	FROM dbo.KHOAHOC
	WHERE (dbo.fuConvertToUnsign1(MAKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@mkh) + '%') 
	ORDER BY [Mã Khóa Học] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemMKHvsNamBD]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemMKHvsNamBD]
@mKH NVARCHAR(10)
,@namBD INT
AS
BEGIN
	SELECT
	ID AS N'ID Khóa Học'
	, MAKHOAHOC AS N'Mã Khóa Học'
	, TENKHOAHOC AS N'Tên Khóa Học'
	, NAMBATDAU AS N'Năm Bắt Đầu'
	, NAMKETTHUC AS N'Năm Kết Thúc'
	, THOIGIANHOCTOITHIEU AS N'TGH Tối Thiểu'
	, THOIGIANHOCTOIDA AS N'TGH Tối Đa'
	, THOIGIANHOCTIEUCHUAN AS N'TGH Quy Định'
	FROM dbo.KHOAHOC
	WHERE (dbo.fuConvertToUnsign1(MAKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@mkh) + '%') OR (dbo.fuConvertToUnsign1(NAMBATDAU) LIKE N'%' + dbo.fuConvertToUnsign1(@namBD) + '%')
	ORDER BY [Mã Khóa Học] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemMKHvsTenKH]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemMKHvsTenKH]
@mKH NVARCHAR(10)
,@tenKH NVARCHAR(100)
AS
BEGIN
	SELECT
	ID AS N'ID Khóa Học'
	, MAKHOAHOC AS N'Mã Khóa Học'
	, TENKHOAHOC AS N'Tên Khóa Học'
	, NAMBATDAU AS N'Năm Bắt Đầu'
	, NAMKETTHUC AS N'Năm Kết Thúc'
	, THOIGIANHOCTOITHIEU AS N'TGH Tối Thiểu'
	, THOIGIANHOCTOIDA AS N'TGH Tối Đa'
	, THOIGIANHOCTIEUCHUAN AS N'TGH Quy Định'
	FROM dbo.KHOAHOC
	WHERE (dbo.fuConvertToUnsign1(MAKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@mkh) + '%') OR (dbo.fuConvertToUnsign1(TENKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@tenKH) + '%')
	ORDER BY [Mã Khóa Học] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemMSSV]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemMSSV]
@mssv NVARCHAR(10)
, @malop NVARCHAR(10)
AS
BEGIN
	SELECT
	MALOP AS N'Mã Lớp'
	, SINHVIEN.ID AS N'ID Sinh Viên'
	, SINHVIEN.MSSV AS N'Mã Số Sinh Viên'
	, SINHVIEN.HO AS N'Họ và Tên Lót'
	, SINHVIEN.TEN AS N'Tên'
	, SINHVIEN.GIOITINH AS N'Giới Tính'
	, SINHVIEN.NGAYSINH AS N'Ngày Sinh'
	, SINHVIEN.SODIENTHOAI AS N'Số Điện Thoại'
	, SINHVIEN.EMAIL AS N'Email'
	, SINHVIEN.DIACHI AS N'Địa Chỉ'
	, IDLOP AS N'ID Lớp'
	FROM dbo.SINHVIEN,dbo.LOP
	WHERE IDLOP = LOP.ID AND (dbo.fuConvertToUnsign1(MSSV) LIKE N'%' + dbo.fuConvertToUnsign1(@mssv) + '%') AND (dbo.fuConvertToUnsign1(MALOP) LIKE N'%' + dbo.fuConvertToUnsign1(@malop) + '%')
	ORDER BY [Tên] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemNamBDKH]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemNamBDKH]
@namBD INT
AS
BEGIN
	SELECT
	ID AS N'ID Khóa Học'
	, MAKHOAHOC AS N'Mã Khóa Học'
	, TENKHOAHOC AS N'Tên Khóa Học'
	, NAMBATDAU AS N'Năm Bắt Đầu'
	, NAMKETTHUC AS N'Năm Kết Thúc'
	, THOIGIANHOCTOITHIEU AS N'TGH Tối Thiểu'
	, THOIGIANHOCTOIDA AS N'TGH Tối Đa'
	, THOIGIANHOCTIEUCHUAN AS N'TGH Quy Định'
	FROM dbo.KHOAHOC
	WHERE (dbo.fuConvertToUnsign1(NAMBATDAU) LIKE N'%' + dbo.fuConvertToUnsign1(@namBD) + '%')
	ORDER BY [Mã Khóa Học] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemQuyen]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemQuyen]
@quyen INT
AS
BEGIN
	SELECT
	TAIKHOAN.ID AS N'ID Tài Khoản'
	, TENTAIKHOAN AS N'Tên Tài Khoản'
	, MATKHAU AS N'Mật Khẩu'
	, HOTEN AS N'Họ Tên'
	, TENHIENTHI AS N'Tên Hiển Thị'
	, SODIENTHOAI AS N'Số Điện Thoại'
	, EMAIL AS N'Email'
	, DIACHI AS N'Địa Chỉ'
	, IDQUYEN AS N'ID Quyền'
	, QUYEN AS N'Quyền'
	FROM dbo.TAIKHOAN , dbo.QUYEN
	WHERE IDQUYEN = @quyen AND IDQUYEN = QUYEN.ID AND IDQUYEN != 1 AND TENTAIKHOAN != N'ADMIN'
	ORDER BY [Tên Tài Khoản] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemtcSV]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemtcSV]
@mssv NVARCHAR(10)
, @ten NVARCHAR(30)
, @malop NVARCHAR(10)
AS
BEGIN
	SELECT
	MALOP AS N'Mã Lớp'
	, SINHVIEN.ID AS N'ID Sinh Viên'
	, SINHVIEN.MSSV AS N'Mã Số Sinh Viên'
	, SINHVIEN.HO AS N'Họ và Tên Lót'
	, SINHVIEN.TEN AS N'Tên'
	, SINHVIEN.GIOITINH AS N'Giới Tính'
	, SINHVIEN.NGAYSINH AS N'Ngày Sinh'
	, SINHVIEN.SODIENTHOAI AS N'Số Điện Thoại'
	, SINHVIEN.EMAIL AS N'Email'
	, SINHVIEN.DIACHI AS N'Địa Chỉ'
	, IDLOP AS N'ID Lớp'
	FROM dbo.SINHVIEN,dbo.LOP
	WHERE IDLOP = LOP.ID AND (dbo.fuConvertToUnsign1(MSSV) LIKE N'%' + dbo.fuConvertToUnsign1(@mssv) + '%') AND (dbo.fuConvertToUnsign1(TEN) LIKE N'%' + dbo.fuConvertToUnsign1(@ten) + '%') AND (dbo.fuConvertToUnsign1(MALOP) LIKE N'%' + dbo.fuConvertToUnsign1(@malop) + '%')
	ORDER BY [Tên] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemTenKH]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemTenKH]
@tenKH NVARCHAR(100)
AS
BEGIN
	SELECT
	ID AS N'ID Khóa Học'
	, MAKHOAHOC AS N'Mã Khóa Học'
	, TENKHOAHOC AS N'Tên Khóa Học'
	, NAMBATDAU AS N'Năm Bắt Đầu'
	, NAMKETTHUC AS N'Năm Kết Thúc'
	, THOIGIANHOCTOITHIEU AS N'TGH Tối Thiểu'
	, THOIGIANHOCTOIDA AS N'TGH Tối Đa'
	, THOIGIANHOCTIEUCHUAN AS N'TGH Quy Định'
	FROM dbo.KHOAHOC
	WHERE (dbo.fuConvertToUnsign1(TENKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@tenKH) + '%')
	ORDER BY [Mã Khóa Học] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemTenKHvsNamBD]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemTenKHvsNamBD]
@tenKH NVARCHAR(100)
,@namBD INT
AS
BEGIN
	SELECT
	ID AS N'ID Khóa Học'
	, MAKHOAHOC AS N'Mã Khóa Học'
	, TENKHOAHOC AS N'Tên Khóa Học'
	, NAMBATDAU AS N'Năm Bắt Đầu'
	, NAMKETTHUC AS N'Năm Kết Thúc'
	, THOIGIANHOCTOITHIEU AS N'TGH Tối Thiểu'
	, THOIGIANHOCTOIDA AS N'TGH Tối Đa'
	, THOIGIANHOCTIEUCHUAN AS N'TGH Quy Định'
	FROM dbo.KHOAHOC
	WHERE (dbo.fuConvertToUnsign1(TENKHOAHOC) LIKE N'%' + dbo.fuConvertToUnsign1(@tenKH) + '%') OR (dbo.fuConvertToUnsign1(NAMBATDAU) LIKE N'%' + dbo.fuConvertToUnsign1(@namBD) + '%')
	ORDER BY [Mã Khóa Học] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemTenSV]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemTenSV]
@ten NVARCHAR(30)
, @malop NVARCHAR(10)
AS
BEGIN
	SELECT
	MALOP AS N'Mã Lớp'
	, SINHVIEN.ID AS N'ID Sinh Viên'
	, SINHVIEN.MSSV AS N'Mã Số Sinh Viên'
	, SINHVIEN.HO AS N'Họ và Tên Lót'
	, SINHVIEN.TEN AS N'Tên'
	, SINHVIEN.GIOITINH AS N'Giới Tính'
	, SINHVIEN.NGAYSINH AS N'Ngày Sinh'
	, SINHVIEN.SODIENTHOAI AS N'Số Điện Thoại'
	, SINHVIEN.EMAIL AS N'Email'
	, SINHVIEN.DIACHI AS N'Địa Chỉ'
	, IDLOP AS N'ID Lớp'
	FROM dbo.SINHVIEN,dbo.LOP
	WHERE IDLOP = LOP.ID AND (dbo.fuConvertToUnsign1(TEN) LIKE N'%' + dbo.fuConvertToUnsign1(@ten) + '%') AND (dbo.fuConvertToUnsign1(MALOP) LIKE N'%' + dbo.fuConvertToUnsign1(@malop) + '%')
	ORDER BY [Tên] ASC
END

GO
/****** Object:  StoredProcedure [dbo].[USP_TimKiemTenSVQLD]    Script Date: 14/12/2020 15:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[USP_TimKiemTenSVQLD]
@ten NVARCHAR(30)
AS
BEGIN
	SELECT
	MALOP AS N'Mã Lớp'
	, SINHVIEN.ID AS N'ID Sinh Viên'
	, SINHVIEN.MSSV AS N'Mã Số Sinh Viên'
	, SINHVIEN.HO AS N'Họ và Tên Lót'
	, SINHVIEN.TEN AS N'Tên'
	, SINHVIEN.GIOITINH AS N'Giới Tính'
	, SINHVIEN.NGAYSINH AS N'Ngày Sinh'
	, SINHVIEN.SODIENTHOAI AS N'Số Điện Thoại'
	, SINHVIEN.EMAIL AS N'Email'
	, SINHVIEN.DIACHI AS N'Địa Chỉ'
	, IDLOP AS N'ID Lớp'
	FROM dbo.SINHVIEN,dbo.LOP
	WHERE IDLOP = LOP.ID AND (dbo.fuConvertToUnsign1(TEN) LIKE N'%' + dbo.fuConvertToUnsign1(@ten) + '%')
	ORDER BY [Tên] ASC
END

GO
USE [master]
GO
ALTER DATABASE [db_QLDSVKKTCN] SET  READ_WRITE 
GO
