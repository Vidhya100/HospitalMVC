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

--get Apppointment Id from Appointment table
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


create table Doctor
(
	DId int Identity Primary Key,
	AId Int not null foreign key(AId) references Appointments(AId)
)

select* from Doctor;
drop table Doctor

create or alter procedure spDoctorAppointment(
	
	@AId int
)
as
begin
	Insert into Doctor values (@AId)
end

create or alter procedure GetAppoinmentList
(
	
	@DId int
)
as 
begin
	select AId from Doctor where DId = @DId;
end

