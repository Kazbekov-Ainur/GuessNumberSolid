using GuessNumberSolid.BLL;
using System;
using System.Collections.Generic;

namespace GuessNumberSolid.DAL
{
    public class InMemoryGameRepository : IGameRepository
    {
        private readonly Dictionary<Guid, GameSession> _games = new Dictionary<Guid, GameSession>();

        public void SaveGame(GameSession game)
        {
            _games[game.GameId] = game;
        }

        public GameSession GetGame(Guid gameId)
        {
            return _games.TryGetValue(gameId, out var game) ? game : null;
        }
    }
}