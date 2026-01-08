using RangarangTestProjectAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch.Adapters;
using Microsoft.JSInterop;
using RangarangTestProjectDATA.ResultObjects;
using RangarangTestProjectDATA.Domain;



namespace RangarangTestProjectAPI.Tools
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(ListResultObject<>), typeof(ListResultObject<>));
            CreateMap(typeof(RowResultObject<>), typeof(RowResultObject<>));
            CreateMap(typeof(BitResultObject), typeof(BitResultObject));

            CreateMap<Product, ProductVM>();
            CreateMap<ProductAdt, ProductAdtVM>();
            CreateMap<ProductAdtPrice, ProductAdtPriceVM>();
            CreateMap<ProductAdtType, ProductAdtTypeVM>();
            CreateMap<ProductSize, ProductSizeVM>();
            CreateMap<ProductPrice, ProductPriceVM>();
            CreateMap<ProductPrintKind, ProductPrintKindVM>();
            CreateMap<ProductDeliver, ProductDeliverVM>();
            CreateMap<ProductDeliverSize, ProductDeliverSizeVM>();
            CreateMap<User, UserVM>();
            CreateMap<ProductMaterial, ProductMaterialVM>();
            CreateMap<ProductMaterialAttribute, ProductMaterialAttributeVM>();


//            CreateMap<SMSMessage, SMSMessageVM>()
//.ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
//    ;

        }
    }

}
