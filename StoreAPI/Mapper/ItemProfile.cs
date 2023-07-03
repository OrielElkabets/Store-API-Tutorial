using AutoMapper;
using StoreAPI.DTO;
using StoreServices.Data.Entity;

namespace StoreAPI.Mapper
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemDTO, ItemEO>().ReverseMap();
            CreateMap<NewItemDTO, ItemEO>();
        }
    }
}
