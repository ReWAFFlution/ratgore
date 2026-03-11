using System.Linq;
using Content.Shared._Rat.Squad;
using Robust.Shared.Configuration;
using Robust.Shared.GameObjects;

namespace Content.Server._Rat.Squad;

/// <summary>
/// Система для управления отрядами.
/// </summary>
public sealed class SquadSystem : EntitySystem
{
    [Dependency] private readonly IConfigurationManager _config = default!;

    // Храним список всех отрядов по фракциям: Faction -> (SquadId -> SquadInfo)
    private readonly Dictionary<string, Dictionary<int, SquadInfo>> _squadsByFaction = new();
    private int _nextSquadId = 1;

    public override void Initialize()
    {
        base.Initialize();
    }

    /// <summary>
    /// Создать новый отряд.
    /// </summary>
    public bool CreateSquad(string faction, string squadName)
    {
        if (!_squadsByFaction.ContainsKey(faction))
        {
            _squadsByFaction[faction] = new Dictionary<int, SquadInfo>();
        }

        var squadId = _nextSquadId++;

        if (_squadsByFaction[faction].ContainsKey(squadId))
            return false;

        _squadsByFaction[faction][squadId] = new SquadInfo(squadId, squadName);
        return true;
    }

    /// <summary>
    /// Удалить отряд.
    /// </summary>
    public bool RemoveSquad(string faction, int squadId)
    {
        if (!_squadsByFaction.ContainsKey(faction))
            return false;

        if (!_squadsByFaction[faction].Remove(squadId))
            return false;

        var query = AllEntityQuery<SquadComponent>();
        while (query.MoveNext(out var uid, out var squadComp))
        {
            if (squadComp.SquadId == squadId)
            {
                RemComp<SquadComponent>(uid);
            }
        }

        return true;
    }

    /// <summary>
    /// Назначить сущность в отряд.
    /// </summary>
    public bool AssignToSquad(EntityUid entity, int squadId)
    {
        foreach (var factionSquads in _squadsByFaction.Values)
        {
            if (factionSquads.ContainsKey(squadId))
            {
                var squadComp = EnsureComp<SquadComponent>(entity);
                squadComp.SquadId = squadId;
                squadComp.SquadName = factionSquads[squadId].Name;
                Dirty(entity, squadComp, MetaData(entity));
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Удалить сущность из отряда.
    /// </summary>
    public void RemoveFromSquad(EntityUid entity)
    {
        RemComp<SquadComponent>(entity);
    }

    /// <summary>
    /// Получить список всех отрядов фракции.
    /// </summary>
    public Dictionary<int, SquadInfo> GetFactionSquads(string faction)
    {
        if (!_squadsByFaction.ContainsKey(faction))
            return new Dictionary<int, SquadInfo>();

        return _squadsByFaction[faction];
    }

    /// <summary>
    /// Получить количество членов в отряде.
    /// </summary>
    public int GetSquadMemberCount(int squadId)
    {
        var count = 0;
        var query = EntityQueryEnumerator<SquadComponent>();
        while (query.MoveNext(out var uid, out var squadComp))
        {
            if (squadComp.SquadId == squadId)
                count++;
        }

        return count;
    }
}

/// <summary>
/// Информация об отряде.
/// </summary>
public sealed record SquadInfo(int Id, string Name);
