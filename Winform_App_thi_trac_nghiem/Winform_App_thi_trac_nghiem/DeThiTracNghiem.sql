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
select * from NguoiDung
select * from CauHoi 
select * from BangThi
select * from DapAn
select * from KetQuaThi
go
insert into BangThi values
('A1', N'Bằng A1', '2024-10-06 09:00:00', N'Trắc nghiệm', '00:15:00'),
('A2', N'Bằng A2', '2024-10-06 09:00:00', N'Trắc nghiệm', '00:15:00'),
('B1', N'Bằng B1', '2024-10-06 09:00:00', N'Trắc nghiệm', '00:17:00'),
('B2', N'Bằng B2', '2024-10-06 09:00:00', N'Trắc nghiệm', '00:20:00');
go 
Insert into NguoiDung values
('052204000123','loi cu be', 18, 'A1'),
('052204000234','huy dep chai', 10, 'A2'),
('052204000567','duy sieu lun', 30, 'A1');
go
insert into CauHoi values 
('A10001', 'Cau hoi so 1', 'dap an 1', 'dap an 2', 'dap an 3', 'dap an 4',  'hinh1.jpg', N'Thuong', 4,'A1' ),
('A10002', 'Cau hoi so 2', 'dap an 11', 'dap an 22', 'dap an 33', 'dap an 44', 'hinh2.jpg', N'Liet', 2,'A1'),
('A20003', 'Cau hoi so 3', 'dap an 111', 'dap an 222', 'dap an 333', 'dap an 444',  'hinh3.jpg', N'Thuong', 1,'A2'),
('B10004', 'Cau hoi so 4', 'dap an 1111', 'dap an 2222', 'dap an 3333', 'dap an 4444',  'hinh4.jpg', N'Thuong', 3,'B1'),
('B20005', 'Cau hoi so 5', 'dap an 11111', 'dap an 22222', 'dap an 33333', 'dap an 44444',  'hinh5.jpg', N'Liet', 4,'B2'),
('A20004', 'Cau hoi so 6', 'dap an 111111', 'dap an 22222', 'dap an 33333', 'dap an 44444',  'hinh6.jpg', N'Thuong', 2,'A2'),
('B20004', 'Cau hoi so 7', 'dap an 1111111', 'dap an 222222', 'dap an 333333', 'dap an 444444',  'hinh7.jpg', N'Liet', 3,'B2');

go 

insert into DapAn values
('DaA1001', 'A10001', 1 ),
('DaA1002', 'A10002', 1 ),
('DaA2003', 'A20003', 0),
('DaA2004', 'A20004', 0),
('DaB1004', 'B10004', 0),
('DaB2004', 'B20004', 1),
('DaB2005', 'B20005', 0);
go 
insert into KetQuaThi (IDNguoiDung, IdBangThi, SoCauDung, SoCauSai, KetQua) values
('052204000123', 'B1', 21, 4, 'Dau'),
('052204000234', 'A2', 25, 0, 'Dau'),
('052204000567', 'B2', 20, 5, 'Truot');


/*delete from NguoiDung where IDNguoiDung in(1, 2, 3, 4, 5, 6) /*dung de xoa idnguoidung voi thuoc tinh int identity*/ */

--DECLARE @DeletedRows TABLE (IDNguoiDung INT);

--DELETE FROM NguoiDung
--OUTPUT DELETED.IDNguoiDung INTO @DeletedRows
--WHERE  IDNguoiDung in(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);

--SELECT * FROM @DeletedRows; -- Trả về ID của bản ghi đã xóa
