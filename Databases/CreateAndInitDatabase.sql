--- Creation de la base de donn�es
CREATE DATABASE [ManageEmployees]
GO
USE [ManageEmployees]
GO

-- Cr�ation de la table de d�partement

CREATE TABLE [Departments] (
	DepartmentId INT IDENTITY PRIMARY KEY,
	Name VARCHAR(100) NOT NULL UNIQUE,
)
GO

-- Cr�ation de la table employ�

CREATE TABLE [Employees] (
	EmployeeId INT IDENTITY(1000,1) PRIMARY KEY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	Position VARCHAR(50) NOT NULL,
	Address VARCHAR(150),
	PhoneNumber VARCHAR(15),
)
GO

ALTER TABLE [Employees] ADD CONSTRAINT UK_Employees_Email UNIQUE (Email)
GO

-- Liaisson d'un departement et l'employ�
CREATE TABLE [EmployeeDepartments] (
	EmployeeDepartmentId INT IDENTITY PRIMARY KEY,
	EmployeeId INT,
	DepartmentId INT,
)
GO

ALTER TABLE [EmployeeDepartments] ADD CONSTRAINT UK_EmployeeDepartments_EmployeeId_DepartmentId UNIQUE (EmployeeId, DepartmentId)
GO

ALTER TABLE [EmployeeDepartments] ADD CONSTRAINT FK_EmployeeDepartments_Employees_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES [Employees](EmployeeId)
GO

ALTER TABLE [EmployeeDepartments] ADD CONSTRAINT FK_EmployeeDepartments_Departments_DepartmentId FOREIGN KEY (DepartmentId) REFERENCES [Departments](DepartmentId)
GO

-- Cr�ation de la table Pr�sence
CREATE TABLE [Attendances] (
	AttendanceId INT IDENTITY PRIMARY KEY,
	EmployeeId INT ,
	ArrivalTime DATETIME NOT NULL,
	DepartureTime DATETIME,
)
GO

ALTER TABLE [Attendances] ADD CONSTRAINT FK_Attendances_Employees_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES [Employees](EmployeeId)
GO
-- Cr�ation de la table Demande de Cong�s
CREATE TABLE [LeaveRequestStatus] (
	LeaveRequestStatusId INT IDENTITY PRIMARY KEY,
	Status VARCHAR(100) NOT NULL UNIQUE,
)
GO

CREATE TABLE [LeaveRequests] (
	LeaveRequestId INT IDENTITY PRIMARY KEY,
	EmployeeId INT,
	RequestDate DATETIME NOT NULL,
	LeaveRequestStatusId INT,
	StartDate DATETIME NOT NULL,
	EndDate DATETIME NOT NULL,
)
GO

ALTER TABLE [LeaveRequests] ADD CONSTRAINT FK_LeaveRequests_Employees_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES [Employees](EmployeeId)
GO

ALTER TABLE [LeaveRequests] ADD CONSTRAINT FK_LeaveRequests_LeaveRequestStatus_LeaveRequestStatusId FOREIGN KEY (LeaveRequestStatusId) REFERENCES [LeaveRequestStatus](LeaveRequestStatusId)
GO

-- Insertion des statuts de la demande de cong�s

INSERT INTO [LeaveRequestStatus] (Status) VALUES ('Pending') , ('Approved'), ('Rejected')
GO