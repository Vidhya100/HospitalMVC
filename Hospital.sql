create database Hospital;
use Hospital;

drop database Hospital

create table Patients(
PId Int Identity Primary key,
Pname varchar(200),
ProfileImg varchar(200),
Email varchar(200),
Number INT,

)   

ALTER TABLE Patients ADD AId Int foreign key (AId) references Appoinment(AId);
ALTER TABLE Patients ADD DId Int foreign key (DId) references Doctor(DoctorId);

insert into Patients values('Rekha','string','rekha@gmail.com',1478523695);
insert into Patients values('Pooja','string','pooja@gmail.com',1478523695);
insert into Patients values('Ankita','','ankita@gmail.com',1478523695);



select * from Patients;

create table Doctor(
	DoctorId Int Identity Primary key,
	Dname varchar(150),
	Degree varchar(200),
	Email varchar(200),
	
)

ALTER TABLE Doctor ADD AId Int foreign key (AId) references Appoinment(AId);

insert into Doctor values('Ankita','MD. Radiology','ankita@gmail.com',1);
insert into Doctor values('Suraj','MD. Medicine','suraj@gmail.com',2);
insert into Doctor values('Kanchan','Gynacologist','kanchan@gmail.com',3);

select * from Doctor;

create table Appoinment(
	AId INT Identity Primary Key,
	Date datetime,
	VisitTime date,
	Condition varchar(200),
	DoctorId Int not null foreign key(DoctorId) references Doctor(DoctorId)
	
)

 ALTER TABLE Appoinment Alter column VisitTime datetime; 
 ALTER TABLE Appoinment Alter column VisitEnd datetime; 
  ALTER TABLE Appoinment Alter column Date date;
 
Drop table Appoinment
ALTER TABLE Appoinment ADD VisitEnd Time;
insert into Appoinment values('12-5-23','00:10:00','injured',3);
insert into Appoinment values('12-5-23','00:10:00','injured',4,'00:12:00');
insert into Appoinment values('12-9-23','00:11:00','Pregnent',4,'00:12:00');
select * from Appoinment;

alter procedure spGqtAllAppointments
	
as
begin
		--select a.AId , p.ProfileImg, p.Pname, p.Email, a.Date,  a.VisitTime, p.Number, d.Dname , a.Condition from Patients as p,
		--Doctor as d , Appoinment as a where p.PId = d.PId and
		--  a.DoctorId= d.DoctorId;

		select a.AId, p.ProfileImg, p.Pname, p.Email, a.Date, a.VisitTime, p.Number, d.Dname, a.Condition from
		Patients as p inner join Doctor as d ON  p.PId = d.PId inner join Appoinment as a on d.DoctorId = a.DoctorId;

		--update Patients SET Patients.AId = AId;
		--update Doctor SET Doctor.AId = AId
end

Exec spGqtAllAppointments;

--select a.AId, p.ProfileImg, p.Pname, p.Email, a.Date, a.VisitTime, p.Number, d.Dname, a.Condition from
--		Patients as p inner join Doctor as d ON  p.PId = d.PId inner join Appoinment as a on d.DoctorId = a.DoctorId;

alter procedure spGetById
(
	@AId int
)
as
begin

	select a.AId, p.ProfileImg, p.Pname, p.Email, a.Date, a.VisitTime, p.Number, d.Dname, a.Condition from
		Patients as p inner join Doctor as d ON  p.PId = d.PId inner join Appoinment as a on d.DoctorId = a.DoctorId where a.AId = @AId;
End

Exec spGetById 2

alter procedure spUpdate
(
	@AId int,
	--@Pname varchar(200),
	--@Email varchar(200),
	@Date date,
	@VisitTime datetime,
	@VisitEnd datetime,
	@Condition varchar(200)
)
as
begin
		--Update Patients SET Pname=@Pname, Email=@Email where AId=@AId ;
		Update Appoinment SET Date=@Date, VisitTime=@VisitTime, VisitEnd=@VisitEnd, Condition = @Condition where AId=@AId;

		--if (select * from appoinment where AId = @AID)
		--then 
		--	Insert into Patients values(Pname= @Pname, Email=@Email)
		--	Insert 
end

Exec spUpdate 3,'Swati','swati@gmail.com','2023-12-07','00:10:00.0000','00:10:15.0000','Highly injured'

create or alter procedure spDelete(
	@AId int
)
as
begin
		--Delete from Patients where AId= @AId;
		--Delete from Appoinment where AId =@AId;
		--Delete Dname from Doctor where AId =@AId;

		--delete a.AId, p.ProfileImg, p.Pname, p.Email, a.Date, a.VisitTime, p.Number, d.Dname, a.Condition from
		--Patients as p inner join Doctor as d ON  p.PId = d.PId inner join Appoinment as a on d.DoctorId = a.DoctorId where AId = @AId;

		--delete Patients, Doctor, Appoinment from Patients inner join Doctor on Patients.PId = Doctor.PId  inner join Appoinment on Doctor.DoctorId = Appoinment.DoctorId
		--where Appoinment.AId = @AId;
end


/*-------------For Doctor View*/
create procedure spGetDocAppoinments
as
begin
end

create table Admin(
    AdminId Int Primary key Identity,
	Username varchar(200),
	Email varchar(200),
	Password varchar(200)
)

create table Register(
	UserId Int Identity Primary key,
	Username varchar(200),
	Email varchar(200),
	Password varchar(200),
	Role varchar(100)
)

create procedure spRegister(
	@Username varchar(200),
	@Email varchar(200),
	@Password varchar(200),
	@Role varchar(200)
)
as
begin 
	insert into Register values(@Username,@Email,@Password,@Role);
end

Exec spRegister "Admin","admin@gmail.com","Admin@12","Admin";

select * from Register;

alter procedure  spLogin
	@Email varchar(200),
	@Password varchar(200)
	--@Role varchar(100)

as
begin
	select * from Register where Email=@Email and Password = @Password;
end

Exec spLogin "vidhyadarade1@gmail.com", Vidhya@12;











