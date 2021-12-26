using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IYRRoomRepository
    {
        public Task<YRRoomDTO> CreateYRRoom(YRRoomDTO yrRoomDTO);
        public Task<YRRoomDTO> UpdateYRRoom(int roomId, YRRoomDTO yrRoomDTO);
        public Task<YRRoomDTO> GetYRRoom(int roomId, string checkInDate = null, string checkOutDate = null);
        public Task<int> DeleteYRRoom(int roomId);
        public Task<IEnumerable<YRRoomDTO>> GetAllYRRooms(string checkInDate = null, string checkOutDate = null);
        public Task<YRRoomDTO> IsRoomUnique(string name, int roomId = 0);
        public Task<bool> IsRoomBooked(int RoomId, string checkInDate, string checkOutDate);
    }
}
