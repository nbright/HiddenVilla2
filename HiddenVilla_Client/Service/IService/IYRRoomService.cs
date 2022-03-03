using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4U_Client.Service.IService
{
    public interface IYRRoomService
    {
        public Task<IEnumerable<YRRoomDTO>> GetYRRooms(string checkInDate, string checkOutDate);
        public Task<YRRoomDTO> GetYRRoomDetails(int roomId, string checkInDate, string checkOutDate);
    }
}
