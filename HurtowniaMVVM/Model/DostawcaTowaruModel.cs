using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HurtowniaMVVM.Model
{
    class DostawcaTowaruModel
    {
        public string Nazwa { get; set; }

        public long Identyfikator { get; set; }

        public OcenyModel OcenyDostawcy { get; set; }

        public double CenaJednostkowaTowaru { get; set; }

        public static DostawcaTowaruModel UzyskajDostawcePoId(long idDostawcy, int idTowaru)
        {
            OcenyModel oceny = OcenyModel.UzyskajOcenyDostawcy(idDostawcy);

            using (SqlConnection connection = new SqlConnection(DostawcaModel.connectionString))
            {
                connection.Open();

                string queryString = "SELECT Nazwa FROM dbo.Dostawca WHERE Identyfikator = " + idDostawcy;
                SqlCommand command = new SqlCommand(queryString, connection);
                var value = command.ExecuteScalar();
                string nazwa = value.ToString();
                queryString = "SELECT CenaJednostkowa FROM dbo.Oferta WHERE IdDostawcy = " + idDostawcy + " AND IdTowaru = " + idTowaru;
                command = new SqlCommand(queryString, connection);
                var value2 = (float)command.ExecuteScalar();
                double cenaJednostkowa = value2;

                return new DostawcaTowaruModel
                {
                    Identyfikator = idDostawcy,
                    Nazwa = nazwa,
                    OcenyDostawcy = oceny,
                    CenaJednostkowaTowaru = cenaJednostkowa
                };
            }
        }

        private static List<long> UzyskajListeIdDostawcowMajacychWOfercieTowar(int idTowaru)
        {
            List<long> listaIdentyfikatorow = new List<long>();

            using (SqlConnection connection = new SqlConnection(DostawcaModel.connectionString))
            {
                connection.Open();

                string queryString = "SELECT IdDostawcy FROM dbo.Oferta WHERE IdTowaru = " + idTowaru;
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var value = (long)reader.GetValue(0);
                    listaIdentyfikatorow.Add(value);
                }
            }
            return listaIdentyfikatorow;
        }

        public static long UzyskajIndeksNajlepszegoDostawcy(int idTowaru)
        {
            List<long> indeksyDostawcow = UzyskajListeIdDostawcowMajacychWOfercieTowar(idTowaru);

            if (indeksyDostawcow.Count == 0)
                return -2;

            List<OcenyModel> ocenyDostawcow = new List<OcenyModel>();
            foreach (long indeksDostawcy in indeksyDostawcow)
            {
                var ocena = OcenyModel.UzyskajOcenyDostawcy(indeksDostawcy);
                if (ocena != null)
                    ocenyDostawcow.Add(ocena);
            }

            if (ocenyDostawcow.Count == 0)
                return -1;

            var idNajlepszegoDostawcy = 0;
            var najlepszaSredniaOcen = 0.0;
            var najgorszaSkladowaOcena = 0;
            foreach (OcenyModel ocena in ocenyDostawcow)
            {
                var sredniaJakosc = ocena.SredniaJakosc;
                var sredniCzasTrwania = ocena.SredniCzasTrwaniaDostawy;
                var srednieOpoznienie = ocena.SrednieOpoznienieDostawcy;


                var biezacaNajgorszaSkladowa = 0;
                if (sredniaJakosc != null && sredniCzasTrwania != null && srednieOpoznienie != null)
                    biezacaNajgorszaSkladowa = Math.Min(sredniaJakosc.Value, Math.Min(sredniCzasTrwania.Value, srednieOpoznienie.Value));

                var sumaOcen = (sredniaJakosc != null ? sredniaJakosc : 0)
                    + (sredniCzasTrwania != null ? sredniCzasTrwania : 0) + (srednieOpoznienie != null ? srednieOpoznienie : 0);
                var biezacaSredniaOcen = (double)(sumaOcen / 3);

                if ((biezacaSredniaOcen == najlepszaSredniaOcen && biezacaNajgorszaSkladowa > najgorszaSkladowaOcena
                     && sredniaJakosc != null && sredniCzasTrwania != null && srednieOpoznienie != null)
                     || biezacaSredniaOcen > najlepszaSredniaOcen)
                {
                    idNajlepszegoDostawcy = ocena.Id;
                    najlepszaSredniaOcen = biezacaSredniaOcen;
                    najgorszaSkladowaOcena = biezacaNajgorszaSkladowa;
                }
            }
            return idNajlepszegoDostawcy;
        }

        public static List<DostawcaTowaruModel> UzyskajListeDostawcowTowaru(int idTowaru)
        {
            List<long> indeksyDostawcow = UzyskajListeIdDostawcowMajacychWOfercieTowar(idTowaru);
            List<DostawcaTowaruModel> listaDostawcow = new List<DostawcaTowaruModel>();

            foreach (long indeks in indeksyDostawcow)
            {
                listaDostawcow.Add(UzyskajDostawcePoId(indeks, idTowaru));
            }

            return listaDostawcow;
        }
    }
}
