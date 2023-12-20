using FirstPlayerMobileApp.Model;
using FirstPlayerMobileApp.MVVM.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FirstPlayerMobileApp.ViewModel
{
    public class PostViewModel: INotifyPropertyChanged
    {
        // Создаем статический экземпляр класса HttpClient
        private static HttpClient HTTPClient = new HttpClient();
        private PostModel _PostModel;
        public PostModel PostModel
        {
            get => _PostModel ?? (_PostModel = new PostModel());
            set
            {
                if (_PostModel != value)
                {
                    _PostModel = value;
                    OnPropertyChanged(nameof(PostModel));
                }
            }
        }
        public ICommand PublishCommand { get; }
        public PostViewModel()
        {
            PostModel.posts = new PostModel.Post(); 
            PublishCommand = new Command(async () => await AddPost());
        }
        private async Task AddPost()
        {
            try
            {
                int UserID = int.TryParse(Preferences.Get("UserId", string.Empty), out int Result) ? Result : 0;
                PostModel.posts.usersId = UserID;
                PostModel.posts.datepublication = DateTime.Now;
                using (var HTTPClient = new HttpClient())
                {
                    var Content = new MultipartFormDataContent();
                    Content.Add(new StringContent(PostModel.posts.datepublication?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""), "datepublication");
                    Content.Add(new StringContent(PostModel.posts.topic ?? ""), "topic");
                    Content.Add(new StringContent(PostModel.posts.posttext ?? ""), "posttext");
                    Content.Add(new StringContent(PostModel.posts.imgformat ?? ""), "imgformat");
                    Content.Add(new StringContent(PostModel.posts.postteg ?? ""), "postteg");
                    Content.Add(new StringContent(PostModel.posts.usersId.ToString()), "usersId");
                    Content.Add(new ByteArrayContent(PostModel.posts.imgpost ?? new byte[0]), "imgpost", "imgpost");
                    var Response = await HTTPClient.PostAsync("https://localhost:7221/Posts", Content);
                }
            }
            catch
            {
            }
        }
        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public static PostModel SelectedPost { get; set; }
        // Эта херня нигде не учавствует но всё работает, странно, но ладно
        public static async Task<PostModel> GetPost(int id)
        {
            try
            {
                HttpResponseMessage Response = await HTTPClient.GetAsync($"https://localhost:7221/Posts/{id}");

                if (Response.IsSuccessStatusCode)
                {
                    string Content = await Response.Content.ReadAsStringAsync();
                    var Post = JsonSerializer.Deserialize<PostModel>(Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    SelectedPost = Post;
                    return Post;
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
        public static async Task<List<PostModel>> GetPosts()
        {
            try
            {
                // Отправляем GET-запрос по указанному URL для получения данных о постах
                HttpResponseMessage Response = await HTTPClient.GetAsync("https://localhost:7221/Posts");
                // Читаем содержимое ответа как строку
                string Content = await Response.Content.ReadAsStringAsync();
                // Проверяем успешность выполнения запроса по статусу ответа
                if (Response.IsSuccessStatusCode)
                {
                    // Десериализуем строку JSON в объект List<PostsModel>
                    var Post = JsonSerializer.Deserialize<List<PostModel>>(Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    // Возвращаем полученный список постов
                    return Post;
                }
                else
                {
                    // Возвращаем null, если запрос завершился неудачно
                    return null;
                }
            }
            catch (Exception)
            {
                // Возвращаем null в случае возникновения исключения при выполнении запроса
                return null;
            }
        }
    }
}
