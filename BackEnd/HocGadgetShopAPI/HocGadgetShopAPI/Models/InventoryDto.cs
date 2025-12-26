namespace HocGadgetShopAPI.Models
{
    public class InventoryDto
    {
        #region Properties

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int AvailableQty { get; set; }

        public int ReOderPoint { get; set; }

        #endregion
    }
}
