Declare @Streetname nvarchar(100) SET @StreetName = 'Spannlandsgatan 40C'
Declare @PostalCode char(5) SET @PostalCode = '70346'
Declare @City nvarchar(100) SET @City = 'Örebro'
Declare @FirstName nvarchar(50) SET @FirstName = 'Linus'
Declare @LastName nvarchar(50) SET @LastName = 'Fahlander'
Declare @Email nvarchar(150) SET @Email = 'linusfahlander@gmail.com'
Declare @PhoneNumber char(10) SET @PhoneNumber = '0725304343'
Declare @AddressId int SET @AddressId = 3

--Saves address to database if not exists and returns id else returns id of already inserted address
IF NOT EXISTS (SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City)
	INSERT INTO Addresses OUTPUT INSERTED.Id VALUES (@StreetName, @PostalCode, @City)
ELSE
	SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City

--Saves Employee to database if not Employee with same email already exists
IF NOT EXISTS (SELECT Id FROM Employees WHERE Email = @Email)
	INSERT INTO Employees VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @AddressId)






SELECT * FROM Addresses
SELECT * FROM Employees

SELECT
	e.Id, e.FirstName, e.LastName, e.Email, e.PhoneNumber,
	a.StreetName, a.PostalCode, a.City
FROM Employees e
JOIN Addresses a ON e.AddressId = a.Id
WHERE e.Email = @Email

