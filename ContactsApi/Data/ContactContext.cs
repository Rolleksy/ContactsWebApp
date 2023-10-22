using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Data
{
    // Klasa Context dla modelu Contact, pozwala na dostęp do danych z DB w postaci DBSet wymodelowane na bazie ContactModel
    public class ContactContext : DbContext
    {
        public DbSet<ContactModel> Contacts { get; set; }

        // Metoda ustalająca adres serwera SQL podczas konfiguracji aplikacji
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ContactsDB");
        }
    }
}
