using System;
using System.Linq;

namespace SpeedrunTimerFix;

public static class Hooks
{
    public static void ApplyInitHooks()
    {
        On.RainWorld.OnModsInit += RainWorld_OnModsInit;
    }

    // All non-init hooks are called from here
    private static void ApplyHooks()
    {
        TimerDisplay_Hooks.ApplyHooks();
        TimerFunction_Hooks.ApplyHooks();
    }

    public static bool IsInit { get; private set; }

    private static void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
    {
        try
        {
            // This needs to be called each init due to a current bug, it should be fixed in the next game update, but there is no harm in calling it multiple times
            ModOptions.RegisterOI();

            if (IsInit)
            {
                return;
            }

            IsInit = true;

            ApplyHooks();

            // Fetch metadata about the mod
            var mod = ModManager.ActiveMods.First(mod => mod.id == Plugin.MOD_ID);

            Plugin.MOD_NAME = mod.name;
            Plugin.VERSION = mod.version;
            Plugin.AUTHORS = mod.authors;
        }
        catch (Exception e)
        {
            e.LogHookException();
        }
        finally
        {
            orig(self);
        }
    }
}
