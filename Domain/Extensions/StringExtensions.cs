using System;

namespace Domain.Extensions;

public static class StringExtensions
{
    public static bool EqualsIgnoreCase(this string left, string right)
    {
        return string.Equals(left, right, StringComparison.OrdinalIgnoreCase);
    }
}