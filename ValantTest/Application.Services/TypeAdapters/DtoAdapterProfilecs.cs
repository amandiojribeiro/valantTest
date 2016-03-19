namespace ValantTest.Application.Services.TypeAdapters
{
    using AutoMapper;
    using Domain.Model;
    using DTO;

    public class DtoAdapterProfile : Profile
    {
        protected override void Configure()
        {
            var mapItem = CreateMap<ItemDto, Item>().ReverseMap();
            var mapNotification = CreateMap<NotificationDto, Notification>().ReverseMap();
        }
    }
}
