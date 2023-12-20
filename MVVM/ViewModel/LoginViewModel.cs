using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using FirstPlayerMobileApp.MVVM.Model;
using Newtonsoft.Json;

namespace FirstPlayerMobileApp.MVVM.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private INavigation _Navigation;
        private UserModel _UserModel;
        private bool _LoginSuccess;
        private string _ErrorMessage;
        private bool _ShowErrorMessage;
        private string _LoginMessage;

        public LoginViewModel(INavigation Navigation)
        {
            _UserModel = new UserModel();
            _Navigation = Navigation;
            LoginCommand = new Command(async () => await Login());
        }
        public bool ShowErrorMessage
        {
            get => _ShowErrorMessage;
            set
            {
                if (_ShowErrorMessage != value)
                {
                    _ShowErrorMessage = value;
                    OnPropertyChanged(nameof(ShowErrorMessage));
                }
            }
        }
        public string LoginMessage
        {
            get => _LoginMessage;
            set
            {
                if (_LoginMessage != value)
                {
                    _LoginMessage = value;
                    OnPropertyChanged(nameof(LoginMessage));
                }
            }
        }
        public UserModel UserModel
        {
            get => _UserModel ?? (_UserModel = new UserModel());
            set
            {
                if (_UserModel != value)
                {
                    _UserModel = value;
                    OnPropertyChanged(nameof(UserModel));
                }
            }
        }
        public bool LoginSuccess
        {
            get => _LoginSuccess;
            set
            {
                if (_LoginSuccess != value)
                {
                    _LoginSuccess = value;
                    OnPropertyChanged(nameof(LoginSuccess));
                }
            }
        }
        public string ErrorMessage
        {
            get => _ErrorMessage;
            set
            {
                if (_ErrorMessage != value)
                {
                    _ErrorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }
        public ICommand LoginCommand { get; }

        private async Task Login()
        {
            try
            {
                using (HttpClient HTTPClient = new HttpClient())
                {
                    var JSON = JsonConvert.SerializeObject(new
                    {
                        UserModel.email,
                        UserModel.passwords
                    });

                    HTTPClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Content = new StringContent(JSON, Encoding.UTF8, "application/json");
                    var Response = await HTTPClient.PostAsync("https://localhost:7221/Login", Content);

                    if (Response.IsSuccessStatusCode)
                    {
                        var ResponseContent = await Response.Content.ReadAsStringAsync();
                        try
                        {
                            var ResponseData = JsonConvert.DeserializeObject<JWTToken>(ResponseContent);
                            UserModel.accessToken = ResponseData.value.accessToken;
                            UserModel.email = ResponseData.value.username;
                            UserModel.UsersId = ResponseData.value.usersid;
                            UserModel.nickname = ResponseData.value.usersnickname;
                            UserModel.Usersimagepath = ResponseData.value.usersimagepath;
                            SaveUserDataToLocalStorage();
                            LoginSuccess = true;
                            ShowErrorMessage = false;
                            LoginMessage = "Вход выполнен успешно";
                            await _Navigation.PushModalAsync(new ProfilePage());
                        }
                        catch (JsonException)
                        {
                            LoginSuccess = false;
                            ShowErrorMessage = true;
                            ErrorMessage = "Неверный формат ответа от сервера";
                        }
                    }
                    else
                    {
                        var ResponseContent = await Response.Content.ReadAsStringAsync();
                        LoginSuccess = false;
                        ShowErrorMessage = true;
                        ErrorMessage = $"{ResponseContent}";
                    }
                }
            }
            catch (Exception ex)
            {
                LoginSuccess = false;
                ShowErrorMessage = true;
                ErrorMessage = ex.Message;
            }
        }
        private void SaveUserDataToLocalStorage()
        {
            Preferences.Set("UserId", _UserModel.UsersId.ToString());
            Preferences.Set("Email", _UserModel.email);
            Preferences.Set("Username", _UserModel.nickname);
            Preferences.Set("CreateTime", _UserModel.createtime.ToString());
            Preferences.Set("AccessToken", _UserModel.accessToken);
            // Сохранение изображения в файл
            string ImagePath = Path.Combine(FileSystem.AppDataDirectory, "ProfileUserImage.jpg");
            File.WriteAllBytes(ImagePath, _UserModel.Usersimagepath);
            Preferences.Set("ImagePath", ImagePath);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
