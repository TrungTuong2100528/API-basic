namespace HocGadgetShopAPI.Models.Dtos.Inventory
{ 
    public class InventoryRequestDto
    {
        #region Properties

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int AvailableQTy { get; set; }

        public int ReOderPoint { get; set; }

        #endregion
    }
}
