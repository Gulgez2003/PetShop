namespace Business.Services.Concrete
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task CreateAsync(ContactPostDTO postDto)
        {
            Contact contact = new Contact()
            {
                Name = postDto.Name,
                Email = postDto.Email,
                Message = postDto.Message
            };

            await _contactRepository.CreateAsync(contact);
            await _contactRepository.SaveAsync();
        }
    }
}
