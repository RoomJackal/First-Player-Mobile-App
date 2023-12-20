using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using FirstPlayerMobileApp.Model;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace FirstPlayerMobileApp.ViewModel
{
    class ProjectAuthorsViewModel
    {
        public static string ProjectAuthors { get; private set; }
        static HttpClient HTTPClient = new HttpClient();
        public static async Task GetProjectAuthors()
        {
            try
            {
                HttpResponseMessage Response = await HTTPClient.GetAsync("https://localhost:7221/AutorsProdject");
                string TextContent = await Response.Content.ReadAsStringAsync();
                ProjectAuthors = TextContent;
            }
            catch { }
        }
    }
}
