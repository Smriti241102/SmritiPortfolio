<Query Kind="Program">
  <Connection>
    <ID>0519d289-75d3-41bb-987f-b75c152d7ea9</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>SUSHIL-MAIN\SQLEXPRESS</Server>
    <Database>FSIS_2018</Database>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
  <RuntimeVersion>6.0</RuntimeVersion>
</Query>

void Main()
{
	try
	{
		
		//Driver
		//display team players with stats BEFORE
		DisplayTeamPlayers(23);
		
		//FOR TESTING: uncomment test to run, comment test not to run
		
		//call service method bad parameters
		//Game_RecordGame(0, null, null); //test one --> game not found
		//Game_RecordGame(23, null, null);  //test two --> missing parameter
		//create instance of PlayerStat with BAD test data
		List<PlayerGameStat> hometeam = BadLoadHomeTeam();
		//Game_RecordGame(23, hometeam, null); //test three --> missing parameter
		List<PlayerGameStat> visitingteam = BadLoadVisitingTeam();
		//Game_RecordGame(233, hometeam, visitingteam); //test four --> game not found
		//Game_RecordGame(23, hometeam, visitingteam); //test five --> scores incorrect
		//Game_RecordGame(25, hometeam, visitingteam); //test six --> game already has stats
		hometeam = BadStatsHomeTeam();
		visitingteam = BadStatsVisitingTeam();
		//Game_RecordGame(23, hometeam, visitingteam); //test seven --> bad game stats



		//create instances of PlayerStat with GOOD test data
		//To rerun, go to your sql and remove any gameid = 23 off the PlayerStats
		//On the displays view 
		//         GamesPlayed (should be +1 between before and after)
		//         Expand PlayerStats and look for GameID 23 instances
		hometeam = GoodHomeTeam();
		visitingteam = GoodVisitingTeam();
		Game_RecordGame(23, hometeam, visitingteam); //test good results 


		//display team players with stats AFTER
		DisplayTeamPlayers(23);
		

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

public class PlayerGameStat
{
	public int PlayerID { get; set; }
	public int Goals { get; set; }
	public int Assists { get; set; }
	public bool Yellow { get; set; }
	public bool Red { get; set; }
	public bool Rostered { get; set; }
}

#region Given Code DO NOT ALTER
private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}

public List<PlayerGameStat> BadLoadHomeTeam()
{
	List<PlayerGameStat> data = new List<PlayerGameStat>();
	data.Add(new PlayerGameStat()
	{ PlayerID = 148, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 158, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 167, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 173, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 188, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 190, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	return data;
}

public List<PlayerGameStat> BadLoadVisitingTeam()
{
	List<PlayerGameStat> data = new List<PlayerGameStat>();
	data.Add(new PlayerGameStat()
	{ PlayerID = 133, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 135, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 143, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 153, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 162, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 170, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	return data;
}

public List<PlayerGameStat> BadStatsHomeTeam()
{
	List<PlayerGameStat> data = new List<PlayerGameStat>();
	data.Add(new PlayerGameStat()
	{ PlayerID = 148, Goals = -1, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 1583, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 167, Goals = -2, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 1733, Goals = 0, Assists = -1, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 188, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 190, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	return data;
}

public List<PlayerGameStat> BadStatsVisitingTeam()
{
	List<PlayerGameStat> data = new List<PlayerGameStat>();
	data.Add(new PlayerGameStat()
	{ PlayerID = 1333, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 135, Goals = -1, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 1433, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 153, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 162, Goals = 0, Assists = -1, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 170, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	return data;
}

public List<PlayerGameStat> GoodHomeTeam()
{
	List<PlayerGameStat> data = new List<PlayerGameStat>();
	data.Add(new PlayerGameStat()
	{ PlayerID = 148, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 158, Goals = 0, Assists = 0, Yellow = true, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 167, Goals = 1, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 173, Goals = 0, Assists = 1, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 188, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 190, Goals = 0, Assists = 0, Yellow = false, Red = true, Rostered = true });
	return data;
}

public List<PlayerGameStat> GoodVisitingTeam()
{
	List<PlayerGameStat> data = new List<PlayerGameStat>();
	data.Add(new PlayerGameStat()
	{ PlayerID = 133, Goals = 0, Assists = 0, Yellow = false, Red = true, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 135, Goals = 2, Assists = 1, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 143, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 153, Goals = 0, Assists = 1, Yellow = true, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 162, Goals = 1, Assists = 1, Yellow = false, Red = false, Rostered = true });
	data.Add(new PlayerGameStat()
	{ PlayerID = 170, Goals = 0, Assists = 0, Yellow = false, Red = false, Rostered = true });
	return data;
}

public void DisplayTeamPlayers(int gameid)
{
	Games results = Games.ToList()
					.Where(x => x.GameID == gameid)
					.FirstOrDefault();
	List<Players> hometeam = Players
						.Where(x => x.TeamID == results.HomeTeamID)
						.ToList();
	List<Players> visitingteam = Players
						.Where(x => x.TeamID == results.VisitingTeamID)
						.ToList();
	hometeam.Dump();
	visitingteam.Dump();
}

#endregion

public void Game_RecordGame(int gameId, 
							List<PlayerGameStat> hometeam, 
							List<PlayerGameStat> visitingteam)
{
	//local variables
	Players playerexists = null;
	Games gameexists = null;
	PlayerStats newplayerstats = null;


	//we need a container to hold x number of Exception messages
	List<Exception> errorlist = new List<Exception>();

	//YOUR CODE HERE
	
	if(gameId == null)
	{
		throw new Exception("ID needs to be supplied!");
	}
	//check if gameid exists or no
	
	bool gameExists = Games
				.Where(x => x.GameID == gameId).Any();
	if (!gameExists)
	{
		throw new Exception($"The Game with game ID:{gameId} does not exist on the database!");
	}
	if(hometeam == null)
	{
		throw new Exception("home Team needs to be supplied!");
	}
	if(visitingteam == null)
	{
		throw new Exception("Visiting Team needs to be supplied!");
	}
	
	//check if gameid exists or no
	
	//player stats for the game do not exist
	bool statsExist = Games
					.Where(x => x.GameID == gameId && x.PlayerStats.Count() > 0).Any();	
	if (statsExist)
	{
		errorlist.Add(new Exception($"The Game with game ID:{gameId} already has Player Stats associated to it!"));
	}
	
	//goals and assists are non negative int
	foreach (var item in hometeam)
	{
		if(item.Goals < 0 || item.Assists <0)
		{
			errorlist.Add(new Exception($"goals and assists should be non-negative integer values for PlayerID:{item.PlayerID}"));
		}
	}
	foreach (var item in visitingteam)
	{
		if(item.Goals < 0 || item.Assists <0)
		{
			errorlist.Add(new Exception($"goals and assists should be non-negative integer values for PlayerID:{item.PlayerID}"));
		}
	}
	
	//check the scores tally
	int homeScoresTotal = 0;
	
	foreach (var item in hometeam)
	{
		homeScoresTotal = homeScoresTotal + item.Goals;
	}
	
	int visitingScoresTotal=0;
	foreach (var item in visitingteam)
	{
		visitingScoresTotal = visitingScoresTotal + item.Goals;
	}
	
	
	if (homeScoresTotal != Games
						.Where(x => x.GameID == gameId)
						.Select(x =>x.HomeTeamScore).First())
						
	{
		errorlist.Add(new Exception("The scores tally for Home Team PLayes and recorded game do not match"));
	}
	
	if (visitingScoresTotal != Games
						.Where(x => x.GameID == gameId)
						.Select(x =>x.VisitingTeamScore).First())
						
	{
		errorlist.Add(new Exception("The scores tally for Visiting Team PLayes and recorded game do not match"));
	}
	
	//Processing
	//Single PLayerStat Record
	
	//Update
	if (errorlist.Count > 0)
		{
		//  throw the list of business processing error(s)
		throw new AggregateException("unable to add new game.  Check concerns", errorlist);
		}
	
	Players Player = null;
	
	foreach (var item in hometeam)
	{
		Player = Players
				.Where(x => x.PlayerID == item.PlayerID).FirstOrDefault();
		if (item.Rostered == true)
		{
			Player.GamesPlayed += 1;
			Players.Update(Player);
		}
		
		if (item.Goals > 0 || item.Assists > 0 || item.Yellow == true || item.Red ==true)
		{
			PlayerStats Playerstats = new PlayerStats()
			{
				GameID = gameId,
				PlayerID = item.PlayerID,
				Goals = item.Goals,
				Assists = item.Assists,
				YellowCard = item.Yellow,
				RedCard = item.Red
			};
			PlayerStats.Add(Playerstats);

		}
	}
	
	foreach (var item in visitingteam)
	{
		Player = Players
				.Where(x => x.PlayerID == item.PlayerID).FirstOrDefault();
		if(item.Rostered == true)
		{
			Player.GamesPlayed += 1;
			Players.Update(Player);
		}
		if (item.Goals > 0 || item.Assists > 0 || item.Yellow == true || item.Red ==true)
		{
			PlayerStats Playerstats = new PlayerStats()
			{
				GameID = gameId,
				PlayerID = item.PlayerID,
				Goals = item.Goals,
				Assists = item.Assists,
				YellowCard = item.Yellow,
				RedCard = item.Red
			};
			PlayerStats.Add(Playerstats);
		}
	}
	SaveChanges();
		
}


