namespace FirstPlayerMobileApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        MainPage = new NavigationPage(new MainPage());
        MainPage = new MainPage();
        NavigationPage.SetHasNavigationBar(MainPage, false);
    }
}
