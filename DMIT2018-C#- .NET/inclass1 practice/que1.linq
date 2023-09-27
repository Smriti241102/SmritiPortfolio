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

Guardians
	.Where(x =>x.Players.Count() >1)
	.OrderByDescending(x => x.Players.Count())
	.Select(x => new
	{
		Name = x.FirstName + " " +x.LastName,
		Children = x.Players
		.OrderBy(x => x.Age)
					.Select(
					x=> new{
						Name= x.FirstName + " " +x.LastName,
						Age = x.Age,
						Gender = x.Gender,
						Team = x.Team.TeamName
					})
		
	})