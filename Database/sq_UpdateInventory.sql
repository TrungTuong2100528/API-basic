create procedure sp_UpdateInventoryData
	@ProductID int,
	@ProductName varchar(100),
	@AvailableQTy int,
	@ReOderPoint int
as
begin
	update Inventory
	set ProductName = @ProductName,
	AvailableQTy = @AvailableQTy,
	ReOderPoint = @ReOderPoint
	where ProductID = @ProductID
end