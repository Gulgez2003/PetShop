namespace Business.Services.Abstract
{
    public interface ISettingService
    {
        Task<List<SettingGetDTO>> GetAllAsync();
        Task<SettingGetDTO> GetByIdAsync(int id);
        Task UpdateAsync(SettingUpdateDTO updateDto);
        public SettingGetDTO GetSetting();
    }
}
