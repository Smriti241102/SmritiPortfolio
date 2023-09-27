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
	.Where(x => x.Wins == Teams
							.Select(x => x.Wins).Max())
	.Select( x => new{
	TeamName = x.TeamName,
	Wins = x.Wins
	})