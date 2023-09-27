<Query Kind="Program">
  <Connection>
    <ID>41abf0ff-b88b-4cbf-93fc-875894f47fa0</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>WA320-22\SQLEXPRESS</Server>
    <Database>FSIS_2018</Database>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
      <TrustServerCertificate>True</TrustServerCertificate>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	try
	{
		//YOUR CODE HERE
		
		//Driver
		//display teams method
		DisplayTeams();
		
		//create instance of GameStat with test data
		GameStat gameinstance;
		
		DateTime DT = new DateTime(2019,05,09,9,15,0);
		
		gameinstance = new GameStat{
		GameDate = DT,
		HomeTeamId = 3,
		HomeTeamScore = 6,
		VisitingTeamId = 4,
		VisitingTeamScore = 4
		};
		
		//call Game_RecordGame(xxxxx)
		
		Game_RecordGame(gameinstance);
		
		//display teams method
		DisplayTeams();
		
		//display games method
		DisplayGames();
		
	}
	catch (ArgumentNullException ex)
	{
		GetInnerException(ex).Message.Dump();
	}
	catch (ArgumentException ex)
	{

		GetInnerException(ex).Message.Dump();
	}
	catch (AggregateException ex)
	{
		//having collected a number of errors
		//	each error should be dumped to a separate line
		foreach (var error in ex.InnerExceptions)
		{
			error.Message.Dump();
		}
	}
	catch (Exception ex)
	{
		GetInnerException(ex).Message.Dump();
	}
}

// You can define other methods, fields, classes and namespaces here

private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}

public class GameStat
{
	public DateTime GameDate {get;set;}
	public int HomeTeamId {get;set;}
	public int HomeTeamScore {get;set;}
	public int VisitingTeamId {get;set;}
	public int VisitingTeamScore {get;set;}
}

public void Game_RecordGame(GameStat item)
{
	

	//we need a container to hold x number of Exception messages
	List<Exception> errorlist = new List<Exception>();
	
	//YOUR CODE HERE
	
	if (item.HomeTeamId == null || item.HomeTeamId == 0)
	{
		throw new ArgumentNullException("No Home Team Supplied!");
	}
	
	if (item.VisitingTeamId == null || item.VisitingTeamId ==0)
	{
		throw new ArgumentNullException("No Visiting Team Supplied!");
	}
	
	if (item.GameDate == null)
	{
		throw new ArgumentNullException("No Game Date Team Supplied!");
	}
	
	//validate entries
	//teamIDSmust be different
	if (item.HomeTeamId == item.VisitingTeamId){
		errorlist.Add(new Exception("Home Team and Visiting Teams must be different!"));
	}
	
	//valid ids
	bool HomeTeamExists = false;
	bool VisitingTeamExists = false;
	
	HomeTeamExists = Teams
			.Where(x => x.TeamID == item.HomeTeamId)
			.Any();
			
	VisitingTeamExists = Teams
			.Where(x => x.TeamID == item.HomeTeamId)
			.Any();
			
	
	if (!HomeTeamExists)
	{
		errorlist.Add(new Exception("Invalid Home Team! Supplied Home Team does not exist on the database."));
	}
	
	if (!VisitingTeamExists)
	{
		errorlist.Add(new Exception("Invalid Visiting Team! Supplied Visiting Team does not exist on the database."));
	}
	
	if (item.HomeTeamScore == null)
	{
		errorlist.Add(new Exception("No score tied to the HomeTeam!"));
	}
	
	if (item.VisitingTeamScore == null)
	{
		errorlist.Add(new Exception("No Score Tied to the Visiting Team!"));
	}
	
	//Game date cannot be a future date
	if (item.GameDate > DateTime.Now)
	{
		errorlist.Add(new Exception("Game date cannot be a future date!"));
	}
	
	//(previous record exists
	bool gameexists = false;
	
	gameexists = Games
				.Where(x => x.HomeTeamID == item.HomeTeamId
							&& x.VisitingTeamID == item.VisitingTeamId
							&& x.GameDate == item.GameDate)
				.Any();		
	
	if (gameexists)
	{
		errorlist.Add(new Exception("The game has been previously recorded in the database!"));
	}
	
	//proceed if no exceptions
	
	
	Games newgame = new Games()
	{
		GameDate = item.GameDate,
		HomeTeamID = item.HomeTeamId,
		VisitingTeamID = item.VisitingTeamId,
		HomeTeamScore = item.HomeTeamScore,
		VisitingTeamScore = item.VisitingTeamScore
	};
	
	Games.Add(newgame);
	
	int winteamid;
	int loseteamid;
	
	if (item.HomeTeamScore > item.VisitingTeamScore)
	{
		winteamid = item.HomeTeamId;
		loseteamid = item.VisitingTeamId;	
	}
	else{
		winteamid = item.VisitingTeamId;
		loseteamid = item.HomeTeamId;
	}
	
			Teams winteamexisting = null;
		Teams loseteamexisting = null;
		
		winteamexisting = Teams
					.Where(x => x.TeamID == winteamid)
					.FirstOrDefault();
		winteamexisting.Wins = winteamexisting.Wins +1;
		Teams.Update(winteamexisting);
		
		loseteamexisting = Teams
					.Where(x => x.TeamID == loseteamid)
					.FirstOrDefault();
		loseteamexisting.Losses = loseteamexisting.Losses + 1;
		Teams.Update(loseteamexisting);
		
		//newgame.Dump();
		//winteamexisting.Dump();
		//loseteamexisting.Dump();
		
		
		//SaveChanges();
		if (errorlist.Count > 0)
		{
		//  throw the list of business processing error(s)
		throw new AggregateException("unable to add new game.  Check concerns", errorlist);
		}
		else
		{
		//  consider data valid
		//  has passed business processing rules
		SaveChanges();
		}
		

}



public void DisplayTeams()
{
	//YOUR CODE HERE
		Teams
	.Select(x => new
	{
		TeamId = x.TeamID,
		Name = x.TeamName,
		Wins = x.Wins,
		Losses = x.Losses
	}).Dump();
}

public void DisplayGames()
{
	//YOUR CODE HERE
		 Games
							.Select(x => new {
								ID = x.GameID,
								Date = x.GameDate,
								HomeTeamID = x.HomeTeamID,
								HomeName = x.Home.TeamName,
								HomeScore = x.HomeTeamScore,
								VisitingTeamID = x.Visiting.TeamID,
								VisitingName = x.Visiting.TeamName,
								VisitingScore = x.VisitingTeamScore
							})
							.OrderByDescending(x => x.Date)
							.Dump();
}