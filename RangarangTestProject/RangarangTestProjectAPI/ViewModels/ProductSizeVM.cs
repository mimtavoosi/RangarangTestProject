
using RangarangTestProjectDATA.Domain;

namespace RangarangTestProjectAPI.ViewModels
{
    public class ProductSizeVM : BaseEntity
    {
       public int ProductId { get; set; }

        public float Length { get; set; }
        public float Width { get; set; }
        public string Name { get; set; }

        public int? SheetCount { get; set; }
        public int? SheetDimensionId { get; set; }
    }

}
