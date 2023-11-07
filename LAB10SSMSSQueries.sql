
---- H�mta alla produkter med deras namn, pris och kategori namn. Sortera p� kategori namn och sen produkt namn
SELECT CategoryName, ProductName, UnitPrice FROM Products
JOIN Categories ON Products.CategoryID = Categories.CategoryID
ORDER BY CategoryName, ProductName
GO

-- H�mta alla kunder och antal ordrar de gjort. Sortera fallande p� antal ordrar.
Select CompanyName, OrderCount = COUNT(Orders.OrderID) FROM Customers
JOIN Orders ON Orders.CustomerID = Customers.CustomerID
GROUP BY CompanyName
ORDER BY OrderCount DESC
GO

-- H�mta alla anst�llda tillsammans med territorie de har hand om (EmployeeTerritories och Territories tabellerna)
SELECT Territory =TerritoryDescription, Employee = FirstName + ' ' + LastName, Title FROM Employees
JOIN EmployeeTerritories ON Employees.EmployeeID = EmployeeTerritories.EmployeeID
JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID
ORDER BY Territory, LastName, FirstName
GO
--ist�llet f�r att skriva antal ordrar, skriv ut summan f�r deras totala orderv�rde
Select CompanyName, OrderTotal = SUM([Order Details].UnitPrice * [Order Details].Quantity * (1 - [Order Details].Discount)) FROM Customers
JOIN Orders ON Orders.CustomerID = Customers.CustomerID
JOIN [Order Details] ON [Order Details].OrderID = Orders.OrderID
GROUP BY CompanyName
ORDER BY OrderTotal DESC
GO