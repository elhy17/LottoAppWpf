using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LottoApp
{
    public partial class MainWindow : Window
    {
// .............................................................................................................
        Random zufall = new Random();
        int geld = 1000;
        int[] meineZahlen = new int[6];
        int[] gewinnZahlen = new int[6];
        int gewählteAnzahl = 0;

        public MainWindow()
        {
            InitializeComponent();
            CreateLottoButtons();
            GeldAnzeige.Text = "Geld dass du hast " + geld + "€";
        }

        private void CreateLottoButtons()
        {
// Erstelle Zahlen Buttons 1-49
            for (int i = 1; i <= 49; i++)
            {
                var btn = new Button
                {
                    Content = i.ToString(),
                    Width = 50,
                    Height = 50,
                    Margin = new Thickness(2),
                    FontSize = 12,
                    FontWeight = FontWeights.Bold,
                    Background = new SolidColorBrush(Color.FromRgb(230, 230, 230)),
                    Tag = i
                };
                btn.Click += ZahlenButton_Click;
                ZahlenGrid.Children.Add(btn);
            }

// Erstellen Gewinnzahlen Anzeige Buttons die nicht klickbar sind
            for (int i = 0; i < 6; i++)
            {
                var btn = new Button
                {
                    Content = "?",
                    Width = 50,
                    Height = 50,
                    Margin = new Thickness(2),
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    Background = new SolidColorBrush(Color.FromRgb(255, 215, 0)),
                    IsEnabled = false
                };
                GewinnzahlenGrid.Children.Add(btn);
            }
        }

        private void ZahlenButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int zahl = (int)button.Tag;

 // Prüfen ob Zahl schon gewählt ist
            bool istGewählt = false;
            for (int i = 0; i < gewählteAnzahl; i++)
            {
                if (meineZahlen[i] == zahl)
                {
                    istGewählt = true;
                    break;
                }
            }

            if (istGewählt)
            {
 // Zahl abwählen
                for (int i = 0; i < gewählteAnzahl; i++)
                {
                    if (meineZahlen[i] == zahl)
                    {
                        for (int j = i; j < gewählteAnzahl - 1; j++)
                        {
                            meineZahlen[j] = meineZahlen[j + 1];
                        }
                        gewählteAnzahl--;
                        break;
                    }
                }
                button.Background = new SolidColorBrush(Color.FromRgb(230, 230, 230));
            }
            else if (gewählteAnzahl < 6)
            {
 // Zahl auswählen
                meineZahlen[gewählteAnzahl] = zahl;
                gewählteAnzahl++;
                button.Background = new SolidColorBrush(Color.FromRgb(100, 200, 100));
            }
            else
            {
                MessageBox.Show("Du kannst nur 6 Zahlen auswählen!");
            }
        }

        private void SpielenButton_Click(object sender, RoutedEventArgs e)
        {
 // Geld prüfen
                        if (geld < 300)
            {
                MessageBox.Show("Du hast nicht genug Geld! Du brauchst 300€ zum Spielen.");
                return;
            }

// Prüfen ob 6 Zahlen gewählt sind
                        if (gewählteAnzahl != 6)
            {
                MessageBox.Show("Bitte wähle genau 6 Zahlen aus!");
                return;
            }

// --------------------------------------------------------------------------------------------------------
                        geld = geld - 300;

// ----------------------------------------------------------------------------------------------------------------------
       for (int i = 0; i < gewinnZahlen.Length; i++)
            {
                         gewinnZahlen[i] = zufall.Next(1, 50);
            }

// ---------------------------------------------------------------------------------------------------------------------------
            ZeigeLottoscheinMitFarben(meineZahlen, gewinnZahlen);

// -------------------------------------------------------------------------------------------------------------------------
            Array.Sort(meineZahlen, 0, gewählteAnzahl);
            Array.Sort(gewinnZahlen);

// -----------------------------------------------------------------------------------------------------------------------------
            int treffer = 0;
            for (int i = 0; i < gewählteAnzahl; i++)
            {
                for (int j = 0; j < gewinnZahlen.Length; j++)
                {
                    if (meineZahlen[i] == gewinnZahlen[j])
                    {
                        treffer++;
                        break; //um doppelte Treffer zu vermeiden
                    }
                }
            }

//-------------------------------------------------------------------------------------------------------------------------
            int gewinn = 0;
            if (treffer == 3)
            {
                gewinn = 350; // 50€ Gewinn
            }
            else if (treffer == 4)
            {
                gewinn = 600; // 300€ Gewinn
            }
            else if (treffer == 5)
            {
                gewinn = 1300; // 1000€ Gewinn
            }
            else if (treffer == 6)
            {
                gewinn = 10300; // 10000€ Gewinn  JACKPOT
            }

            geld = geld + gewinn;

// --------------------------------------------------------------------------------------------------------------
            GeldAnzeige.Text = "Geld dass du hast " + geld + "€";
            if (geld >= 1000)
                GeldAnzeige.Foreground = Brushes.Green;
            else if (geld >= 300)
                GeldAnzeige.Foreground = Brushes.Orange;
            else
                GeldAnzeige.Foreground = Brushes.Red;

            if (treffer < 3)
            {
                Ergebnis.Text = "Du hast " + treffer + " Treffer. Leider nichts gewonnen!";
                Ergebnis.Foreground = Brushes.Red;
            }
            else if (treffer == 3)
            {
                Ergebnis.Text = "Du hast " + treffer + " Treffer! Du gewinnst 50€!";
                Ergebnis.Foreground = Brushes.Blue;
            }
            else if (treffer == 4)
            {
                Ergebnis.Text = "Du hast " + treffer + " Treffer! Du gewinnst 300€!";
                Ergebnis.Foreground = Brushes.Blue;
            }
            else if (treffer == 5)
            {
                Ergebnis.Text = "SUPER! Du hast " + treffer + " Treffer! Du gewinnst 1000€!";
                Ergebnis.Foreground = Brushes.Green;
            }
            else if (treffer == 6)
            {
                Ergebnis.Text = "JACKPOT! Du hast alle 6 Treffer! Du gewinnst 10000€!";
                Ergebnis.Foreground = Brushes.Gold;
            }

            if (geld < 300)
            {
                MessageBox.Show("Achtung! Du hast nicht mehr genug Geld für ein weiteres Spiel! Bitte Geld überweisen.");
            }
        }

// --------------------------------------------------------------------------------------------------------------------
        private void ZeigeLottoscheinMitFarben(int[] meineZahlen, int[] gewinnZahlen)
        {
// Gewinnzahlen in den Buttons anzeigen
            var gewinnButtons = GewinnzahlenGrid.Children.Cast<Button>().ToList();
            for (int i = 0; i < gewinnZahlen.Length; i++)
            {
                gewinnButtons[i].Content = gewinnZahlen[i].ToString();
                gewinnButtons[i].Background = new SolidColorBrush(Color.FromRgb(255, 215, 0));
            }

            string meineZahlenText = "Deine Zahlen: ";
            for (int i = 0; i < gewählteAnzahl; i++)
            {
                if (i > 0) meineZahlenText += ", ";

 // ---------------------------------------------------------------------------------------------------------------------
                bool istTreffer = false;
                for (int j = 0; j < gewinnZahlen.Length; j++)
                {
                    if (meineZahlen[i] == gewinnZahlen[j])
                    {
                        istTreffer = true;
                        break;
                    }
                }

                meineZahlenText += meineZahlen[i].ToString();
            }
            DeineZahlen.Text = meineZahlenText;
            DeineZahlen.Foreground = Brushes.Blue;

            string gewinnZahlenText = "Gewinnzahlen: ";
            for (int i = 0; i < gewinnZahlen.Length; i++)
            {
                if (i > 0) gewinnZahlenText += ", ";

// --------------------------------------------------------------------------------------------
                bool istTreffer = false;
                for (int j = 0; j < gewählteAnzahl; j++)
  {
         if (gewinnZahlen[i] == meineZahlen[j])
 {
         istTreffer = true;
         break;
         }
           }
 gewinnZahlenText += gewinnZahlen[i].ToString();
  }
       Gewinnzahlen.Text = gewinnZahlenText;
 Gewinnzahlen.Foreground = Brushes.Green;
        }
    }
}