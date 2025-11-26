using CommunityToolkit.Mvvm.ComponentModel;

namespace LotCom.Core.Types;

/// <summary>
/// A serial identifier for Basket Labels that follows a strictly incrementing nine-digit format, using leading zeroes.
/// </summary>
public partial class LotNumber : ObservableObject
{
    /// <summary>
    /// The absolute lowest digit literal that can be assigned to a Lot Number.
    /// </summary>
    private const int MinValue = 0;

    /// <summary>
    /// The absolute highest digit literal that can be assigned to a Lot Number.
    /// </summary>
    private const int MaxValue = 999999999;

    /// <summary>
    /// The raw literal value of the Lot Number for self-made.
    /// </summary>
    [ObservableProperty]
    public partial int Literal { get; set; }

    /// <summary>
    /// A formatted version of the Lot Number's literal value. 
    /// Prepends '0' digit characters to the front of the string to enforce nine-digit formatting requirements.
    /// </summary>
    [ObservableProperty]
    public partial string Formatted { get; set; }

    /// <summary>
    /// Confirms that Value is a valid value for this datatype.
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    private static bool IsValidValue(int Value)
    {
        return Value <= MaxValue && Value >= MinValue;
    }

    /// <summary>
    /// Formats the current Literal value according to the Datatype's formatting requirements.
    /// </summary>
    /// <returns>The Literal value as a Formatted string.</returns>
    private string FormatLiteral()
    {
        string FormattedLiteral = Literal.ToString();
        while (FormattedLiteral.Length < 9)
        {
            FormattedLiteral = $"0{FormattedLiteral}";
        }
        return FormattedLiteral;
    }

    /// <summary>
    /// Creates a new LotNumber from Value.
    /// </summary>
    /// <param name="Value"></param>
    public LotNumber(int Value)
    {
        // confirm that Value falls within the allowed literal range
        if (!IsValidValue(Value))
        {
            throw new ArgumentException($"'{Value}' is outside the allowed range of the LotNumber class.", nameof(Value));
        }
        Literal = Value;
        Formatted = FormatLiteral();
    }

    /// <summary>
    /// Creates a new LotNumber from a string Value.
    /// </summary>
    /// <param name="value"></param>
    public LotNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("LotNumber cannot be null or empty.", nameof(value));
        }

        Formatted = value.Trim();
    }

    /// <summary>
    /// Implicit conversion from string to LotNumber
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator LotNumber(string value) => new LotNumber(value);

    /// <summary>
    /// Implicit conversion from int to LotNumber
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator LotNumber(int value) => new LotNumber(value);

    /// <summary>
    /// Converts the object into a string.
    /// For self-made parts: returns formatted nine-digit string.
    /// For supplier parts: returns the original supplier lot number.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Formatted;
    }
}