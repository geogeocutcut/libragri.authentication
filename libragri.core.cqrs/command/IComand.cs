namespace libragri.core.cqrs
{
    public interface ICommand:IMessage
    {
        string Id{get;set;}
    }
}