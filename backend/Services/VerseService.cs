using Encourager.Api.Data;
using Encourager.Api.Models;

namespace Encourager.Api.Services;

public class VerseService
{
    public (Verse Verse, int Index) GetRandom(string language = "en")
    {
        var verses = GetVerses(language);
        var index = Random.Shared.Next(verses.Length);
        return (verses[index], index);
    }

    public (Verse Verse, int Index) GetByIndex(string language, int index)
    {
        var verses = GetVerses(language);
        var safeIndex = Math.Clamp(index, 0, verses.Length - 1);
        return (verses[safeIndex], safeIndex);
    }

    private static Verse[] GetVerses(string language) => language switch
    {
        "am" => AmharicVerses.Verses,
        "fi" => FinnishVerses.Verses,
        _ => EnglishVerses.Verses,
    };
}
