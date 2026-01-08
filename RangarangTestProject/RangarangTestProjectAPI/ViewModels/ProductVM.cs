
using RangarangTestProjectDATA.Domain;

namespace RangarangTestProjectAPI.ViewModels
{
    public class ProductVM : BaseEntity
    {
        public int ProductGroupId { get; set; }
        public int WorkTypeId { get; set; }
        public byte ProductType { get; set; }

        public string Circulation { get; set; }
        public string CopyCount { get; set; }
        public string PageCount { get; set; }

        public byte PrintSide { get; set; }

        public bool IsDelete { get; set; } = false;
        public bool IsCalculatePrice { get; set; } = true;
        public bool IsCustomCirculation { get; set; } = false;
        public bool IsCustomSize { get; set; } = false;
        public bool IsCustomPage { get; set; } = false;

        public int? MinCirculation { get; set; }
        public int? MaxCirculation { get; set; }
        public int? MinPage { get; set; }
        public int? MaxPage { get; set; }

        public float? MinWidth { get; set; }
        public float? MaxWidth { get; set; }
        public float? MinLength { get; set; }
        public float? MaxLength { get; set; }

        public int SheetDimensionId { get; set; }

        public string FileExtension { get; set; }

        public bool IsCmyk { get; set; } = false;
        public float CutMargin { get; set; } = 0;
        public float PrintMargin { get; set; } = 0;

        public bool IsCheckFile { get; set; }

    }
}
