create procedure sp_DeleteCustomerDetails
	@CustomerId int
As
Begin
	Delete from CustomerDetails
	where CustomerId = @CustomerId
end