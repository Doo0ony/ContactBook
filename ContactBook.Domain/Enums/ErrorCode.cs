namespace ContactBook.Domain.Enums;

public enum ErrorCode
{
    None,
    NotFound,
    ValidationError,
    Conflict,
    Unauthorized,
    InternalError,
    UnprocessableEntity,
}