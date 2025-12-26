namespace HocGadgetShopAPI.Models
{   //B2. Request đến ASP.NET API
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
