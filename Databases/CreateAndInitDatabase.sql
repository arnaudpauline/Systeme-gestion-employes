CREATE DATABASE [ManageEmployees]
GO

USE [ManageEmployees]
GO

-- Création du département
CREATE TABLE Departments 
(
	DepartmentId INT IDENTITY PRIMARY KEY,
	Name VARCHAR(50) NOT NULL,
	Address VARCHAR (150),
	Description VARCHAR(300),
)
GO

ALTER TABLE Departments ADD CONSTRAINT UK_Departments_Name UNIQUE (Name)
GO

-- Création de l'employé

CREATE TABLE Employees 
(
	EmployeeId INT IDENTITY(1000, 1) PRIMARY KEY,
	FistName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	BirthDate DATETIME NOT NULL,
	Email VARCHAR(50) NOT NULL,
	PhoneNumber VARCHAR(13) ,
	Position VARCHAR(150),
)
GO

ALTER TABLE Employees ADD CONSTRAINT UK_Employees_Email UNIQUE (Email)
GO

-- Création Association Employé et departement
CREATE TABLE EmployeeDepartments 
(
	EmployeeDepartmentId INT IDENTITY PRIMARY KEY,
	EmployeeId INT NOT NULL,
	DepartmentId INT NOT NULL,
)
GO

ALTER TABLE EmployeeDepartments ADD CONSTRAINT FK_EmployeeDepartments_EmployeeId FOREIGN KEY  (EmployeeId) REFERENCES Employees (EmployeeId)
GO

ALTER TABLE EmployeeDepartments ADD CONSTRAINT FK_EmployeeDepartments_DepartmentId FOREIGN KEY  (DepartmentId) REFERENCES Departments (DepartmentId)
GO


-- Création Présence
CREATE TABLE Attendances 
(
	AttendanceId INT IDENTITY PRIMARY KEY,
	EmployeeId INT NOT NULL,
	ArrivingDate DATETIME NOT NULL,
	DepartureDate DATETIME,
)
GO

ALTER TABLE Attendances ADD CONSTRAINT FK_Attendances_EmployeeId FOREIGN KEY (EmployeeId) REFERENCES Employees (EmployeeId)
GO

--

CREATE TABLE LeaveRequestStatus
(
	LeaveRequestStatusId INT IDENTITY PRIMARY KEY,
	Status VARCHAR(50) NOT NULL UNIQUE,
)
GO

INSERT INTO LeaveRequestStatus (Status) VALUES ('Pending'), ('Accepted'),('Rejected')
GO

-- Création de demande de congés

CREATE TABLE LeaveRequests 
(
	LeaveRequestId INT IDENTITY PRIMARY KEY,
	EmployeeId INT NOT NULL,
	RequestDate DATETIME NOT NULL,
	StartDate DATETIME NOT NULL,
	EndDate DATETIME NOT NULL,
	LeaveRequestStatusId INT NOT NULL,
)
GO

ALTER TABLE LeaveRequests ADD CONSTRAINT FK_LeaveRequests_EmployeeId FOREIGN KEY  (EmployeeId) REFERENCES Employees (EmployeeId)
GO

ALTER TABLE LeaveRequests ADD CONSTRAINT FK_LeaveRequests_LeaveRequestStatusId FOREIGN KEY  (LeaveRequestStatusId) REFERENCES LeaveRequestStatus (LeaveRequestStatusId)
GO
