using System.Collections.Generic;
using Content.Client.Weapons.Ranged.Components;
using Content.Shared.Prototypes;
using Robust.Client.GameObjects;
using Robust.Shared.GameObjects;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility; // Frontier

namespace Content.IntegrationTests.Tests;

/// <summary>
/// Tests all entity prototypes with the MagazineVisualsComponent.
/// </summary>
[TestFixture]
public sealed class MagazineVisualsSpriteTest
{
    private readonly HashSet<string> _ignoredPrototypes = new()
    {
        "ADTWeaponPistolX01",
    };

    [Test]
    public async Task MagazineVisualsSpritesExist()
    {
        await using var pair = await PoolManager.GetServerClient();
        var client = pair.Client;
        var protoMan = client.ResolveDependency<IPrototypeManager>();
        var componentFactory = client.ResolveDependency<IComponentFactory>();

        await client.WaitAssertion(() =>
        {
            Assert.Multiple(() =>
            {
                foreach (var proto in protoMan.EnumeratePrototypes<EntityPrototype>())
                {
                    if (proto.Abstract || pair.IsTestPrototype(proto) || _ignoredPrototypes.Contains(proto.ID))
                        continue;

                    if (!proto.TryGetComponent<MagazineVisualsComponent>(out var visuals, componentFactory))
                        continue;

                    Assert.That(proto.TryGetComponent<SpriteComponent>(out var sprite, componentFactory),
                        @$"{proto.ID} has MagazineVisualsComponent but no SpriteComponent.");
                    Assert.That(proto.HasComponent<AppearanceComponent>(componentFactory),
                        @$"{proto.ID} has MagazineVisualsComponent but no AppearanceComponent.");

                    var toTest = new List<(int, string)>();
                    if (sprite.LayerMapTryGet(GunVisualLayers.Mag, out var magLayerId))
                        toTest.Add((magLayerId, ""));
                    if (sprite.LayerMapTryGet(GunVisualLayers.MagUnshaded, out var magUnshadedLayerId))
                        toTest.Add((magUnshadedLayerId, "-unshaded"));

                    Assert.That(toTest, Is.Not.Empty,
                        @$"{proto.ID} has MagazineVisualsComponent but no Mag or MagUnshaded layer map.");

                    var start = visuals.ZeroVisible ? 0 : 1;
                    foreach (var (id, midfix) in toTest)
                    {
                        // Frontier: exception safety
                        try
                        {
                            Assert.That(sprite.TryGetLayer(id, out var layer));
                            var rsi = layer.ActualRsi;
                            for (var i = start; i < visuals.MagSteps; i++)
                            {
                                var state = $"{visuals.MagState}{midfix}-{i}";
                                Assert.That(rsi.TryGetState(state, out _),
                                    @$"{proto.ID} has MagazineVisualsComponent with MagSteps = {visuals.MagSteps}, but {rsi.Path} doesn't have state {state}!");
                            }

                            // MagSteps includes the 0th step, so sometimes people are off by one.
                            var extraState = $"{visuals.MagState}{midfix}-{visuals.MagSteps}";
                            Assert.That(rsi.TryGetState(extraState, out _), Is.False,
                                @$"{proto.ID} has MagazineVisualsComponent with MagSteps = {visuals.MagSteps}, but more states exist!");
                        }
                        catch (Exception e)
                        {
                            Assert.Fail(@$"Exception thrown for {proto.ID} when getting layer {id}: {e}!");
                            break;
                        }
                        // End Frontier: exception safety
                    }
                }
            });
        });

        await pair.CleanReturnAsync();
    }
}
