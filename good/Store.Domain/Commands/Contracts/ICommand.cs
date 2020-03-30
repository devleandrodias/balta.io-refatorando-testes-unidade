namespace Store.Domain.Commands.Contracts
{
    public interface ICommand
    {
        void Validate();
    }
}