using P4U_Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace P4U_Client.Service
{
    public class YRRoomService : IYRRoomService
    {
        private readonly HttpClient _client;

        public YRRoomService(HttpClient client)
        {
            _client = client;
        }

        public async Task<YRRoomDTO> GetYRRoomDetails(int roomId, string checkInDate, string checkOutDate)
        {
            var response = await _client.GetAsync($"api/yrroom/{roomId}?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var room = JsonConvert.DeserializeObject<YRRoomDTO>(content);
                return room;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }

        }

        public async Task<IEnumerable<YRRoomDTO>> GetYRRooms(string checkInDate, string checkOutDate)
        {
            var response = await _client.GetAsync($"api/yrroom?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            var content = await response.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<IEnumerable<YRRoomDTO>>(content);
            return rooms;
        }
    }
}
