using System.Reflection;

namespace Testerke.Modules.Users.Application;

/// <summary>
///     Provides a reference to the assembly of the Users application module.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    ///     Assembly reference used for dependency injection and other assembly-related operations.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}