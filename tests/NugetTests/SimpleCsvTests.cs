using CsvData;

namespace Tests;

public class SimpleCsvTests
{
    private static readonly object[][] TestData =
    [
        [Simple.Fireball, 15, 5, 15],
        [Simple.Frostbolt, 12, 5, 20],
        [Simple.HeavyStrike, 20, 0.7f, 5]
    ];

    [TestCaseSource(nameof(TestData))]
    public void VerifySpellValues(Simple.Spell spell, int expectedDamage, float expectedRange, int expectedCost)
    {
        Assert.Multiple(() =>
        {
            Assert.That(spell.Damage, Is.EqualTo(expectedDamage));
            Assert.That(spell.Range, Is.EqualTo(expectedRange));
            Assert.That(spell.Cost, Is.EqualTo(expectedCost));
        });
    }
}
