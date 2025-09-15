using System;
using System.Linq;

namespace NetGameState.Level;

public enum Chapter
{
    Unknown = -1,
    One,            // Shore
    Two,            // Tropics
    Three,          // Alpine / Mesa
    Four,           // Caldera
    Five,           // Kiln
    Six             // Peak
}

public static class ChapterExtensions
{
    public static Chapter Start => Enum.GetValues(typeof(Chapter))
        .Cast<Chapter>()
        .Where(c => c != Chapter.Unknown)
        .Min();

    public static Chapter End => Enum.GetValues(typeof(Chapter))
        .Cast<Chapter>()
        .Max();
}