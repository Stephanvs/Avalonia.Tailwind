using System;
using System.Collections.Generic;
using System.Linq;

using Avalonia.Controls;
using Avalonia.Input;

namespace Avalonia.Tailwind.Controls
{
  public static class PseudoClassHelper
  {
    public static IEnumerable<string> GetPseudoClasses(Type type)
      => type switch
      {
        not null when type == typeof(InputElement) => [":disabled", ":focus", ":focus-visible", ":pointerover"],
        not null when type == typeof(Button) => [":pressed"],
        not null when type == typeof(TabItem) => [":pressed", ":selected"],
        // not null when type == typeof(ToggleButton) => [":checked", ":unchecked", ":indeterminate"],
        // not null when type == typeof(ItemsControl) => [":empty", ":singleitem"],
        _ => [],
      };

    public static IEnumerable<string> GetPseudoClassesRecursive(Type type) 
      => type == null
        ? Array.Empty<string>()
        : GetPseudoClasses(type).Union(GetPseudoClassesRecursive(type.BaseType));

    public static IEnumerable<string> GetAllPseudoClasses(IEnumerable<Type> types = null)
      => (types ?? AvaloniaControlHelper.GetAvaloniaControls()).SelectMany(GetPseudoClassesRecursive).Distinct();
  }
}
