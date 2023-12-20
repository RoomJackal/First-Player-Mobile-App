namespace FirstPlayerMobileApp;

public partial class SidebarTemplate : ContentView
{
	public SidebarTemplate()
	{
		InitializeComponent();
	}
    // ������ �������� �� �������� ������� �������
    private async void GoToTheAuthorsPageButton(object sender, EventArgs e)
    {
        var ProjectAuthorsPage = new ProjectAuthorsPage();
        await Navigation.PushModalAsync(ProjectAuthorsPage);
        NavigationPage.SetHasNavigationBar(ProjectAuthorsPage, false);
    }
    // ������ �������� �� �������� ������� �������
    private async void GoToThePostCreationPageButton(object sender, EventArgs e)
    {
        var PostCreationPage = new PostCreationPage();
        await Navigation.PushModalAsync(PostCreationPage);
        NavigationPage.SetHasNavigationBar(PostCreationPage, false);
    }
}