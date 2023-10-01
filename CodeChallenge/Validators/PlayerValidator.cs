using CodeChallenge.Domain;
using FluentValidation;

namespace CodeChallenge.Validators
{
    public class PlayerValidator : AbstractValidator<Player>
    {
        public PlayerValidator()
        {
            RuleFor(player => player.Name)
                .NotEmpty()
                .WithMessage("Player name is not provided");
            RuleFor(player => player.Position)
               .NotEmpty()
               .WithMessage("Player position is not provided");
            RuleFor(player => player.PlayerId)
               .GreaterThan(0)
               .WithMessage("Player Id is not valid");
        }
    }
}
