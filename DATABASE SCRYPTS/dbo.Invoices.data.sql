SET IDENTITY_INSERT [dbo].[Invoices] ON
INSERT INTO [dbo].[Invoices] ([Id], [UserId],[Created],[Status]) VALUES (1,2,GETDATE(),'In Progres')
INSERT INTO [dbo].[Invoices] ([Id], [UserId],[Created],[Status]) VALUES (2,2,GETDATE(),'Completed')
INSERT INTO [dbo].[Invoices] ([Id], [UserId],[Created],[Status]) VALUES (3,3,GETDATE(),'In Progres')
INSERT INTO [dbo].[Invoices] ([Id], [UserId],[Created],[Status]) VALUES (4,3,GETDATE(),'Completed')
SET IDENTITY_INSERT [dbo].[Invoices] OFF
