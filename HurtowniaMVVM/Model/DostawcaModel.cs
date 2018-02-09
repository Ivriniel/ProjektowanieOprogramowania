using System;
using System.Data.SqlClient;


namespace HurtowniaMVVM
{
    /// <summary>
    /// Model dostawcy przyjmowanego do bazy. Zawiera wszystkie pola opisujące dostawcę. Łączy się z bazą w celu pobrania lub załadowania danych.
    /// </summary>
    class DostawcaModel
    {
        /// <param name="Nazwa">
        /// Parametr typu string opisujący nazwę formalną dostawcy.
        /// </param>
        public string Nazwa;
        /// <param name="Identyfikator">
        /// Parametr typu long opisujący identyfikator dostawcy będący numerem NIP lub VAT EU.
        /// </param>
        public long Identyfikator;
        /// <param name="Email">
        /// Parametr typu string opisujący email dostawcy.
        /// </param>
        public string Email;
        /// <param name="NumerTelefonu">
        /// Parametr typu string opisujący numer telefonu dostawcy.
        /// </param>
        public string NumerTelefonu;
        /// <param name="Kraj">
        /// Parametr typu string opisujący kraj zarejstrowania dostawcy.
        /// </param>
        public string Kraj;
        /// <param name="Ulica">
        /// Parametr typu string opisujący ulicę adresu dostawcy.
        /// </param>
        public string Ulica;
        /// <param name="Miasto">
        /// Parametr typu string opisujący miasto adresu dostawcy.
        /// </param>
        public string Miasto;
        /// <param name="KodPocztowy">
        /// Parametr typu string opisujący kode pocztowy adresu dostawcy.
        /// </param>
        public string KodPocztowy;
        /// <param name="Budynek">
        /// Parametr typu int opisujący numer budynku adresu dostawcy.
        /// </param>
        public int Budynek;
        /// <param name="Mieszkanie">
        /// Parametr typu int opisujący numer mieszkania adresu  dostawcy.
        /// </param>
        public int Mieszkanie;
        /// <param name="Uwagi">
        /// Parametr typu string opisujący uwagi względem dostawcy.
        /// </param>
        public string Uwagi;
        /// <param name="Ocena">
        /// Parametr typu float opisujący ocenę dostawcy.
        /// </param>
        public float Ocena;
        /// <param name="prawoDoZapisu">
        /// Parametr typu bool opisujący, czy dostawca może zostac wpisany do bazy (czy inne parametry są prawidłowe).
        /// </param>
        public bool prawoDoZapisu = false;
        /// <param name="connectionString">
        /// Parametr typu string opisujący łacze z bazą danych.
        /// </param>
        public static string connectionString = @"Data Source=F18;Initial Catalog=HurtowniaDb;Integrated Security=True";

        /// <summary>
        /// Metoda, w któej dodawany jest dostawca o podanych parametrach. Nawiązywane jest połączenie z bazą oraz próba załadowania nowego dostawcy. W przypadku niepowodzenia zwracana jest wartość bool false, w przeciwnym razie, true.
        /// </summary>
        /// <returns>Wartość typu bool, informująca o powodzeniu operacji dodawania dostawcy.</returns>
        public bool DodajDostawce()
        {
            bool sukces = true;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO dbo.Dostawca(Nazwa,Identyfikator,Email, NumerTelefonu, Uwagi, Miasto, KodPocztowy, Ulica, Budynek, Mieszkanie, KrajZarejstrowania)VALUES(@Nazwa,@Identyfikator,@Email, @NumerTelefonu, @Uwagi, @Miasto, @KodPocztowy, @Ulica, @Budynek, @Mieszkanie, @Kraj)";
            cmd.Parameters.AddWithValue("@Nazwa", Nazwa);
            cmd.Parameters.AddWithValue("@Identyfikator", Identyfikator);
            cmd.Parameters.AddWithValue("@Email", Email);
            if (NumerTelefonu != null)
                cmd.Parameters.AddWithValue("@NumerTelefonu", NumerTelefonu);
            else
                cmd.Parameters.AddWithValue("@NumerTelefonu", DBNull.Value);
            if (Uwagi != null)
                cmd.Parameters.AddWithValue("@Uwagi", Uwagi);
            else
                cmd.Parameters.AddWithValue("@Uwagi", DBNull.Value);
            if (Miasto != null)
                cmd.Parameters.AddWithValue("@Miasto", Miasto);
            else
                cmd.Parameters.AddWithValue("@Miasto", DBNull.Value);
            if (KodPocztowy != null)
                cmd.Parameters.AddWithValue("@KodPocztowy", KodPocztowy);
            else
                cmd.Parameters.AddWithValue("@KodPocztowy", DBNull.Value);
            if (Ulica != null)
                cmd.Parameters.AddWithValue("@Ulica", Ulica);
            else
                cmd.Parameters.AddWithValue("@Ulica", DBNull.Value);
            if (Mieszkanie != null)
                cmd.Parameters.AddWithValue("@Mieszkanie", Mieszkanie);
            else
                cmd.Parameters.AddWithValue("@Mieszkanie", DBNull.Value);
            if (Budynek != null)
                cmd.Parameters.AddWithValue("@Budynek", Budynek);
            else
                cmd.Parameters.AddWithValue("@Budynek", DBNull.Value);

            cmd.Parameters.AddWithValue("@Kraj", Kraj);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                sukces = false;
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return sukces;
        }
    }
}
