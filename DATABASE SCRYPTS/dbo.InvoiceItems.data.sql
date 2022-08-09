SET IDENTITY_INSERT [dbo].[InvoiceItems] ON
INSERT INTO [dbo].[InvoiceItems] ([Id], [InvoiceId],[ProductId],[SizeId],[Amount]) VALUES (1, 1, 1, 5,2)
INSERT INTO [dbo].[InvoiceItems] ([Id], [InvoiceId],[ProductId],[SizeId],[Amount]) VALUES (2, 3, 3, 2,2)
SET IDENTITY_INSERT [dbo].[InvoiceItems] OFF
