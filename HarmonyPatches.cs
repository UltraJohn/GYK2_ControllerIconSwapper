using HarmonyLib;
using LazyBearTechnology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerIconSwapper
{
	[HarmonyPatch]
	public static class HarmonyPatches
	{
		[HarmonyPatch(typeof(ControllerIconLibrary), "UpdateInputDeviceForPC")]
		[HarmonyPostfix]
		public static void Postfix(
			ref List<ControllerIconData> ___currentIcons,
			List<ControllerIconData> ___dualSenseControllerIcons,
			List<ControllerIconData> ___dualShockControllerIcons,
			List<ControllerIconData> ___xBoxControllerIcons,
			List<ControllerIconData> ___joyConControllerIcons)
		{
			if (!LazyInput.IsGamepadActive) return;

			// Apply the icon list selected in the config file
			switch (Plugin.PreferredIcons.Value)
			{
				case Plugin.TargetIconType.DualSense:
					___currentIcons = ___dualSenseControllerIcons;
					break;
				case Plugin.TargetIconType.DualShock:
					___currentIcons = ___dualShockControllerIcons;
					break;
				case Plugin.TargetIconType.Xbox:
					___currentIcons = ___xBoxControllerIcons;
					break;
				case Plugin.TargetIconType.JoyCon:
					___currentIcons = ___joyConControllerIcons;
					break;
			}
		}
	}
}
