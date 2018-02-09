using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;
using System.Windows;
using HurtowniaMVVM.View;
using HurtowniaMVVM.Model;

namespace HurtowniaMVVM.ViewModel
{
    class SzczegolyZamowieniaViewModel : BindableBase
    {
        public ICommand AnulujZamowienieCommand { get; set; }
        public ICommand ZlozZamowienieCommand { get; set; }
        public ICommand WybierzTowarCommand { get; set; }
        public ICommand WybierzDostawceCommand { get; set; }

        #region gettersAndSetters
        private long _identyfikatorDostawcy;
        public long IdentyfikatorDostawcy
        {
            get { return _identyfikatorDostawcy; }
            set { _identyfikatorDostawcy = value; }
        }

        private string _nazwaDostawcy;
        public string NazwaDostawcy
        {
            get { return _nazwaDostawcy; }
            set
            {
                if (value != _nazwaDostawcy)
                {
                    _nazwaDostawcy = value;
                    OnPropertyChanged("NazwaDostawcy");
                }
            }
        }

        private int _idTowaru;
        public int IdTowaru
        {
            get { return _idTowaru; }
            set { _idTowaru = value; }
        }

        private bool _wybranoTowar;
        public bool WybranoTowar
        {
            get { return _wybranoTowar; }
            set
            {
                if (value != _wybranoTowar)
                {
                    _wybranoTowar = value;
                    OnPropertyChanged("WybranoTowar");
                }
            }
        }

        private bool _wybranoDostawce;
        public bool WybranoDostawce
        {
            get { return _wybranoDostawce; }
            set
            {
                if (value != _wybranoDostawce)
                {
                    _wybranoDostawce = value;
                    OnPropertyChanged("WybranoDostawce");
                }
            }
        }

        private bool _wybierzDostawceEnabled;
        public bool WybierzDostawceEnabled
        {
            get { return _wybierzDostawceEnabled; }
            set
            {
                if (value != _wybierzDostawceEnabled)
                {
                    _wybierzDostawceEnabled = value;
                    OnPropertyChanged("WybierzDostawceEnabled");
                }
            }
        }

        private string _towarMessage;
        public string TowarMessage
        {
            get { return _towarMessage; }
            set
            {
                if (value != _towarMessage)
                {
                    _towarMessage = value;
                    OnPropertyChanged("TowarMessage");
                }
            }
        }
        #endregion

        public SzczegolyZamowieniaViewModel()
        {
            AnulujZamowienieCommand = new DelegateCommand(Anuluj);
            ZlozZamowienieCommand = new DelegateCommand(ZlozZamowienie);
            WybierzTowarCommand = new DelegateCommand(WybierzTowar);
            WybierzDostawceCommand = new DelegateCommand(WybierzDostawce);
        }

        private void WybierzDostawce()
        {
            SystemOcenyDostawcowView view = new SystemOcenyDostawcowView
            {
                DataContext = new SystemOcenyDostawcowViewModel(IdTowaru, DostawcaTowaruModel.UzyskajIndeksNajlepszegoDostawcy(IdTowaru))
            };
            view.Show();
            Application.Current.Windows[0].Close();
        }

        private void WybierzTowar()
        {
            Random random = new Random();
            IdTowaru = random.Next(3, 10);
            WybranoTowar = true;
            WybierzDostawceEnabled = true;
            TowarMessage = $"Towar o id {IdTowaru}";
            WybranoDostawce = false;
            NazwaDostawcy = "";
            IdentyfikatorDostawcy = 0;
        }

        private void ZlozZamowienie()
        {
            MenuView szczegolyZamowieniaView = new MenuView();
            szczegolyZamowieniaView.Show();
            Application.Current.Windows[0].Close();
        }

        private void Anuluj()
        {
            MenuView szczegolyZamowieniaView = new MenuView();
            szczegolyZamowieniaView.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
