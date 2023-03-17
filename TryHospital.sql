create database HospitalDB
use HospitalDB

--Registration table
create table Register(
	UserId Int Identity,
	Username varchar(200),
	Email varchar(200),
	Password varchar(200),
	Role varchar(100),

	Fullname varchar(250),
	ProfileIng varchar(250),
	Degree varchar(250),
	Address varchar(250),
	Primary key(UserID, Username)
)

drop table Register

--register user
create or alter procedure spRegister
(
	@Username varchar(200),
	@Email varchar(200),
	@Password varchar(200),
	@Role varchar(200),

	@Fullname varchar(250),
	@ProfileIng varchar(250),
	@Degree varchar(250),
	@Address varchar(250)
)
as 
begin 
	Insert into Register(Username,Email,Password,Role,Fullname,ProfileIng,Degree,Address)
	 values (@Username,@Email,@Password,@Role,@Fullname,@ProfileIng,@Degree,@Address);
end

truncate table Register
select * from Register
---Add User

create or alter procedure  spLogin
	@Email varchar(200),
	@Password varchar(200)
	--@Role varchar(100)

as
begin
	select * from Register where Email=@Email and Password = @Password;
end

Exec spLogin "vidhyadarade1@gmail.com", Vidhya@12;

-- get doc list
create or alter procedure spGetDocList
(	
	@Role varchar(250)
)
as
begin
	select UserId,ProfileIng,Username,Degree,Address from Register where Role = @Role
end

---appoinment table
create table Appointments
(
	AId int Identity Primary key,
	PId int,
	DId int,

	Pname varchar(250),
	Dname varchar(250),
	ProfileImg varchar(250),
	Email varchar(250),
	Date Date,
	VisitStartTime datetime,
	VisiteEndTime datetime,
	Number bigint,
	Condition varchar(250)

)

drop table Appointments

---create new appointment by inserting data
create or alter procedure spCreateAppointments
(
	
	@DId int,
	@PId int,

	@Pname varchar(250),
	@Dname varchar(250),
	@ProfileImg varchar(250),
	@Email varchar(250),
	@Date Date,
	@VisitStartTime datetime,
	@VisiteEndTime datetime,
	@Number bigint,
	@Condition varchar(250)
)
as 
begin
	Insert into Appointments values (
	
	@DId ,
	@PId,
	@Pname,
	@Dname,
	@ProfileImg ,
	@Email ,
	@Date ,
	@VisitStartTime ,
	@VisiteEndTime ,
	@Number ,
	@Condition )
end

--get Apppointment Id from Appointment table for doctor
create or alter procedure spGetAppointmentID
(
	
	@DId int,
	@PId int
)
as 
begin
	Select AId from Appointments where PId=@PId and DId = @DId
end

--Get All appointment details 
create or alter procedure spAppoinmentDetails
(
	@AId int
)
as
begin
	select DId,PId,ProfileImg,Pname,Email,Date,VisitStartTime,VisiteEndTime,Dname,Number,Condition from Appointments where AId =@AId
end

-----doctor table
create table Doctor
(
	Id int,
	DId int,
	primary key(Id,DId),
	ishide bit default 0,
	AId Int foreign key(AId) references Appointments(AId)
)
----Patient table
create table Patient
(
	Id int,
	PId int,
	primary key(Id,PId),
	ishide bit default 0,
	AId Int foreign key(AId) references Appointments(AId)
)

select* from Doctor;
drop table Doctor

---Add AId in doctor table
create or alter procedure spDoctorAppointment(
	
	@AId int
)
as
begin
	Insert into Doctor values (@AId)
end
------get  apponment Id from doctor table
create or alter procedure GetAppoinmentList
(
	
	@DId int
)
as 
begin
	select AId from Doctor where DId = @DId;
end

----insert values in doctor and patient table
create or alter procedure spDPAppointment
(	
	@AId int,
	@DId int,
	@PId int
)
as
begin
	insert into Doctor values(@DId,@AId)
	insert into Patient values(@PId,@AId)
end

---get Aid from patient where PId=PId and not hide
create or alter procedure GetPAppoinmentList
(	
	@PId int
)
as
begin
	 select AId from Patient where PId = @PId and isHide=0;  
end
---get appoinment details from appoinment where AId and PId given
create or alter  procedure GetPatientAppoinments  
(  
 @AId int,  
 @PId int  
)  
as  
begin  
 SELECT AId,ProfileImg,Pname,Email,Date,VisitStartTime,VisiteEndTime,Number,Dname,Condition FROM Appointments WHERE AId = @AId and PId = @PId  
end

---get appoinment details from appoinment for given AId
create or alter  procedure spAppoinmentDetails  
(  
 @AId int  
)  
as  
begin  
 select AId,DId,PId,ProfileImg,Pname,Email,Date,VisitStartTime,VisiteEndTime,Dname,Number,Condition from Appointments where AId =@AId  
end

--get aid from doctor where DId is given
create or alter  procedure GetAppoinmentList  
(  
   
 @DId int  
)  
as   
begin  
 select AId from Doctor where DId = @DId and isHide=0;  
end
---get appoinment details from appoinments where Aid and Did given
create or alter  procedure GetDocAppoinments  
(  
 @AId int,  
 @DId int  
)  
as  
begin  
 SELECT AId,ProfileImg,Pname,Email,Date,VisitStartTime,VisiteEndTime,Number,Dname,Condition FROM Appointments WHERE AId = @AId and DId = @DId  
end

---select all from appoinments for admin
create or alter  procedure GetAllAppoinments  
as  
begin  
 select AId,DId,PId,ProfileImg,Pname,Email,Date,VisitStartTime,VisiteEndTime,Dname,Number,Condition from Appointments  
end

---update details of Appoinment
create or alter  procedure spUpdate  
(  
 @AId int,  
 --@DId int,  
 --@PId int,  
 @ProfileImg varchar(250),  
 @Pname varchar(200),  
 @Email varchar(200),  
 @Date datetime,  
 @VisitStartTime datetime,  
 @VisiteEndTime datetime,  
 @Number bigint,  
 @Dname varchar(250),  
 @Condition varchar(200)  
)  
as  
begin  
  Update Appointments SET ProfileImg=@ProfileImg,Pname=@Pname,Email=@Email,Date=@Date,VisitStartTime=@VisitStartTime, VisiteEndTime=@VisiteEndTime,Number=@Number,Dname=@Dname,Condition = @Condition where AId=@AId;  
end

--for removing from Ui but stored in database used flags
create or alter  procedure spDelete  
(  
 @AId int  
)  
as  
begin  
  Update Doctor Set  isHide=1 where AId = @AId;  
  Update Patient Set  isHide=1 where AId = @AId;  
end

select *from Doctor
select * from Patient
select * from Appointments

alter table Doctor Add column isHide bit default 0
alter table Patient Add column isHide bit default 0

exec sp_helptext 'GetPAppoinmentList'
exec sp_helptext 'GetPatientAppoinments'
exec sp_helptext 'spAppoinmentDetails'

exec sp_helptext '[dbo].[GetAppoinmentList]'
exec sp_helptext '[dbo].[GetDocAppoinments]'

exec sp_helptext '[dbo].[GetAllAppoinments]'
exec sp_helptext '[dbo].[spUpdate]'
exec sp_helptext '[dbo].[spDelete]'

exec sp_helptext 'spDPAppointment'


