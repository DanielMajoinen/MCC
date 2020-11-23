using NPoco;

namespace Moula.Data.Dto
{
    [TableName("Account")]
    [PrimaryKey("Id")]
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
    }
}
