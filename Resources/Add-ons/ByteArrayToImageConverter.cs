using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayerMobileApp  
{
    class ByteArrayToImageConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] byteArray && byteArray.Length > 0)
            {
                ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(byteArray));
                return imageSource;
            }
            else
            {
                // Возвращаем пустое изображение или другое значение по умолчанию
                return ImageSource.FromFile("placeholder_image.png");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
