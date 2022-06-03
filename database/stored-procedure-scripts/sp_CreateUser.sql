CREATE PROCEDURE [dbo].[sp_CreateUser]
	@Username nvarchar(100),
	@Firstname nvarchar(100),
	@Middlename nvarchar(100) = null,
	@Lastname nvarchar(100),
	@Contact nvarchar(100) = null
AS
BEGIN
	if exists (select 1 from dbo."User" u where lower(u.Username) = lower(@Username))
		throw 60002, 'Username already exists', 1

	declare @UserId uniqueidentifier;
	set @UserId = NEWID();

	insert into dbo."User" (UserId, Username, Firstname, Middlename, Lastname, Contact, DateCreated, DateUpdated)
	values (@UserId, @Username, @Firstname, @Middlename, @Lastname, @Contact, GETUTCDATE(), GETUTCDATE());

	select * from dbo."User" u where u.UserId = @UserId;
END
GO


