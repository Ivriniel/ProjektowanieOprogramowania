using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using System.Windows;
using HurtowniaMVVM.View;
using HurtowniaMVVM.Model;
using System.ComponentModel;
using System.Windows.Data;

namespace HurtowniaMVVM.ViewModel
{
    class ListaDostawcowViewModel : BindableBase
    {
        public ICommand AnulujWyborCommand { get; set; }
        public ICommand WybierzDostawceCommand { get; set; }

        #region gettersAndSetters
        private int _idTowaru;
        public int IdTowaru
        {
            get { return _idTowaru; }
            set { _idTowaru = value; }
        }

        private string _labelMessage;
        public string LabelMessage
        {
            get { return _labelMessage; }
            set
            {
                if (value != _labelMessage)
                {
                    _labelMessage = value;
                    OnPropertyChanged("LabelMessage");
                }
            }
        }

        private DostawcaTowaruModel _wybranyDostawca;
        public DostawcaTowaruModel WybranyDostawca
        {
            get { return _wybranyDostawca; }
            set
            {
                if (value != _wybranyDostawca)
                {
                    _wybranyDostawca = value;
                    OnPropertyChanged("WybranyDostawca");
                }
            }
        }

        private ICollectionView _listaDostawcowTowaru;
        public ICollectionView ListaDostawcowTowaru
        {
            get { return _listaDostawcowTowaru; }
            set
            {
                if (value != _listaDostawcowTowaru)
                {
                    _listaDostawcowTowaru = value;
                    OnPropertyChanged("ListaDostawcowTowaru");
                }
            }
        }
        #endregion

        public ListaDostawcowViewModel(int idTowaru)
        {
            IdTowaru = idTowaru;
            var _listaDostawcow = DostawcaTowaruModel.UzyskajListeDostawcowTowaru(IdTowaru);
            ListaDostawcowTowaru = CollectionViewSource.GetDefaultView(_listaDostawcow);
            LabelMessage = "Lista dostawców towaru o id " + idTowaru;

            AnulujWyborCommand = new DelegateCommand(Anuluj);
            WybierzDostawceCommand = new DelegateCommand(Wybierz);
        }

        private void Wybierz()
        {
            long idDostawcy = WybranyDostawca.Identyfikator;
            string nazwaDostawcy = WybranyDostawca.Nazwa.Trim();

            SzczegolyZamowieniaView view = new SzczegolyZamowieniaView
            {
                DataContext = new SzczegolyZamowieniaViewModel()
            };
            ((SzczegolyZamowieniaViewModel)view.DataContext).IdTowaru = IdTowaru;
            ((SzczegolyZamowieniaViewModel)view.DataContext).TowarMessage = $"Towar o id {IdTowaru}";
            ((SzczegolyZamowieniaViewModel)view.DataContext).WybranoTowar = true;
            ((SzczegolyZamowieniaViewModel)view.DataContext).WybierzDostawceEnabled = true;
            ((SzczegolyZamowieniaViewModel)view.DataContext).IdentyfikatorDostawcy = idDostawcy;
            ((SzczegolyZamowieniaViewModel)view.DataContext).NazwaDostawcy = nazwaDostawcy;
            ((SzczegolyZamowieniaViewModel)view.DataContext).WybranoDostawce = true;
            view.Show();
            Application.Current.Windows[0].Close();
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }

        private void Anuluj()
        {
            SzczegolyZamowieniaView view = new SzczegolyZamowieniaView
            {
                DataContext = new SzczegolyZamowieniaViewModel()
            };
            ((SzczegolyZamowieniaViewModel)view.DataContext).IdTowaru = IdTowaru;
            ((SzczegolyZamowieniaViewModel)view.DataContext).TowarMessage = $"Towar o id {IdTowaru}";
            ((SzczegolyZamowieniaViewModel)view.DataContext).WybranoTowar = true;
            ((SzczegolyZamowieniaViewModel)view.DataContext).WybierzDostawceEnabled = true;
            view.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
