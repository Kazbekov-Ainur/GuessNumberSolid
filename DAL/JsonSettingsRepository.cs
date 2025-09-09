using GuessNumberSolid.BLL;
using Microsoft.Extensions.Configuration;

using System;

namespace GuessNumberSolid.DAL
{
    public class JsonSettingsRepository : ISettingsRepository
    {
        private readonly IConfiguration _configuration;

        public JsonSettingsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public GameSettings GetSettings()
        {
            return new GameSettings
            {
                MinNumber = _configuration.GetValue<int>("GameSettings:MinNumber", 1),
                MaxNumber = _configuration.GetValue<int>("GameSettings:MaxNumber", 100),
                MaxAttempts = _configuration.GetValue<int>("GameSettings:MaxAttempts", 10)
            };
        }

        public void SaveSettings(GameSettings settings)
        {
            // Реализация сохранения настроек (опционально)
            throw new NotImplementedException();
        }
    }
}