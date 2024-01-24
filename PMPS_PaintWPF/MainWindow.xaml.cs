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
        private bool editMode = false;
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

    }
}