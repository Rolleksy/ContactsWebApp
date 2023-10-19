
using ContactsApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using ContactsApi.Models;

namespace ContactsApi.Controllers
{
    public static class ContactModelEndpoints
    {
        public static void MapContactModelEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/ContactModel").WithTags(nameof(ContactModel));

            group.MapGet("/", async (ContactContext db) =>
            {
                return await db.Contacts.ToListAsync();
            })
            .WithName("GetAllContactModels")
            .WithOpenApi();

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

            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, ContactModel contactModel, ContactContext db) =>
            {
                var affected = await db.Contacts
                    .Where(model => model.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                      .SetProperty(m => m.Id, contactModel.Id)
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

            group.MapPost("/", async (ContactModel contactModel, ContactContext db) =>
            {
                db.Contacts.Add(contactModel);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/ContactModel/{contactModel.Id}", contactModel);
            })
            .WithName("CreateContactModel")
            .WithOpenApi();

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