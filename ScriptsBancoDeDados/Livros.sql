CREATE TABLE Livros (
	id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	nome nvarchar(50) NOT NULL,
	autor nvarchar(50) NOT NULL,
	quantidade int default 0
);