Use DMIT1508
Go
Drop Table MickeyMouseGrade
Drop Table MickeyMouseCourse
Drop Table MickeyMouseStudent
Go
Create Table MickeyMouseStudent
(
	StudentID		decimal (8,0)	Not Null
									Constraint PK_MickeyMouseStudent_StudentID 
										Primary Key Clustered,
	FirstName		varchar (30)	Null,
	LastName		varchar (40)	Null,
	Address			varchar (50)	Null,
	Phone			varchar (10)	Null,
	EnrollmentDate	datetime		Null,
	Gender			char (1)		Not Null
);
Create Table MickeyMouseCourse
(
	CourseID		char (8)		Not Null,
	CourseName		varchar (50)	Null,
	CourseHours		decimal (3,0)	Null,
	CourseCost		decimal (6,2)	Null,
	Constraint PK_MickeyMouseCourse_CourseID Primary Key Clustered
		(CourseID)
);

Create Table MickeyMouseGrade							
(
	StudentID		decimal (8,0)	Not Null
		Constraint FK_MickeyMouseGrade_StudentID_To_MickeyMouseStudent_StudentID
			References MickeyMouseStudent (StudentID),
	CourseID		char (8)		Not Null,
	Mark			decimal (5,2)	Null,
	Constraint PK_MickeyMouseGrade_StudentID_CourseID Primary Key Clustered
		(StudentID, CourseID),
	Constraint FK_MickeyMouseGrade_CourseID_To_MickeyMouseCourse_CourseID
		Foreign Key (CourseID) References MickeyMouseCourse (CourseID)
);
Go