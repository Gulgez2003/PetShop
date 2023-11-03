namespace Business.Services.Abstract
{
    public interface IContactService
    {
        Task CreateAsync(ContactPostDTO postDto);
    }
}
