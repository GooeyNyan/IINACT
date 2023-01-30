using Advanced_Combat_Tracker;
using System;
using System.Diagnostics;
using RainbowMage.OverlayPlugin;

namespace Cactbot
{
    public class FFXIVPlugin
    {
        private ILogger logger_;
        private IActPluginV1 ffxiv_plugin_;

        public FFXIVPlugin(ILogger logger)
        {
            ffxiv_plugin_ = ActGlobals.oFormActMain.FfxivPlugin;
        }

        public string GetLocaleString()
        {
            return "cn";
            //switch (GetLanguageId())
            //{
            //    case 1:
            //        return "en";
            //    case 2:
            //        return "fr";
            //    case 3:
            //        return "de";
            //    case 4:
            //        return "ja";
            //    case 5:
            //        return "cn";
            //    case 6:
            //        return "ko";
            //    default:
            //        return null;
            //}
        }

        public int GetLanguageId()
        {
            if (ffxiv_plugin_ == null)
            {
                logger_.Log(LogLevel.Error, "BaitSignatureFoundMultipleMatchesErrorMessage");
                return 0;
            }

            try
            {
                // Cannot "just" cast to FFXIV_ACT_Plugin.FFXIV_ACT_Plugin here, because
                // ACT uses LoadFrom which places the assembly into its own loading
                // context.  Use dynamic here to make this choice at runtime.
                dynamic plugin_derived = ffxiv_plugin_;
                return (int)plugin_derived.DataRepository.GetSelectedLanguageID();
            }
            catch (Exception e)
            {
                logger_.Log(LogLevel.Error, "BaitSignatureFoundMultipleMatchesErrorMessage", e.ToString());
                return 0;
            }
        }

        public void RegisterProcessChangedHandler(Action<Process> handler)
        {
            var del = new FFXIV_ACT_Plugin.Common.ProcessChangedDelegate(handler);
            try
            {
                // See note in GetLanguageId.
                dynamic plugin_derived = ffxiv_plugin_;
                plugin_derived.DataSubscription.ProcessChanged += del;
            }
            catch (Exception e)
            {
                logger_.Log(LogLevel.Error, "BaitSignatureFoundMultipleMatchesErrorMessage", e.ToString());
            }
        }

        public Process GetCurrentProcess()
        {
            if (ffxiv_plugin_ == null)
            {
                return null;
            }

            try
            {
                dynamic plugin_derived = ffxiv_plugin_;
                var process = plugin_derived.DataRepository.GetCurrentFFXIVProcess();
                return process;
            }
            catch (Exception e)
            {
                logger_.Log(LogLevel.Error, "BaitSignatureFoundMultipleMatchesErrorMessage", e.ToString());
                return null;
            }
        }
    }
}
