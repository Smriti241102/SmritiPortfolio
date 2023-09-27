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

//Question6 : For the month of March, list the total earnings per employee along with the 
//number of shifts, the regular earnings, and overtime earnings.

Schedules.ToList()
	.Where(x => x.Day.Month == 3)
	.GroupBy(x =>x.EmployeeID)
	
	.Select(x => new
	{
		
		Name = x.First().Employee.FirstName + " " + x.First().Employee.LastName,
		RegularEarning = x
						.Where(x =>x.OverTime == false)
						.Select (x => x.HourlyWage*(x.Shift.EndTime.Hour - x.Shift.StartTime.Hour)).Sum(),
						
		OverTimeEarnings = x
						.Where(x =>x.OverTime == true)
						.Select (x => x.HourlyWage*(x.Shift.EndTime.Hour - x.Shift.StartTime.Hour)*(decimal)1.5).Sum(),
		
		NumberOfShifts =x.Count()			
	})

