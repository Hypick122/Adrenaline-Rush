using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace Hypick.Patches;

[HarmonyPatch(typeof(PlayerControllerB))]
class PlayerControllerBPatch
{
	[HarmonyPatch("Update")]
	[HarmonyPrefix]
	static void Update(PlayerControllerB __instance, ref StartOfRound ___playersManager)
	{
		if (___playersManager.fearLevel >= 0.4f)
		{
			float multiplier = ___playersManager.fearLevel * (Plugin.Config.Multiplier / 100);

			if (Plugin.Config.ChangeSpeed)
				__instance.sprintMultiplier = Mathf.Lerp(__instance.sprintMultiplier, 1f + multiplier, Time.deltaTime * 0.25f);

			if (Plugin.Config.ChangeFOV)
			{
				float targetFOV = Mathf.Clamp(66f * multiplier, 66f, Plugin.Config.MaxFOV);
				__instance.gameplayCamera.fieldOfView = Mathf.Lerp(__instance.gameplayCamera.fieldOfView, targetFOV, 0.25f);

				// code from FovAdjust
				if (__instance.gameplayCamera.fieldOfView > 67)
				{
					float visorLerpAmount = (__instance.gameplayCamera.fieldOfView - 66f) / (Plugin.Config.MaxFOV - 66f);
					visorLerpAmount = Mathf.Lerp(visorLerpAmount, Mathf.Sin(visorLerpAmount * Mathf.PI / 2), 0.6f);
					__instance.localVisor.localScale = Vector3.LerpUnclamped(new Vector3(0.68f, 0.80f, 0.95f), new Vector3(0.68f, 0.35f, 0.99f), visorLerpAmount);
				}
				else
					__instance.localVisor.localScale = new Vector3(0.36f, 0.49f, 0.49f);
			}

			// Plugin.Log.LogInfo(">> fearLevel = " + ___playersManager.fearLevel);
			// Plugin.Log.LogInfo(">> fieldOfView = " + __instance.gameplayCamera.fieldOfView);
			// Plugin.Log.LogInfo(">> sprintMultiplier = " + __instance.sprintMultiplier);
			// Plugin.Log.LogInfo("");
		}

		// if (UnityInput.Current.GetKeyDown("G"))
		// 	__instance.JumpToFearLevel(2.0f);
	}
}
