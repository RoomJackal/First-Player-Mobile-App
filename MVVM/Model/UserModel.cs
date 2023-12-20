using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayerMobileApp.MVVM.Model
{
    public class UserModel
    {
        public string email { get; set; }
        public string nickname { get; set; }
        public string passwords { get; set; }
        public byte[] Usersimagepath { get; set; }
        public int UsersId { get; set; }
        public DateTime createtime { get; set; }
        public string accessToken { get; set; }
    }
}