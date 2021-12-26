using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Data
{
    public class YRRoomImage
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string RoomImageUrl { get; set; }
        [ForeignKey("RoomId")]
        public virtual YRRoom YRRoom { get; set; }

    }
}