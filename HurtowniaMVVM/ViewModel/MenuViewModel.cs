using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using HurtowniaMVVM.View;

namespace HurtowniaMVVM.ViewModel
{
    /// <summary>
    /// Klasa reagująca na działania użytkownika w widoku menu i otwierająca odpowiednie inne widoki.
    /// </summary>
    class MenuViewModel:BindableBase
    {

        public ICommand DodajDostCommand { get; set; }

        public ICommand ZlozZamowienieCommand { get; set; }

        /// <summary>
        /// Konstruktor wiążący komendy z widoku z odpowiednimi metodami w klasie.
        /// </summary>
        public MenuViewModel()
        {
            DodajDostCommand = new DelegateCommand(OtworzDodDost);
            ZlozZamowienieCommand = new DelegateCommand(OtworzSzczegolyZamowienia);
        }

        /// <summary>
        /// Metoda otwierająca widok dodawania dostawcy jako reakcję na komendę DodajDostCommand wywoływaną wciśnięciem przycisku "Dodaj dostawcę" w widoku. Metoda zamyka obecny widok.
        /// </summary>
        private void OtworzDodDost()
        {
            DodajDostawceView noweOkno = new DodajDostawceView();
            noweOkno.Show();
            Application.Current.Windows[0].Close();
        }

        /// <summary>
        /// Metoda otwierająca widok szczegółów zamówienia jako reakcję na komendę ZlozZamowienieCommand wywoływaną wciśnięciem przycisku "Zloz zamowienie" w widoku. Metoda zamyka obecny widok.
        /// </summary>
        private void OtworzSzczegolyZamowienia()
        {
            SzczegolyZamowieniaView szczegolyZamowieniaView = new SzczegolyZamowieniaView();
            szczegolyZamowieniaView.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
