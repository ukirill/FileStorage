Delete From [dbo].[Documents]
GO
Delete From [dbo].[Users]
GO
Insert Into [dbo].[Users](Id, FirstName, LastName, Email, Password, Salt)
Values(NEWID(),N'Андрей', N'Андреев', N'andreev@gmail.com', N'i/uWkO7HyewyknXiATHTRtXQSY+1c0oK/vV7ZeGiHlE=', CONVERT(varbinary(MAX), '0xC8B9B7C41C87B3D8', 1)),
(NEWID(),N'Иван', N'Иванов', N'ivanov@gmail.com', N'zsxXpelI0L9W/o5DC68J/8Z1vAI1t0raL7eL6rVRPYk=', CONVERT(varbinary(MAX), '0xF9E1984018CFC2B3', 1)),
(NEWID(),N'Петр', N'Петров', N'petrov@gmail.com', N'FmlRRtMONTEZT3HQb6eSZ1qRRwGJvUuOY7dF+Mjj5sQ=', CONVERT(varbinary(MAX), '0x8D4F617146C94C25', 1)) 
GO