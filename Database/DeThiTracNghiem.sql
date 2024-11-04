Use master
go
--IF Exists (select * from sysdatabases where name = 'DeThiTracNghiem')
--Drop database DeThiTracNghiem
--go
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
							IdCauHoi int primary key /*check(IdCauHoi like'[A-B][0-9][0-9][0-9][0-9][0-9]')*/ not null,
							Debai nvarchar(max) not null,
							DapAn1 nvarchar (max) not null,
							DapAn2 nvarchar (max) not null,
							DapAn3 nvarchar(max) not null,
							DapAn4 nvarchar(max) not null,
							HinhAnh NVARCHAR(MAX) not null,
							LoaiCauHoi nvarchar(10) check (LoaiCauHoi in( N'Thuong', N'Liet',N'SaHinh')) not null,
							DapAnDung tinyint check(DapAnDung between 1 and 4) not null,
							KetQuaBaiThi tinyint check(KetQuaBaiThi between 0 and 4) default 0,
							IdBangThi nvarchar(2), 
							KetQuaCauHoi nvarchar(5) check(KetQuaCauHoi between N'Đúng' and N'Sai'),
							foreign key (IdBangThi) references BangThi(IdBangThi),
						)
	go
	create table KetQuaThi(
							 IDKetQua int IDENTITY PRIMARY KEY, 
							 CCCD nvarchar(12),
							 NgayThi Date,
							 SoCauDung int, 
							 SoCauSai int,  
							 TrangThai nvarchar(8) check(TrangThai between N'Đậu' and N'Trượt'),
							 FOREIGN KEY (CCCD) REFERENCES NguoiDung(CCCD)					
						  )

	go
	select * from NguoiDung
	select * from CauHoi 
	select * from BangThi
	select * from KetQuaThi
go
insert into BangThi values
('A1', N'Bằng A1', '2024-10-06 09:00:00', N'Trắc nghiệm', '00:20:00'),
('A2', N'Bằng A2', '2024-10-06 09:00:00', N'Trắc nghiệm', '00:15:00'),
('B1', N'Bằng B1', '2024-10-06 09:00:00', N'Trắc nghiệm', '00:17:00'),
('B2', N'Bằng B2', '2024-10-06 09:00:00', N'Trắc nghiệm', '00:20:00');

--insert into KetQuaThi (CCCD, SoCauDung, SoCauSai, TrangThai) values
--('052204000123', 21, 4, N'Đậu'),
--('052204000234', 25, 0, N'Đậu'),
--('052204000567', 20, 5, N'Trượt');
--delete from KetQuaThi
--*delete from CauHoi where IdCauHoi in(1, 2, 3, 4, 5, 6) /*dung de xoa idnguoidung voi thuoc tinh int identity*/ */

--DECLARE @DeletedRows TABLE (IDNguoiDung INT);

--DELETE FROM NguoiDung
--OUTPUT DELETED.IDNguoiDung INTO @DeletedRows
--WHERE  IDNguoiDung in(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);

--SELECT * FROM @DeletedRows; -- Trả về ID của bản ghi đã xóa
