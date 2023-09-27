void Main()
{
	try
	{
		
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

public class Vendors
{
	public int VendorID {get; set;}
	public string VendorName {get; set;}
}

public class VendorInfo
{
	public int VendorID {get; set;}
	public string VendorPhone {get; set;}
	public string City {get; set;}
	public int PurchaseOrderID {get; set;}
	public decimal Subtotal {get; set;}
}

public class PurchaseOrder
{
	public int PurchaseOrderID {get; set;}
	public int StockItemID {get; set;}
	public string Description {get; set;}
	public int ReOrderLevel {get; set;}
	public int QuantityOnOrder {get; set;}
	public int QuantityToOrder {get; set;}
	public int QuantityOnHand {get; set;}
	public decimal Price {get; set;}
}

public class VendorInventory
{
	public int StockItemID {get; set;}
	public string Description {get; set;}
	public int ReOrderLevel {get; set;}
	public int QuantityOnOrder {get; set;}
	public int Buffer {get; set;}
	public int QuantityOnHand {get; set;}
	public decimal Price {get; set;}
}

//public List<Vendors> Vendors_FetchVendors()
//{
//	var vendorsList = Vendors
//	.Select(x => new Vendors
//	{
//		VendorID = x.VendorID,
//		VendorName = x.VendorName
//	}).Dump();
//	return vendorsList.ToList();
//}

public void Fetch_VendorInfo(int vendorID)
{
	var vendorInfo = PurchaseOrders
					.Where(x => x.VendorID == vendorID)
					.Select(x => new
								{
									VendorID = x.VendorID,
									VendorPhone = x.Vendor.Phone,
									City =  x.Vendor.City,
									PurchaseOrderID = x.PurchaseOrderID,
									Subtotal = x.SubTotal,
								})
					.Dump();
}

public List<PurchaseOrder> Fetch_PurchaseOrder (int vendorID)
{
	return PurchaseOrderDetails
	.Where(x => x.PurchaseOrder.VendorID == vendorID)
	.Select(x => new PurchaseOrder()
	{
		PurchaseOrderID = x.PurchaseOrderID,
		StockItemID = x.StockItemID,
		Description = x.StockItem.Description,
		ReOrderLevel = x.StockItem.ReOrderLevel,
		QuantityOnOrder = x.StockItem.QuantityOnOrder,
		QuantityToOrder = x.Quantity,
		QuantityOnHand = x.StockItem.QuantityOnHand,
		Price = x.PurchasePrice		
	})
	.ToList();
}

