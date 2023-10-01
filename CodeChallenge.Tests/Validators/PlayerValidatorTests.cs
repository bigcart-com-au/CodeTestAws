using CodeChallenge.Domain;
using CodeChallenge.Validators;
using FluentValidation.TestHelper;

namespace CodeChallenge.Tests.Validators
{
    public class PlayerValidatorTests
    {
        private readonly PlayerValidator _validator;

        public PlayerValidatorTests()
        {
            _validator = new PlayerValidator();
        }

        [Fact]
        public void Validate_InvalidPlayerName_ShouldHaveValidationError() {
            var player = new Player
            {
                Name = string.Empty,
                PlayerId = 1,
                Position = "WR"
            };

            var result = _validator.TestValidate(player);

            result.ShouldHaveValidationErrorFor(p => p.Name)
                .WithErrorMessage("Player name is not provided");

        }

        [Fact]
        public void Validate_InvalidPlayerId_ShouldHaveValidationError()
        {
            var player = new Player
            {
                Name = "Alice",
                PlayerId = 0,
                Position = "WR"
            };

            var result = _validator.TestValidate(player);

            result.ShouldHaveValidationErrorFor(p => p.PlayerId)
                .WithErrorMessage("Player Id is not valid");

        }

        [Fact]
        public void Validate_InvalidPlayerPosition_ShouldHaveValidationError()
        {
            var player = new Player
            {
                Name = "Bob",
                PlayerId = 1,
                Position = string.Empty
            };

            var result = _validator.TestValidate(player);

            result.ShouldHaveValidationErrorFor(p => p.Position)
                .WithErrorMessage("Player position is not provided");

        }
    }
}
