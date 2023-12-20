namespace FirstPlayerMobileApp;

public partial class HeaderTemplate : ContentView
{
	public HeaderTemplate()
	{
		InitializeComponent();
	}
    // ������ �������� �� �������� �������
    private async void GoToTheProfileButton(object sender, EventArgs e)
    {
        var ProfilePage = new ProfilePage();
        await Navigation.PushModalAsync(ProfilePage);
        NavigationPage.SetHasNavigationBar(ProfilePage, false);
    }
    // ������ �������� �� �������� � �������
    private async void GoToTheMainPageButton(object sender, EventArgs e)
    {
        var MainPage = new MainPage();
        await Navigation.PushModalAsync(MainPage);
        NavigationPage.SetHasNavigationBar(MainPage, false);
    }
    // ������ �������� �� �������� �� ������ � ������������
    private async void GoToTheLoginAndRegistrationPageButton(object sender, EventArgs e)
    {
        var LoginAndRegistrationPage = new LoginAndRegistrationPage();
        await Navigation.PushModalAsync(LoginAndRegistrationPage);
        NavigationPage.SetHasNavigationBar(LoginAndRegistrationPage, false);
    }
}