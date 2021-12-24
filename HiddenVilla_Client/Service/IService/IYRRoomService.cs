using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service.IService
{
    public interface IYRRoomService
    {
        public Task<IEnumerable<YRRoomDTO>> GetHotelRooms(string checkInDate, string checkOutDate);
        public Task<YRRoomDTO> GetHotelRoomDetails(int roomId, string checkInDate, string checkOutDate);
    }
}
