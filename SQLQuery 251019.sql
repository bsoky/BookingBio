USE BookingDB;

CREATE TABLE Customers (
  customerId int PRIMARY KEY IDENTITY (1,1) NOT NULL  
  ,email varchar(255) UNIQUE NOT NULL
);

CREATE TABLE Bookings (
  bookingId int PRIMARY KEY IDENTITY  (1,1) NOT NULL
  ,customerId int 
  ,bookingForDate smalldatetime
  ,bookingMadeDate smalldatetime
  ,allSeatsId int
  
);

CREATE TABLE AllSeats (
  allSeatsId int PRIMARY KEY IDENTITY (1,1) NOT NULL
  ,loungeId int NOT NULL
  ,rowNumber nvarchar(10) NOT NULL
  ,seatNumber int NOT NULL
);

CREATE TABLE Lounges (
  loungeId int PRIMARY KEY IDENTITY(1,1) NOT NULL
  ,seatCount int NOT NULL
);

CREATE TABLE MovieShowings (
  movieShowingsId int PRIMARY KEY IDENTITY (1,1) NOT NULL
  ,loungeId int
  ,movieId int
  ,movieShowingTime smalldatetime
);

CREATE TABLE Movies (
  movieId int PRIMARY KEY IDENTITY (1,1) NOT NULL
  ,movieName nvarchar(150) UNIQUE NOT NULL
);

CREATE TABLE UserAccounts ( 
  userAccountId int PRIMARY KEY IDENTITY (1,1) NOT NULL 
  ,accountName nvarchar(100) UNIQUE NOT NULL
  ,accountPassword nvarchar(400) NOT NULL
  ,customerId int NOT NULL
  ,customerName varchar(255)
  ,phoneNumber nvarchar(30) 
  ,salt nvarchar(100)
);


ALTER TABLE Bookings ADD FOREIGN KEY (customerId) REFERENCES Customers (customerId);

ALTER TABLE AllSeats ADD FOREIGN KEY (loungeId) REFERENCES Lounges (loungeId);

ALTER TABLE Bookings ADD FOREIGN KEY (allSeatsId) REFERENCES AllSeats (allSeatsId);

ALTER TABLE MovieShowings ADD FOREIGN KEY (loungeId) REFERENCES Lounges (loungeId);

ALTER TABLE MovieShowings ADD FOREIGN KEY (movieId) REFERENCES Movies (movieId);

ALTER TABLE UserAccounts ADD FOREIGN KEY (customerId) REFERENCES Customers (customerId);
