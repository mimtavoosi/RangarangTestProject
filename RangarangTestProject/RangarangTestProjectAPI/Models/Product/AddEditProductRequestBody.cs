using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectAPI.Models.Product
{
    public class AddEditProductRequestBody
    {
        public int ID { get; set; } = 0;

        [Display(Name = "کد گروه محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ProductGroupId { get; set; }

        [Display(Name = "کد نوع کار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int WorkTypeId { get; set; }

        [Display(Name = "نوع محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public byte ProductType { get; set; }

        [Display(Name = "شرح گردش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Circulation { get; set; }

        [Display(Name = "تعداد کپی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CopyCount { get; set; }

        [Display(Name = "تعداد صفحه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PageCount { get; set; }

        [Display(Name = "طرف چاپی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, byte.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
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

        [Display(Name = "کد ابعاد صفحه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int SheetDimensionId { get; set; }

        public string FileExtension { get; set; }

        public bool IsCmyk { get; set; } = false;

        public float CutMargin { get; set; } = 0;
        public float PrintMargin { get; set; } = 0;

        public bool IsCheckFile { get; set; }
    }
}
