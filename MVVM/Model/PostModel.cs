using SkiaSharp;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayerMobileApp.MVVM.Model
{
    public class PostModel
    {
        public class Post
        {
            public int id { get; set; }
            public DateTime? datepublication { get; set; }
            public string topic { get; set; }
            public string posttext { get; set; }
            public byte[] imgpost { get; set; }
            public string imgformat { get; set; }
            public string postteg { get; set; }
            public int postrating { get; set; }
            public int postviews { get; set; }
            public int postcomments { get; set; }
            public int usersId { get; set; }
            public object users { get; set; }
        }
        public Post posts { get; set; }
        public string postAutors { get; set; }
        public byte[] autorsImg { get; set; }
        public int autorsId { get; set; }
    }
}
