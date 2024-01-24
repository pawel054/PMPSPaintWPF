using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PMPS_PaintWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool eraseMode = false;
        private bool selectMode = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.Strokes.Clear();
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if(inkCanvas != null && colorPicker.SelectedColor.HasValue)
            {
                inkCanvas.DefaultDrawingAttributes.Color = colorPicker.SelectedColor.Value;
            }
        }

        private void ThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(inkCanvas != null && thicknessSlider != null)
            {
                inkCanvas.DefaultDrawingAttributes.Width = thicknessSlider.Value;
                inkCanvas.DefaultDrawingAttributes.Height = thicknessSlider.Value;
            }
        }

        private void EraseButton_Click(object sender, RoutedEventArgs e)
        {
            eraseMode = !eraseMode;

            if(eraseMode)
            {
                eraseBtn.Background = new SolidColorBrush(Colors.Gray);
                inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            }
            else
            {
                eraseBtn.Background = new SolidColorBrush(Colors.Black);
                inkCanvas.DefaultDrawingAttributes.Color = colorPicker.SelectedColor.Value;
                inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            }
        }

        private void SelectButton_Click(Object sender, RoutedEventArgs e)
        {
            selectMode = !selectMode;

            if(selectMode)
            {
                selectBtn.Background = new SolidColorBrush(Colors.Gray);
                inkCanvas.EditingMode = InkCanvasEditingMode.Select;
            }
            else
            {
                selectBtn.Background = new SolidColorBrush(Colors.Black);
                inkCanvas.DefaultDrawingAttributes.Color = colorPicker.SelectedColor.Value;
                inkCanvas.EditingMode= InkCanvasEditingMode.Ink;
            }
        }

        private void InkCanvas_PreviewMouseUp(object sender, MouseEventArgs e)
        {
            if (selectMode)
            {
                inkCanvas.EditingMode = InkCanvasEditingMode.Select;
            }
            else
            {
                if(eraseMode == false)
                {
                    inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pliki obrazów|*.jpg;*.png;*.bmp;";
            if(saveFileDialog.ShowDialog() == true )
            {
                SaveAsImage(saveFileDialog.FileName);
            }
        }

        private void SaveAsImage(string fileName)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight, 96d, 96d, PixelFormats.Default);
            rtb.Render(inkCanvas);

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using(var fs = new FileStream(fileName, FileMode.Create))
            {
               encoder.Save(fs);
            }
        }
    }
}