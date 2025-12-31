create procedure sp_UpdateCustomerDetails
@CustomerId int,
@FirstName varchar(50),
@LastName varchar(50),
@Email	varchar(50),
@RegistrationDate date,
@Phone varchar(50)
as
begin
	update CustomerDetails
	Set FirstName = @FirstName,
	LastName = @LastName,
	Email = @Email,
	RegistrationDate = @RegistrationDate,
	Phone = @Phone
	where CustomerId = @CustomerId
end