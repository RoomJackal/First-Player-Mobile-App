using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace FirstPlayerMobileApp
{
    public class NumberConverter : IValueConverter
    {
        public object Convert(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture)
        {
            if (Value is int IntValue) 
            {
                if (IntValue >= 1000 && IntValue < 1000000)
                {
                    return $"{(double)IntValue / 1000:F1}K";
                }
                else if (IntValue >= 1000000)
                {
                    return $"{(double)IntValue / 1000000:F1}M";
                }
                else
                {
                    return IntValue.ToString();
                }
            }
            return Value;
        }
        public object ConvertBack(object Value, Type TargetType, object Parameter, System.Globalization.CultureInfo Culture)
        {
            throw new NotImplementedException();
        }
    }
}
