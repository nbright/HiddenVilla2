
using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class YRRoomRepository : IYRRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public YRRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<YRRoomDTO> CreateYRRoom(YRRoomDTO yrRoomDTO)
        {
            YRRoom yrRoom = _mapper.Map<YRRoomDTO, YRRoom>(yrRoomDTO);
            yrRoom.CreatedDate = DateTime.Now;
            yrRoom.CreatedBy = "";
            var addedYRRoom = await _db.YRRooms.AddAsync(yrRoom);
            await _db.SaveChangesAsync();
            return _mapper.Map<YRRoom, YRRoomDTO>(addedYRRoom.Entity);
        }

        public async Task<int> DeleteYRRoom(int roomId)
        {
            var roomDetails = await _db.YRRooms.FindAsync(roomId);
            if (roomDetails != null)
            {

                var allimages = await _db.YRRoomImages.Where(x => x.RoomId == roomId).ToListAsync();

                _db.YRRoomImages.RemoveRange(allimages);
                _db.YRRooms.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<YRRoomDTO>> GetAllYRRooms(string checkInDateStr, string checkOutDatestr)
        {
            try
            {
                IEnumerable<YRRoomDTO> yrRoomDTOs =
                            _mapper.Map<IEnumerable<YRRoom>, IEnumerable<YRRoomDTO>>
                            (_db.YRRooms);
                            //(_db.YRRooms.Include(x => x.YRRoomImages));
                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDatestr))
                {
                    foreach (YRRoomDTO yrRoom in yrRoomDTOs)
                    {
                        yrRoom.IsBooked = await IsRoomBooked(yrRoom.Id, checkInDateStr, checkOutDatestr);
                    }
                }
                return yrRoomDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<YRRoomDTO> GetYRRoom(int roomId, string checkInDateStr, string checkOutDatestr)
        {
            try
            {
                //YRRoomDTO yrRoom = _mapper.Map<YRRoom, YRRoomDTO>(
                //    await _db.YRRooms.Include(x => x.YRRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));
                YRRoomDTO yrRoom = _mapper.Map<YRRoom, YRRoomDTO>(
                    await _db.YRRooms.FirstOrDefaultAsync(x => x.Id == roomId));
                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDatestr))
                {
                    yrRoom.IsBooked = await IsRoomBooked(roomId, checkInDateStr, checkOutDatestr);
                }

                return yrRoom;
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }


        public async Task<bool> IsRoomBooked(int RoomId, string checkInDatestr, string checkOutDatestr)
        {
            try
            {
                if (!string.IsNullOrEmpty(checkOutDatestr) && !string.IsNullOrEmpty(checkInDatestr))
                {
                    DateTime checkInDate = DateTime.ParseExact(checkInDatestr, "yyyy. MM. dd", null);
                    DateTime checkOutDate = DateTime.ParseExact(checkOutDatestr, "yyyy. MM. dd", null);
                    //var existingBooking = await _db.RoomOrderDetails.Where(x => x.RoomId == RoomId && x.IsPaymentSuccessful && (IsPaymentSuccessful 나중에 다시고려
                    var existingBooking = await _db.RoomOrderDetails.Where(x => x.RoomId == RoomId &&
                       //check if checkin date that user wants does not fall in between any dates for room that is booked
                       ((checkInDate < x.CheckOutDate && checkInDate.Date >= x.CheckInDate)
                       //check if checkout date that user wants does not fall in between any dates for room that is booked
                       || (checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date <= x.CheckInDate.Date)
                       )).FirstOrDefaultAsync();

                    if (existingBooking != null)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        //if unique returns null else returns the room obj
        public async Task<YRRoomDTO> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
                if (roomId == 0)
                {
                    YRRoomDTO yrRoom = _mapper.Map<YRRoom, YRRoomDTO>(
                        await _db.YRRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));

                    return yrRoom;
                }
                else
                {
                    YRRoomDTO yrRoom = _mapper.Map<YRRoom, YRRoomDTO>(
                        await _db.YRRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()
                        && x.Id != roomId));

                    return yrRoom;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<YRRoomDTO> UpdateYRRoom(int roomId, YRRoomDTO yrRoomDTO)
        {
            try
            {
                if (roomId == yrRoomDTO.Id)
                {
                    //valid
                    YRRoom roomDetails = await _db.YRRooms.FindAsync(roomId);
                    YRRoom room = _mapper.Map<YRRoomDTO, YRRoom>(yrRoomDTO, roomDetails);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updatedRoom = _db.YRRooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<YRRoom, YRRoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    //invalid
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
