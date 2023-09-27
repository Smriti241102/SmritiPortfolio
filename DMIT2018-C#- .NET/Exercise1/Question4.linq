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

//From the shifts scheduled for NAIT's placement contracts, show the number of employees needed for
//each day (ordered by day-of-week). Display the name of the day of week
//(Sunday, as the first day of the week, is number zero) and the number of employees needed.

Shifts
	.GroupBy(x => x.DayOfWeek) 
	.Select(s => new
	{
	DayOfWeek = s.First().DayOfWeek == 0? "Sun":
				s.Key == 1? "Mon":
				s.Key == 2? "Tue":
				s.Key == 3? "Wed":
				s.Key == 4? "Thu":
				s.Key == 5? "Fri":
				s.Key == 6? "Sat": "invalid",
	NumberOfEmployees = s.Sum(e => e.NumberOfEmployees)
	
	})