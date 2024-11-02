using CsvData;

namespace NugetTests;

public class AdvancedCsvTests
{
    private static readonly object[][] TestData =
    [
        [Advanced.Melee, "melee", TimeSpan.FromSeconds(1), 5.0f, 1.0f, 0f, 0f],
        [Advanced.Ranged, "ranged", TimeSpan.FromSeconds(1.5), 5.0f, 10.0f, 0f, 0f],
        [Advanced.Fireball, "fireball", TimeSpan.FromSeconds(3), 100.0f, 10.0f, 3.0f, 5.0f]
    ];

    [TestCaseSource(nameof(TestData))]
    public void VerifyAbilityValues(Advanced.Ability ability, string expectedIdString, TimeSpan expectedCooldown,
        float expectedDamage, float expectedRange, float expectedAoERange, float expectedAoEDamage)
    {
        Assert.Multiple(() =>
        {
            Assert.That(ability.Id.Value, Is.EqualTo(expectedIdString));
            Assert.That(ability.Cooldown, Is.EqualTo(expectedCooldown));
            Assert.That(ability.Damage, Is.EqualTo(expectedDamage));
            Assert.That(ability.Range, Is.EqualTo(expectedRange));
            Assert.That(ability.AoERange, Is.EqualTo(expectedAoERange));
            Assert.That(ability.AoEDamage, Is.EqualTo(expectedAoEDamage));
        });
    }
}