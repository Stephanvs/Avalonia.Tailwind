using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Avalonia.Controls;

namespace Avalonia.Tailwind.Controls
{
  public static class AvaloniaControlHelper
  {
    private static IEnumerable<Type> GetAvaloniaControls(Assembly assembly)
      => assembly
        .GetTypes()
        .Where(t => t.IsSubclassOf(typeof(Control)))
        .Where(t => !t.IsAbstract);

    private static Assembly[] GetDefaultAssemblies()
      => [typeof(Control).Assembly, Assembly.GetEntryAssembly()];

    public static IEnumerable<Type> GetAvaloniaControls(Assembly[] assemblies = null)
    {
      var a = (assemblies == null || assemblies.Length == 0)
        ? GetDefaultAssemblies()
        : assemblies;

      foreach (var assembly in a)
        foreach (var controlType in GetAvaloniaControls(assembly))
          yield return controlType;
    }

    private static IEnumerable<AvaloniaProperty> GetAvaloniaProperties(Type controlType)
      => AvaloniaPropertyRegistry.Instance.GetRegistered(controlType);

    public static IEnumerable<(Type controlType, AvaloniaProperty property)>
      GetAvaloniaControlProperties(IEnumerable<Type> controlTypes)
        => controlTypes.SelectMany(c => GetAvaloniaProperties(c).Select(p => (c, p)));

    public static IEnumerable<(Type controlType, AvaloniaProperty property)> GetAvaloniaControlPropertiesFiltered(
      IEnumerable<Type> controlTypes)
    {
      /* first get all controlType / property tuples  */
      var controlProperties = controlTypes
        .SelectMany(control =>
          GetAvaloniaProperties(control)
          .Select(property => (control, property)))
        .ToArray();

      /* but we don't return all of them */
      /* we want to return only a property of the most parent control, as all selected based on "Is()" will match it */
      return controlProperties.GroupBy(cp => cp.property, cp => cp.control)
        .SelectMany(group => group
          .Where(type => !group.Any(type.IsSubclassOf))
          .Select(type => (control: type, property: group.Key))
        );
    }
  }
}
