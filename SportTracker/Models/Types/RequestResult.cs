using OneOf;

namespace SportTracker.Models.Types;


[GenerateOneOf]
public sealed partial class RequestResult<T> : OneOfBase<T, Exception>
{
    public bool IsSuccess => this.IsT0;
    public bool IsFailure => this.IsT1;
    public Exception Error => this.AsT1;
    public T ActualValue => this.AsT0;
}
