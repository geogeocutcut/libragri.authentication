
namespace libragri.core.cqrs
{
    public abstract class CommandHandler<TData,TCommand> : IHandler 
        where TCommand  : ICommand<TData>

    {
        public abstract TData handle(TCommand commandtodo);

        public  object handle(object cmd)
        {
            return handle((TCommand)cmd);
        }
    }
}