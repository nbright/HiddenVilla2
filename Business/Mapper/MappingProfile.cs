using AutoMapper;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<YRRoomDTO, YRRoom>();
            CreateMap<YRRoom, YRRoomDTO>();

            CreateMap<YRRoomImage, YRRoomImageDTO>().ReverseMap();

            //CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>().ForMember(x => x.YRRoomDTO, opt => opt.MapFrom(c => c.YRRoom));
            //CreateMap<RoomOrderDetailsDTO, RoomOrderDetails>();

        }
    }
}
