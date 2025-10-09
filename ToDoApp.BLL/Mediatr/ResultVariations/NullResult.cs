using FluentResults;

namespace ToDoApp.BLL.Mediatr.ResultVariations
{
    public class NullResult<T> : Result<T>
    {
        public NullResult()
            : base()
        {
        }
    }
}
