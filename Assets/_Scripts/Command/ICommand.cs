namespace Command
{
    public interface ICommand
    {
        void Execute();
    }
    public interface ICommand<T>
    {
        void Execute(T data);
    }
    public interface ICommand<TData, TItem>
    {
        void Execute(TData data, TItem item);
    }
}
