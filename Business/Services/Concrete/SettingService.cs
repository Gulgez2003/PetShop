namespace Business.Services.Concrete
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IMapper _mapper;
        public SettingService(ISettingRepository settingRepository, IMapper mapper)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
        }

        public async Task<List<SettingGetDTO>> GetAllAsync()
        {
            List<Setting> settings = await _settingRepository.GetAllAsync();
            if (settings.Count == 0)
            {
                throw new NotFoundException(Messages.SettingNotFound);

            }
            return _mapper.Map<List<SettingGetDTO>>(settings);
        }

        public async Task<SettingGetDTO> GetByIdAsync(int id)
        {
            Setting setting = await _settingRepository.GetAsync(s => s.Id == id);
            if (setting == null)
            {
                throw new NotFoundException(Messages.SettingNotFound);
            }
            return _mapper.Map<SettingGetDTO>(setting);
        }

        public SettingGetDTO GetSetting()
        {
            Setting setting = _settingRepository.FirstOrDefault(s => s.Id == 1);
            SettingGetDTO settingGetDto = new SettingGetDTO()
            {
                Information = setting.Information,
                PhoneNumber = setting.PhoneNumber,
                Email = setting.Email,
                Address = setting.Address,
                FaceBookIcon = setting.FaceBookIcon,
                TwitterIcon = setting.TwitterIcon,
                InstagramIcon = setting.InstagramIcon
            };
            return settingGetDto;
        }

        public async Task UpdateAsync(SettingUpdateDTO updateDto)
        {
            Setting setting = await _settingRepository.GetAsync(s => s.Id == updateDto.settingGetDTO.Id);
            if (setting == null)
            {
                throw new NotFoundException(Messages.SettingNotFound);
            }

            setting.Information = updateDto.settingPostDTO.Information;
            setting.PhoneNumber = updateDto.settingPostDTO.PhoneNumber;
            setting.Email = updateDto.settingPostDTO.Email;
            setting.Address = updateDto.settingPostDTO.Address;
            setting.FaceBookIcon = updateDto.settingPostDTO.FaceBookIcon;
            setting.TwitterIcon = updateDto.settingPostDTO.TwitterIcon;
            setting.InstagramIcon = updateDto.settingPostDTO.InstagramIcon;

            _settingRepository.Update(setting);
            await _settingRepository.SaveAsync();
        }
    }
}
