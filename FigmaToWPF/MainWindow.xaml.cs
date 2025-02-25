using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FigmaToWPF
{
    public partial class MainWindow : Window
    {
        private double dpiScale = 1.0; // По умолчанию 100%

        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик выбора DPI
        private void DpiSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DpiSelector.SelectedItem is ComboBoxItem selectedItem)
            {
                if (double.TryParse(selectedItem.Tag.ToString(), System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out double parsedValue))
                {
                    dpiScale = parsedValue;
                }
                else
                {
                    MessageBox.Show("Ошибка конвертации DPI!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Конвертация значений
        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            // Читаем данные из полей
            double layoutSize = ParseInput(LayoutInput.Text);
            double fontSize = ParseInput(FontInput.Text);
            double cornerRadius = ParseInput(CornerInput.Text);

            // Пересчитываем под DPI
            double layoutWpf = ConvertToWPF(layoutSize);
            double fontWpf = ConvertToWPF(fontSize);
            double cornerWpf = ConvertToWPF(cornerRadius);

            // Вывод результата
            LayoutOutput.Text = layoutWpf.ToString("0.##");
            FontOutput.Text = fontWpf.ToString("0.##");
            CornerOutput.Text = cornerWpf.ToString("0.##");
        }

        // Метод для преобразования значений в WPF
        private double ConvertToWPF(double figmaValue)
        {
            return figmaValue / dpiScale;
        }

        // Безопасный парсинг числа
        private double ParseInput(string input)
        {
            return double.TryParse(input, out double result) ? result : 0;
        }

   
    }
}
