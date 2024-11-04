Use master
go
IF Exists (select * from sysdatabases where name = 'DeThiTracNghiem')
	Drop database DeThiTracNghiem
go
create database DeThiTracNghiem
go
use DeThiTracNghiem
go
Create table BangThi(
						IdBangThi nvarchar(2) primary key,					
						TenBangThi nvarchar(10) not null,
						GioThi Datetime default getdate() not null,
						HinhThuc nvarchar(20) not null,
						ThoiGian time,
					)
go
Create table NguoiDung(
						CCCD nvarchar(12) Primary key,
						Hoten nvarchar(30) not null,						
						Tuoi tinyint,
						IdBangThi nvarchar(2),
						foreign key (IdBangThi) references BangThi(IdBangThi)

					  )
go
Create table CauHoi(
						IdCauHoi nvarchar(6) primary key check(IdCauHoi like'[A-B][0-9][0-9][0-9][0-9][0-9]') not null,
						Debai nvarchar(200) not null,
						DapAn1 nvarchar (200) not null,
						DapAn2 nvarchar (200) not null,
						DapAn3 nvarchar(200) not null,
						DapAn4 nvarchar(200) not null,
						HinhAnh NVARCHAR(MAX) not null,
						LoaiCauHoi nvarchar(10) check (LoaiCauHoi in( N'Thuong', N'Liet',N'SaHinh')) not null,
						DapAnDung tinyint check(DapAnDung between 1 and 4) not null,
						IdBangThi nvarchar(2), 
						foreign key (IdBangThi) references BangThi(IdBangThi),
					)

go
Create table DapAn(	
						IdDapAn nvarchar(8) primary key,
						IdCauHoi nvarchar(6),
						CauDung bit NOT NULL,
						Foreign key(IdCauHoi) references CauHoi(IdCauHoi)
				  )
go
create table KetQuaThi(
						 IDKetQua int IDENTITY PRIMARY KEY, 
						 IDNguoiDung nvarchar(12),  
						 IdBangThi nvarchar(2),  
						 SoCauDung int, 
						 SoCauSai int,  
					     ThoiGianLamBai time, 
						 KetQua nvarchar(5),
						 FOREIGN KEY (IDNguoiDung) REFERENCES NguoiDung(IDNguoiDung),
						 FOREIGN KEY (IdBangThi) REFERENCES BangThi(IdBangThi)
					  )

go
select * from BangThi
go
insert into BangThi values
('A1', N'Bằng A1', '2024-10-06 09:00:00', N'Trắc nghiệm', '00:15:00'),
('A2', N'Bằng A2', '2024-10-06 09:00:00', N'Trắc nghiệm', '00:15:00'),
('B1', N'Bằng B1', '2024-10-06 09:00:00', N'Trắc nghiệm', '00:17:00'),
('B2', N'Bằng B2', '2024-10-06 09:00:00', N'Trắc nghiệm', '00:20:00');

/*delete from NguoiDung where IDNguoiDung in(1, 2, 3, 4, 5, 6) /*dung de xoa idnguoidung voi thuoc tinh int identity*/ */

--DECLARE @DeletedRows TABLE (IDNguoiDung INT);

--DELETE FROM NguoiDung
--OUTPUT DELETED.IDNguoiDung INTO @DeletedRows
--WHERE  IDNguoiDung in(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);

--SELECT * FROM @DeletedRows; -- Trả về ID của bản ghi đã xóa
