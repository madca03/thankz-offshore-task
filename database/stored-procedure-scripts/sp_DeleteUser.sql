CREATE PROCEDURE [dbo].[sp_DeleteUser]
	@UserId uniqueidentifier
AS
BEGIN
	if not exists (select 1 from dbo."User" u where u.UserId = @UserId)
		throw 60001, 'User id not found', 1;

	declare @Username nvarchar(100);
	declare @Firstname nvarchar(100);
	declare @Middlename nvarchar(100);
	declare @Lastname nvarchar(100);
	declare @Contact nvarchar(100);
	declare @DateCreated datetime;
	declare @DateUpdated datetime;

	select 
		@Username = u.Username,
		@Firstname = u.Firstname,
		@Middlename = u.Middlename,
		@Lastname = u.Lastname,
		@Contact = u.Contact,
		@DateCreated = u.DateCreated,
		@DateUpdated = u.DateUpdated
	from dbo."User" u
	where u.UserId = @UserId;

	delete from dbo."User"
	where UserId = @UserId;

	select @UserId as UserId,
	@Username as Username,
	@Firstname as Firstname,
	@Middlename as Middlename,
	@Lastname as Lastname,
	@Contact as Contact,
	@DateCreated as DateCreated,
	@DateUpdated as DateUpdated;
END
GO


