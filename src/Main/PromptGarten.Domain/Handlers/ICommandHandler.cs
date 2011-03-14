namespace PromptGarten.Domain.Commands
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand cmd);
    }
}