Use Lab2A
Go

Drop Table ConsignmentDetails
Drop Table StaffTraining
Drop Table Consignment
Drop Table Category
Drop Table Training
Drop Table Staff
Drop Table Customer
Drop Table StaffType
Drop Table Reward
Drop Table CustomerType
Go

Create Table CustomerType
(
	CustomerTypeID			char(1)			Not Null
													Constraint PK_CustomerType_CustomerTypeID
													Primary Key Clustered,
	CustomerTypeDescription	varchar(30)		Not Null
)
Go

Create Table Reward
(
	RewardID				char(4)			Not Null
													Constraint PK_Reward_RewardID
													Primary Key Clustered,
	RewardDescription		varchar(30)		Null,
	DiscountPercentage		TinyInt			Not Null
)
Go


Create Table Customer
(
	CustomerID				int Identity(1,1)	Not Null
													Constraint PK_Customer_CustomerID
													Primary Key Clustered,
	FirstName				Varchar(30)		Not Null,
	LastName				Varchar(30)		Not Null,
	StreetAddress			Varchar(30)		Null,
	City					Varchar(30)		Null,
	Province				Char(2)			Null
													Constraint CK_Province Check(Province like '[A-Z][A-Z]'),
	PostalCode				Char(6)			Null
													Constraint CK_PostalCode Check(PostalCode like '[A-Z][0-9][A-Z][0-9][A-Z][0-9]'),
	Phone					Char(14)		Null
													Constraint CK_Phone Check(Phone like'[0-9]-[0-9][0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]'),
	CustomerTypeID			Char(1)			Not Null
													Constraint FK_Customer_CustomerTypeID_To_CustomerType_CustomerTypeID
													References CustomerType (CustomerTypeID),
	RewardID				Char(4)			Not Null
													Constraint FK_Customer_RewardID_To_Reward_RewardID
													References Reward (RewardID)
)
Go

 Create Table StaffType
 (
	StaffTypeID				SmallInt Identity(1,1)	Not Null
													Constraint PK_StaffType_StaffTypeID
													Primary Key Clustered,
	StaffTypeDescription	Varchar(30)		Not Null
)
Go

Create Table Staff
(
	StaffId					char(6)			Not Null
													Constraint PK_Staff_StaffID
													Primary Key Clustered,
	FirstName				Varchar(30)		Not Null,
	LastName				Varchar(30)		Not Null,
	Active					Char(1)			Not Null,
	Wage					SmallMoney		Not Null,
	StaffTypeId				SmallInt		Not Null
													Constraint FK_Staff_StaffTypeId_To_StaffType_StaffTypeId
													References StaffType (StaffTypeId)
)
Go

Create Table Training
(
	TrainingID				Int Identity(1,1)		Not Null
													Constraint PK_Training_TrainingID
													Primary Key Clustered,
	StartDate				SmallDateTime	Not null,
	EndDate			    	SmallDateTime	Not null,
	TrainingDescription		Varchar(70)		Not Null,
													Constraint CK_EndDate Check(EndDate>= StartDate)
)
Go


Create Table Category
(
	CategoryCode			Char(3)			Not Null
													Constraint PK_Category_CategoryCode
													Primary Key Clustered,
	CategoryDescription		Varchar(50)		Not Null,
	Cost					SmallMoney		Not Null
													Constraint CK_Cost Check(Cost>=0)
)
Go

Create Table StaffTraining
(
	StaffID					Char(6)			Not Null
													Constraint FK_StaffTraining_StaffID_To_Staff_StaffID
													References Staff (StaffID),
	TrainingID				Int				Not Null,
	PassOrFail				Char(1)			Null,

	Constraint PK_StaffTraining_StaffTraining_TrainingID Primary Key Clustered
	(StaffID, TrainingID)
)
Go

Create Table Consignment
(
	ConsignmentID			Int Identity(1,1)	Not Null
													Constraint PK_Consignment_ConsignmentID
													Primary Key Clustered,
	Date					DateTime		Not Null,
	SubTotal				SmallMoney		Not Null,
	GST						SmallMoney		Not Null,
	Total					SmallMoney		Not Null,
	RewardsDiscount			Decimal(9,2)	Not Null,
	CustomerID				Int				Not Null
													Constraint FK_Consignment_CustomerID_To_Customer_CustomerID
													References Customer (CustomerID),
	StaffID					Char(6)			Not Null
													Constraint FK_Consignment_StaffID_To_Staff_StaffID
													References Staff (StaffID)
)
Go



Create Table ConsignmentDetails
(
	ConsignmentID			Int				Not Null,
	LineID					Int				Not Null,
	ItemDescription			Varchar(40)		Not Null,
	StartPrice				SmallMoney		Not Null,
	LowestPrice				SmallMoney		Not Null,
	CategoryCode			Char(3)			Not Null
													Constraint FK_ConsignmentDetails_CategoryCode_To_Category_CategoryCode
													References Category (CategoryCode),
	Constraint PK_ConsignmentDetails_ConsignmentID_LineID Primary Key Clustered
	(ConsignmentID, LineID)
)
Go

Alter Table Customer
Add
	email					Varchar(30)		Null
													Constraint CK_email check(email like '%___@___%.__%');
Go


Alter Table Staff
Add
	Active_YN				char(1)			Null default 'Y';
Go

Create nonclustered index ix_CustomerTypeID
on Customer(CustomerTypeID)
Go

Create nonclustered index ix_RewardID
on Customer(RewardID)
Go

Create nonclustered index ix_StaffTypeID
on Staff(StaffTypeID)
Go

Create nonclustered index ix_CutomerID
on Consignment(CustomerID)
Go

Create nonclustered index ix_StaffID
on Consignment(StaffID)
Go

Create nonclustered index ix_StaffID
on StaffTraining(StaffID)
Go

Create nonclustered index ix_TrainingID
on StaffTraining(TrainingID)
Go

Create nonclustered index ix_ConsignmentID
on ConsignmentDetails(ConsignmentID)
Go

Create nonclustered index ix_CategoryCode
on ConsignmentDetails(CategoryCode)
Go
