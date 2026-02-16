--Creating all tables for database
CREATE TABLE Genre(
	Genre_id int NOT NULL,
	GenreName nvarchar (50) NOT NULL,
	CONSTRAINT PK_Genre PRIMARY KEY CLUSTERED
	(
		Genre_id
	),
)

CREATE TABLE Reader (
	Reader_id int IDENTITY (1, 1) NOT NULL,
	LastName nvarchar (70) NOT NULL,
	FirstName nvarchar (70) NOT NULL,
	Gender char (1) NULL,
	BirthDate date NULL,
	"Address" nvarchar (110) NULL,
	City nvarchar (60) NULL,
	Phone nvarchar (15) NULL,
	Email nvarchar (100) NULL,
	Registered datetime2(2) NULL,
	CONSTRAINT PK_Reader PRIMARY KEY CLUSTERED
	(
		Reader_id
	),
	CONSTRAINT "CK_BirthDate" CHECK (BirthDate < getdate())
)

CREATE TABLE Book(
	Book_id int IDENTITY (1, 1) NOT NULL,
	Author nvarchar (254) NULL,
	Title nvarchar (254) NULL,
	YearOfRelease int NULL,
	Genre_id int NULL,
	"Description" nvarchar (254) NULL,
	CONSTRAINT PK_Book PRIMARY KEY CLUSTERED
	(
		Book_id
	),
	CONSTRAINT CK_YearOfRelease CHECK (YearOfRelease <= year(getdate())),
	CONSTRAINT FK_Genre FOREIGN KEY
	(
		Genre_id
	) REFERENCES dbo.Genre (Genre_id)
)

CREATE TABLE Staff(
	Staff_id int IDENTITY (1, 1) NOT NULL,
	LastName nvarchar (70) NOT NULL,
	FirstName nvarchar (70) NOT NULL,
	Title nvarchar (30) NULL,
	CONSTRAINT PK_Staff PRIMARY KEY CLUSTERED
	(
		Staff_id
	)
)

CREATE TABLE Loans(
	Loan_id int IDENTITY (1, 1) NOT NULL,
	Reader_id int NULL,
	Staff_id int NULL,
	Book_id int NULL,
	LoanDate datetime2(2) NULL,
	Note nvarchar (254) NULL,
	CONSTRAINT PK_Loan_id PRIMARY KEY CLUSTERED
	(
		Loan_id
	),
	CONSTRAINT FK_Loans_Reader_id FOREIGN KEY
	(
		Reader_id
	) REFERENCES dbo.Reader (Reader_id),
	CONSTRAINT FK_Loans_Staff_id FOREIGN KEY
	(
		Staff_id
	) REFERENCES dbo.Staff (Staff_id),
	CONSTRAINT FK_Loans_Book_id FOREIGN KEY
	(
		Book_id
	) REFERENCES dbo.Book (Book_id)
)

CREATE TABLE Recievings(
	Loan_id int NOT NULL,
	Staff_id int NULL,
	RecievingDate datetime2(2) NULL,
	Note nvarchar (254) NULL,
	CONSTRAINT PK_Recievings_Loan_id PRIMARY KEY CLUSTERED
	(
		Loan_id
	),
	CONSTRAINT FK_Recievings_Loan_id FOREIGN KEY
	(
		Loan_id
	) REFERENCES dbo.Loans (Loan_id),
	CONSTRAINT FK_Recievings_Staff_id FOREIGN KEY
	(
		Staff_id
	) REFERENCES dbo.Staff (Staff_id),
)


CREATE TABLE [Currently Loaned](
	Loan_id int NOT NULL,
	CONSTRAINT PK_CurrentlyLoaned_Loan_id PRIMARY KEY CLUSTERED
	(
		Loan_id
	),
	CONSTRAINT FK_CurrentlyLoaned_Loan_id FOREIGN KEY
	(
		Loan_id
	) REFERENCES dbo.Loans (Loan_id)
)


CREATE TABLE Payments(
	Payment_id int IDENTITY (1, 1) NOT NULL,
	Reader_id int NULL,
	Amount decimal(18, 2) NULL,
	PaymentDate datetime2(2) NULL
	CONSTRAINT PK_Payments PRIMARY KEY CLUSTERED
	(
		Payment_id
	),
	CONSTRAINT FK_Reader_id FOREIGN KEY
	(
		Reader_id
	) REFERENCES dbo.Reader (Reader_id)
)

CREATE TABLE Arrears
(
	Reader_id int NOT NULL,
	"Days" int NOT NULL,
	CONSTRAINT PK_Arrears PRIMARY KEY CLUSTERED
	(
		Reader_id
	),
	CONSTRAINT FK_Arrears_Reader_id FOREIGN KEY
	(
		Reader_id
	) REFERENCES dbo.Reader (Reader_id)
)