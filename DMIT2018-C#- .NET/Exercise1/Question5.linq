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

//List all the employees with the most years of experience.

EmployeeSkills
	.GroupBy(x =>x.EmployeeID)
	.Where(x => x.Sum(e =>e.YearsOfExperience) == EmployeeSkills
					   							.GroupBy(x => x.EmployeeID)
												.Select( s => s.Sum(e =>e.YearsOfExperience)).Max())
	.Select(s =>  new
	{
	
		Name = s.First().Employee.FirstName + " " + s.First().Employee.LastName,	
		YOE = s.Sum(e => e.YearsOfExperience)
	})