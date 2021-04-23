namespace MstShop_ServerSide.Core.DTOs.Orders
{
    public class OrderBasketDetail
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public int Price { get; set; }

        public string ImageName { get; set; }

        public int Count { get; set; }
    }
}
