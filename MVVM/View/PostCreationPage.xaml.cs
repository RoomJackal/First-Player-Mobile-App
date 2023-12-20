using FirstPlayerMobileApp.MVVM.Model;
using FirstPlayerMobileApp.ViewModel;

namespace FirstPlayerMobileApp;

public partial class PostCreationPage : FlyoutPage
{
    private readonly PostViewModel _PostViewModel;
    public PostCreationPage()
    {
        InitializeComponent();
        _PostViewModel = new PostViewModel();
        BindingContext = _PostViewModel;
    }
    private async void SelectPhotoButton(object sender, EventArgs e)
    {
        var PostPhoto = await MediaPicker.PickPhotoAsync();
        if (PostPhoto != null)
        {
            using (var Stream = await PostPhoto.OpenReadAsync())
            {
                _PostViewModel.PostModel.posts.imgpost = await ReadStream(Stream);
            }

            SelectedPhoto.Source = ImageSource.FromFile(PostPhoto.FullPath);
        }
    }

    private async Task<byte[]> ReadStream(System.IO.Stream ReadStream)
    {
        using (MemoryStream _MemoryStream = new MemoryStream())
        {
            await ReadStream.CopyToAsync(_MemoryStream);
            return _MemoryStream.ToArray();
        }
    }
}