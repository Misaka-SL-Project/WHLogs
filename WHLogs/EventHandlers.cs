using System;
using System.Text;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using NorthwoodLib.Pools;
using Respawning;
using Scp914;

namespace WHLogs
{
    public class EventHandlers
    {
        public void OnWaitingForPlayers()
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {Plugin.Singleton.Translation.WaitingForPlayers}");
        }

        public void OnDecontaminating(DecontaminatingEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {Plugin.Singleton.Translation.DecontaminationHasBegun}");
        }

        public void OnGeneratorActivated(GeneratorActivatedEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.GeneratorFinished, ev.Generator.CurRoom, Generator079.mainGenerator.totalVoltage + 1)}");
        }

        public void OnStartingWarhead(StartingEventArgs ev)
        {
            var vars = ev.Player == null
                ? new object[] {Warhead.DetonationTimer}
                : new object[] {ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, Warhead.DetonationTimer};
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(ev.Player == null ? Plugin.Singleton.Translation.WarheadStarted : Plugin.Singleton.Translation.PlayerWarheadStarted, vars)}");
        }

        public void OnStoppingWarhead(StoppingEventArgs ev)
        {
            var vars = ev.Player == null
                ? Array.Empty<object>()
                : new object[] {ev.Player.Nickname, ev.Player.UserId, ev.Player.Role};
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(ev.Player == null ? Plugin.Singleton.Translation.CanceledWarhead : Plugin.Singleton.Translation.PlayerCanceledWarhead, vars)}");
        }

        public void OnWarheadDetonated()
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {Plugin.Singleton.Translation.WarheadHasDetonated}");
        }

        public void OnUpgradingItems(UpgradingItemsEventArgs ev)
        {
            {
                StringBuilder players = StringBuilderPool.Shared.Rent();
                StringBuilder items = StringBuilderPool.Shared.Rent();

                foreach (Player player in ev.Players)
                    players.Append(player.Nickname).Append(" (").Append(player.UserId).Append(") [").Append(player.Role)
                        .Append(']').AppendLine();

                foreach (Pickup item in ev.Items)
                    items.Append(item.ItemId.ToString()).AppendLine();

                Plugin.Singleton.GameLogsQueue.Add(
                    $"{Date} {string.Format(Plugin.Singleton.Translation.Scp914HasProcessedTheFollowingPlayers, players, items)}");
                StringBuilderPool.Shared.Return(players);
                StringBuilderPool.Shared.Return(items);
            }
        }

        public void OnSendingRemoteAdminCommand(SendingRemoteAdminCommandEventArgs ev)
        {
            Plugin.Singleton.CommandLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.UsedCommand, ev.CommandSender.Nickname, ev.CommandSender.SenderId ?? Plugin.Singleton.Translation.DedicatedServer, ev.Sender.Role, ev.Name, string.Join(" ", ev.Arguments))}");
        }

        public void OnSendingConsoleCommand(SendingConsoleCommandEventArgs ev)
        {
            Plugin.Singleton.CommandLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasRunClientConsoleCommand, ev.Player.Nickname, ev.Player.UserId ?? Plugin.Singleton.Translation.DedicatedServer, ev.Player.Role, ev.Name, string.Join(" ", ev.Arguments))}");
        }

        public void OnRoundStarted()
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.RoundStarting, Player.Dictionary.Count)}");
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.RoundEnded, ev.LeadingTeam, Player.Dictionary.Count, CustomNetworkManager.slots)}");
        }

        public void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(ev.NextKnownTeam == SpawnableTeamType.ChaosInsurgency ? Plugin.Singleton.Translation.ChaosInsurgencyHaveSpawned : Plugin.Singleton.Translation.NineTailedFoxHaveSpawned, ev.Players.Count)}");
        }

        public void OnChangingScp914KnobSetting(ChangingKnobSettingEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.Scp914KnobSettingChanged, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.KnobSetting)}");
        }

        public void OnUsedMedicalItem(UsedMedicalItemEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.UsedMedicalItem, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Item)}");
        }

        public void OnInteractingTesla(InteractingTeslaEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasTriggeredATeslaGate, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasPickedUp, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Pickup.ItemId)}");
        }

        public void OnInsertingGeneratorTablet(InsertingGeneratorTabletEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.GeneratorInserted, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnEjectingGeneratorTablet(EjectingGeneratorTabletEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.GeneratorEjected, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnGainingLevel(GainingLevelEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.GainedLevel, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.OldLevel, ev.NewLevel)}");
        }

        public void OnEscapingPocketDimension(EscapingPocketDimensionEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasEscapedPocketDimension, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnEnteringPocketDimension(EnteringPocketDimensionEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasEnteredPocketDimension, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnActivatingWarheadPanel(ActivatingWarheadPanelEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.AccessedWarhead, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasTriggeredATeslaGate, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnThrowingGrenade(ThrowingGrenadeEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.ThrewAGrenade, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Type)}");
        }

        public void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker != null && ev.Target != null)
                Plugin.Singleton.PvPLogsQueue.Add(
                    $"{Date} {string.Format(Plugin.Singleton.Translation.HasDamagedForWith, ev.Attacker.Nickname, ev.Attacker.UserId, ev.Attacker.Role, ev.Target.Nickname, ev.Target.UserId, ev.Target.Role, ev.Amount, DamageTypes.FromIndex(ev.Tool).name)}");
        }

        public void OnDying(DyingEventArgs ev)
        {
            if (ev.Killer != null && ev.Target != null)
                Plugin.Singleton.PvPLogsQueue.Add(
                    $"{Date} {string.Format(Plugin.Singleton.Translation.HasKilledWith, ev.Killer.Nickname, ev.Killer.UserId, ev.Killer.Role, ev.Target.Nickname, ev.Target.UserId, ev.Target.Role, DamageTypes.FromIndex(ev.HitInformation.Tool).name)}");
        }

        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(ev.Door.NetworkTargetState ? Plugin.Singleton.Translation.HasClosedADoor : Plugin.Singleton.Translation.HasOpenedADoor, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Door.GetNametag())}");
        }

        public void OnInteractingElevator(InteractingElevatorEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.CalledElevator, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnInteractingLocker(InteractingLockerEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.UsedLocker, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnIntercomSpeaking(IntercomSpeakingEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasStartedUsingTheIntercom, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnHandcuffing(HandcuffingEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasBeenHandcuffedBy, ev.Target.Nickname, ev.Target.UserId, ev.Target.Role, ev.Cuffer.Nickname, ev.Cuffer.UserId, ev.Cuffer.Role)}");
        }

        public void OnRemovingHandcuffs(RemovingHandcuffsEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasBeenFreedBy, ev.Target.Nickname, ev.Target.UserId, ev.Target.Role, ev.Cuffer.Nickname, ev.Cuffer.UserId, ev.Cuffer.Role)}");
        }

        public void OnTeleporting(TeleportingEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.Scp106Teleported, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnItemDropped(ItemDroppedEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasDropped, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Pickup.ItemId)}");
        }

        public void OnVerified(VerifiedEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasJoinedTheGame, ev.Player.Nickname, ev.Player.UserId, Plugin.Singleton.Config.ShowIPAdresses ? ev.Player.IPAddress : "REDACTED")}");
        }

        public void OnDestroying(DestroyingEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.LeftServer, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.ChangedRole, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.NewRole)}");
        }

        public void OnChangingItem(ChangingItemEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.ItemChanged, ev.Player.Nickname, ev.Player.UserId, ev.OldItem.id, ev.NewItem.id)}"
            );
        }

        public void OnActivatingScp914(ActivatingEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.Scp914HasBeenActivated, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, Scp914Machine.singleton.knobState)}");
        }

        public void OnContaining(ContainingEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.Scp106WasContained, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        private string Date => $"[{DateTime.Now:HH:mm:ss}]";
    }
}