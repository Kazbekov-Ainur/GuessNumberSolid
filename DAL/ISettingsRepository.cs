using GuessNumberSolid.BLL;

namespace GuessNumberSolid.DAL
{
    public interface ISettingsRepository
    {
        GameSettings GetSettings();
        void SaveSettings(GameSettings settings);
    }
}