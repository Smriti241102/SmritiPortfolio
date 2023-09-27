<Query Kind="Program">
  <Connection>
    <ID>65db5b2b-708b-4384-848e-3594d718dcec</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>SUSHIL-MAIN\SQLEXPRESS</Server>
    <Database>eTools2021</Database>
    <DisplayName>eTools2021</DisplayName>
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
		PurchaseOrderDetails.Where(x => x.PurchaseOrderID == 358).Dump();
		PurchaseOrders.Where(x => x.PurchaseOrderID == 358).Dump();
		ReceiveOrders.Where(x => x.PurchaseOrderID == 358).Dump();
		StockItems.Where(x => x.StockItemID ==5567 || x.StockItemID ==5566).Dump();

		List<PODetails> list1 = new List<PODetails>();
		
		List<PODetails> l2 = PO_FetchPOInfo(358);
		
		l2[1].ReceivedQty = 3;
		l2[1].ReturnedQty = 1;
		l2[1].Reason = "reason defined1";
		
		l2[2].ReceivedQty = 3;
		l2[2].ReturnedQty = 1;
		l2[2].Reason = "reason defined2";
		
		list1.Add(l2[1]);
		list1.Add(l2[2]);
	
		list1.Dump();
		Receive_ReceiveItems(358, 2, list1, null, false, null);
		
		
		PurchaseOrderDetails.Where(x => x.PurchaseOrderID == 358).Dump();
		PurchaseOrders.Where(x => x.PurchaseOrderID == 358).Dump();
		ReceiveOrders.Where(x => x.PurchaseOrderID == 358).Dump();
		StockItems.Where(x => x.StockItemID ==5567 || x.StockItemID ==5566).Dump();
		
	}
	catch(Exception ex)
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

// You can define other methods, fields, classes and namespaces here
//Query Models
public class POList
{
    public int PurchaseOrderID {get; set;}
    public DateTime? OrderDate {get; set;}
    public int VendorID {get; set;}
    public string Phone {get; set;} 
}

//Command Models
public class PODetails
{
    public int StockItemID {get; set;}
    public string Description {get; set;}
    public int Quantity  {get; set;}
    public int OutstandingQty {get; set;}
    public int? ReceivedQty {get; set;}
    public int? ReturnedQty {get; set;}
    public string? Reason {get; set;}
}

public class UnorderedReturns 
{
    public int ID {get; set;}
    public string Description {get; set;}
    public string VSN {get; set;}
    public int Qty {get; set;}
}

//TRX methods
public List<POList> PO_FetchPO()
{
	return PurchaseOrders
	.Where(x=> x.Closed == false)
	.Select(x => new POList()
	{
		PurchaseOrderID= x.PurchaseOrderID,
		OrderDate= x.OrderDate,
		VendorID= x.VendorID,
		Phone= x.Vendor.Phone,
	}).ToList();
}

public List<PODetails> PO_FetchPOInfo(int PoId)
{
	return PurchaseOrderDetails
	.Where(x => x.PurchaseOrderID == PoId)
	.Select(x => new PODetails()
	{
		StockItemID = x.StockItemID,
		Description= x.StockItem.Description,
		Quantity = x.Quantity,
		OutstandingQty = x.Quantity - x.ReceiveOrderDetails.Sum(x=>x.QuantityReceived)
		
	}).ToList();
}

void Receive_ReceiveItems(int POId,int employeeID, List<PODetails> receivingList, List<UnorderedReturns>? returnlist, bool ForceClose,string? reason)
{


	if (POId == null || POId == 0)
	{
		throw new Exception("PO ID not supplied!");
	}
	if (employeeID == null || employeeID == 0)
	{
		throw new Exception("EmployeeID ID not supplied!");
	}
	if (receivingList == null || receivingList.Count() == 0)
	{
		throw new Exception("No items have been supplied in the receiving list ID not supplied!");
	}
	
	foreach(var item in receivingList)
	{
		if (item.ReturnedQty != null && item.ReturnedQty<0 && item.Reason == null)
		{
			throw new Exception("You need to supply Reason if returning an item!");
		}
		StockItems stockitem1 = StockItems.Where(x =>x.StockItemID==item.StockItemID).FirstOrDefault();
		if (item.ReceivedQty > stockitem1.QuantityOnOrder)
		{
			throw new Exception("Receiving Quantity can not be more than Quantity Ordered!");
		}
	}
	
	
	PurchaseOrders poOrder = PurchaseOrders.Where(x => x.PurchaseOrderID ==POId).FirstOrDefault();

	ReceiveOrders newOrder = new ReceiveOrders();
	newOrder.PurchaseOrderID = POId;
	newOrder.ReceiveDate = DateTime.Now;
	newOrder.EmployeeID = employeeID;
	ReceiveOrders.Add(newOrder);
	
	foreach (var item in receivingList)
	{
		PurchaseOrderDetails podetail = PurchaseOrderDetails.Where(x=>x.PurchaseOrderID == POId && x.StockItemID == item.		StockItemID).FirstOrDefault();
		
		ReceiveOrderDetails newOrderDetails = new ReceiveOrderDetails();
		newOrderDetails.QuantityReceived = (int)item.ReceivedQty;
		//Add to THE received Order & PODetails table
		newOrder.ReceiveOrderDetails.Add(newOrderDetails);
		podetail.ReceiveOrderDetails.Add(newOrderDetails);
		
	
	    //returnedOrder Details table update
		if (item.ReturnedQty > 0)
		{
		ReturnedOrderDetails returnitem = new()
		{
			ItemDescription = item.Description,
			Quantity = (int)item.ReturnedQty,
			Reason = (string)item.Reason,
			VendorStockNumber = StockItems
								.Where(x => x.StockItemID == item.StockItemID)
								.Select(x =>x.VendorStockNumber)
								.FirstOrDefault()		
		};
		//Add item returned to returned table
			podetail.ReturnedOrderDetails.Add(returnitem);
			newOrder.ReturnedOrderDetails.Add(returnitem);
		}
		
		
		//update StockItems table
		StockItems stockitem = StockItems.Where(x =>x.StockItemID==item.StockItemID).FirstOrDefault();
		
		stockitem.QuantityOnHand += (int)item.ReceivedQty;
		stockitem.QuantityOnOrder -= (int)item.ReceivedQty;
		
		 StockItems.Update(stockitem);
	
	}
	
	if (returnlist != null && returnlist.Count() > 0)
	{
	foreach(var item in returnlist)
	{
		UnOrderedItems unordered = new ();
		unordered.ItemName = item.Description;
		unordered.VendorProductID = item.VSN;
		unordered.Quantity = item.Qty;
		
		UnOrderedItems.Add(unordered);
	}
	}
	
	bool close = true;
	foreach (var item in receivingList)
	{
		if (item.OutstandingQty != item.Quantity)
		{
			close = false;
		}
	}
	
	if (ForceClose == true || close == true)
	{
		
		poOrder.Closed = true;
		if (ForceClose == true)
		{
			poOrder.Notes += reason;
		}
	}

	SaveChanges();
	
	
	
}
















