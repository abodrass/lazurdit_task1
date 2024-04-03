using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;

namespace lazurdit_task1.Client.Services.ContactService
{
    public class ContactService : IContactService

    {

        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public ContactService(HttpClient http ,NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<Contact> Contacts { get; set; } = new List<Contact>();

        public async Task  GetContacts()
        {
            var result = await _http.GetFromJsonAsync<List<Contact>>("api/contact");
            if (result != null) 
            {
                Contacts = result;
            }
        }

        public async Task<Contact> GetSingleContact(int id)
        {
            var result = await _http.GetFromJsonAsync<Contact>($"api/contact/{id}");
            if (result != null)
            {
                return result;
            }
            throw new Exception("user not found! ");

        }

        public async Task CreateContact(Contact user)
        {
           
                var result = await _http.PostAsJsonAsync("api/contact", user);
            try
            {
                var response = await result.Content.ReadFromJsonAsync<List<Contact>>();
                if (response != null)
                {
                    Contacts = response;
                    _navigationManager.NavigateTo("contacts");
                }
                else
                {
                    // Handle the case where the response is null
                    // This could indicate an empty response or a parsing error
                    // You may want to log this or display an error message to the user
                }
            }
            catch (JsonException ex)
            {
                // Handle JSON parsing errors
                // Log the exception or display an error message to the user
                _navigationManager.NavigateTo("contacts");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log the exception or display an error message to the user
                _navigationManager.NavigateTo("contacts");
            }


        }



        private async Task SetRespons(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Contact>>();
            if (response != null)
            {
                Contacts = response;
                _navigationManager.NavigateTo("contacts");
            }
        }

        public async Task UpdateContact(Contact user)
        {
            var result = await _http.PutAsJsonAsync(($"api/contact/{user.Id}"), user);
            await SetRespons(result);

        }

        public async Task DeleteContact(int id)
        {
            var result = await _http.DeleteAsync(($"api/contact/{id}"));
            try
            {
                var response = await result.Content.ReadFromJsonAsync<List<Contact>>();
                if (response != null)
                {
                    Contacts = response;
                    _navigationManager.NavigateTo("contacts");
                }
                else
                {
                    // Handle the case where the response is null
                    // This could indicate an empty response or a parsing error
                    // You may want to log this or display an error message to the user
                }
            }
            catch (JsonException ex)
            {
                // Handle JSON parsing errors
                // Log the exception or display an error message to the user
                _navigationManager.NavigateTo("contacts");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log the exception or display an error message to the user
                _navigationManager.NavigateTo("contacts");
            }


        }
    }
}
