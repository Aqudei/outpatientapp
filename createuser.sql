USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [outpatient]    Script Date: 16/02/2019 3:56:39 PM ******/
CREATE LOGIN [outpatient] WITH PASSWORD='outpatient', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [outpatient] ENABLE
GO

ALTER SERVER ROLE [sysadmin] ADD MEMBER [outpatient]
GO

ALTER SERVER ROLE [diskadmin] ADD MEMBER [outpatient]
GO

ALTER SERVER ROLE [dbcreator] ADD MEMBER [outpatient]
GO

ALTER SERVER ROLE [bulkadmin] ADD MEMBER [outpatient]
GO

