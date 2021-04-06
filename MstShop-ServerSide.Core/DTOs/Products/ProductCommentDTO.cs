namespace MstShop_ServerSide.Core.DTOs.Products
{
    public class ProductCommentDTO
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string UserFullName { get; set; }

        public string CreateDate { get; set; }

        public string Text { get; set; }
    }
}
