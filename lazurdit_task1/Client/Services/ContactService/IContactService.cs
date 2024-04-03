namespace lazurdit_task1.Client.Services.ContactService
{
    public interface IContactService
    {
        List<Contact> Contacts { get; set; }
        Task GetContacts();
        Task<Contact> GetSingleContact(int id);
        Task CreateContact(Contact user);
        Task UpdateContact(Contact user);
        Task DeleteContact(int id);
    }
}
