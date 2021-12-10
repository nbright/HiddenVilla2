using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IYRImagesRepository
    {
        public Task<int> CreateYRRoomImage(YRRoomImageDTO image);

        public Task<int> DeleteYRRoomImageByImageId(int imageId);
        public Task<int> DeleteYRRoomImageByRoomId(int roomId);

        public Task<int> DeleteYRRoomImageByImageUrl(string imageUrl);

        public Task<IEnumerable<YRRoomImageDTO>> GetYRRoomImages(int roomId);
    }
}
