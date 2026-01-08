
using RangarangTestProjectDATA.Domain;

namespace RangarangTestProjectAPI.ViewModels
{
    public class ProductDeliverVM  : BaseEntity
    {
         public int ProductId { get; set; }

        public string Name { get; set; }

        public bool IsIncreased { get; set; } = true;

        public int StartCirculation { get; set; }
        public int EndCirculation { get; set; }

        public byte PrintSide { get; set; }
        public float Price { get; set; }
        public byte CalcType { get; set; }

    }

}
