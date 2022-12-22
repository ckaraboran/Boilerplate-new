using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Boilerplate.Domain.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage]
public class RecordAlreadyExistsException : Exception
{
    public RecordAlreadyExistsException(string message) : base(message)
    {
    }

    protected RecordAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}