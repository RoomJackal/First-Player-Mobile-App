namespace FirstPlayerMobileApp;

public partial class HeaderTemplate : ContentView
{
	public HeaderTemplate()
	{
		InitializeComponent();
	}
    //  нопка перехода на страницу профил€
    private async void GoToTheProfileButton(object sender, EventArgs e)
    {
        var ProfilePage = new ProfilePage();
        await Navigation.PushModalAsync(ProfilePage);
        NavigationPage.SetHasNavigationBar(ProfilePage, false);
    }
    //  нопка перехода на страницу с постами
    private async void GoToTheMainPageButton(object sender, EventArgs e)
    {
        var MainPage = new MainPage();
        await Navigation.PushModalAsync(MainPage);
        NavigationPage.SetHasNavigationBar(MainPage, false);
    }
    //  нопка перехода на страницу со входом и регистрацией
    private async void GoToTheLoginAndRegistrationPageButton(object sender, EventArgs e)
    {
        var LoginAndRegistrationPage = new LoginAndRegistrationPage();
        await Navigation.PushModalAsync(LoginAndRegistrationPage);
        NavigationPage.SetHasNavigationBar(LoginAndRegistrationPage, false);
    }
}