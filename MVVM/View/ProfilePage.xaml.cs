using FirstPlayerMobileApp.MVVM.Model;
using FirstPlayerMobileApp.MVVM.ViewModel;

namespace FirstPlayerMobileApp;

public partial class ProfilePage : FlyoutPage
{
    // ViewModel ��� ������ � ������� ������������
    private RegistrationViewModel _RegistrationViewModel;
    private LoginViewModel _LoginViewModel;
    public ProfilePage()
	{
		InitializeComponent();
        // ������������� ViewModel � ��������� �� � �������� ��������� ��������
        _RegistrationViewModel = GetRegistrationUserData();
        BindingContext = _RegistrationViewModel;
        _LoginViewModel = GetLoginUserData();
        BindingContext = _LoginViewModel;
    }
    // ����� ��� ��������� ������ ������������ �� ���������� ���������
    private RegistrationViewModel GetRegistrationUserData()
    {
        try
        {
            // �������� ����� ViewModel ��� ������ � ������� ������������
            var RegistrationViewModel = new RegistrationViewModel(null);
            // ��������� ����� ������������ �� ��������
            RegistrationViewModel.UserModel = new UserModel
            {
                nickname = Preferences.Get("Username", string.Empty),
            };
            // ��������� ���� � ����� ����������� �� ��������
            string ImagePath = Preferences.Get("ImagePath", string.Empty);
            // ������ ������ ����������� �� ����� � ��������� � ������ ������������
            RegistrationViewModel.UserModel.Usersimagepath = File.ReadAllBytes(ImagePath);
            // ��������� ������� �������� ������������ �� ��������
            string CreateTime = Preferences.Get("CreateTime", string.Empty);
            // ������� �������� ������� �������� ������������
            if (DateTime.TryParse(CreateTime, out DateTime _CreateTime))
            {
                RegistrationViewModel.UserModel.createtime = _CreateTime;
            }
            // ����������� ��������� ViewModel
            return RegistrationViewModel;
        }
        catch
        { return null; }
    }
    private LoginViewModel GetLoginUserData()
    {
        try
        {
            // �������� ����� ViewModel ��� ������ � ������� ������������
            var LoginViewModel = new LoginViewModel(null);
            // ��������� ����� ������������ �� ��������
            LoginViewModel.UserModel = new UserModel
            {
                nickname = Preferences.Get("Username", string.Empty),
            };
            // ��������� ���� � ����� ����������� �� ��������
            string ImagePath = Preferences.Get("ImagePath", string.Empty);
            // ������ ������ ����������� �� ����� � ��������� � ������ ������������
            LoginViewModel.UserModel.Usersimagepath = File.ReadAllBytes(ImagePath);
            // ��������� ������� �������� ������������ �� ��������
            string CreateTime = Preferences.Get("CreateTime", string.Empty);
            // ������� �������� ������� �������� ������������
            if (DateTime.TryParse(CreateTime, out DateTime _CreateTime))
            {
                LoginViewModel.UserModel.createtime = _CreateTime;
            }
            // ����������� ��������� ViewModel
            return LoginViewModel;
        }
        catch { return null; }
    }
    // ������, ������� ������� ������ �� ���������� ���������
    private async void OnClearDataClickedButton(object sender, EventArgs e)
    {
        Preferences.Remove("UserId");
        Preferences.Remove("Email");
        Preferences.Remove("Username");
        Preferences.Remove("CreateTime");
        Preferences.Remove("AccessToken");
        Preferences.Remove("ImagePath");
        // �������������� ������� �� ������� �������� ����� �������� ������ �� ���������� ���������
        var MainPage = new MainPage();
        await Navigation.PushModalAsync(MainPage);
        NavigationPage.SetHasNavigationBar(MainPage, false);
    }
    // ������, ������� ���������� �������������� �������
    private void ShowEditProfileButton(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        StackLayout StackLayout = (StackLayout)button.Parent;
        VerticalStackLayout EditProfile = StackLayout.FindByName<VerticalStackLayout>("EditProfile");
        EditProfile.IsVisible = !EditProfile.IsVisible;
    }
    // ������, ������� ��������� ������� ��� ������ ��������
    private async void SelectProfilePhotoButton(object sender, EventArgs e)
    {
        var Result = await MediaPicker.PickPhotoAsync();
        if (Result != null)
        {
            // ����������� ���������� ����������� � ImageButton
            Profile.Source = ImageSource.FromFile(Result.FullPath);
        }
    }
}