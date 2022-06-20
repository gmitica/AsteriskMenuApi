using System;
using System.Collections.Generic;
using AutoMapper;
using Data.DTO.Category;
using Data.DTO.Item;
using Data.DTO.Order;
using Data.DTO.OrderItem;
using Data.DTO.Restaurant;
using Data.DTO.RestaurantItemCategory;
using Data.DTO.Table;
using Data.DTO.UserRestaurants;
using Data.DTO.Users;
using Data.Entities;
using Data.Infrastructure;

namespace Data.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurant, RestaurantAddDTO>().ReverseMap();
            CreateMap<User, UserAddDTO>().ReverseMap();
            CreateMap<User, UserAuthenticateDTO>().ReverseMap();
            CreateMap<UserRestaurantAddUpdateDTO, UserRestaurant>()
                .ForMember(x => x.User,
                    c => c.MapFrom(
                        x => new User()
                        {
                            Id = x.UserId,
                            FirstName = x.FirstName,
                            LastName = x.SurName,
                            Email = x.Email,
                            Password = x.Password,
                            isActive = x.IsActive,
                            IsManaged = true
                        })
                )
                .ForMember(x=>x.RestaurantId,c=>c.MapFrom(x=>x.RestaurantId))
                .ReverseMap();
            
           
            CreateMap<Table, TableUpdateDTO>().ReverseMap();
            CreateMap<Table, TableAddDTO>().ReverseMap();
            CreateMap<CategoryUpdateDTO, Category>().ReverseMap();
            CreateMap<CategoryAddDTO, Category>()
                .ForMember(x => x.RestaurantItemCategories,
                    c => c.MapFrom(
                        x => new[] {new RestaurantItemCategory {RestaurantId = x.RestaurantId, CategoryId = x.Id, Active = true}})
                ).ReverseMap();
            CreateMap<ItemUpdateDTO, Item>().ReverseMap();
            CreateMap<ItemAddDTO, Item>() .ReverseMap();
            CreateMap<RestaurantItemCategoryAddUpdateDTO, RestaurantItemCategory>().ReverseMap();
            CreateMap<OrderAddDTO,Order>().ReverseMap();
            CreateMap<OrderUpdateDTO,Order>().ReverseMap();
            CreateMap<OrderItemDTO, OrderItem>().ReverseMap();
            CreateMap<RestaurantItemCategoryOrder, RestaurantItemCategory>().ReverseMap();

        }
    }
}