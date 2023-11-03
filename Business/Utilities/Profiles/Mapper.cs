using Entities.DTOs.SubCategoryDTOs;

namespace Business.Utilities.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CategoryPostDTO, Category>();
            CreateMap<Category, CategoryGetDTO>();
            CreateMap<CategoryUpdateDTO, Category>();

            CreateMap<AnimalPostDTO, Animal>();
            CreateMap<Animal, AnimalGetDTO>();
            CreateMap<AnimalUpdateDTO, Animal>();

            CreateMap<ProductPostDTO, Product>();
            CreateMap<Product, ProductGetDTO>();
            CreateMap<ProductUpdateDTO, Product>();

            CreateMap<SubCategoryPostDTO, SubCategory>();
            CreateMap<SubCategory, SubCategoryGetDTO>();
            CreateMap<SubCategoryUpdateDTO, SubCategory>();

            CreateMap<ServicePostDTO, Service>();
            CreateMap<ServiceUpdateDTO, Service>();
            CreateMap<Service, ServiceGetDTO>();

            CreateMap<SettingPostDTO, Setting>();
            CreateMap<Setting, SettingGetDTO>();

            CreateMap<TestimonialPostDTO, Testimonial>();
            CreateMap<Testimonial, TestimonialGetDTO>();

            CreateMap<OrderPostDTO, Order>();
            CreateMap<Order, OrderGetDTO>();
        }
    }
}
