using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SzyfrowanieWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int kluczSzyfrujący = 1;

        public MainWindow()
        {
            InitializeComponent();

            // Inicjalizacja ComboBox z kluczami szyfrowania
            for (int i = 1; i <= 34; i++)
            {
                cmbEncryptionKey.Items.Add(i);
                cmbDecryptionKey.Items.Add(i); // Inicjalizacja ComboBox z kluczami deszyfrowania
            }
            cmbEncryptionKey.SelectedIndex = 0;
            cmbDecryptionKey.SelectedIndex = 0; 
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            // Pobieranie tekstu z pola txtPlainText
            string tekst = txtPlainText.Text.ToLower(); // Zamiana na małe litery
            tekst = RemoveWhitespaceAndPunctuation(tekst); // Usunięcie spacji i znaków interpunkcyjnych

            // Pobieranie klucza szyfrowania z ComboBox
            if (cmbEncryptionKey.SelectedItem != null)
            {
                kluczSzyfrujący = (int)cmbEncryptionKey.SelectedItem;
            }

            // Szyfrowanie tekstu za pomocą klasy Crypt
            string zaszyfrowanyTekst = Crypt.Encrypt(tekst, kluczSzyfrujący);

            // Wyświetlenie zaszyfrowanego tekstu w polu txtEncryptedText
            txtEncryptedText.Text = zaszyfrowanyTekst;
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            // Pobieranie zaszyfrowanego tekstu z pola txtEncryptedInput
            string zaszyfrowanyTekst = txtEncryptedInput.Text.ToLower(); // Zamiana na małe litery
            zaszyfrowanyTekst = RemoveWhitespaceAndPunctuation(zaszyfrowanyTekst); // Usunięcie spacji i znaków interpunkcyjnych

            // Pobieranie klucza deszyfrowania z ComboBox
            if (cmbDecryptionKey.SelectedItem != null)
            {
                kluczSzyfrujący = (int)cmbDecryptionKey.SelectedItem;
            }

            // Deszyfrowanie tekstu za pomocą klasy Crypt
            string odszyfrowanyTekst = Crypt.Decrypt(zaszyfrowanyTekst, kluczSzyfrujący);

            // Wyświetlenie odszyfrowanego tekstu w polu txtDecryptedText
            txtDecryptedText.Text = odszyfrowanyTekst;
        }


        // Funkcja usuwająca spacje i znaki interpunkcyjne
        private string RemoveWhitespaceAndPunctuation(string input)
        {
            string result = string.Join("", input.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            return new string(result.ToCharArray().Where(c => !char.IsPunctuation(c)).ToArray());
        }
    }

    public class Crypt
    {
        public static string Encrypt(string text, int key)
        {
         char[] alfabetPolski = { 'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'q', 'r', 's', 'ś', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'ź', 'ż' };

        char[] result = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsLetter(c))
                {
                    int index = Array.IndexOf(alfabetPolski, c);
                    int newIndex = (index + key) % alfabetPolski.Length;
                    result[i] = alfabetPolski[newIndex];
                }
                else
                {
                    result[i] = c;
                }
            }
            return new string(result);
        }


        public static string Decrypt(string text, int key)
        {
            char[] alfabetPolski = { 'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'q', 'r', 's', 'ś', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'ź', 'ż' };

            char[] result = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsLetter(c))
                {
                    int index = Array.IndexOf(alfabetPolski, c);
                    int newIndex = (index - key + alfabetPolski.Length) % alfabetPolski.Length; // Zmniejsz indeks litery (lewo)
                    result[i] = alfabetPolski[newIndex];
                }
                else
                {
                    result[i] = c;
                }
            }
            return new string(result);
        }

    }


}
