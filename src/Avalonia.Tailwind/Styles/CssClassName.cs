using System;
using System.Linq;
using System.Text;

namespace Avalonia.Tailwind.Styles
{
  public static class CssClassName
  {
    private static string GetClassNameCamelCase(params string[] parts)
    {
      var strBuilder = new StringBuilder();

      static string Capitalize(string part)
        => $"{char.ToUpperInvariant(part[0])}{part[1..]}";

      static string Lower(string part)
        => $"{char.ToLowerInvariant(part[0])}{part[1..]}";

      for (int i = 0; i < parts.Length; i++)
        strBuilder.Append(i == 0 ? Lower(parts[i]) : Capitalize(parts[i]));

      return strBuilder.ToString();
    }

    private static string GetClassNameSeparated(string separator, params string[] parts)
      => string.Join(separator, parts.Where(p => !string.IsNullOrEmpty(p)).Select(p => p.ToLower()));

    public static string GetClassName(ClassNamingStrategy namingStrategy, params string[] parts)
      => namingStrategy switch
      {
        ClassNamingStrategy.CamelCase => GetClassNameCamelCase(parts),
        ClassNamingStrategy.Underscore => GetClassNameSeparated("_", parts),
        ClassNamingStrategy.KebabCase => GetClassNameSeparated("-", parts),
        _ => throw new ArgumentException($"Unknown naming strategy: {namingStrategy}", nameof(namingStrategy)),
      };
  }
}
