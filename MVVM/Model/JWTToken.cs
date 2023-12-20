using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayerMobileApp.MVVM.Model
{
    public class JWTToken
    {
        public TokenValue value { get; set; }
        public class TokenValue
        {
            public string accessToken { get; set; }
            public string username { get; set; }
            public int usersid { get; set; }
            public string usersnickname { get; set; }
            public byte[] usersimagepath { get; set; }
        }   
    }
}
