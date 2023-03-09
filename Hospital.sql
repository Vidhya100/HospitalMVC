create database Hospital;
use Hospital;

create table Patients(
PId Int Identity Primary key,
Pname varchar(200),
ProfileImg varchar(200),
Email varchar(200),
Number INT
)

create table Doctor(
	DoctorId Int Identity Primary key,
	Dname varchar(150),
	Degree varchar(200),
	Email varchar(200),
	PId Int not null foreign key (PId) references Patients(PId)

)
create table Appoinment(
	AId INT Identity Primary Key,
	Date DATE,
	VisitTime Time,
	Condition varchar(200),
	PId Int not null foreign key (PId) references Patients(PId),
	DoctorId Int not null foreign key(DoctorId) references Doctor(DoctorId)
	
)
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