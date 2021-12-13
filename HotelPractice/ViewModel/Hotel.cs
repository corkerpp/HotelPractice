using HotelPractice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelPractice.ViewModel
{
    public partial class Hotel : DbContext
    {
       [DisplayName("飯店id")]
        public int HotelId { get; set; }
        [DisplayName("飯店名稱")]
        public string HotelName { get; set; }
        [DisplayName("電話")]
        public string HtlTel { get; set; }
        [DisplayName("地址")]
        public string HtlAddress { get; set; }
        [DisplayName("圖片檔名")]
        public string HtlPic { get; set; }
        [DisplayName("圖片")]
        public byte[] HtlByteImage { get; set; }
    }
}