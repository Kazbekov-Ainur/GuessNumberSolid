using GuessNumberSolid.BLL;
using System;

namespace GuessNumberSolid.DAL
{
    public interface IGameRepository
    {
        void SaveGame(GameSession game);
        GameSession GetGame(Guid gameId);
    }
}