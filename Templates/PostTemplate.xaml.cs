using FirstPlayerMobileApp.MVVM.Model;
using FirstPlayerMobileApp.ViewModel;
using System.Collections.ObjectModel;
using static FirstPlayerMobileApp.MVVM.Model.PostModel;

namespace FirstPlayerMobileApp;

public partial class PostTemplate : ContentView
{
    // Объявляем приватное поле для хранения коллекции постов
    private ObservableCollection<PostModel> PostsList;
    public PostTemplate()
	{
        InitializeComponent();
        // Устанавливаем BindingContext текущего экземпляра класса как самого себя
        BindingContext = this;
        // Создаем новый экземпляр ObservableCollection для хранения постов
        PostsList = new ObservableCollection<PostModel>();
        // Устанавливаем источник данных для PostListView
        PostListView.ItemsSource = PostsList;
        // Вызываем метод для загрузки постов
        LoadPosts();
    }
    // Асинхронный метод для загрузки постов
    private async void LoadPosts()
    {
        // Вызываем статический метод GetPosts() из класса PostViewModel для получения списка постов
        var Posts = await PostViewModel.GetPosts();
        // Проверяем, что список постов не пуст и не является null
        if (Posts != null && Posts.Any())
        {
            // Добавляем каждый пост из полученного списка в коллекцию PostsList
            foreach (var Post in Posts)
            {
                PostsList.Add(Post);
            }
        }
    }
    //// Кнопка перехода на страницу комментариев к посту
    //private async void GoToTheCommentPostPageButton(object sender, EventArgs e)
    //{
    //    var Button = (ImageButton)sender;
    //    var SelectedPost = Button?.BindingContext as Post; // Изменение здесь
    //    if (SelectedPost != null)
    //    {
    //        var CommentPostPage = new CommentPostPage(SelectedPost);
    //        await Navigation.PushModalAsync(CommentPostPage);
    //        NavigationPage.SetHasNavigationBar(CommentPostPage, false);
    //    }
    //}
}