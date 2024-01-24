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
        InkCanvas inkCanvas;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearButton_Clicked(object sender, RoutedEventArgs e)
        {
            inkCanvas.Strokes.Clear();
        }

        private void ColorPicker_SelectedColorChange(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if(inkCanvas != null && colorPicker.SelectedColor.HasValue)
            {
                inkCanvas.DefaultDrawingAttributes.Color = colorPicker.SelectedColor.Value;
            }
        }

        private void ThicknessSlider_ValueChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(inkCanvas != null && thicknessSlider != null)
            {
                inkCanvas.DefaultDrawingAttributes.Width = thicknessSlider.Value;
                inkCanvas.DefaultDrawingAttributes.Height = thicknessSlider.Value;
            }
        }

        private void EraseButton_Clicked(object sender, RoutedEventArgs e)
        {
            eraseMode = !eraseMode;

            if(eraseMode)
            {
                inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            }
            else
            {
                inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            }
        }

        private void SelectButton_Clicked(Object sender, RoutedEventArgs e)
        {
            selectMode = !selectMode;

            if(selectMode)
            {
                inkCanvas.EditingMode = InkCanvasEditingMode.Select;
            }
            else
            {
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
    }
}