Use Lab3
Go

--Question 1
Drop Procedure AddStaffType 
Drop Procedure UpdateReward
Drop Procedure DeleteConsignmentDetail
Drop Procedure LookUpCustomerHistory
Drop Procedure UnusedCategories
Drop Procedure LookUpStaffType
Drop Procedure AddConsignmentItem
Go

Create Procedure AddStaffType 
	(@StaffTypeDescription	varchar(30)= null)
As
Begin
	If @StaffTypeDescription is null
		Begin
			RaisError ('YOU need to pass a description to add a new StaffType',16,1)
		End
	Else
		Begin
			If not exists (Select StaffTypeDescription from StaffType Where StaffTypeDescription=@StaffTypeDescription)
			Begin
				Insert into StaffType
					(StaffTypeDescription)
				Values
					(@StaffTypeDescription)
			End

			Else
				RaisError ('This StaffType already exists',16,1)
		End

End
Return
Go


--Question 2
Create Procedure UpdateReward
	(@RewardID 				char(4),
	@RewardDescription		varchar(30)=null,
	@DiscountPercentage		tinyint)
As
Begin
	If Exists (Select RewardID From Reward Where RewardID=@RewardID)
		Begin
			Update Reward 
			Set 
			RewardDescription=@RewardDescription,
			DiscountPercentage=@DiscountPercentage
			Where 
			RewardID=@RewardID
		End
End
Return
Go

--Question 3
Create Procedure DeleteConsignmentDetail
	(@ConsignmentID			int,
	@LineID					int)
As
Begin
	If exists (Select ConsignmentID, LineID from ConsignmentDetails where ConsignmentID=@ConsignmentID and LineID=@LineID)
		Begin
		Delete ConsignmentDetails
		Where ConsignmentID=@ConsignmentID and LineID=@LineID
		End
	
End
Return
Go


--Question 4
Create Procedure LookUpCustomerHistory 
	(@CustomerID				int)
AS
Begin
	Select FirstName +' '+ LastName 'Full Name', Date, Total, ItemDescription 
	From Customer
	Left Outer Join Consignment on Customer.CustomerID=Consignment.CustomerID
	Left Outer Join ConsignmentDetails on Consignment.ConsignmentID=ConsignmentDetails.ConsignmentID
	Where Customer.CustomerID=@CustomerID

End
Return
Go

--Question 5
Create Procedure UnusedCategories 
As
Begin
	Select CategoryCode, CategoryDescription
	From Category
	Where CategoryCode not in 
		(Select CategoryCode from ConsignmentDetails)
End
Return
Go

--Question 6
Create Procedure LookUpStaffType
	(@StaffTypeDescription	varchar(30))
As
Begin
	Select StaffTypeID, StaffTypeDescription
	From StaffType
	Where StaffTypeDescription like '%'+@StaffTypeDescription+'%'
End
Return
Go


--Question 7
Create Procedure AddConsignmentItem 
	(@ConsignmentID			int,
	@ItemDescription			varchar(40),
	@StartPrice				smallmoney,
	@LowestPrice				smallmoney,
	@CategoryCode			char(3))
As
Begin
	Declare @LineID int
	Select @LineID= 1+(select Top(1) LineID From ConsignmentDetails where ConsignmentID=@ConsignmentID order by LineID Desc)

	Declare @Cost smallmoney
	Select @Cost=Cost from Category where CategoryCode=@CategoryCode

	Declare @Subtotal smallmoney
	Select @Subtotal= @Cost+(Select Subtotal From Consignment where ConsignmentID=@ConsignmentID)

	Declare @GST smallmoney
	Select @GST=0.05*@Subtotal

	Update Consignment
	Set Subtotal=@Subtotal,
	GST=@GST,
	Total=@Subtotal+@GST
	where ConsignmentID=@ConsignmentID

	Insert into ConsignmentDetails
	(ConsignmentID,lineID,ItemDescription, StartPrice, LowestPrice, CategoryCode)
	Values
	(@ConsignmentID, @LineID, @ItemDescription, @StartPrice, @LowestPrice, @CategoryCode)
		
End
Return
Go
