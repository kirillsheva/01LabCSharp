using System.Windows.Controls;

namespace _01LabShevchenko.Views
{
    /// <summary>
    /// Interaction logic for ZodiacView.xaml
    /// </summary>
    public partial class ZodiacView : UserControl
    {
        private ZodiacViewModel _viewModel;
        public ZodiacView()
        {
            InitializeComponent();
            DataContext = _viewModel = new ZodiacViewModel();
        }
    }
}
