using Avalonia.Media;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Avalonia.Tailwind.Colors;

public interface IColorDefinitionProvider
{
  ImmutableArray<(string name, ImmutableArray<Color> colors)> ColorDefinitions { get; }
}

public class ColorPaletteDefinitionProvider(
  IEnumerable<(string name, ImmutableArray<Color> colors)> colorDefinitions)
  : IColorDefinitionProvider
{
  public ImmutableArray<(string name, ImmutableArray<Color> colors)> ColorDefinitions { get; } = [..colorDefinitions];
}