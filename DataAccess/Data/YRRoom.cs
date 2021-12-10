using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Data
{
    public class YRRoom
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 점유율
        /// </summary>
        [Required]
        public int Occupancy { get; set; }
        /// <summary>
        /// 일반요금
        /// </summary>
        [Required]
        public double RegularRate { get; set; }
        public string Details { get; set; }
        /// <summary>
        /// 면적
        /// </summary>
        public string SqFt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<YRRoomImage> HotelRoomImages { get; set; }
    }
}
