using Content.Shared.Speech;
using Robust.Shared.Prototypes;
using Content.Shared.Inventory;

namespace Content.Shared._Art.TTS;

public sealed class TTSRadioPlayEvent : EntityEventArgs
{
    public readonly EntityUid MessageSource;
    public string Voice;

    public TTSRadioPlayEvent(EntityUid message, string voice)
    {
        MessageSource = message;
        Voice = voice;
    }
}
