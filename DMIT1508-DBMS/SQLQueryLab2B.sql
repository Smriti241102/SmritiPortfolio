use lab2B
Go

--QUESTION 1
--Part a
Select FirstName+ ' ' +LastName'Customer Name',City
From Customer
where CustomerID='2'
GO

--Part b
Select FirstName, LastName, count(Consignment.StaffID)'No. of Consignments'
From Staff
Left OUTER JOIN Consignment on Staff.StaffID=Consignment.StaffID
Group By Staff.FirstName,LastName
Go

--Part c
select MIN(Cost)'Minimum Ctegory Cost'
from Category
Go

--Part d
Select FirstName, LastName
From Customer
Left Outer join Consignment on Consignment.CustomerID=Customer.CustomerID
Group By FirstName, LastName
Having SUM(Subtotal) > 100
Go

--Part e
Select Staff.FirstName +' '+Staff.LastName'Staff Name',Customer.FirstName+' '+Customer.LastName'Customer Name'
From Staff
Left Outer join Consignment on Staff.StaffID=Consignment.StaffID
Left Outer Join Customer on Consignment.CustomerID=Customer.CustomerID
Group By Staff.FirstName,Staff.LastName,Customer.FirstName,Customer.LastName
Go

--Part f
Select FirstName, LastName
From Staff
Where LastName like 'P%' OR LastName like 'B%' OR LastName like 'J%'
go

--Part g
Select DateName(mm,Date),SUM(Subtotal)
From Consignment
where Year(Date)=Year(GETDATE())-1
group By DateName(mm,Date),DatePart(mm,Date)
Order By DatePart(mm,Date)
GO

--Part h
Select StaffTypeDescription
From StaffType
Left Outer Join Staff on StaffType.StaffTypeID=Staff.StaffTypeID
where FirstName is Null
Go

--Part i
Select CategoryDescription
From Category 
Inner Join ConsignmentDetails ON Category.CategoryCode=ConsignmentDetails.CategoryCode
group by CategoryDescription,Category.CategoryCode
Having Count(*)>=All(select count(*) from ConsignmentDetails
							 Group By CategoryCode)
Go


--Part j
Select FirstName+' '+LastName'Full Name'
From Staff
Where len(LastName) between 4 and 7
Union
Select FirstName+' '+LastName
From Customer
Where len(LastName) between 4 and 7
GO

--Question2
--Part a
Create view CustomerSummary
As
Select Customer.CustomerID,FirstName, LastName, ItemDescription
From Customer
INNER JOIN Consignment on Customer.CustomerID=Consignment.CustomerID
Inner JOIN ConsignmentDetails on  Consignment.ConsignmentID=ConsignmentDetails.ConsignmentID
Go

--Part b
select CustomerID, FirstName+' '+ LastName 'Full Name', Count(*)'No. of Items'
From CustomerSummary
Group By CustomerID,FirstName,LastName
Go

--Question3
--Part a
Insert into Staff (StaffID, FirstName, LastName, Active, Wage, StaffTypeID)
Values (231464, 'Tim', 'McGraw', 'N', 27.00, 2),
(456585, 'Otis', 'Redding', 'Y',(select AVG(Wage) from Staff),3) 

--Part b
Update Reward
Set DiscountPercentage=DiscountPercentage+4
Where RewardDescription like '%Customer%'
Go

--Part c
Update Staff
Set Wage=Wage*1.12
Where StaffTypeID=(select StaffTypeID from StaffType where StaffTypeDescription like 'Human Resources')
Go

--Part d
Delete StaffType
Where StaffTypeID not in (select StaffTypeID from Staff)
Go

