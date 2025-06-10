using System.Diagnostics.CodeAnalysis;

namespace Testerke.Common.Domain.Enums;

/// <summary>
///     Provides a base class for strongly typed enumerations with a name and an integer value.
/// </summary>
/// <param name="Name">The name of this enumeration member.</param>
/// <param name="Value">The value of this enumeration member.</param>
/// <typeparam name="TEnum">The type of the enumeration.</typeparam>
public abstract record SmartEnum<TEnum>(string Name, int Value) : SmartEnum<TEnum, int>(Name, Value)
    where TEnum : SmartEnum<TEnum, int>;

/// <summary>
///     Provides a base class for strongly typed enumerations with a name and a value.
/// </summary>
/// <typeparam name="TEnum">The type of the enumeration.</typeparam>
/// <typeparam name="TValue">The type of the value of an enumeration.</typeparam>
public abstract record SmartEnum<TEnum, TValue>
    where TEnum : SmartEnum<TEnum, TValue>
    where TValue : IEquatable<TValue>
{
    private static readonly List<TEnum> EnumValues = [];

    /// <summary>
    ///     Constructs a new instance of the SmartEnum class.
    /// </summary>
    /// <param name="name">The name of this enumeration.</param>
    /// <param name="value">The value of this enumeration.</param>
    protected SmartEnum(string name, TValue value)
    {
        Name = name;
        Value = value;
        EnumValues.Add((TEnum)this);
    }

    public string Name { get; }

    public TValue Value { get; }

    /// <summary>
    ///     Tries to convert the specified value to its corresponding enumeration member.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to convert.</typeparam>
    /// <param name="value">The value to convert to an enumeration member.</param>
    /// <param name="result">
    ///     When this method returns <c>true</c>, contains the enumeration member that corresponds to the
    ///     specified <paramref name="value" />. If the conversion fails, this parameter is <c>null</c>.
    /// </param>
    /// <returns><c>true</c> if the <paramref name="value" /> was successfully converted; otherwise, <c>false</c>.</returns>
    public static bool TryFromValue(TValue value, [NotNullWhen(true)] out TEnum? result)
    {
        result = EnumValues.FirstOrDefault(e => e.Value.Equals(value));
        return result is not null;
    }

    /// <summary>
    ///     Tries to convert the specified name to its corresponding enumeration member.
    /// </summary>
    /// <param name="name">The name to convert.</param>
    /// <param name="result">
    ///     When this method returns <c>true</c>, contains the enumeration member that corresponds to the
    ///     specified <paramref name="name" />. If the conversion fails, this parameter is <c>null</c>.
    /// </param>
    /// <returns><c>true</c> if the <paramref name="name" /> was successfully converted; otherwise, <c>false</c>.</returns>
    public static bool TryFromName(string name, [NotNullWhen(true)] out TEnum? result)
    {
        result = EnumValues.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return result is not null;
    }

    /// <summary>
    ///     Tries to convert the specified value to its corresponding enumeration member.
    /// </summary>
    /// <typeparam name="TValue">The type of the value to convert.</typeparam>
    /// <param name="value">The value to convert to an enumeration member.</param>
    /// <returns>
    ///     Either the enumeration member that corresponds to the specified <paramref name="value" /> if found,
    ///     otherwise this method will throw an <see cref="ArgumentException" />.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when no enumeration member matches the given <paramref name="value" />.</exception>
    public static TEnum FromValue(TValue value)
    {
        return EnumValues.FirstOrDefault(e => e.Value.Equals(value))
               ?? throw new ArgumentException("Invalid value.", nameof(value));
    }

    /// <summary>
    ///     Tries to convert the specified name to its corresponding enumeration member.
    /// </summary>
    /// <param name="name">The name to convert to an enumeration member.</param>
    /// <returns>
    ///     Either the enumeration member that corresponds to the specified <paramref name="name" /> if found,
    ///     otherwise this method will throw an <see cref="ArgumentException" />.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when no enumeration member matches the given <paramref name="name" />.</exception>
    public static TEnum FromName(string name)
    {
        return EnumValues.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
               ?? throw new ArgumentException("Invalid value.", nameof(name));
    }
}