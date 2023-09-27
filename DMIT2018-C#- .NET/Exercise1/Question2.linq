<Query Kind="Expression">
  <Connection>
    <ID>9c4626e6-0b49-4acb-a50f-1f3d45150491</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>SUSHIL-MAIN\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>WorkSchedule</Database>
  </Connection>
</Query>


	
//Show all skills requiring a ticket
//and which employees have those skills. Include all the data 
//as seen in the following image. Order the employees by years of experience 
//(highest to lowest). Use the following text for the levels: 1 = Novice, 2 = Proficient, 3 = Expert. 
//(Hint: Use nested ternary operators to handle the levels as text.)

Skills
	.Where(x => x.RequiresTicket == true)
	.Select(x=> new
	{
		Description = x.Description,
		Employees =	EmployeeSkills
					.Where(e => e.SkillID == x.SkillID)
					.Select(s => 
					new 
					{
						EmployeeID = s.Employee.FirstName + " " +s.Employee.LastName,
						Level= s.Level == 1? "Novice":
							   s.Level == 2? "Proficient":
							   s.Level == 3? "Expert": null,
						YearsOfExperience = s.YearsOfExperience
					}).OrderByDescending(x => x.YearsOfExperience)
	})
	

