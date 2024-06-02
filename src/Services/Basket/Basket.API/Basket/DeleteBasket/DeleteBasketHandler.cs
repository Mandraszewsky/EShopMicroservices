
namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBackedResult>;
public record DeleteBackedResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBackedResult>
{
    public async Task<DeleteBackedResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        //todo: delete from db and cache
        await repository.DeleteBasket(command.UserName, cancellationToken);

        return new DeleteBackedResult(true);
    }
}
