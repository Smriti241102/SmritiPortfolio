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

//List all employees with multiple skills; ignore employees with only one skill. 
//Show the name of the employee and the list of their skillsets; for each skill, show the name of the skill, the level of competance and
//the years of experience. Use the following text for the levels: 1 = Novice, 2 = Proficient, 3 = Expert.

Employees
	.Where(x => x.EmployeeSkills.Count() >1)
	.Select (x=> new
	{
			Name= x.FirstName + " " + x.LastName,
			Skills= EmployeeSkills
					.Where(e => e.EmployeeID == x.EmployeeID)
					.Select(e => 
							new 
							{
							Description= e.Skill.Description,
							Level= e.Level == 1? "Novice":
							   e.Level == 2? "Proficient":
							   e.Level == 3? "Expert": null,
							YearsOfExperience= e.YearsOfExperience
							})
	}
	)

	