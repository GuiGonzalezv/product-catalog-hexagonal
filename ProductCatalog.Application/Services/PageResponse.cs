
namespace ProductCatalog.Application.Services
{
    public class PageResponse<T>
    {
        public int TotalItems { get; set; }         
        public int PageNumber { get; set; }         
        public int PageSize { get; set; }           
        public int TotalPages { get; set; }         
        public IEnumerable<T> Items { get; set; }  

        public PageResponse(IEnumerable<T> items, int totalItems, int pageNumber, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        }
    }

}
