<<<<<<< HEAD
﻿using Robust.Shared.Serialization;
=======
using Robust.Shared.Serialization;
>>>>>>> 7be28591cf ([Port] Sector Frontier tickrate)

namespace Content.Shared._Lua.Performance;

[Serializable, NetSerializable]
public sealed class ServerPerfUpdateEvent : EntityEventArgs
{
    public float ServerFpsAvg { get; init; }
    public ushort ServerTickRate { get; init; }
<<<<<<< HEAD
}
=======
}
>>>>>>> 7be28591cf ([Port] Sector Frontier tickrate)
