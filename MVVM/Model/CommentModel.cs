using FirstPlayerMobileApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayerMobileApp
{
    public class CommentModel
    {
        public class Comment
        {
            public int id { get; set; }
            public DateTime? datePublication { get; set; }
            public string text { get; set; }
            public int usersId { get; set; }
            public object user { get; set; }
            public int postsId { get; set; }
            public object post { get; set; }
        }
        public Comment comment { get; set; }
        public string commentAutors { get; set; }
        public byte[] imageAutor { get; set; }
    }
}
