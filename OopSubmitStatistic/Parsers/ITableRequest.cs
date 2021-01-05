using Google.Apis.Sheets.v4.Data;

namespace OopSubmitStatistic
{
    public interface ITableRequest<T>
    {
        string Id { get; }
        string Range { get; }

        T Parse(ValueRange values);
    }
}