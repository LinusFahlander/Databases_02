CREATE TABLE Addresses (
	Id int not null identity primary key,
	StreetName nvarchar(100) not null,
	PostalCode char(5) not null,
	City nvarchar(100) not null
)
GO

CREATE TABLE Employees (
	Id int not null identity primary key,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	Email nvarchar(150) not null unique,
	PhoneNumber char(10) null,
	AddressId int not null references Addresses(Id)
)
