using System;
using System.Collections.Generic;
using Avalonia.Tailwind;
using Avalonia.Tailwind.Styles;
using Avalonia.Tailwind.Controls;

namespace Avalonia
{
  public static class AppBuilderExtensions
  {
    public static AppBuilder UseTailwind(
      this AppBuilder builder,
      IStyleDefinitions definitions = null,
      IEnumerable<Type> controlTypes = null,
      ClassNamingStrategy namingStrategy = ClassNamingStrategy.Underscore)
      => builder.AfterSetup(appBuilder =>
      {
        var styles = StyleUtils.CreateStyles(
          definitions ?? new DefaultStyleDefinitionProvider().Definitions,
          controlTypes ?? new AvaloniaControlTypeProvider().GetAvaloniaControls(),
          namingStrategy
        );

        appBuilder.Instance?.Styles.AddRange(styles);
      });
  }
}