using System;

namespace CentralLendingApi.Services.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"L'entité \"{name}\" ({key}) n'a pas été trouvé.")
        {
        }
    }
}
