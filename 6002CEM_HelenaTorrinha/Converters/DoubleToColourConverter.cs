using System.Globalization;

namespace _6002CEM_HelenaTorrinha.Converters
{
    public class DoubleToColourConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var curentValue = (double)value;
            return curentValue switch
            {
                0 => Colors.Black,
                < 18 => Colors.DeepPink,
                > 18 and < 25 => Colors.Green,
                _ => Colors.Red
            };
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
}

