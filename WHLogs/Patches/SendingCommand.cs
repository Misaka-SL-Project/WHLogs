using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Exiled.API.Features;
using HarmonyLib;
using NorthwoodLib.Pools;
using RemoteAdmin;
using static HarmonyLib.AccessTools;

namespace WHLogs.Patches
{
    // Totally not a Copy-Paste from DI
    [HarmonyPatch(typeof(CommandProcessor), nameof(CommandProcessor.ProcessQuery))]
    public class SendingCommand
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
            const int index = 0;

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Call, Method(typeof(SendingCommand), nameof(LogCommand))),
            });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }

        private static void LogCommand(string query, CommandSender s)
        {
            Player sender = Player.Get(s);
            string[] args = query.Trim().Split(QueryProcessor.SpaceArray, 512, StringSplitOptions.RemoveEmptyEntries);
            if (args[0].StartsWith("$"))
                return;
            Plugin.Singleton.CommandLogsQueue.Add(
                $"[{EventHandlers.Date}] {string.Format(Plugin.Singleton.Translation.UsedCommand, sender.Nickname ?? "Dedicated Server", sender.UserId ?? Plugin.Singleton.Translation.DedicatedServer, sender.Role, args[0], string.Join(" ", args.Where(a => a != args[0])))}");
        }
    }
}
