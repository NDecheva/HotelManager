using AutoMapper;
using HotelManager.Data.Entities;
using HotelManager.Shared.Dtos;
using HotelManager.Shared.Enum;
using HotelManagerMVC.ViewModels;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.PortableExecutable;

namespace HotelManagerMVC
{
    internal class AutoMapperConfiguration : Profile

    {
        public AutoMapperConfiguration()

        {

            CreateMap<Client, ClientDto>().ReverseMap();

            CreateMap<ClientDto, ClientEditVM>().ReverseMap();

            CreateMap<ClientDto, ClientDetailsVM>().ReverseMap();

            CreateMap<ClientReservation, ClientReservationDto>().ReverseMap();

            CreateMap<ClientReservationDto, ClientReservationEditVM>().ReverseMap();

            CreateMap<ClientReservationDto, ClientReservationDetailsVM>().ReverseMap();

            CreateMap<Reservation, ReservationDto>().ReverseMap();

            CreateMap<ReservationDto, ReservationEditVM>().ReverseMap();

            CreateMap<ReservationDto, ReservationDetailsVM>().ReverseMap();

            CreateMap<Room, RoomDto>().ReverseMap();

            CreateMap<RoomDto, RoomEditVM>().ReverseMap();
            CreateMap<RoomDto, RoomDetailsVM>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<UserDto, UserEditVM>().ReverseMap();

            CreateMap<UserDto, UserDetailsVM>().ReverseMap();
        }

    }
}



