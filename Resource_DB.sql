if not exists (select * from sys.databases where name='CustomResource')
	CREATE DATABASE CustomResource;
GO
USE CustomResource;

if not exists (select * from sysobjects where name='Resource')
	CREATE TABLE Resource(
		[resource_id] [int] IDENTITY(1,1) NOT NULL,
		[resource_type][nvarchar](255),
		[resource_name] [nvarchar](50) NOT NULL,
		[culture_code] [nvarchar](50) NULL,
		[resource_value] [nvarchar](100) NULL,
	CONSTRAINT [pk_resource] PRIMARY KEY (resource_id));
GO

SET IDENTITY_INSERT Resource ON;
 INSERT INTO Resource([resource_id], [resource_type], [resource_name], [culture_code], [resource_value]) VALUES (1, N'CommonTerms', N'helloWorld', null, N'Hello World! Default');
 INSERT INTO Resource([resource_id], [resource_type], [resource_name], [culture_code], [resource_value]) VALUES (2, N'CommonTerms', N'helloWorld', N'en-GB', N'Hello World! en-GB');
 INSERT INTO Resource([resource_id], [resource_type], [resource_name], [culture_code], [resource_value]) VALUES (4, N'CommonTerms', N'helloWorld', N'es', N'Hola Mundo! es');
 INSERT INTO Resource([resource_id], [resource_type], [resource_name], [culture_code], [resource_value]) VALUES (5, N'CommonTerms', N'helloWorld', N'es-ES', N'Hola Mundo! es-ES');
SET IDENTITY_INSERT Resource OFF;