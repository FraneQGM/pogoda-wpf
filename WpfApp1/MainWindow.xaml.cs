
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        // Lista wszystkich pomiarów
        private List<Pomiar> pomiary = new List<Pomiar>();

        public MainWindow()
        {
            InitializeComponent();

            dpData.SelectedDate = DateTime.Today;

            // Przykładowe pomiary
            pomiary.Add(new Pomiar("Warszawa", DateTime.Today, 12));
            pomiary.Add(new Pomiar("Kraków", DateTime.Today, -4));
            pomiary.Add(new Pomiar("Poznań", DateTime.Today, 8));
            pomiary.Add(new Pomiar("Wrocław", DateTime.Today, 15));
            pomiary.Add(new Pomiar("Gdańsk", DateTime.Today, 6));

            Odswiez();
        }

        // Odświeżanie listy i wyznaczanie MAX/MIN
        private void Odswiez()
        {
            // Usuwamy stare oznaczenia
            foreach (var p in pomiary)
                p.Typ = "";

            if (pomiary.Count > 0)
            {
                var max = pomiary.OrderByDescending(p => p.Temperatura).First();
                var min = pomiary.OrderBy(p => p.Temperatura).First();

                max.Typ = "MAX";
                min.Typ = "MIN";
            }

            // Lista stacji do wyboru
            string wybrana = cmbStacje.SelectedItem as string;

            cmbStacje.SelectionChanged -= cmbStacje_SelectionChanged;

            cmbStacje.Items.Clear();
            cmbStacje.Items.Add("Wszystkie stacje");

            foreach (var stacja in pomiary
                         .Select(p => p.Stacja)
                         .Distinct()
                         .OrderBy(s => s))
            {
                cmbStacje.Items.Add(stacja);
            }

            if (wybrana != null && cmbStacje.Items.Contains(wybrana))
                cmbStacje.SelectedItem = wybrana;
            else
                cmbStacje.SelectedIndex = 0;

            cmbStacje.SelectionChanged += cmbStacje_SelectionChanged;

            WyswietlPomiary();
        }

        // Wyświetlanie pomiarów z uwzględnieniem filtra
        private void WyswietlPomiary()
        {
            string stacja = cmbStacje.SelectedItem as string;

            if (stacja == null || stacja == "Wszystkie stacje")
            {
                dgPomiary.ItemsSource = null;
                dgPomiary.ItemsSource = pomiary;
            }
            else
            {
                dgPomiary.ItemsSource = null;
                dgPomiary.ItemsSource = pomiary
                    .Where(p => p.Stacja == stacja)
                    .ToList();
            }
        }

        // Filtrowanie stacji
        private void cmbStacje_SelectionChanged(
            object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            WyswietlPomiary();
        }

        // Dodawanie pomiaru
        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            string stacja = txtStacja.Text.Trim();

            if (stacja == "")
            {
                MessageBox.Show("Podaj nazwę stacji!");
                return;
            }

            if (!dpData.SelectedDate.HasValue)
            {
                MessageBox.Show("Wybierz datę!");
                return;
            }

            if (!double.TryParse(
                    txtTemperatura.Text.Replace(',', '.'),
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double temperatura))
            {
                MessageBox.Show("Podaj poprawną temperaturę!");
                return;
            }

            pomiary.Add(new Pomiar(
                stacja,
                dpData.SelectedDate.Value,
                temperatura));

            txtStacja.Clear();
            txtTemperatura.Clear();

            Odswiez();

            lblInfo.Text = "Dodano pomiar.";
        }

        // Wczytywanie pomiarów z pliku
        private void btnWczytaj_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Pliki tekstowe (*.txt)|*.txt";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    List<Pomiar> nowePomiary = new List<Pomiar>();

                    foreach (string linia in File.ReadAllLines(dialog.FileName))
                    {
                        string[] dane = linia.Split(';');

                        if (dane.Length != 3)
                            continue;

                        if (!DateTime.TryParseExact(
                                dane[1].Trim(),
                                "yyyy-MM-dd",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out DateTime data))
                            continue;

                        if (!double.TryParse(
                                dane[2].Trim().Replace(',', '.'),
                                NumberStyles.Float,
                                CultureInfo.InvariantCulture,
                                out double temperatura))
                            continue;

                        nowePomiary.Add(new Pomiar(
                            dane[0].Trim(),
                            data,
                            temperatura));
                    }

                    pomiary = nowePomiary;

                    Odswiez();

                    lblInfo.Text = "Wczytano " + pomiary.Count + " pomiarów.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd odczytu pliku: " + ex.Message);
                }
            }
        }

        // Zapisywanie pomiarów do pliku
        private void btnZapisz_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "Pliki tekstowe (*.txt)|*.txt";
            dialog.FileName = "pomiary.txt";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    List<string> linie = pomiary.Select(p =>
                        p.Stacja + ";" +
                        p.Data.ToString("yyyy-MM-dd") + ";" +
                        p.Temperatura.ToString(
                            CultureInfo.InvariantCulture)
                    ).ToList();

                    File.WriteAllLines(dialog.FileName, linie);

                    lblInfo.Text = "Zapisano pomiary.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd zapisu pliku: " + ex.Message);
                }
            }
        }
    }

    // Klasa przechowująca pojedynczy pomiar
    public class Pomiar
    {
        public string Stacja { get; set; }
        public DateTime Data { get; set; }
        public double Temperatura { get; set; }

        public string Typ { get; set; } = "";

        public Pomiar(string stacja, DateTime data, double temperatura)
        {
            Stacja = stacja;
            Data = data;
            Temperatura = temperatura;
        }
    }
}
