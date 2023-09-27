<Query Kind="Expression">
  <Connection>
    <ID>6161a2ae-7fd2-411b-b336-fb8c9830183d</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>SUSHIL-MAIN\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>FSIS_2018</Database>
  </Connection>
</Query>

Teams
	
	.Select(x => new{
		Team = x.TeamName,
		Coach = x.Coach,
		Players =x.Players
				.OrderBy(x => x.LastName)
				.ThenBy(x => x.FirstName)
				.Select( x => new{
								LastName = x.LastName,
								FirstName = x.FirstName,
								Gender = x.Gender,
								Age = x.Age
		})
	}).OrderBy(x => x.Team)