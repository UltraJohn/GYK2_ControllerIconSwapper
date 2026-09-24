using BepInEx;
using GK2.Framework;

namespace ControllerIconSwapper.Framework
{
	[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
	[BepInDependency(MyPluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency(FrameworkPlugin.PluginGuid, BepInDependency.DependencyFlags.HardDependency)]
	public sealed class FrameworkBridgePlugin : BaseUnityPlugin
	{
		public const string PluginGuid = "ControllerIconSwapper.Framework";
		public const string PluginName = "Controller Icon Swapper - GK2 Framework Integration";
		public const string PluginVersion = MyPluginInfo.PLUGIN_VERSION;

		private void Awake()
		{
			Plugin main = Plugin.Instance;
			if (main == null)
			{
				Logger.LogError("ControllerIconSwapper instance is unavailable.");
				return;
			}

			FrameworkApi.RegisterMod(new FrameworkBridge(), main.Config);
		}

		private sealed class FrameworkBridge : Gk2ModBase
		{
			private readonly Gk2ModMetadata metadata = new Gk2ModMetadata(
				MyPluginInfo.PLUGIN_GUID,
				MyPluginInfo.PLUGIN_NAME,
				"UltraJohn",
				MyPluginInfo.PLUGIN_VERSION,
				"Change controller icons between any device.",
				supportsRuntimeToggle: false,
				requiresKnownBuild: false,
				frameworkManagesEnabledState: false);

			public override Gk2ModMetadata Metadata => metadata;

			public override void OnRegister(Gk2ModContext context)
			{
				// Section/key must match Plugin.cs's Config.Bind call so this
				// binds the same ConfigEntry<TargetIconType> the main mod reads.
				context.Settings.AddEnum<Plugin.TargetIconType>(
					"General",
					"TargetIconType",
					Plugin.TargetIconType.DualShock,
					"Preferred controller icons",
					"Select which controller icons to show when a gamepad is active.");
			}
		}
	}
}
