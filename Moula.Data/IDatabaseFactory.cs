using NPoco;

namespace Moula.Data
{
    public interface IDatabaseFactory
    {
        IDatabase CreateConnection();
    }
}
