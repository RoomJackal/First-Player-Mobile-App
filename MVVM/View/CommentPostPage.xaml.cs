using FirstPlayerMobileApp.MVVM.Model;
using FirstPlayerMobileApp.MVVM.ViewModel;
using FirstPlayerMobileApp.ViewModel;
using System.Collections.ObjectModel;

namespace FirstPlayerMobileApp;

public partial class CommentPostPage : FlyoutPage
{
    private ObservableCollection<PostModel> PostsList;
    private ObservableCollection<CommentModel> CommentsList;

    public CommentPostPage(PostModel Post)
    {
        InitializeComponent();
        BindingContext = this;
        PostsList = new ObservableCollection<PostModel> { Post };
        CommentsList = new ObservableCollection<CommentModel>();
        PostListView.ItemsSource = PostsList;
        PostComments.ItemsSource = CommentsList;
        LoadComments(Post.posts.id);
    }

    private async void LoadComments(int PostID)
    {
        var Comments = await CommentViewModel.GetComments(PostID);

        if (Comments != null && Comments.Any())
        {
            CommentsList.Clear();

            foreach (var Comment in Comments)
            {
                if (Comment.comment.postsId == PostID)
                {
                    CommentsList.Add(Comment);
                }
            }
        }
    }
}