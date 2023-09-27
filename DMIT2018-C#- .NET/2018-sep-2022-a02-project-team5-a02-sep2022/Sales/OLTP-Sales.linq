<Query Kind="Program">
  <Connection>
    <ID>4369b9c3-da8a-47cb-aa6c-92e3aea8f45a</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>DESKTOP-6SE03NC\SQLEXPRESS</Server>
    <Database>eTools2021</Database>
    <DisplayName>eTools</DisplayName>
    <Persist>true</Persist>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	try
	{
	
	     Fetch_Category();
		 int categoryId = 2;
		 Fetch_CartItems_By(categoryId);
		List<CheckOutTRX> newlist = new List<CheckOutTRX>();
		newlist.Add(new CheckOutTRX(){
		SellingPrice = 10,
		Quantity = 2,
		ItemDescription = "Item112",
		StockItemID = 23,
		});
		
		//Other Test Cases Un-Commented
		//// List<CheckOutTRX> newlist = new List<CheckOutTRX>();
		//newlist.Add(new CheckOutTRX(){
		//SellingPrice = 65,
		//Quantity = 3,
		//ItemDescription = "Full power drill",
		//StockItemID = 34,
		//});
		//
		//// List<CheckOutTRX> newlist = new List<CheckOutTRX>();
		//newlist.Add(new CheckOutTRX(){
		//SellingPrice = 89,
		//Quantity = 9,
		//ItemDescription = "Torque phillip driver",
		//StockItemID = 35,
		//});
		
		AddToSales(newlist, "C", null,  1);
		//Refund Method Testing Error 
//		The INSERT statement conflicted with the FOREIGN KEY constraint "FK_SaleRefundsSales_SaleID". The conflict occurred in database "eTools2021", table "dbo.Sales", column 'SaleID'.
//The statement has been terminated.
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
private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}
// You can define other methods, fields, classes and namespaces here


public class Item
{
public int StockItemID {get; set;}
public string ItemDescription {get; set;}
public decimal SellingPrice {get; set;}
public int QuantityOnHand {get; set;}
}
public class Category 
{
	public int CategoryID{get; set;}
	public string Description{get; set;}
}

public class GetCartItems {
public string ItemDescription {get; set;}
public int ItemID {get; set;}
public int Quantity {get; set;}
}

public class RefundTRX
 {
public int StockItemID{get; set;}
public int Quantity{get; set;}
public int ReturnQuantity{get; set;}
public string Description{get; set;}
}
public class CheckOutTRX {
public string ItemDescription {get; set;}
public int StockItemID  {get; set;}
public int Quantity  {get; set;}
public decimal SellingPrice {get; set;}
public decimal? Total {get; set;}
}

public void Fetch_Category()
{
 	var eachCategory = Categories
					 .Select(x=>new
								  {
								     Description = x.Description,
									 ItemQuantity = x.StockItems.Count()
								  })
								  .ToList();
								 eachCategory.Dump();
}
public void Fetch_CartItems_By(int categoryID)
{
 	var Item = StockItems
					 .Where(x=>x.CategoryID == categoryID)
					 .Select(x=>new 
								  { 
								     ItemDescription = x.Description,
									 QuantityOnHand = x.QuantityOnHand,
									 SellingPrice = x.SellingPrice
								  }).ToList();
								  Item.Dump();
}
public void AddToSales(List<CheckOutTRX>newList, string PaymentType, int? CouponId , int EmployeeId)
{
List<Exception> errorList = new List<Exception>();

		Employees employeeExists = Employees
							.Where(e => e.EmployeeID == EmployeeId)
							.FirstOrDefault();
		if(employeeExists == null)
		{
		throw new ArgumentNullException("Employee is invalid");
		}
		
		if(CouponId != null)
		{
			Coupons IsCouponValid = Coupons
							.Where(c => c.CouponID == CouponId)
							.FirstOrDefault();
			if(IsCouponValid == null)
			{
			throw new ArgumentException("Invalid Cupon!");
			}
			Coupons IscouponExists = Coupons
									.Where(c => c.CouponID == CouponId && DateTime.Today >= c.StartDate && DateTime.Today <= c.EndDate)
									.FirstOrDefault();
		}
			
		if(PaymentType==null )
		{
			errorList.Add(new Exception("Payment type canot be null"));
		}
		
		 if(PaymentType!= "M"||PaymentType!="C"||PaymentType!="D")
		 {
		 	errorList.Add(new Exception("Payment type is Invalid!"));
		  }
		

  decimal subtotal = 0;
        foreach( var item in  newList ) 
		{
		item.Total = item.SellingPrice * item.Quantity; 
		subtotal = subtotal + (int)(item.Total);
		}
		Sales newSales = new Sales();
		newSales.SaleDate = DateTime.Now; 
		newSales.PaymentType = PaymentType;
	    newSales.EmployeeID = EmployeeId;
		newSales.SubTotal = subtotal; 
		newSales.TaxAmount = 0.05m * newSales.SubTotal; 
		newSales.CouponID = CouponId;
		Sales.Add(newSales);
		
		foreach (var eachItem in newList)
		{
		    SaleDetails newSaleDetails = new SaleDetails();
			//newSaleDetails.StockItemID = eachItem.StockItemID; 
			newSaleDetails.SellingPrice = eachItem.SellingPrice;
			newSaleDetails.Quantity = eachItem.Quantity;
			//Nav to fk
			newSales.SaleDetails.Add(newSaleDetails);
			//Inventory 
			StockItems stockitem = new StockItems();
			stockitem = StockItems.Where(x =>x.StockItemID==eachItem.StockItemID).FirstOrDefault();
			stockitem.Dump();
			stockitem.QuantityOnHand -= eachItem.Quantity;
			 
			stockitem.SaleDetails.Add(newSaleDetails);
			
			StockItems.Update(stockitem);
		}
		SaveChanges();
		
}


public void SalesRefund(SaleRefunds refund)
{
   List<Exception> errorlist = new List<Exception>();
		int saleID = refund.SaleID;
		int employeID = refund.EmployeeID;
		decimal taxAmount = refund.TaxAmount;
		decimal subTotal = refund.SubTotal;
		
		Employees IsemployeeExists = Employees
							.Where(eachEmployee => eachEmployee.EmployeeID == employeID)
							.FirstOrDefault();
		if(IsemployeeExists == null)
		{
		throw new ArgumentNullException("Employee is invalid");
		}	
		    refund = new SaleRefunds()
			{
			   SaleID = saleID,
			   EmployeeID=employeID,
               TaxAmount = taxAmount,
			   SubTotal = subTotal,
			}.
			Dump();			
			SaleRefunds.Add(refund);		
			SaveChanges();
}
 public void AddRefundDetails(SaleRefundDetails refundDetails)
 
 {
 	    int stockItemID = refundDetails.StockItemID;
    
	bool itemexists = true ;
    decimal price = refundDetails.SellingPrice;
	int quantity = refundDetails.Quantity;
    int salerefundid = refundDetails.SaleRefundID;
	int stockItemid = refundDetails.StockItemID;
	
	List<Exception> errorlist = new List<Exception>();
	
	itemexists = StockItems.Where(si => si.StockItemID == stockItemID).Any();
	
	
	if (quantity < 0)
	{
		errorlist.Add(new Exception ("The Quantity can not be non negative value"));
	}
	
	if (!itemexists)
	{
		errorlist.Add(new Exception("This Item is invalid!"));
	}
	if (price < 0)
	{
		errorlist.Add(new Exception ("The Price can not be negative value"));
	}
	if (errorlist.Count > 0)
	{
			 throw new Exception ("Check Concetns ");
	}
	       else
	 		{
			
			  refundDetails = new SaleRefundDetails()
			{
			   SaleRefundID = salerefundid,
			   StockItemID = stockItemid,
               SellingPrice = price,
			   Quantity = quantity,
			}
			.Dump();			
			SaleRefundDetails.Add(refundDetails);	
			
			
		    StockItems newstock = StockItems
			.Where(x =>x.StockItemID==refundDetails.StockItemID)
			.FirstOrDefault();
			//Inventory
			newstock.QuantityOnHand += (int)refundDetails.Quantity;
		    StockItems.Update(newstock);
			SaveChanges();
			}
	 }



