using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using HurtowniaMVVM.View;
using System.Windows;
using System.Text.RegularExpressions;

namespace HurtowniaMVVM.ViewModel
{
    /// <summary>
    /// Klasa pośrednicząca pomiędzy widokami biorącymi udział w procesie dodawania dostawcy, a modelem.
    /// </summary>
    public class DodajDostawceViewModel:BindableBase
    {
        public ICommand DodajDostOKCommand { get; set; }
        public ICommand DodajDostPowrotCommand { get; set; }
        public ICommand TakCommand { get; set; }
        public ICommand NieCommand { get; set; }
        public ICommand AnulujCommand { get; set; }

        private static string _bladWiadomosc=" Blad!";


        #region gettersAndSetters

        private static string _nipVat="";

        public string NipVat
        {
            get { return _nipVat; }
            set
            {
                SetProperty(ref _nipVat, value);
                OnPropertyChanged(() => NipVat);
            }
        }

        private static string _nazwa = "";

        public string Nazwa
        {
            get { return _nazwa; }
            set
            {
                SetProperty(ref _nazwa, value);
                OnPropertyChanged(() => Nazwa);
            }
        }

        private static string _email = "";

        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
                OnPropertyChanged(() => Email);
            }
        }

        private static string _telefon = "";

        public string Telefon
        {
            get { return _telefon; }
            set
            {
                SetProperty(ref _telefon, value);
                OnPropertyChanged(() => Telefon);
            }
        }

        private static string _kraj = "";

        public string Kraj
        {
            get { return _kraj; }
            set
            {
                SetProperty(ref _kraj, value);
                OnPropertyChanged(() => Kraj);
            }
        }

        private static string _ulica = "";

        public string Ulica
        {
            get { return _ulica; }
            set
            {
                SetProperty(ref _ulica, value);
                OnPropertyChanged(() => Ulica);
            }
        }

        private static string _uwagi = "";

        public string Uwagi
        {
            get { return _uwagi; }
            set
            {
                SetProperty(ref _uwagi, value);
                OnPropertyChanged(() => Uwagi);
            }
        }

        private static string _miasto = "";

        public string Miasto
        {
            get { return _miasto; }
            set
            {
                SetProperty(ref _miasto, value);
                OnPropertyChanged(() => Miasto);
            }
        }

        private static string _kodPocztowy = "";

        public string KodPocztowy
        {
            get { return _kodPocztowy; }
            set
            {
                SetProperty(ref _kodPocztowy, value);
                OnPropertyChanged(() => KodPocztowy);
            }
        }

        private static string _budynek = "";

        public string Budynek
        {
            get { return _budynek; }
            set
            {
                SetProperty(ref _budynek, value);
                OnPropertyChanged(() => Budynek);
            }
        }

        private static string _mieszkanie = "";

        public string Mieszkanie
        {
            get { return _mieszkanie; }
            set
            {
                SetProperty(ref _mieszkanie, value);
                OnPropertyChanged(() => Mieszkanie);
            }
        }

        #endregion


        /// <param name="_dostawca">
        /// Parametr zawierający wszystkie dane nowego dostawcy.
        /// </param>
        private static DostawcaModel _dostawca;

        /// <summary>
        /// Konstruktor przypisujący komendy z widoku odpowiednim metodom w kodzie oraz inicjalizujący obiekt modelu dostawcy do dodawnia.
        /// </summary>
        public DodajDostawceViewModel()
        {
            DodajDostOKCommand = new DelegateCommand(DodajDostawceDodaj);
            DodajDostPowrotCommand = new DelegateCommand(DodajDostawcePowrot);
            TakCommand = new DelegateCommand(Tak);
            NieCommand = new DelegateCommand(Nie);
            AnulujCommand = new DelegateCommand(AnulujCom);
            _dostawca = new DostawcaModel();
        }

        /// <summary>
        /// Walidator wpisanego adresu email - zwraca wartość zależną od tego, czy adres jest poprawny, czy nie.
        /// </summary>
        /// <param name="s"> Wartość wpisanego adresu Email.</param>
        /// <returns>Zwraca wartość bool - true dla poprawnego adresu Email, false w przeciwnym razie.</returns>
        public bool IsValidEmailAddress(string s)
        {
            return Regex.IsMatch(s,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Metoda sprawdzająca, czy wpisane dane w widoku są poprawne.
        /// </summary>
        private void Sprawdz()
        {
            _bladWiadomosc = "";
            if (Nazwa.Length > 1 && Nazwa.Length < 34)
                if (NipVat.Length > 0 && long.TryParse(NipVat, out _dostawca.Identyfikator))
                    if (Email.Length > 0 && Email.Length < 255 && IsValidEmailAddress(Email))
                        if (IsValidTelefonNumer(Telefon))
                            if (Uwagi.Length < 256)
                                if (Miasto.Length < 41)
                                    if (KodPocztowy.Length < 256)
                                        if (Ulica.Length < 41)
                                            if (Budynek.Length==0 || (int.TryParse(Budynek, out _dostawca.Budynek) && _dostawca.Budynek>0))
                                                if (Mieszkanie.Length==0 || (int.TryParse(Mieszkanie, out _dostawca.Mieszkanie) && _dostawca.Mieszkanie > 0))
                                                    if (Kraj.Length > 0 && Kraj.Length < 51)
                                                        _dostawca.prawoDoZapisu = true;
                                                    else { _bladWiadomosc += "Nazwa kraju jest polem obowiazkowym, max 50 znakow! \n"; }
                                                else { _bladWiadomosc += "Mieszkanie jest numerem! \n"; }
                                            else { _bladWiadomosc += "Budynek jest numerem! \n"; }
                                        else { _bladWiadomosc += "Nazwa ulicy max 40 znakow \n"; }
                                    else { _bladWiadomosc += "Kod pocztowy max 255 znakow! \n"; }
                                else { _bladWiadomosc += "Nazwa miasta max 40 znakow \n"; }
                            else { _bladWiadomosc += "Uwagi max 255 znakow! \n"; }
                        else { _bladWiadomosc += "Numer telefonu max 12 znakow, min 9 \n"; }
                    else { _bladWiadomosc += " Zly format adresu Email \n"; }
                else { _bladWiadomosc += "NIP lub VAT EU jest obowiazkowym polem numerycznym \n"; _bladWiadomosc += NipVat; }
            else { _bladWiadomosc += "Nazwa jest polem obowiazkowym max 35 znakow \n"; }
        }

        /// <summary>
        /// Walidator numeru telefonu.
        /// </summary>
        /// <param name="telefon">Wpisana przez użytkownika i sprawdzana wartość numeru telefonu.</param>
        /// <returns>Zwraca wartość bool opisującą, czy podany numer jest prawidłowy (true), czy też nie (false).</returns>
        public bool IsValidTelefonNumer(string telefon)
        {
            bool prawidlowy = true;
            if (telefon.Length == 0) return prawidlowy;
            if (telefon.Length < 9 || telefon.Length > 12)
                return false;
            prawidlowy= Regex.Match(telefon, @"^(\+[0-9]{11})|([0-9]{9})$").Success;
            return prawidlowy;
        }

        /// <summary>
        /// Metoda zamykająca obecny widok i otwierająca widok menu głównego.
        /// </summary>
        private void DodajDostawcePowrot()
        {
            MenuView noweOkno = new MenuView();
            noweOkno.Show();
            Application.Current.Windows[0].Close();
        }

        /// <summary>
        /// Metoda otwierająca okno potwierdzenia dodawania dostawcy.
        /// </summary>
        private void DodajDostawceDodaj()
        {
            PotwierdzenieView noweOkno = new PotwierdzenieView();
            noweOkno.Show();
        }

        /// <summary>
        /// Metoda wycofująca dodawanie nowego dostawcy, zamykająca okno potwierdzenia, okno dodawania dostawcy i otwierająca okno menu głównego.
        /// </summary>
        private void Nie()
        {
            MessageBox.Show("Dodawanie anulowane", "Potwierdzenie", MessageBoxButton.OK);
            MenuView noweOkno = new MenuView();
            noweOkno.Show();
            Application.Current.Windows[3].Close();
            Application.Current.Windows[2].Close();
            ResetOkna();
            Application.Current.Windows[0].Close();
        }

        /// <summary>
        /// Metoda potwierdzająca dodanie nowego dostawcy. Wywołuje sprawdzenie parametrów oraz próbę dodania dostawcy. Wywołuje w widoku okienko informujące o sukcesie bądź niepowodzeniu transakcji.
        /// </summary>
        private void Tak()
        {
            Sprawdz();
            if (_dostawca.prawoDoZapisu)
            {
                Przypisanie();
                bool Sukces=DodajNowegoDostawce();
                if (Sukces)
                {
                    _dostawca = new DostawcaModel();
                    MessageBox.Show("Dodawanie zakonczone sukcesem", "Potwierdzenie", MessageBoxButton.OK);
                    MenuView noweOkno = new MenuView();
                    noweOkno.Show();
                    Application.Current.Windows[3].Close();
                    Application.Current.Windows[2].Close();
                    ResetOkna();
                    Application.Current.Windows[0].Close();
                }
                else
                {
                    MessageBox.Show("Niepoprawne dane - taki NIP lub VAT juz istnieje", "Potwierdzenie", MessageBoxButton.OK);
                    Application.Current.Windows[2].Close();
                }
            }
            else
            {
                MessageBox.Show(_bladWiadomosc, "Potwierdzenie", MessageBoxButton.OK);
                Application.Current.Windows[2].Close();
            }
        }

        /// <summary>
        /// Metoda odbierająca z modelu wynik operacji dodawania dostawcy.
        /// </summary>
        /// <returns>Zwraca wartość typu bool - true w przypadku powodzenia dodawania nowego dostawcy, false w przeciwnym razie.</returns>
        private bool DodajNowegoDostawce()
        {
            return _dostawca.DodajDostawce();
        }

        /// <summary>
        /// Metoda reagująca na komendę anulowania dodawania nowego dostawcy w okienku potwierdzenie - zamyka okno potwierdzenia, wraca do widoku dodawania dostawcy.
        /// </summary>
        private void AnulujCom()
        {
            Application.Current.Windows[2].Close();
        }

        /// <summary>
        /// Metoda czyszcząca wszystkie pola w widoku dodawania dostawcy.
        /// </summary>
        public void ResetOkna()
        {
            _nipVat = "";
            _nazwa = "";
            _email = "";
            _telefon = "";
            _kraj = "";
            _ulica = "";
            _uwagi = "";
            _miasto = "";
            _kodPocztowy = "";
            _budynek = "";
            _mieszkanie = "";
        }

        /// <summary>
        /// Metoda przypisująca wartość null dla pól bez wpisanych wartości w instancji dostawcy.
        /// </summary>
        public void Przypisanie()
        {
            _dostawca.Email = _email;
            _dostawca.KodPocztowy = ((_kodPocztowy.Length==0)?null:_kodPocztowy);
            _dostawca.Kraj = _kraj;
            _dostawca.Miasto = ((_miasto.Length == 0) ? null : _miasto);
            _dostawca.Nazwa = _nazwa;
            _dostawca.NumerTelefonu = ((_telefon.Length == 0) ? null : _telefon);
            _dostawca.Ulica = ((_ulica.Length == 0) ? null : _ulica);
            _dostawca.Uwagi = ((_uwagi.Length == 0) ? null : _uwagi);
        }
    }
}
