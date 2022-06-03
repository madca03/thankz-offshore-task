CREATE PROCEDURE [dbo].[sp_GetUser]
	@UserId uniqueidentifier = null,
	@Username nvarchar(100) = null,
	@Limit int = null,
	@Skip int = null
AS
BEGIN
	if len(@UserId) > 0
	begin
		select * from dbo."User" u where u.UserId = @UserId;
	end
	else if len(@Username) > 0
	begin
		select * from dbo."User" u where lower(u.Username) = lower(@Username);
	end
	else
	begin
		if @Limit > 0 and @Skip > 0
		begin
			select t1.UserId, t1.Username, t1.Firstname, t1.Middlename, t1.Lastname, t1.Contact, 
			t1.DateCreated, t1.DateUpdated
			from (
				select ROW_NUMBER() over (ORDER BY u.UserId) as row_num, *
				from dbo."User" u
			) as t1
			where t1.row_num between @Skip + 1 and @Skip + @Limit;
		end

		else if @Limit > 0 and @Skip <= 0
		begin
			select top (@Limit) * from dbo."User";
		end

		else if @Limit <= 0 and @Skip > 0
		begin
			select t1.UserId, t1.Username, t1.Firstname, t1.Middlename, t1.Lastname, t1.Contact,
			t1.DateCreated, t1.DateUpdated
			from (
				select ROW_NUMBER() over (ORDER BY u.UserId) as row_num, *
				from dbo."User" u
			) as t1
			where t1.row_num > @Skip;
		end

		else
		begin
			select t1.UserId, t1.Username, t1.Firstname, t1.Middlename, t1.Lastname, t1.Contact,
			t1.DateCreated, t1.DateUpdated
			from dbo."User" t1;
		end
	end
END
GO


