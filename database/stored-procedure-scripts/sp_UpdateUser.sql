CREATE PROCEDURE [dbo].[sp_UpdateUser]
	@UserId uniqueidentifier,
	@Username nvarchar(100) = null, -- required field
	@Firstname nvarchar(100) = null, -- required field
	@Middlename nvarchar(100) = null, -- optional field
	@Lastname nvarchar(100) = null, -- required field
	@Contact nvarchar(100) = null -- optional field
AS
BEGIN
	if not exists (select 1 from dbo."User" u where u.UserId = @UserId)
		throw 60001, 'User id not found', 1;

	if exists (select 1 from dbo."User" u where u.UserId != @UserId and lower(u.Username) = lower(@Username))
		throw 60002, 'Username already exists', 1

	declare @updatedUsername nvarchar(100);
	declare @updatedFirstname nvarchar(100);
	declare @updatedMiddlename nvarchar(100);
	declare @updatedLastname nvarchar(100);
	declare @updatedContact nvarchar(100);

	select 
		@updatedUsername = u.Username,
		@updatedFirstname = u.Firstname,
		@updatedMiddlename = u.Middlename,
		@updatedLastname = u.Lastname,
		@updatedContact = u.Contact
	from dbo."User" u
	where u.UserId = @UserId;

	if len(@Username) > 0
		set @updatedUsername = @Username

	if len(@Firstname) > 0
		set @updatedFirstname = @Firstname

	if len(@Lastname) > 0
		set @updatedLastname = @Lastname

	if len(@Middlename) > 0
		set @updatedMiddlename = @Middlename

	if len(@Contact) > 0
		set @updatedContact = @Contact

	declare @dtnow datetime;
	set @dtnow = GETUTCDATE();

	update u
	set
	u.Username = @updatedUsername,
	u.Firstname = @updatedFirstname,
	u.Middlename = @updatedMiddlename,
	u.Lastname = @updatedLastname,
	u.Contact = @updatedContact,
	u.DateUpdated = @dtnow
	from dbo."User" u
	where u.UserId = @UserId;

	select * from dbo."User" u where u.UserId = @UserId;
END
GO


