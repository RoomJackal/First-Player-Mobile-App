using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using FirstPlayerMobileApp.MVVM.Model;
using Newtonsoft.Json;

namespace FirstPlayerMobileApp.MVVM.ViewModel
{
    public class RegistrationViewModel: INotifyPropertyChanged
    {
        // Навигация между страницами
        private INavigation _Navigation;
        // Модель пользователя
        private UserModel _UserModel;
        // Флаг успешной регистрации
        private bool _RegistrationSuccess;
        // Сообщение об ошибке
        private string _ErrorMessage;
        // Флаг отображения сообщения о успешной регистрации
        private bool _ShowRegistrationMessage;
        // Флаг отображения сообщения об ошибке
        private bool _ShowErrorMessage;
        // Сообщение о регистрации
        private string _RegistrationMessage;
        // Конструктор, инициализирующий модель пользователя и команду регистрации
        public RegistrationViewModel(INavigation Navigation)
        {
            _UserModel = new UserModel();
            _Navigation = Navigation;
            RegisterCommand = new Command(async () => await Register());
        }
        // Флаг отображения сообщения о успешной регистрации
        public bool ShowRegistrationMessage
        {
            get => _ShowRegistrationMessage;
            set
            {
                if (_ShowRegistrationMessage != value)
                {
                    _ShowRegistrationMessage = value;
                    OnPropertyChanged(nameof(ShowRegistrationMessage));
                }
            }
        }
        // Флаг отображения сообщения об ошибке
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
        // Сообщение о регистрации
        public string RegistrationMessage
        {
            get => _RegistrationMessage;
            set
            {
                if (_RegistrationMessage != value)
                {
                    _RegistrationMessage = value;
                    OnPropertyChanged(nameof(RegistrationMessage));
                }
            }
        }
        // Модель пользователя
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
        // Флаг успешной регистрации
        public bool RegistrationSuccess
        {
            get => _RegistrationSuccess;
            set
            {
                if (_RegistrationSuccess != value)
                {
                    _RegistrationSuccess = value;
                    OnPropertyChanged(nameof(RegistrationSuccess));
                }
            }
        }
        // Сообщение об ошибке
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
        // Команда для выполнения регистрации
        public ICommand RegisterCommand { get; }
        // Метод регистрации пользователя
        private async Task Register()
        {
            try
            {
                // Установка времени создания пользователя
                UserModel.createtime = DateTime.Now;
                // Использование HttpClient для отправки запроса на сервер
                using (HttpClient HTTPClient = new HttpClient())
                {
                    // Сериализация модели пользователя в JSON
                    var JSON = JsonConvert.SerializeObject(new
                    {
                        UserModel.email,
                        UserModel.nickname,
                        UserModel.passwords,
                        UserModel.createtime
                    });
                    // Настройка заголовков запроса
                    HTTPClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // Создание контента запроса
                    var Content = new StringContent(JSON, Encoding.UTF8, "application/json");
                    // Отправка POST-запроса на сервер
                    var Response = await HTTPClient.PostAsync("https://localhost:7221/Registration", Content);

                    if (Response.IsSuccessStatusCode)
                    {
                        // Обработка успешного ответа от сервера
                        var ResponseContent = await Response.Content.ReadAsStringAsync();
                        try
                        {
                            // Десериализация ответа сервера с токеном
                            var ResponseData = JsonConvert.DeserializeObject <JWTToken>(ResponseContent);
                            // Обновление данных пользователя
                            UserModel.accessToken = ResponseData.value.accessToken;
                            UserModel.email = ResponseData.value.username;
                            UserModel.UsersId = ResponseData.value.usersid;
                            UserModel.nickname = ResponseData.value.usersnickname;
                            UserModel.Usersimagepath = ResponseData.value.usersimagepath;
                            // Сохранение данных пользователя в локальное хранилище
                            SaveUserDataToLocalStorage();
                            // Успешная регистрация, отображение сообщения и переход на страницу профиля
                            RegistrationSuccess = true;
                            ShowRegistrationMessage = true;
                            RegistrationMessage = "Пользователь успешно зарегистрирован";
                            await _Navigation.PushModalAsync(new ProfilePage());
                        }
                        catch (JsonException)
                        {
                            // Ошибка десериализации ответа от сервера
                            RegistrationSuccess = false;
                            ShowErrorMessage = true;
                            ErrorMessage = "Неверный формат ответа от сервера";
                        }
                    }
                    else
                    {
                        // Обработка ошибочного ответа от сервера
                        var ResponseContent = await Response.Content.ReadAsStringAsync();
                        RegistrationSuccess = false;
                        ShowErrorMessage = true;
                        ErrorMessage = $"{ResponseContent}";
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений при выполнении регистрации
                RegistrationSuccess = false;
                ShowErrorMessage = true;
                ErrorMessage = ex.Message;
            }
        }
        // Метод сохранения данных пользователя в локальное хранилище
        public void SaveUserDataToLocalStorage()
        {
            Preferences.Set("UserId", _UserModel.UsersId.ToString());
            Preferences.Set("Email", _UserModel.email);
            Preferences.Set("Username", _UserModel.nickname);
            //Preferences.Set("ImagePath", Convert.ToBase64String(_UserModel.Usersimagepath));
            Preferences.Set("CreateTime", _UserModel.createtime.ToString());
            Preferences.Set("AccessToken", _UserModel.accessToken);
            // Сохранение изображения в файл
            string ImagePath = Path.Combine(FileSystem.AppDataDirectory, "ProfileUserImage.jpg");
            File.WriteAllBytes(ImagePath, _UserModel.Usersimagepath);
            Preferences.Set("ImagePath", ImagePath);
        }
        // Событие изменения свойств для обновления привязок
        public event PropertyChangedEventHandler PropertyChanged;
        // Метод вызывает событие изменения свойства
        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        internal void GetRegistrationUserData()
        {
            throw new NotImplementedException();
        }
    }
}