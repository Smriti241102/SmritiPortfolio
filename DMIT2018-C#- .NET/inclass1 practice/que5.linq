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

Players
.Where(x =>x.PlayerStats.Count() >0)
.Select(x => new{
	name= x.FirstName + " " +x.LastName,
	teamname= x.Team.TeamName,
	goals = x.PlayerStats.Sum(x =>x.Goals),
	assists = x.PlayerStats.Sum(x =>x.Assists),
	redcards = x.PlayerStats
				.Where(c =>c.RedCard == true)
				.Select(c => c).Count(),
	yellowcards = x.PlayerStats
				.Where(c =>c.YellowCard == true)
				.Select(c => c).Count()
}).OrderBy(x => x.name)