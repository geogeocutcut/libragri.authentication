
namespace libragri.core.cqrs
{
    public interface ICommandHandler<TData,TCommand> : IHandler 
        where TCommand  : ICommand

    {
        TData handle(TCommand commandtodo);
    }
}