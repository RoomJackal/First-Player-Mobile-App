namespace FirstPlayerMobileApp;

public partial class SidebarTemplate : ContentView
{
	public SidebarTemplate()
	{
		InitializeComponent();
	}
    //  нопка перехода на страницу авторов проекта
    private async void GoToTheAuthorsPageButton(object sender, EventArgs e)
    {
        var ProjectAuthorsPage = new ProjectAuthorsPage();
        await Navigation.PushModalAsync(ProjectAuthorsPage);
        NavigationPage.SetHasNavigationBar(ProjectAuthorsPage, false);
    }
    //  нопка перехода на страницу авторов проекта
    private async void GoToThePostCreationPageButton(object sender, EventArgs e)
    {
        var PostCreationPage = new PostCreationPage();
        await Navigation.PushModalAsync(PostCreationPage);
        NavigationPage.SetHasNavigationBar(PostCreationPage, false);
    }
}