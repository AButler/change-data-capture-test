SET NOCOUNT ON

USE [master]
GO

-- Drop and recreate database
IF DB_ID('UserDatabase') IS NOT NULL
BEGIN
  ALTER DATABASE [UserDatabase] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
  DROP DATABASE [UserDatabase]
END
GO

CREATE DATABASE [UserDatabase]
GO

USE [UserDatabase]
GO

-- Create Tables

-- Table: User Types
CREATE TABLE [dbo].[UserTypes](
  [Id] [int] PRIMARY KEY IDENTITY NOT NULL,
  [Code] [nvarchar](5) NOT NULL,
  [Name] [nvarchar](100) NOT NULL
)
GO

-- Table: Users
CREATE TABLE [dbo].[Users](
  [Id] [int] PRIMARY KEY IDENTITY NOT NULL,
  [UserId] [nvarchar](200) NOT NULL,
  [FirstName] [nvarchar](100) NOT NULL,
  [LastName] [nvarchar](100) NOT NULL,
  [DisplayName] [nvarchar](200) NOT NULL,
  [EmailAddress] [nvarchar](100) NOT NULL,
  [UserTypeId] int NOT NULL FOREIGN KEY REFERENCES UserTypes(Id),
  [IsDisabled] bit NOT NULL DEFAULT 0
)
GO

-- Table: Systems
CREATE TABLE [dbo].[Systems](
	[Id] [int] PRIMARY KEY IDENTITY NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
)
GO

-- Table: SystemAccess
CREATE TABLE [dbo].[SystemAccess](
	[Id] [int] PRIMARY KEY IDENTITY NOT NULL,
	[UserId] int NOT NULL FOREIGN KEY REFERENCES Users(Id),
  [SystemId] int NOT NULL FOREIGN KEY REFERENCES Systems(Id),
  [Start] datetime2 NOT NULL,
  [End] datetime2 NULL
)
GO

-- Seed data
INSERT INTO UserTypes ([Code], [Name])
VALUES (
  'S01', 'Standard'
), 
(
  'A01', 'Administrator'
)
GO

INSERT INTO Users ([UserId], [FirstName], [LastName], [DisplayName], [EmailAddress], [UserTypeId])
VALUES (
  'bob.bobertson', 'Bobert', 'Bobertson', 'Bob Bobertson', 'bob.bobertson@sample.com', (SELECT TOP 1 Id FROM [dbo].[UserTypes] ut WHERE ut.[Code] = 'A01')
),
(
  'bobetta.bobertson', 'Bobetta', 'Bobertson', 'Bobetta Bobertson', 'bobetta.bobertson@sample.com', (SELECT TOP 1 Id FROM [dbo].[UserTypes] ut WHERE ut.[Code] = 'S01')
)

INSERT INTO Systems([Name])
VALUES ( 'Admin Site' ),
( 'Microsoft 365' ),
( 'Google Analytics' ),
( 'Microsoft Azure Portal' ),
( 'HR System' )