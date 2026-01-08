
using RangarangTestProjectDATA.Domain;

namespace RangarangTestProjectAPI.ViewModels
{
    public class ProductMaterialVM : BaseEntity
    {
        public int ProductId { get; set; }
        public int MaterialId { get; set; }

        public string Name { get; set; }

        public bool IsJeld { get; set; } = false;
        public bool Required { get; set; } = false;
        public bool IsCustomCirculation { get; set; } = false;
        public bool IsCombinedMaterial { get; set; } = false;

        public int? Weight { get; set; }

    }

}
