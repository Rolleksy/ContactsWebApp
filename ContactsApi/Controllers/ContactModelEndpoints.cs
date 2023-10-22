
using ContactsApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using ContactsApi.Models;

namespace ContactsApi.Controllers
{
    // Klasa odpowiedzialna za mapowanie punktów końcowych API
    public static class ContactModelEndpoints
    {
        // Metoda mapująca
        public static void MapContactModelEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/ContactModel").WithTags(nameof(ContactModel));

            // Mapowanie metody GET - pobiera wszystkie kontakty w postaci listy
            group.MapGet("/", async (ContactContext db) =>
            {
                return await db.Contacts.ToListAsync();
            })
            .WithName("GetAllContactModels")
            .WithOpenApi();

            // Mapowanie metody GET - pobiera pojedynczy kontakt wg ID
            group.MapGet("/{id}", async Task<Results<Ok<ContactModel>, NotFound>> (int id, ContactContext db) =>
            {
                return await db.Contacts.AsNoTracking()
                    .FirstOrDefaultAsync(model => model.Id == id)
                    is ContactModel model
                        ? TypedResults.Ok(model)
                        : TypedResults.NotFound();
            })
            .WithName("GetContactModelById")
            .WithOpenApi();

            // Mapowanie metody PUT - edytujemy pojedynczy kontakt wg ID
            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, ContactModel contactModel, ContactContext db) =>
            {
                var affected = await db.Contacts
                    .Where(model => model.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                      //.SetProperty(m => m.Id, contactModel.Id)
                      .SetProperty(m => m.FirstName, contactModel.FirstName)
                      .SetProperty(m => m.LastName, contactModel.LastName)
                      .SetProperty(m => m.PhoneNumber, contactModel.PhoneNumber)
                      .SetProperty(m => m.Mail, contactModel.Mail)
                      .SetProperty(m => m.Password, contactModel.Password)
                      .SetProperty(m => m.DateOfBirth, contactModel.DateOfBirth)
                      .SetProperty(m => m.Category, contactModel.Category)
                    );

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("UpdateContactModel")
            .WithOpenApi();

            // Mapowanie metody POST - dodajemy nowy kontakt do DB
            group.MapPost("/", async (ContactModel contactModel, ContactContext db) =>
            {
                db.Contacts.Add(contactModel);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/ContactModel/{contactModel.Id}", contactModel);
            })
            .WithName("CreateContactModel")
            .WithOpenApi();

            // Mapowanie metody DELETE - usuwanie kontaktu wg ID
            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, ContactContext db) =>
            {
                var affected = await db.Contacts
                    .Where(model => model.Id == id)
                    .ExecuteDeleteAsync();

                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("DeleteContactModel")
            .WithOpenApi();
        }
    }
}