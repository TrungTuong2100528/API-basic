namespace HocGadgetShopAPI.Models.Entity
{
    public class InventoryEntity
    {
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int AvailableQty { get; private set; }
        public int ReOrderPoint { get; private set; }

        //constructor không tham số 
        public InventoryEntity()
        {
        }

        public void SetProductInfo(int productId, string productName)
        {
            ProductId = productId;
            ProductName = productName;
        }

        public void SetInitialQuantity(int quantity)
        {
            if (quantity < 0)
                throw new Exception("Số lượng không được âm");

            AvailableQty = quantity;
        }

        public void SetReOrderPoint(int reOrderPoint)
        {
            if (reOrderPoint < 0)
                throw new Exception("ReOrderPoint không được âm");

            ReOrderPoint = reOrderPoint;
        }
    }
}
