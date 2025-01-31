using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace HotelManagerMVC.ViewModels
{
    public class ReleaseRoomEditVM
    {
        [DisplayName("Release Room")]
        public int RoomId { get; set; }

        public IEnumerable<SelectListItem> RoomList { get; set; }
    }
}
