
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/02/2018 08:59:07
-- Generated from EDMX file: D:\BSUIR\Test\Test\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [JournalDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Data_Driver]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Data] DROP CONSTRAINT [FK_Data_Driver];
GO
IF OBJECT_ID(N'[dbo].[FK_Data_Employees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Data] DROP CONSTRAINT [FK_Data_Employees];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Data]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Data];
GO
IF OBJECT_ID(N'[dbo].[Driver]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Driver];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Data'
CREATE TABLE [dbo].[Data] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nchar(20)  NOT NULL,
    [applicationDateAndTime] nchar(100)  NOT NULL,
    [dateAndTimeOfCarProvision] nchar(100)  NOT NULL,
    [purposesOfUsingAuto] nchar(100)  NOT NULL,
    [route] nchar(200)  NOT NULL,
    [nameDocument] nchar(100)  NOT NULL,
    [id_employe] int  NOT NULL,
    [cargo] nchar(50)  NOT NULL,
    [id_driver] int  NULL,
    [applicationStatus] nchar(20)  NOT NULL
);
GO

-- Creating table 'Driver'
CREATE TABLE [dbo].[Driver] (
    [id_driver] int IDENTITY(1,1) NOT NULL,
    [name] nchar(100)  NOT NULL,
    [mobilePhone] nchar(15)  NOT NULL,
    [status] bit  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [id] int IDENTITY(1,1) NOT NULL,
    [fullName] nchar(30)  NOT NULL,
    [password] nchar(20)  NOT NULL,
    [accessRights] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Data'
ALTER TABLE [dbo].[Data]
ADD CONSTRAINT [PK_Data]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id_driver] in table 'Driver'
ALTER TABLE [dbo].[Driver]
ADD CONSTRAINT [PK_Driver]
    PRIMARY KEY CLUSTERED ([id_driver] ASC);
GO

-- Creating primary key on [id] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_driver] in table 'Data'
ALTER TABLE [dbo].[Data]
ADD CONSTRAINT [FK_Data_Driver]
    FOREIGN KEY ([id_driver])
    REFERENCES [dbo].[Driver]
        ([id_driver])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Data_Driver'
CREATE INDEX [IX_FK_Data_Driver]
ON [dbo].[Data]
    ([id_driver]);
GO

-- Creating foreign key on [id_employe] in table 'Data'
ALTER TABLE [dbo].[Data]
ADD CONSTRAINT [FK_Data_Employees]
    FOREIGN KEY ([id_employe])
    REFERENCES [dbo].[Employees]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Data_Employees'
CREATE INDEX [IX_FK_Data_Employees]
ON [dbo].[Data]
    ([id_employe]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------