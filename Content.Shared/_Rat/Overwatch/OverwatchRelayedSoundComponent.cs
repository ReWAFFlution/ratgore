using Robust.Shared.GameStates;

namespace Content.Shared._Rat.Overwatch;

/// <summary>
/// Компонент для ретрансляции звуков при наблюдении через камеру Overwatch.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class RatOverwatchRelayedSoundComponent : Component
{
    /// <summary>
    /// Сущность ретранслируемого звука.
    /// </summary>
    [DataField, AutoNetworkedField]
    public EntityUid? Relay;
}
