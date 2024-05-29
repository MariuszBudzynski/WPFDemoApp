using System.Globalization;
using System.Windows.Data;

namespace WPFDemoApp.Converters
{
	//this is needed to change the text in the UI
	public class BooleanToTextDecorationConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool && (bool)value)
			{
				return TextDecorations.Strikethrough;
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			//ToDO
			throw new NotImplementedException();
		}
	}
}
