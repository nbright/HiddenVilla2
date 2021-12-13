using Business.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    public class YRRoomController : Controller
    {
        private readonly IYRRoomRepository _yrRoomRepository;
        public YRRoomController(IYRRoomRepository yrRoomRepository)
        {
            _yrRoomRepository = yrRoomRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetYRRooms(string checkInDate = null, string checkOutDate = null)
        {
            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be supplied"
                });
            }
            if (!DateTime.TryParseExact(checkInDate, "yyyy. MM. dd", new CultureInfo("ko-KR"), DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckIn date format. valid format will be yyyy/MM/dd"
                });
            }
            if (!DateTime.TryParseExact(checkOutDate, "yyyy. MM. dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckOut date format. valid format will be yyyy/MM/dd"
                });
            }

            var allRooms = await _yrRoomRepository.GetAllYRRooms(checkInDate, checkOutDate);
            return Ok(allRooms);
        }
        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetYRRoom(int? roomId)
        {
            //에러처리
            if (roomId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title="",
                    ErrorMessage = "Invalid Room Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var roomDetails = await _yrRoomRepository.GetYRRoom(roomId.Value);
            if (roomDetails == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Room Detail",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(roomDetails);
        }
    }
}
