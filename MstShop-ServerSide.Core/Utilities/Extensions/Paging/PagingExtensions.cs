using MstShop_ServerSide.Core.DTOs.Paging;
using System.Linq;

namespace MstShop_ServerSide.Core.Utilities.Extensions.Paging
{
    public static class PagingExtensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> queryable, BasePaging pager)
        {
            return queryable.Skip(pager.SkipEntity).Take(pager.TakeEntity);
        }
    }
}
