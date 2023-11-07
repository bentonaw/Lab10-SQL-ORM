
---- Hämta alla produkter med deras namn, pris och kategori namn. Sortera på kategori namn och sen produkt namn
SELECT CategoryName, ProductName, UnitPrice FROM Products
JOIN Categories ON Products.CategoryID = Categories.CategoryID
ORDER BY CategoryName, ProductName
GO

-- Hämta alla kunder och antal ordrar de gjort. Sortera fallande på antal ordrar.
Select CompanyName, OrderCount = COUNT(Orders.OrderID) FROM Customers
JOIN Orders ON Orders.CustomerID = Customers.CustomerID
GROUP BY CompanyName
ORDER BY OrderCount DESC
GO

-- Hämta alla anställda tillsammans med territorie de har hand om (EmployeeTerritories och Territories tabellerna)
SELECT Territory =TerritoryDescription, Employee = FirstName + ' ' + LastName, Title FROM Employees
JOIN EmployeeTerritories ON Employees.EmployeeID = EmployeeTerritories.EmployeeID
JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID
ORDER BY Territory, LastName, FirstName
GO
--istället för att skriva antal ordrar, skriv ut summan för deras totala ordervärde
Select CompanyName, OrderTotal = SUM([Order Details].UnitPrice * [Order Details].Quantity * (1 - [Order Details].Discount)) FROM Customers
JOIN Orders ON Orders.CustomerID = Customers.CustomerID
JOIN [Order Details] ON [Order Details].OrderID = Orders.OrderID
GROUP BY CompanyName
ORDER BY OrderTotal DESC
GO