namespace CodeChallenge.Common
{
    public static class PlayerPositions
    {
        public static IEnumerable<int> Rank(int? playerPosition,
            int playerId,
            IEnumerable<int> playerPositions)
        {
            var updatedPositions = new List<int>();

            if (playerPosition == null || playerPosition > playerPositions.Count())
            {
                updatedPositions.AddRange(playerPositions);
                updatedPositions.Add(playerId);

                return updatedPositions;
            }

            updatedPositions.AddRange(playerPositions);
            updatedPositions.Insert(playerPosition.Value, playerId);
            
            return updatedPositions;
        }
    }
}
