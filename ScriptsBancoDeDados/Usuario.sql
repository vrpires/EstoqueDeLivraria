CREATE TABLE Usuario (
	id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	login nvarchar(50) NOT NULL,
	senha nvarchar(10) NOT NULL
);