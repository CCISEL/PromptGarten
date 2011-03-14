namespace PromptGarten.Domain.Handlers
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand cmd);
    }
}