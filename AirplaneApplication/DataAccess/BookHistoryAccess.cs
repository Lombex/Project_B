public class BookHistoryAccess : DataAccess<BookHistoryModel>
{
    public BookHistoryAccess()
    {
        path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/bookhistory.json"));
    }
}