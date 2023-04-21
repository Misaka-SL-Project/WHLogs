using System;
using System.Linq;
using System.Text;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Warhead;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.EventArgs.Scp079;
using Exiled.Events.EventArgs.Scp106;
using Exiled.Events.EventArgs.Scp914;
using Exiled.Events.EventArgs.Cassie;
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
                $"{Date} {string.Format(Plugin.Singleton.Translation.GeneratorFinished, ev.Generator.Room.Name, Generator.List.Count(x => x.IsEngaged) + 1)}");
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

        public void OnUsedMedicalItem(UsedItemEventArgs ev)
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
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasPickedUp, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Pickup.Type)}");
        }

        public void OnGainingLevel(GainingLevelEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.GainedLevel, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.NewLevel)}");
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

        public void OnThrowingGrenade(ThrownProjectileEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.ThrewAGrenade, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Projectile.Type)}");
        }

        public void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker != null && ev.Player != null)
                Plugin.Singleton.PvPLogsQueue.Add(
                    $"{Date} {string.Format(Plugin.Singleton.Translation.HasDamagedForWith, ev.Attacker.Nickname, ev.Attacker.UserId, ev.Attacker.Role, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Amount, ev.DamageHandler.Type)}");
        }

        public void OnDying(DyingEventArgs ev)
        {
            if (ev.Attacker != null && ev.Player != null)
                Plugin.Singleton.PvPLogsQueue.Add(
                    $"{Date} {string.Format(Plugin.Singleton.Translation.HasKilledWith, ev.Attacker.Nickname, ev.Attacker.UserId, ev.Attacker.Role, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.DamageHandler.Type)}");
        }

        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(ev.Door.IsOpen ? Plugin.Singleton.Translation.HasClosedADoor : Plugin.Singleton.Translation.HasOpenedADoor, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Door.Nametag)}");
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
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasBeenHandcuffedBy, ev.Target.Nickname, ev.Target.UserId, ev.Target.Role, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnRemovingHandcuffs(RemovingHandcuffsEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasBeenFreedBy, ev.Target.Nickname, ev.Target.UserId, ev.Target.Role, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnTeleporting(TeleportingEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.Scp106Teleported, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role)}");
        }

        public void OnItemDropped(DroppingItemEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.HasDropped, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, ev.Item.Type)}");
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
                $"{Date} {string.Format(Plugin.Singleton.Translation.ItemChanged, ev.Player.Nickname, ev.Player.UserId, ev.NewItem.Type)}"
            );
        }

        public void OnActivatingScp914(ActivatingEventArgs ev)
        {
            Plugin.Singleton.GameLogsQueue.Add(
                $"{Date} {string.Format(Plugin.Singleton.Translation.Scp914HasBeenActivated, ev.Player.Nickname, ev.Player.UserId, ev.Player.Role, Exiled.API.Features.Scp914.KnobStatus)}");
        }

        public static string Date => $"[{DateTime.Now:HH:mm:ss}]";
    }
}