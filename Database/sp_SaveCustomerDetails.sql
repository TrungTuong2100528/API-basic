Create Procedure sp_SaveCustomerDetails
	@CustomerId int,
	@FirstName varchar(50),
	@LastName varchar(50),
	@Email varchar(50),
	@RegistrationDate Date,
	@Phone varchar(15)
As
begin
	insert CustomerDetails(
		CustomerId,
		FirstName,
		LastName,
		Email,
		RegistrationDate,
		Phone
	)
	values(
		@CustomerId,
		@FirstName,
		@LastName,
		@Email,
		@RegistrationDate,
		@Phone
	)
end