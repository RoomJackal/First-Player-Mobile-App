using FirstPlayerMobileApp.MVVM.Model;
using FirstPlayerMobileApp.MVVM.ViewModel;

namespace FirstPlayerMobileApp;

public partial class ProfilePage : FlyoutPage
{
    // ViewModel дл€ работы с данными пользовател€
    private RegistrationViewModel _RegistrationViewModel;
    private LoginViewModel _LoginViewModel;
    public ProfilePage()
	{
		InitializeComponent();
        // »нициализаци€ ViewModel и установка ее в качестве контекста прив€зок
        _RegistrationViewModel = GetRegistrationUserData();
        BindingContext = _RegistrationViewModel;
        _LoginViewModel = GetLoginUserData();
        BindingContext = _LoginViewModel;
    }
    // ћетод дл€ получени€ данных пользовател€ из локального хранилища
    private RegistrationViewModel GetRegistrationUserData()
    {
        try
        {
            // —оздание новой ViewModel дл€ работы с данными пользовател€
            var RegistrationViewModel = new RegistrationViewModel(null);
            // ”становка имени пользовател€ из настроек
            RegistrationViewModel.UserModel = new UserModel
            {
                nickname = Preferences.Get("Username", string.Empty),
            };
            // ѕолучение пути к файлу изображени€ из настроек
            string ImagePath = Preferences.Get("ImagePath", string.Empty);
            // „тение байтов изображени€ из файла и установка в модель пользовател€
            RegistrationViewModel.UserModel.Usersimagepath = File.ReadAllBytes(ImagePath);
            // ѕолучение времени создани€ пользовател€ из настроек
            string CreateTime = Preferences.Get("CreateTime", string.Empty);
            // ѕопытка парсинга времени создани€ пользовател€
            if (DateTime.TryParse(CreateTime, out DateTime _CreateTime))
            {
                RegistrationViewModel.UserModel.createtime = _CreateTime;
            }
            // ¬озвращение созданной ViewModel
            return RegistrationViewModel;
        }
        catch
        { return null; }
    }
    private LoginViewModel GetLoginUserData()
    {
        try
        {
            // —оздание новой ViewModel дл€ работы с данными пользовател€
            var LoginViewModel = new LoginViewModel(null);
            // ”становка имени пользовател€ из настроек
            LoginViewModel.UserModel = new UserModel
            {
                nickname = Preferences.Get("Username", string.Empty),
            };
            // ѕолучение пути к файлу изображени€ из настроек
            string ImagePath = Preferences.Get("ImagePath", string.Empty);
            // „тение байтов изображени€ из файла и установка в модель пользовател€
            LoginViewModel.UserModel.Usersimagepath = File.ReadAllBytes(ImagePath);
            // ѕолучение времени создани€ пользовател€ из настроек
            string CreateTime = Preferences.Get("CreateTime", string.Empty);
            // ѕопытка парсинга времени создани€ пользовател€
            if (DateTime.TryParse(CreateTime, out DateTime _CreateTime))
            {
                LoginViewModel.UserModel.createtime = _CreateTime;
            }
            // ¬озвращение созданной ViewModel
            return LoginViewModel;
        }
        catch { return null; }
    }
    //  нопка, котора€ удал€ет данные из локального хранилища
    private async void OnClearDataClickedButton(object sender, EventArgs e)
    {
        Preferences.Remove("UserId");
        Preferences.Remove("Email");
        Preferences.Remove("Username");
        Preferences.Remove("CreateTime");
        Preferences.Remove("AccessToken");
        Preferences.Remove("ImagePath");
        // јвтоматический переход на главную страницу после удалени€ данных из локального хранилища
        var MainPage = new MainPage();
        await Navigation.PushModalAsync(MainPage);
        NavigationPage.SetHasNavigationBar(MainPage, false);
    }
    //  нопка, котора€ показывает редактирование профил€
    private void ShowEditProfileButton(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        StackLayout StackLayout = (StackLayout)button.Parent;
        VerticalStackLayout EditProfile = StackLayout.FindByName<VerticalStackLayout>("EditProfile");
        EditProfile.IsVisible = !EditProfile.IsVisible;
    }
    //  нопка, котора€ открывает галерею дл€ выбора аватарки
    private async void SelectProfilePhotoButton(object sender, EventArgs e)
    {
        var Result = await MediaPicker.PickPhotoAsync();
        if (Result != null)
        {
            // ќтображение выбранного изображени€ в ImageButton
            Profile.Source = ImageSource.FromFile(Result.FullPath);
        }
    }
}