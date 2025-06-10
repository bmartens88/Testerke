namespace Testerke.Common.Domain.Monads;

/// <summary>
///     Enumeration representing different types of errors that can occur in the application.
/// </summary>
public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    Problem = 2,
    NotFound = 3,
    Conflict = 4
}