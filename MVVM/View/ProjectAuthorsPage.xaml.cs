using FirstPlayerMobileApp.ViewModel;

namespace FirstPlayerMobileApp;

public partial class ProjectAuthorsPage : FlyoutPage
{
    public ProjectAuthorsPage()
	{
		InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetProjectAuthorsLabel();
    }
    private async Task GetProjectAuthorsLabel()
    {
        try
        {
            await ProjectAuthorsViewModel.GetProjectAuthors();
            var ProjectAuthors = ProjectAuthorsViewModel.ProjectAuthors;
            ProjectAuthorsLabel.Text = ProjectAuthors;
        }
        catch (Exception ex)
        {
            ProjectAuthorsLabel.Text = "Ошибка при получении данных: " + ex.Message;
        }
    }
    public async void GoBackToTheAuthorsPageButton()
    {
        await Navigation.PopModalAsync();
    }
}