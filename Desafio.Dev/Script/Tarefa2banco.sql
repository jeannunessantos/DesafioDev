
DECLARE @dbname nvarchar(MAX)
SET @dbname = N'desafio_dev'

/* Cria o banco de dados desafio_dev */
IF (NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
BEGIN
	CREATE DATABASE desafio_dev
END
GO

/* Seleciona o banco de dados desafio_dev */
USE desafio_dev
GO


/* Cria a tabela de departamento */
IF NOT EXISTS(SELECT * FROM SYS.OBJECTS WHERE TYPE IN ('U','V') AND NAME= 'Departamento')
BEGIN 
	CREATE TABLE Departamento (
	Id int,
	Nome Varchar(200),
	CONSTRAINT PK_Departamento PRIMARY KEY (Id)
	)
	/* Insere alguns departamentos */
	INSERT Departamento (Id, Nome) VALUES (1,'TI')
	INSERT Departamento (Id, Nome) VALUES (2,'Vendas')
END
GO

/* Cria a tabela de pessoa */
IF NOT EXISTS(SELECT * FROM SYS.OBJECTS WHERE TYPE IN ('U','V') AND NAME= 'Pessoa')
BEGIN 
	CREATE TABLE Pessoa 
	(
		Id int,
		Nome Varchar(200),
		Salario Int,
		DeptId int
		CONSTRAINT PK_Pessoa PRIMARY KEY (Id)
		FOREIGN KEY (DeptId) REFERENCES Departamento(Id)
	)
	/* Insere algumas pessoas */
	INSERT INTO Pessoa(Id, Nome, Salario, DeptId) VALUES (1, 'Joe', 70000, 1)
	INSERT INTO Pessoa(Id, Nome, Salario, DeptId) VALUES (2, 'Henry', 80000, 2)
	INSERT INTO Pessoa(Id, Nome, Salario, DeptId) VALUES (3, 'San', 60000, 2)
	INSERT INTO Pessoa(Id, Nome, Salario, DeptId) VALUES (4, 'Max', 90000, 1)
END
GO

/* Faz a consulta retornando o os maiores salários por departamento */
SELECT 
	 d.Nome	Departamento
	,p.Nome Pessoa
	,p.Salario
FROM Pessoa					p 
	INNER JOIN Departamento	d ON (p.DeptId = d.Id)
WHERE p.Salario in (
	SELECT 
		MAX(pp.Salario) 
	FROM Pessoa pp 
	GROUP BY pp.DeptId)