using HurtowniaMVVM.ViewModel;
using MahApps.Metro.Controls;

namespace HurtowniaMVVM
{
    /// <summary>
    /// Logika interakcji dla klasy MenuView.xaml
    /// </summary>
    public partial class MenuView : MetroWindow
    {
        public MenuView()
        {
            InitializeComponent();
            DataContext = new MenuViewModel();
        }
    }
}
