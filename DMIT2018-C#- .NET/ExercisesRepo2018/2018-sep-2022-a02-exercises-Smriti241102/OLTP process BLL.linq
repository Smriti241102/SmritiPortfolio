<Query Kind="Program">
  <Connection>
    <ID>9c4626e6-0b49-4acb-a50f-1f3d45150491</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>SUSHIL-MAIN\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>WorkSchedule</Database>
  </Connection>
  <RuntimeVersion>6.0</RuntimeVersion>
</Query>

void Main()
{
	try{
	//query region
	List<SkillItem> Skills_display = Skills_FetchSkills();
	
	//Skills_display.Dump();
	
	//command methhod
	
	
	List <RegisterSkill> regskills = new List<RegisterSkill>();
	/*
	regskills.Add(new RegisterSkill(){
		SelectedSkill = false,
		SkillId = 2,
		level = 1,
		YearsOfExperience = 2,
		Wage = 32		
	});
	
	regskills.Add(new RegisterSkill(){
		SelectedSkill = true,
		SkillId = 4,
		level = 1,
		YearsOfExperience = 5,
		Wage = 33		
	});
	
	regskills.Add(new RegisterSkill(){
		SelectedSkill = true,
		SkillId = 5,
		level = 1,
		YearsOfExperience = 6,
		Wage = 34		
	});
	
	regskills.Add(new RegisterSkill(){
		SelectedSkill = true,
		SkillId = 3,
		YearsOfExperience = null,
		level = 2,
		Wage = 35		
	});
	
	*/
	
	Employee_RegisterSkills( "Smriti", "Rani", "780.555.0190", regskills);
	}
	catch (ArgumentNullException ex)
	{
		GetInnerException(ex).Message.Dump();
	}
	
	catch (Exception ex)
	{
		GetInnerException(ex).Message.Dump();
	}
}

#region Methods
private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}
#endregion

//query models

public class SkillItem
{
    public int SkillId {get; set;}
    public string SkillName {get; set;}
}

public class RegisterSkill
{
    public bool SelectedSkill {get; set;}
    public int SkillId {get; set;}
	public int level {get; set;}
    public int? YearsOfExperience {get; set;}
    public decimal Wage {get; set;}
}

public List<SkillItem> Skills_FetchSkills()
{
	IEnumerable<SkillItem> SkillList = Skills
							.Select(x => new SkillItem{
								SkillId = x.SkillID,
								SkillName = x.Description							
							});

	return SkillList.ToList();

}



void Employee_RegisterSkills(string firstName, string lastName, string homePhone, List<RegisterSkill> SkillList )
{
	bool employeeExists = false;
	Employees employee = null;
	EmployeeSkills employeeSkills = null;
	
	if (string.IsNullOrWhiteSpace(firstName))
	{
		throw new ArgumentNullException("First name is missing");
	}
	
	if (string.IsNullOrWhiteSpace(lastName))
	{
		throw new ArgumentNullException("Last name is missing");
	}
	
	if (string.IsNullOrWhiteSpace(homePhone))
	{
		throw new ArgumentNullException("Home Phone is missing");
	}
	
		

	
	bool zeroSkill = true;
	
	foreach (var item in SkillList)
	{
		
		if (item.SelectedSkill == true)
		{
			if (item.level == null || item.level < 1 ||item.level>3)
			{
				throw new ArgumentNullException("For each Skill, Level is required");
			}
			if (item.YearsOfExperience != null)
			{
				if(item.YearsOfExperience >50 || item.YearsOfExperience <1)
				{
					throw new Exception("Years of Experience should be in the range of 1 to 50 inclusive");
				}
			}
			
			if (item.Wage == null)
			{
				throw new ArgumentNullException("Hourly wage needs to be entered");
			}
			else{
			  if (item.Wage <15 || item.Wage >100)
			  {
			  	throw new Exception($"Hourly Wage must fall between $15.00 and $100.00 inclusive. {item.Wage}");
			  }
			}
		zeroSkill= false;
		}
	}
	
	if (zeroSkill)
	{
		throw new Exception($"The employee must select at least one Skill");
	}
	
	
	//check if the employee already exists
	employeeExists = Employees
					.Where(x => x.FirstName == firstName 
								&& x.LastName == lastName
								&& x.HomePhone == homePhone)
								.Select(x => x.EmployeeID)
								.Any();
	
	
	//Business Process
	if(employeeExists)
	{
		throw new Exception($"Employee named {firstName} {lastName} with Home Phone: {homePhone} already exists!");
	}
	
	
	
	else{
		
		employee = new Employees()
		{
			EmployeeID = Employees.Count() + 1,
			FirstName = firstName,
			LastName = lastName,
			HomePhone = homePhone
		};
		
		foreach (var item in SkillList)
		{	
		if (item.SelectedSkill == true)
			{
			employeeSkills = new EmployeeSkills(){
				EmployeeSkillID = EmployeeSkills.Count() + employee.EmployeeSkills.Count() +1,
				EmployeeID = employee.EmployeeID,
				SkillID = item.SkillId,
				Level = item.level,
				YearsOfExperience = item.YearsOfExperience,
				HourlyWage = item.Wage,
				
				Skill= Skills
						.Where(x => x.SkillID == item.SkillId).FirstOrDefault()
			};
			
		
			employee.EmployeeSkills.Add(employeeSkills);
			}
		}
		

		
		//employee.Dump();
		
		Employees.Add(employee);
		SaveChanges();
		
	
	}
}












