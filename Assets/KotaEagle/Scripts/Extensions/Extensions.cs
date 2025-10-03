using System;
using System.Collections.Generic;
using UnityEngine;

[UnityEngine.Scripting.Preserve]
public static class Extensions
{
  public static string Colorify<T>(this T value, Color color)
  {
    return "<color=" + color.ToHex() + ">" + value + "</color>";
  }
  public static string Colorify<T>(this T value, string hex)
  {
    return "<color=" + hex + ">" + value + "</color>";
  }

  public static string Size<T>(this T value, float size = 40f)
  {
    return "<size=" + size + ">" + value + "</size>";
  }

  public static string B<T>(this T value)
  {
    return "<b>" + value + "</b>";
  }

  public static string I<T>(this T value)
  {
    return "<i>" + value + "</i>";
  }

  public static string U<T>(this T value)
  {
    return "<u>" + value + "</u>";
  }

  public static string S<T>(this T value)
  {
    return "<s>" + value + "</s>";
  }

  public static string Sup<T>(this T value)
  {
    return "<sup>" + value + "</sup>";
  }

  public static string Sub<T>(this T value)
  {
    return "<sub>" + value + "</sub>";
  }

  public static Color ToColor(this string hex)
  {
    if (ColorUtility.TryParseHtmlString(hex, out Color color))
      return color;
    return Color.gray;
  }

  public static string ToHex(this Color color)
  {
    return "#" + ColorUtility.ToHtmlStringRGBA(color);
  }

  public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
  {
    if (source == null) throw new ArgumentNullException("source");
    if (action == null) throw new ArgumentNullException("action");

    foreach (T item in source) action(item);
  }
}