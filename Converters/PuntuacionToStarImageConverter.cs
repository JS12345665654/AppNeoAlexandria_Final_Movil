using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Prueba.Converters
{
    public class PuntuacionToStarImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int puntuacion = 0;

            // Validar el tipo de valor
            if (value is int)
                puntuacion = (int)value;
            else if (value is int?)
                puntuacion = ((int?)value).GetValueOrDefault();

            // Asegurar rango válido de 0 a 5
            if (puntuacion < 0)
                puntuacion = 0;
            else if (puntuacion > 5)
                puntuacion = 5;

            // Retornar nombre de imagen de estrellas
            return $"star_{puntuacion}.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}