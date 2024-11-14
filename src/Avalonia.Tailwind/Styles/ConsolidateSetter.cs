using System;
using Avalonia.Controls;
using Avalonia.Styling;

namespace Avalonia.Tailwind
{
  public class ConsolidateSetter() : Setter
  {
    private readonly Setter internalSetter = new();

    public new AvaloniaProperty Property { get; set; }

    public new object Value { get; set; }

    public ConsolidateSetter(AvaloniaProperty property, object value)
      : this()
    {
      this.Property = property;
      this.Value = value;
    }

    private static Thickness Merge(StyledElement control, AvaloniaProperty<Thickness> property, Thickness newValue)
    {
      var oldValue = control.GetValue<Thickness>(property);

      return new Thickness
      (
        left:   double.IsNaN(newValue.Left)   ? oldValue.Left   : newValue.Left,
        top:    double.IsNaN(newValue.Top)    ? oldValue.Top    : newValue.Top,
        right:  double.IsNaN(newValue.Right)  ? oldValue.Right  : newValue.Right,
        bottom: double.IsNaN(newValue.Bottom) ? oldValue.Bottom : newValue.Bottom
      );
    }

    private static object GetConsolidatedValue(StyledElement control, AvaloniaProperty property, object value)
    {
      return (property, value) switch
      {
        (StyledProperty<Thickness> p, Thickness t) => Merge(control, p, t),
        _ => value,
      };
    }

    private Setter GetSetter(StyledElement control)
    {
      var setter = this.internalSetter;
      setter.Value = GetConsolidatedValue(control, this.Property, this.Value);
      setter.Property = this.Property;
      return setter;
    }

    // public IDisposable Apply(Style style, StyledElement control, IObservable<bool> activator)
    // {
    //   var setter = GetSetter(control);
    //   return setter.Apply(style, control, activator);
    // }
  }
}
