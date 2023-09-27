Select
   Customers.CompanyName,
   count(*) as purchases
FROM
   Customers JOIN Orders on Customers.CustomerID = Orders.CustomerID
GROUP BY
   Customers.CompanyName