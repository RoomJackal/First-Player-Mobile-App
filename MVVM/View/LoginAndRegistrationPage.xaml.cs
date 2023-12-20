using FirstPlayerMobileApp.MVVM.ViewModel;

namespace FirstPlayerMobileApp;

public partial class LoginAndRegistrationPage : FlyoutPage
{
    RegistratoinAndLoginViewModel _RegistratoinAndLoginViewModel;
    public LoginAndRegistrationPage()
    {
        InitializeComponent();
        // � ����������� ������� Navigation, ����� ��������� ������� ������� �� �������� �������
        _RegistratoinAndLoginViewModel = new RegistratoinAndLoginViewModel(Navigation);
        BindingContext = _RegistratoinAndLoginViewModel;
    }
    // ������, ������� ���������� ����� ����� � �������
    private void ShowLoginFormButton(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        StackLayout StackLayout = (StackLayout)button.Parent;
        VerticalStackLayout Login = StackLayout.FindByName<VerticalStackLayout>("Login");
        VerticalStackLayout Registration = StackLayout.FindByName<VerticalStackLayout>("Registration");
        Login.IsVisible = !Login.IsVisible;
        Registration.IsVisible = false;
    }
    // ������, ������� ���������� ����� ����������� ������������
    private void ShowRegistrationFormButton(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        StackLayout StackLayout = (StackLayout)button.Parent;
        VerticalStackLayout Registration = StackLayout.FindByName<VerticalStackLayout>("Registration");
        VerticalStackLayout Login = StackLayout.FindByName<VerticalStackLayout>("Login");
        Registration.IsVisible = !Registration.IsVisible;
        Login.IsVisible = false;
    }
}