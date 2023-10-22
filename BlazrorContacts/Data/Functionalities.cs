using System.Text.RegularExpressions;

namespace BlazrorContacts.Data
{
    public class Functionalities
    {
        public Functionalities()
        {
        }

        public bool SprawdzZlozonoscHasla(string haslo)
        {
            // Sprawdzenie długości hasła
            if (haslo.Length < 8)
                return false;

            // Sprawdzenie czy hasło zawiera co najmniej jedną cyfrę
            if (!haslo.Any(char.IsDigit))
                return false;

            // Sprawdzenie czy hasło zawiera co najmniej jedną wielką literę
            if (!haslo.Any(char.IsUpper))
                return false;

            // Sprawdzenie czy hasło zawiera co najmniej jedną małą literę
            if (!haslo.Any(char.IsLower))
                return false;

            // Sprawdzenie czy hasło zawiera co najmniej jeden znak specjalny
            if (!Regex.IsMatch(haslo, @"[!@#$%^&*(),.?\:{ }|<>]"))
                return false;

            // Hasło spełnia wszystkie kryteria
            return true;
        }
    }
}
