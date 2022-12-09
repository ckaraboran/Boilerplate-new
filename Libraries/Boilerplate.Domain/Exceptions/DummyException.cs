using System.Runtime.Serialization;

namespace Boilerplate.Domain.Exceptions;

[Serializable]
public class DummyException : Exception
{
    public DummyException(string message) : base(message)
    {
    }

    protected DummyException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}