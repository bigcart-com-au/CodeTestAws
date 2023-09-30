using CodeChallenge.Common;
using FluentAssertions;

namespace CodeChallenge.Tests
{
    public class PlayerPositionsTest
    {
        [Fact]
        public void Rank_NoPlayerPositionProvided_AddToEnd() 
        {
           var playerPositions = new int[] { 1, 2, 3 };

           var result = PlayerPositions.Rank(null, 4, playerPositions);

           result.Should().BeEquivalentTo(new int[] { 1, 2, 3,4});
        }

        [Fact]
        public void Rank_PlayerPositionProvidedIsGreaterThanListSize_AddToEnd()
        {
            var playerPositions = new int[] { 1, 2, 3 };

            var result = PlayerPositions.Rank(10, 4, playerPositions);

            result.Should().BeEquivalentTo(new int[] { 1, 2, 3, 4 });
        }

        [Fact]
        public void Rank_PlayerPositionProvidedIsWithinRange_ShufflePositions()
        {
            var playerPositions = new int[] { 1, 2, 3 };

            var result = PlayerPositions.Rank(3, 4, playerPositions);

            result.Should().BeEquivalentTo(new int[] { 1, 2, 4, 3 });
        }

        [Fact]
        public void Rank_PlayerPositionProvidedIsFirst_ShufflePositions()
        {
            var playerPositions = new int[] { 1, 2, 3 };

            var result = PlayerPositions.Rank(1, 4, playerPositions);

            result.Should().BeEquivalentTo(new int[] { 4, 1, 2, 3 });
        }
    }
}
