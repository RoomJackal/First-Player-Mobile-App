using FirstPlayerMobileApp.MVVM.Model;
using FirstPlayerMobileApp.ViewModel;
using System.Collections.ObjectModel;

namespace FirstPlayerMobileApp;

public partial class MainPage : FlyoutPage
{
    private ObservableCollection<PostModel> PostsList;
    public MainPage()
    {
        InitializeComponent();
        // ������������� BindingContext �������� ���������� ������ ��� ������ ����
        BindingContext = this;
        // ������� ����� ��������� ObservableCollection ��� �������� ������
        PostsList = new ObservableCollection<PostModel>();
        // ������������� �������� ������ ��� PostListView
        PostListView.ItemsSource = PostsList;
        // �������� ����� ��� �������� ������
        LoadPosts();
    }
    private async void LoadPosts()
    {
        // �������� ����������� ����� GetPosts() �� ������ PostViewModel ��� ��������� ������ ������
        var Posts = await PostViewModel.GetPosts();
        // ���������, ��� ������ ������ �� ���� � �� �������� null
        if (Posts != null && Posts.Any())
        {
            // ��������� ������ ���� �� ����������� ������ � ��������� PostsList
            foreach (var Post in Posts)
            {
                PostsList.Add(Post);
            }
        }
    }
    // ������ �������� �� �������� ������������ � �����
    private async void GoToTheCommentPostPageButton(object sender, EventArgs e)
    {
        var Button = (ImageButton)sender;
        var SelectedPost = Button?.BindingContext as PostModel;
        if (SelectedPost != null)
        {
            var CommentPostPage = new CommentPostPage(SelectedPost);
            await Navigation.PushModalAsync(CommentPostPage);
            NavigationPage.SetHasNavigationBar(CommentPostPage, false);
        }
    }
}