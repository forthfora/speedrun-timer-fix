using MoreSlugcats;
using RWCustom;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using static MoreSlugcats.SpeedRunTimer;

namespace SpeedrunTimerFix;

public static class Utils
{
    public static RainWorldGame? RainWorldGame => Custom.rainWorld?.processManager?.currentMainLoop as RainWorldGame;


    // Optional: Shows milliseconds depending on the mod's configuration
    public static string GetIGTFormatOptionalMs(this TimeSpan timeSpan)
    {
        return timeSpan.GetIGTFormat(ModOptions.ShowMilliseconds.Value);
    }

    // Conditional: Shows milliseconds if the Remix timer is enabled or speedrun verification is enabled
    public static string GetIGTFormatConditionalMs(this TimeSpan timeSpan)
    {
        return timeSpan.GetIGTFormat((ModManager.MMF && MMF.cfgSpeedrunTimer.Value) || Custom.rainWorld.options.validation);
    }


    public static CampaignTimeTracker? GetCampaignTimeTracker()
    {
        return (Custom.rainWorld?.processManager?.currentMainLoop as RainWorldGame)?.GetCampaignTimeTracker();
    }

    public static CampaignTimeTracker? GetCampaignTimeTracker(this RainWorldGame? game)
    {
        return game?.GetStorySession?.saveStateNumber?.GetCampaignTimeTracker();
    }

    public static CampaignTimeTracker? GetCampaignTimeTracker(this SlugcatStats.Name? slugcat)
    {
        return SpeedRunTimer.GetCampaignTimeTracker(slugcat);
    }


    public static void LogHookException(this Exception e, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "")
    {
        Plugin.Logger.LogError($"Caught exception applying a hook! May not be fatal, but likely to cause issues." +
                               $"\nRelated to ({Path.GetFileNameWithoutExtension(filePath)}.{memberName}). Details:" +
                               $"\n{e}\n{e.StackTrace}");
    }
}
