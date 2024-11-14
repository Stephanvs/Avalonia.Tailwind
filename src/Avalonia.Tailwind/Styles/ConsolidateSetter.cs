using Avalonia.Styling;

namespace Avalonia.Tailwind
{
  public class ConsolidateSetter() : Setter
  {
    public ConsolidateSetter(AvaloniaProperty property, object value)
      : this()
    {
      Property = property;
      Value = GetConsolidatedValue(property, value);
    }

    private static object GetConsolidatedValue(AvaloniaProperty property, object value)
    {
      return (property, value) switch
      {
        (StyledProperty<Thickness> p, Thickness t) => ReplaceNanWithZero(t),
        _ => value,
      };
    }

    private static Thickness ReplaceNanWithZero(Thickness newValue)
    {
      return new Thickness
      (
        left:   double.IsNaN(newValue.Left)   ? 0 : newValue.Left,
        top:    double.IsNaN(newValue.Top)    ? 0 : newValue.Top,
        right:  double.IsNaN(newValue.Right)  ? 0 : newValue.Right,
        bottom: double.IsNaN(newValue.Bottom) ? 0 : newValue.Bottom
      );
    }
  }
}
