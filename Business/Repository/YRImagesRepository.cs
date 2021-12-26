using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class YRImagesRepository : IYRImagesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public YRImagesRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<int> CreateYRRoomImage(YRRoomImageDTO imageDTO)
        {



            var image = _mapper.Map<YRRoomImageDTO, YRRoomImage>(imageDTO);

            await _db.YRRoomImages.AddAsync(image);

            return await _db.SaveChangesAsync();

            
            //yrRoom.CreatedDate = DateTime.Now;
            //yrRoom.CreatedBy = "";
            //var addedYRRoom = await _db.YRRooms.AddAsync(yrRoom);
            //await _db.SaveChangesAsync();
            //return _mapper.Map<YRRoom, YRRoomDTO>(addedYRRoom.Entity);
        }

        public async Task<int> DeleteYRRoomImageByImageId(int imageId)
        {
            var image = await _db.YRRoomImages.FindAsync(imageId);
            _db.YRRoomImages.Remove(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteYRRoomImageByImageUrl(string imageUrl)
        {
            var allImages = await _db.YRRoomImages.FirstOrDefaultAsync
                               (x => x.RoomImageUrl.ToLower() == imageUrl.ToLower());
            if (allImages == null)
            {
                return 0;
            }
            _db.YRRoomImages.Remove(allImages);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteYRRoomImageByRoomId(int roomId)
        {
            var imageList = await _db.YRRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
            _db.YRRoomImages.RemoveRange(imageList);
            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<YRRoomImageDTO>> GetYRRoomImages(int roomId)
        {
            return _mapper.Map<IEnumerable<YRRoomImage>, IEnumerable<YRRoomImageDTO>>(
             await _db.YRRoomImages.Where(x => x.RoomId == roomId).ToListAsync());
        }
    }
}
