create procedure sp_searchInventory
	@productName varchar(50) = NULL
as
Begin
	select * from Inventory
	where ProductName Like '%' + @productName + '%'
end