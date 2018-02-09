using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using System.Windows;
using HurtowniaMVVM.View;
using HurtowniaMVVM.Model;

namespace HurtowniaMVVM.ViewModel
{
    class SystemOcenyDostawcowViewModel : BindableBase
    {
        public ICommand ZatwierdzWyborCommand { get; set; }
        public ICommand OtworzListeCommand { get; set; }
        public ICommand AnulujWyborCommand { get; set; }

        #region gettersAndSetters
        private int _idTowaru;
        public int IdTowaru
        {
            get { return _idTowaru; }
            set { _idTowaru = value; }
        }

        private long _identyfikatorNajlepszegoDostawcy;
        public long IdentyfikatorNajlepszegoDostawcy
        {
            get { return _identyfikatorNajlepszegoDostawcy; }
            set { _identyfikatorNajlepszegoDostawcy = value; }
        }

        private string _nazwaNajlepszegoDostawcy;
        public string NazwaNajlepszegoDostawcy
        {
            get { return _nazwaNajlepszegoDostawcy; }
            set { _nazwaNajlepszegoDostawcy = value; }
        }

        private string _systemMessage;
        public string SystemMessage
        {
            get { return _systemMessage; }
            set
            {
                if (value != _systemMessage)
                {
                    _systemMessage = value;
                    OnPropertyChanged("SystemMessage");
                }
            }
        }

        private Visibility _znalezionoDostawce = Visibility.Collapsed;
        public Visibility ZnalezionoDostawce
        {
            get { return _znalezionoDostawce; }
            set
            {
                if (value != _znalezionoDostawce)
                {
                    _znalezionoDostawce = value;
                    OnPropertyChanged("ZnalezionoDostawce");
                }
            }
        }

        private Visibility _nieZnalezionoOcenionegoDostawcy = Visibility.Collapsed;
        public Visibility NieZnalezionoOcenionegoDostawcy
        {
            get { return _nieZnalezionoOcenionegoDostawcy; }
            set
            {
                if (value != _nieZnalezionoOcenionegoDostawcy)
                {
                    _nieZnalezionoOcenionegoDostawcy = value;
                    OnPropertyChanged("NieZnalezionoOcenionegoDostawcy");
                }
            }
        }

        private Visibility _nieZnalezionoZadnegoDostawcy = Visibility.Collapsed;
        public Visibility NieZnalezionoZadnegoDostawcy
        {
            get { return _nieZnalezionoZadnegoDostawcy; }
            set
            {
                if (value != _nieZnalezionoZadnegoDostawcy)
                {
                    _nieZnalezionoZadnegoDostawcy = value;
                    OnPropertyChanged("NieZnalezionoZadnegoDostawcy");
                }
            }
        }
        #endregion

        public SystemOcenyDostawcowViewModel(int idTowaru, long idDostawcy)
        {
            IdTowaru = idTowaru;
            IdentyfikatorNajlepszegoDostawcy = idDostawcy;

            if (IdentyfikatorNajlepszegoDostawcy == -2)
            {
                SystemMessage = "System nie znalazł żadnego dostawcy dostarczającego ten towar!";
                NieZnalezionoZadnegoDostawcy = Visibility.Visible;
            }
            else if (IdentyfikatorNajlepszegoDostawcy == -1)
            {
                SystemMessage = "System nie znalazł żadnych ocenionych dostawców z tym towarem!";
                NieZnalezionoOcenionegoDostawcy = Visibility.Visible;
            }
            else
            {
                DostawcaTowaruModel najlepszyDostawca = DostawcaTowaruModel.UzyskajDostawcePoId(IdentyfikatorNajlepszegoDostawcy, IdTowaru);
                NazwaNajlepszegoDostawcy = najlepszyDostawca.Nazwa.Trim();
                SystemMessage = $"Wybrano dostawcę o najlepszej ocenie:";
                ZnalezionoDostawce = Visibility.Visible;
            }
            ZatwierdzWyborCommand = new DelegateCommand(ZatwierzWybor);
            OtworzListeCommand = new DelegateCommand(OtworzListe);
            AnulujWyborCommand = new DelegateCommand(Anuluj);
        }

        private void ZatwierzWybor()
        {
            SzczegolyZamowieniaView view = new SzczegolyZamowieniaView
            {
                DataContext = new SzczegolyZamowieniaViewModel()
            };
            ((SzczegolyZamowieniaViewModel)view.DataContext).IdTowaru = IdTowaru;
            ((SzczegolyZamowieniaViewModel)view.DataContext).TowarMessage = $"Towar o id {IdTowaru}";
            ((SzczegolyZamowieniaViewModel)view.DataContext).WybranoTowar = true;
            ((SzczegolyZamowieniaViewModel)view.DataContext).WybierzDostawceEnabled = true;
            ((SzczegolyZamowieniaViewModel)view.DataContext).IdentyfikatorDostawcy = IdentyfikatorNajlepszegoDostawcy;
            ((SzczegolyZamowieniaViewModel)view.DataContext).NazwaDostawcy = NazwaNajlepszegoDostawcy;
            ((SzczegolyZamowieniaViewModel)view.DataContext).WybranoDostawce = true;
            view.Show();
            Application.Current.Windows[0].Close();
        }

        private void OtworzListe()
        {
            ListaDostawcowView view = new ListaDostawcowView
            {
                DataContext = new ListaDostawcowViewModel(IdTowaru)
            };
            view.Show();
            Application.Current.Windows[0].Close();
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
