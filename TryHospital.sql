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
	begin try
		Insert into Register(Username,Email,Password,Role,Fullname,ProfileIng,Degree,Address)
		 values (@Username,@Email,@Password,@Role,@Fullname,@ProfileIng,@Degree,@Address);
	end try
	begin catch
		Print 'Registration Failed'
		return -1
	end catch
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
	begin try
		select * from Register where Email=@Email and Password = @Password;

		if @@ROWCOUNT = 0
		begin
			print 'Invalid email or password.';
			return -1;
		end
		else
		begin
			print 'Login successful.';
			return 0;
		end
	end try
	begin catch
		print ERROR_MESSAGE();
		return -1;
	end catch
end

Exec spLogin "vidhyadarade1@gmail.com", Vidhya@12;

-- get doc list
create or alter procedure spGetDocList
(	
	@Role varchar(250)
)
as
begin
	begin try
		select UserId,ProfileIng,Username,Degree,Address from Register where Role = @Role

		if @@ROWCOUNT = 0
		begin
			print 'No records found.';
			return -1;
		end
		else
		begin
			print 'Records found.';
			return 0;
		end
	end try
	begin catch
		print ERROR_MESSAGE();
		return -1;
	end catch
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
	Date Datetime,
	VisitStartTime datetime,
	VisiteEndTime datetime,
	Number bigint,
	Condition varchar(250)

)

drop table Appointments
ALTER TABLE Appointments ALTER COLUMN ProfileImg nvarchar(max);

---create new appointment by inserting data
create or alter procedure spCreateAppointments
(
	
	
	@PId int,
	@DId int,

	@Pname varchar(250),
	@Dname varchar(250),
	@ProfileImg varchar(250),
	@Email varchar(250),
	@Date Datetime,
	@VisitStartTime datetime,
	@VisiteEndTime datetime,
	@Number bigint,
	@Condition varchar(250),
	@isHide int
)
as 
begin
	begin try

		Insert into Appointments values (
	

		@PId,
		@DId ,
		@Pname,
		@Dname,
		@ProfileImg ,
		@Email ,
		@Date ,
		@VisitStartTime ,
		@VisiteEndTime ,
		@Number ,
		@Condition,
		@isHide
		 )
		 print 'Appoinment created successfully'
		 ---retrun indicate the status of the operation successful or not.
		 return 0;
	end try
	begin catch
		print ERROR_MESSAGE();
	end catch
end

--get Apppointment Id from Appointment table for doctor and patient
create or alter procedure spGetAppointmentID
(
	
	@DId int,
	@PId int
)
as 
begin
	begin try
		Select AId from Appointments where DId=@DId and PId = @PId
		return 0
	end try
	begin catch
		print ERROR_MESSAGE();
		return -1;
	end catch
end

--Get All appointment details 
create or alter procedure spAppoinmentDetails
(
	@AId int
)
as
begin
	begin try
		select DId,PId,ProfileImg,Pname,Email,Date,VisitStartTime,VisiteEndTime,Dname,Number,Condition from Appointments where AId =@AId
	end try
	begin catch
		print ERROR_MESSAGE();
	end catch
end

-----doctor table
create table Doctor
(
	Id int Identity,
	DId int,
	primary key(Id,DId),
	AId Int foreign key(AId) references Appointments(AId),
	isHide bit default 0
)
----Patient table
create table Patient
(
	Id int identity,
	PId int,
	primary key(Id,PId),
	AId Int foreign key(AId) references Appointments(AId),
	isHide bit default 0
)

select* from Doctor;
drop table Doctor
drop table Patient
drop table Appointments

---Add AId in doctor table
--create or alter procedure spDoctorAppointment(
	
--	@AId int
--)
--as
--begin
--		Insert into Doctor values (@AId)
--end
------get  apponment Id from doctor table
create or alter procedure GetAppoinmentList
(
	
	@DId int
)
as 
begin
	begin try
		select AId from Doctor where DId = @DId 
	end try
	begin catch
		print ERROR_MESSAGE();
	end catch
end

----insert values in doctor and patient table
create or alter procedure spDPAppointment
(	
	@AId int,
	@DId int,
	@PId int,
	@isHide int
)
as
begin
	begin try
		insert into Doctor values(@DId,@AId,@isHide)
		insert into Patient values(@PId,@AId,@isHide)
	end try
	begin catch
		print ERROR_MESSAGE();
	end catch
end

---get Aid from patient where PId=PId and not hide
create or alter procedure GetPAppoinmentList
(	
	@PId int
)
as
begin
	begin try
		select AId from Patient where PId = @PId and isHide=0
	end try
	begin catch
		print ERROR_MESSAGE();
	end catch
end
---get appoinment details from appoinment where AId and PId given
create or alter  procedure GetPatientAppoinments  
(  
 @AId int,  
 @PId int  
)  
as  
begin 
	begin try 
		SELECT AId,ProfileImg,Pname,Email,Date,VisitStartTime,VisiteEndTime,Number,Dname,Condition FROM Appointments WHERE AId = @AId and PId = @PId  
	end try
	begin catch
		print ERROR_MESSAGE();
	end catch
end

---get appoinment details from appoinment for given AId
create or alter  procedure spAppoinmentDetails  
(  
 @AId int  
)  
as  
begin
	begin try  
		select AId,DId,PId,ProfileImg,Pname,Email,Date,VisitStartTime,VisiteEndTime,Dname,Number,Condition from Appointments where AId =@AId
	end try
	begin catch
		print ERROR_MESSAGE();
	end catch  
end

--get aid from doctor where DId is given
create or alter  procedure GetAppoinmentList  
(  
   
 @DId int  
)  
as   
begin
	begin try  
		select AId from Doctor where DId = @DId  and isHide=0;  
	end try
	begin catch
		print ERROR_MESSAGE();
	end catch
end
---get appoinment details from appoinments where Aid and Did given
create or alter  procedure GetDocAppoinments  
 (  
 @AId int,  
 @DId int  
)  
as  
begin  
	begin try
		SELECT AId,ProfileImg,Pname,Email,Date,VisitStartTime,VisiteEndTime,Number,Dname,Condition FROM Appointments WHERE AId = @AId and DId = @DId  
	end try
	begin catch
		print ERROR_MESSAGE();
	end catch
end

---select all from appoinments for admin
create or alter  procedure GetAllAppoinments  
as  
begin 
	begin try 
		select AId,DId,PId,ProfileImg,Pname,Email,Date,VisitStartTime,VisiteEndTime,Dname,Number,Condition from Appointments  where isHide=0
	end try
	begin catch
		print ERROR_MESSAGE();
	end catch
end

--select  AId,DId,PId,ProfileImg,Pname,Email,Date,VisitStartTime,VisiteEndTime,Dname,Number,Condition from Appointments where isHide=0
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
	begin try
		 Update Appointments SET ProfileImg=@ProfileImg,Pname=@Pname,Email=@Email,Date=@Date,VisitStartTime=@VisitStartTime, VisiteEndTime=@VisiteEndTime,Number=@Number,Dname=@Dname,Condition = @Condition where AId=@AId;
	end try
	begin catch
		print ERROR_MESSAGE()
	end catch  
end

--for removing from Ui but stored in database used flags
create or alter  procedure spRemove  
(  
 @AId int  
)  
as  
begin  
	 begin try
        if exists (select 1 from Appointments where AId = @AId)
			 begin
				update Doctor set isHide = 1 where AId = @AId;
				update Patient set isHide = 1 where AId = @AId;
				update Appointments set isHide = 1 where AId = @AId;
			end
        else
			begin
				print 'Appointment not found';
			end
    end try
    begin catch
        print ERROR_MESSAGE() ;
    end catch
  --Update Doctor Set  isHide=1 where AId = @AId;  
  --Update Patient Set  isHide=1 where AId = @AId; 
  --Update Appointments Set  isHide=1 where AId = @AId; 
end

---for deleting permenantly
create or alter procedure spDelete
(
	@AId int 
)
as
begin
		begin
			delete from Patient where AId=@AId
			delete from Doctor where AId=@AId
		end
			delete from Appointments Where AId=@AId
end

select *from Doctor
select * from Patient
select * from Appointments 
select * from Register

delete from Register where UserId=13

alter table Doctor Add isHide int DEFAULT 0
alter table Patient Add isHide int DEFAULT 0
alter table Appointments Add isHide int DEFAULT 0

alter table Doctor Drop column isHide

insert into doctor(DId,AId) values(4,15)
insert into Patient(PId,AId) values(2,15)

delete from Patient where PId = 2 and AId = 16

exec sp_helptext 'GetPAppoinmentList'
exec sp_helptext 'GetPatientAppoinments'
exec sp_helptext 'spAppoinmentDetails'

exec sp_helptext '[dbo].[GetAppoinmentList]'
exec sp_helptext '[dbo].[GetDocAppoinments]'

exec sp_helptext '[dbo].[GetAllAppoinments]'
exec sp_helptext '[dbo].[spUpdate]'
exec sp_helptext '[dbo].[spDelete]'

exec sp_helptext '[dbo].[spDoctorAppointment]'

exec sp_helptext 'spDPAppointment'


