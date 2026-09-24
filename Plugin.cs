using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace ControllerIconSwapper
{
	[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin
	{
		internal static new ManualLogSource Logger;
		// Define enum for the config dropdown choices
		public enum TargetIconType
		{
			DualSense,
			DualShock,
			Xbox,
			JoyCon
		}

		public static Plugin Instance { get; private set; }
		public static ConfigEntry<TargetIconType> PreferredIcons;

		private void Awake()
		{
			// Plugin startup logic
			Instance = this;
			Logger = base.Logger;
			Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

			// Bind the config setting (creates BepInEx/config/com.yourname.dualsenseicons.cfg)
			PreferredIcons = Config.Bind(
				"General",                       // Section
				"TargetIconType",                // Key
				TargetIconType.DualShock,        // Default value
				"Select which controller icons to show when a gamepad is active." // Description
			);

			var _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
			_harmony.PatchAll();
		}
	}
}
