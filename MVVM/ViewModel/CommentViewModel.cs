using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FirstPlayerMobileApp.MVVM.ViewModel
{
    class CommentViewModel
    {
        private static readonly HttpClient HTTPClient = new HttpClient();
        public static async Task<List<CommentModel>> GetComments(int postsId)
        {
            try
            {
                HttpResponseMessage Response = await HTTPClient.GetAsync($"https://localhost:7221/Comments?postId={postsId}");
                string Content = await Response.Content.ReadAsStringAsync();

                if (Response.IsSuccessStatusCode)
                {
                    var Comments = JsonSerializer.Deserialize<List<CommentModel>>(Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Comments;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

