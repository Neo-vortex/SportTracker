using System.Text.RegularExpressions;

namespace SportTracker.Utilities;

/// <summary>
/// Transforms the route parameters to lowercase and replaces spaces with dashes.
/// </summary>
public sealed partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    /// <summary>
    /// Transforms the route parameters to lowercase and replaces spaces with dashes.
    /// </summary>
    /// <param name="value">input route</param>
    /// <returns></returns>
    public string TransformOutbound(object? value)
    {
        return (value == null ? null : MyRegex().Replace(value.ToString() ?? throw new InvalidOperationException(), "$1-$2").ToLower()) ?? throw new InvalidOperationException();
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex MyRegex();
}