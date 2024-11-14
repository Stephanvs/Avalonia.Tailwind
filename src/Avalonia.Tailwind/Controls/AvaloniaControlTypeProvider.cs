using System;
using System.Collections.Generic;
using System.Reflection;

namespace Avalonia.Tailwind.Controls
{
  public interface IAvaloniaControlTypeProvider
  {
    IEnumerable<Type> GetAvaloniaControls();
  }

  public class AvaloniaControlTypeProvider(params Assembly[] assemblies)
    : IAvaloniaControlTypeProvider
  {
    public IEnumerable<Type> GetAvaloniaControls()
      => AvaloniaControlHelper.GetAvaloniaControls(assemblies);
  }
}
