Use Quiz4
Go

--Drop Tables
Drop Table WageHistory
Drop Table SaleItem
Drop Table FunSeekerMembership
Drop Table Sale
Drop Table Item
Drop Table Membership
Drop Table FunSeeker
Drop Table ClubMembership
Drop Table RideLog
Drop Table Club
Drop Table Staff
Drop Table Task
Drop Table Ride
Drop Table Area
Drop Table Category
Go

--Create Tables
Create Table Category
(
	CategoryCode			int	identity(1,1)	Not Null
												Constraint PK_Category_CategoryCode
													Primary Key Clustered,
	CategoryName			varchar(50) 		Not Null,
	MinimumHeight			smallint			Not Null
)

Create Table Area
(
	AreaID					char(4) 			Not Null
												Constraint PK_Area_AreaID
													Primary Key Clustered,
	AreaName				varchar(50)			Not Null,
	AreaDescription			varchar(150)		Not Null
)

Create Table Ride
(
	RideID					int	identity(1,1)	Not Null 
												Constraint PK_Ride_RideID
													Primary Key Clustered,
	RideName				varchar(50)			Not Null,
	RideDescription			varchar(150)		Not Null,
	CategoryCode			int					Not Null 
												Constraint FK_Ride_CategoryCode_To_Category_CategoryCode
													References Category(CategoryCode),
	AreaID					char(4)				Not Null 
												Constraint FK_Ride_AreaID_To_Area_AreaID
													References Area(AreaID)
)

Create Table Task
(
	TaskID					int	identity(1,1)	Not Null 
												Constraint PK_Task_TaskID
													Primary Key Clustered,
	TaskDescription			varchar(150)		Not Null
)

Create Table Staff
(
	StaffID					int	identity(1,1)	Not Null 
												Constraint PK_Staff_StaffID
													Primary Key Clustered,
	FirstName				varchar(30)			Not Null,
	LastName				varchar(30)			Not Null,
	HireDate				smalldatetime		Not Null,
	ReleasedDate			smalldatetime		Null,
	Phone					char(13)			Not Null,
	Wage					smallmoney			Not Null
)

Create Table Club
(
	ClubCode				char(2)				Not Null 
												Constraint PK_Club_ClubCode
													Primary Key Clustered,
	ClubName				varchar(50)			Not Null
)

Create Table RideLog
(
	RideID					int					Not Null 
												Constraint FK_RideLog_RideID_To_Ride_RideID
													References Ride(RideID),
	LogNumber				int					Not Null,
	DateCompleted			smalldatetime		Not Null,
	Notes					varchar(150)		Not Null,
	TaskID					int					Not Null 
												Constraint FK_RideLog_TaskID_To_Task_TaskID
													References Task(TaskID),
	StaffID					int					Not Null 
												Constraint FK_RideLog_StaffID_To_Staff_StaffID
													References Staff(StaffID),
	Constraint PK_RideLog_RideID_LogNumber
		Primary Key Clustered (RideID, LogNumber)
)

Create Table ClubMembership 
(
	StaffID					int					Not Null 
												Constraint FK_ClubMembership_StaffID_To_Staff_StaffID
													References Staff(StaffID),
	ClubCode				char(2)				Not Null 
												Constraint FK_ClubMembership_ClubCode_To_Club_ClubCode
													References Club(ClubCode),
	DateJoined				smalldatetime		Not Null,
	Constraint PK_ClubMembership_StaffID_ClubCode
		Primary Key Clustered (StaffID, ClubCode)
) 

Create Table FunSeeker
(
	FunSeekerID				int	identity(1,1)	Not Null 
												Constraint PK_FunSeeker_FunSeekerID
													Primary Key Clustered,
	FirstName				varchar(30)			Not Null,
	LastName				varchar(30)			Not Null,
	Address					varchar(30)			Not Null,
	City					varchar(30)			Not Null,
	Province				char(2)				Not Null,
	PostalCode				char(6)				Not Null,
	Status					varchar(20)			Not Null
)

Create Table Membership
(
	MembershipID			int	identity(1,1)	Not Null 
												Constraint PK_Membership_MembershipID
													Primary Key Clustered,
	MembershipName			varchar(100)		Not Null,
	MembershipDescription	varchar(100)		Not Null,
	Cost					smallmoney			Not Null
)

Create Table Item
(
	ItemNumber				int	identity(1,1)	Not Null
												Constraint PK_Item_ItemNumber
													Primary Key Clustered,
	Description				varchar(50)			Not Null,
	Price					smallmoney			Not Null
)

Create Table Sale
(
	SaleNumber				int	identity(1,1)	Not Null 
												Constraint PK_Sale_SaleNumber
													Primary Key Clustered,
	SaleDate				datetime			Not Null
												Constraint DF_Sale_SaleDate
													Default GetDate(),
	SubTotal				smallmoney			Not Null,
	GST						smallmoney			Not Null,
	Total					smallmoney			Not Null,
	FunSeekerID				int					Not Null
												Constraint FK_Sale_SaleNumber_To_FunSeeker_FunSeekerID
													References FunSeeker(FunSeekerID)
)

Create Table FunSeekerMembership
(
	FunSeekerID				int					Not Null 
												Constraint FK_FunSeekerMembership_FunSeekerID_To_FunSeeker_FunSeekerID
													References FunSeeker(FunSeekerID),
	MembershipID			int					Not Null 
												Constraint FK_FunSeekerMembership_MembershipID_To_Membership_MembershipID
													References Membership(MembershipID),
	StartDate				smalldatetime		Not Null,
	EndDate					smalldatetime		Not Null,
	StaffID					int					Not Null 
												Constraint FK_FunSeekerMemership_StaffID_To_Staff_StaffID
													References Staff(StaffID)
	Constraint PK_FunSeekerMembership_FunseekerID_MembershipID_StartDate 
		Primary Key Clustered (FunseekerID, MembershipID, StartDate) 
)

Create Table SaleItem
(
	SaleNumber				int					Not Null
												Constraint FK_SaleItem_SaleNumber_To_Sale_SaleNumber
													References Sale(SaleNumber),
	ItemNumber				int					Not Null
												Constraint FK_SaleItem_ItemNumber_To_Item_ItemNumber
													References Item(ItemNumber),
	Quantity				smallint			Not Null,
	Price					smallmoney			Not Null,
	ExtendedPrice			smallmoney			Not Null,
	Constraint PK_SaleItem_SaleNumber_ItemNumber
		Primary Key Clustered (SaleNumber, ItemNumber)
)

Create Table WageHistory
(
	WageHistoryID			int identity(1,1)	Not Null
												Constraint PK_WageHistory_WageHistoryID
													Primary Key Clustered,
	StaffID					int					Not Null,
	ChangedDate				datetime			Not Null,
	OldWage					smallmoney			Not Null,
	NewWage					smallmoney			Not Null
)
go


-- Insert test data
Insert into Category
	(CategoryName, MinimumHeight)
Values 
	('Little Riders', 36), 
	('Teen', 48), 
	('Adult', 60)

Insert into Area
	(AreaID, AreaName, AreaDescription)
Values 
	('A100', 'Scary Zone', 'Rides sure to scare you!'), 
	('A200', 'Wild Zone', 'Wild rides!'), 
	('A300', 'Fun Zone', 'All about fun'), 
	('A400', 'Mystery Zone', 'All about the mystery')

Insert into Ride
	(RideName, RideDescription, CategoryCode, AreaID)
Values
	('Normalization Drop', 'Triple loop Coaster', 2, 'A100'), 
	('Table Turner', 'Triple loop Coaster', 1, 'A200'), 
	('Dizzy DML Dive', 'Triple loop Coaster', 1, 'A100'), 
	('Stored Procedure Mystery Cavern', 'Triple loop Coaster', 1, 'A200'), 
	('View or View not', 'Triple loop Coaster', 1, 'A100'), 
	('Group by Garage', 'Triple loop Coaster', 1, 'A200'), 
	('DataType Drop', 'Triple loop Coaster', 1, 'A300'), 
	('Alter Reality', 'Triple loop Coaster', 1, 'A200'), 
	('Select Your Own Adventure', 'Triple loop Coaster', 1, 'A300')

Insert into Task
	(TaskDescription)
Values
	('Seat cleaning'), 
	('Harness Inspection'), 
	('Painting'), 
	('Greasing'), 
	('Test Drive'), 
	('Rust Prevention'), 
	('Scheduled Parts Replacement'), 
	('Unscheduled Parts Replacement'), 
	('Seat Repair'), 
	('Harness Repair')

Insert into Staff
	(FirstName, LastName, HireDate, ReleasedDate, Phone, wage)
Values
	('Jason', 'Normalform', 'Jan 1 2020', null, '(780)555-1234', 25), 
	('Susie', 'Orderby', 'Aug 6 2021', null, '(780)555-2345', 23), 
	('Sally', 'Having', 'Jun 24 2021', null, '(780)555-3456', 22), 
	('Jerry', 'Joins', 'Sep 2 2020', null, '(780)555-4567', 28), 
	('Ali', 'Alias', 'Sep 18 2021', null, '(780)555-5678', 21), 
	('Victoria', 'Viewster', 'Jan 20 2018', null, '(780)555-6789', 22)

Insert into Club
	(ClubCode, ClubName)
Values
	('NF', 'Normal Form Club'), 
	('RA', 'Raise Error Debate Club'), 
	('VW', 'View the World Travel Club'), 
	('TH', 'Target Happy Nerf Club'), 
	('JW', 'Join the World Charity Club')

Insert into RideLog
	(RideID, LogNumber, DateCompleted, Notes, TaskID, StaffID)
Values
	(8, 1, 'Apr 15 2020', 'No notes', 1, 1), 
	(9, 1, 'Jun 2 2020', 'No notes', 2, 1), 
	(1, 1, 'Jun 1 2021', 'No notes', 4, 1), 
	(1, 2, 'Jun 18 2021', 'No notes', 3, 1), 
	(5, 1, 'Jul 3 2021', 'No notes', 3, 1), 
	(6, 1, 'Sep 5 2021', 'No notes', 5, 1), 
	(8, 2, 'Jun 1 2021', 'No notes', 4, 1), 
	(9, 2, 'Jan 1 2022', 'No notes', 4, 1)

Insert into ClubMembership
	(StaffID, ClubCode, DateJoined)
Values
	(1, 'NF', 'Jan 1 2022'), 
	(1, 'JW', 'Feb 1 2022'), 
	(2, 'NF', 'Jan 1 2022'), 
	(3, 'NF', 'Jan 1 2022'), 
	(3, 'TH', 'Mar 1 2022'), 
	(4, 'NF', 'Jan 1 2022'), 
	(3, 'JW', 'Feb 1 2022')


Insert into FunSeeker
	(Firstname, LastName, Address, City, Province, PostalCode, status)
Values
	('Sheldon', 'Cooper', '101 Geek Street', 'Edmonton', 'AB', 'T8E1J3', 'Beginner'), 
	('Leonard', 'Hofstader', '101 Geek Street', 'Edmonton', 'AB', 'T8E1J3', 'Beginner'), 
	('Howard', 'Wolowitz', '23 Astronaut Avenue', 'Edmonton', 'AB', 'T8A1J4', 'Beginner'), 
	('Stuart', 'Bloom', '101 Geek Street', 'Edmonton', 'AB', 'T8E1H3', 'Beginner'), 
	('Barry', 'Kripke', '101 Geek Street', 'Edmonton', 'AB', 'T8E1J3', 'Beginner'), 
	('Amy', 'Farah Fowler', '101 Geek Street', 'Edmonton', 'AB', 'T8f1J3', 'Beginner'), 
	('Raj', 'Koothrappali', '101 Geek Street', 'Edmonton', 'AB', 'T8A1J3', 'Beginner'), 
	('Leslie', 'Winkle', '101 Geek Street', 'Edmonton', 'AB', 'T8L1J3', 'Beginner'), 
	('Bert', 'Kibbler', '101 Geek Street', 'Edmonton', 'AB', 'T8L1J3', 'Beginner')

Insert into Membership
	( MembershipName, MembershipDescription, Cost)
Values
	('Monthly Ride A Lot', 'One month ride all day pass', 50), 
	('Yearly Ride A Lot', 'One year ride all day pass', 500), 
	('Senior Monthly Ride A Lot', 'One month day senior ride all day pass', 20), 
	('Student Monthly Ride A Lot', 'One month day student ride all day pass', 25)

Insert into FunSeekerMembership
	(FunSeekerID, MembershipID, StartDate, EndDate, StaffID)
Values
	(1, 1, 'Dec 1 2011', 'Dec 31 2011', 1), 
	(1, 1, 'Jan 1 2021', 'Jan 31 2021', 1), 
	(2, 1, 'Jan 1 2022', 'Jan 31 2022', 2), 
	(4, 1, 'Jan 1 2022', 'Jan 31 2022', 2),
	(5, 2, 'Feb 1 2022', 'Feb 28 2022', 3),
	(6, 1, 'Mar 1 2022', 'Mar 31 2022', 4),
	(1, 1, 'Feb 1 2022', 'Feb 28 2022', 5), 
	(3, 1, 'Jan 1 2022', 'Jan 31 2022', 6),
	(6, 1, 'Sep 1 2020', 'Oct 1 2020', 5), 
	(6, 2, 'Jun 1 2021', 'Jun 1 2022', 5), 
	(6, 1, 'Nov 1 2020', 'Dec 1 2020', 5), 
	(6, 1, 'Dec 1 2020', 'Jan 1 2021', 5), 
	(2, 1, 'Aug 1 2021', 'Sep 1 2021', 5)

Insert into Item
	(Description, Price)
Values
	('T-Shirt', 10), 
	('Hat', 5), 
	('PostCard', 2), 
	('Coffee Mug', 4), 
	('Coloring Book', 2), 
	('Stuffed Mascot', 15), 
	('Collectable Coin', 10)

Insert into Sale
	(SubTotal, GST, Total, FunSeekerID)
Values
	(24, 1.2, 25.20, 1)
Insert into SaleItem
	(SaleNumber, ItemNumber, Quantity, Price, ExtendedPrice)
Values
	(1, 1, 2, 10, 20)
Insert into SaleItem
	(SaleNumber, ItemNumber, Quantity, Price, ExtendedPrice)
Values
	(1, 4, 1, 4, 4)

Insert into Sale
	(SubTotal, GST, Total, FunSeekerID)
Values
	(14, .7, 14.7, 2)
Insert into SaleItem
	(SaleNumber, ItemNumber, Quantity, Price, ExtendedPrice)
Values
	(2, 3, 2, 2, 4)
Insert into SaleItem
	(SaleNumber, ItemNumber, Quantity, Price, ExtendedPrice)
Values
	(2, 7, 1, 10, 10)

Insert into Sale
	(SubTotal, GST, Total, FunSeekerID)
Values
	(19, .95, 19.95, 3)
Insert into SaleItem
	(SaleNumber, ItemNumber, Quantity, Price, ExtendedPrice)
Values
	(3, 5, 2, 2, 4)
Insert into SaleItem
	(SaleNumber, ItemNumber, Quantity, Price, ExtendedPrice)
Values
	(3, 6, 1, 15, 15)

Insert into Sale
	(SubTotal, GST, Total, FunSeekerID)
Values
	(24, 1.2, 25.20, 1)
Insert into SaleItem
	(SaleNumber, ItemNumber, Quantity, Price, ExtendedPrice)
Values
	(4, 1, 2, 10, 20)
Insert into SaleItem
	(SaleNumber, ItemNumber, Quantity, Price, ExtendedPrice)
Values
	(4, 4, 1, 4, 4)

Insert into Sale
	(SubTotal, GST, Total, FunSeekerID)
Values
	(24, 1.2, 25.20, 1)
Insert into SaleItem
	(SaleNumber, ItemNumber, Quantity, Price, ExtendedPrice)
Values
	(5, 1, 2, 10, 20)
Insert into SaleItem
	(SaleNumber, ItemNumber, Quantity, Price, ExtendedPrice)
Values
	(5, 4, 1, 4, 4)

Insert into Sale
	(SubTotal, GST, Total, FunSeekerID)
Values
	(24, 1.2, 25.20, 1)
Insert into SaleItem
	(SaleNumber, ItemNumber, Quantity, Price, ExtendedPrice)
Values
	(6, 1, 2, 10, 20)
Insert into SaleItem
	(SaleNumber, ItemNumber, Quantity, Price, ExtendedPrice)
Values
	(6, 4, 1, 4, 4)
Go

-- Create Triggers Here
--	ques 1
Drop Trigger TR1
Drop Trigger TR2
Drop Trigger TR3
Drop Trigger TR4
Go


--ques1
CREATE TRIGGER TR1 
ON SaleItem
FOR Insert
AS
Declare @SubTotal smallmoney
Declare @GST smallmoney
Declare @Total smallmoney

If @@ROWCOUNT>0
Begin
	Select @SubTotal=(Select (SubTotal+inserted.ExtendedPrice)  from Sale inner join inserted on Sale.SaleNumber=inserted.SaleNumber where Sale.SaleNumber=inserted.SaleNumber)
	Select @GST= @SubTotal*0.05
	Select @Total=@SubTotal+@GST

	Update Sale
	Set SubTotal=@SubTotal,
	GST=@GST,
	Total=@Total
	where SaleNumber=(Select inserted.SaleNumber from inserted inner join SaleItem on SaleItem.SaleNumber=inserted.SaleNumber where SaleItem.SaleNumber=inserted.SaleNumber and SaleItem.ItemNumber=inserted.ItemNumber)

	If @@ERROR<>0
			BEGIN
			RaisError('Insert into Sale failed',16,1)
			END
End
Return
Go

--ques2
Create Trigger TR2 
on Staff
for Update
As
Declare @StaffID int
Declare @OldWage smallmoney
Declare @NewWage smallmoney

 If Update(Wage) and @@ROWCOUNT>0
 Begin
	If exists(Select deleted.Wage from deleted inner join inserted on deleted.StaffID=inserted.StaffID where inserted.Wage!=deleted.Wage)
	Begin
	Select @StaffID=(Select deleted.StaffID from inserted inner join deleted on deleted.StaffID=inserted.StaffID where deleted.StaffID=inserted.StaffID)
	Select @OldWage=(Select deleted.Wage from inserted inner join deleted on deleted.StaffID=inserted.StaffID where deleted.StaffID=inserted.StaffID)
	Select @NewWage=(Select inserted.Wage from inserted inner join deleted on deleted.StaffID=inserted.StaffID where deleted.StaffID=inserted.StaffID)

	Insert into WageHistory
	(StaffID,ChangedDate,OldWage,NewWage)
	Values
	(@StaffID,GetDate(),@OldWage,@NewWage)

	If @@ERROR<>0
			BEGIN
			RaisError('Insert into WageHistory failed',16,1)
			END
	End
 End
 Return
GO

--ques3
Create Trigger TR3
ON ClubMemberShip
For Insert
As
Declare @ReleasedDate smalldatetime

 If @@ROWCOUNT>0
 Begin
	Select @ReleasedDate=(Select ReleasedDate from Staff inner join inserted on Staff.StaffID=inserted.StaffID where  Staff.StaffID=inserted.StaffID)
		   

	If @ReleasedDate is not NULL
	begin
		RaisError('Cannot join Club because the Staff no loger works here',16,1)
		Rollback transaction
	End
 End
 Return
Go

--	ques4
Create Trigger TR4
on Area
for Insert, Update
As
If @@ROWCOUNT>0
 Begin
     If Update (AreaID) or Update (AreaName)
	 Begin
	 RaisError('AreaID OR AreaName can no loger be altered or added.',16,1)
	 RollBack Transaction
	 End

End
Return
Go
