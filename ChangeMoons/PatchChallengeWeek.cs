using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using ChangeMoons.GUI;
using HarmonyLib;

namespace ChangeMoons
{
    public class PatchChallengeWeek : MonoBehaviour
    {
        [HarmonyPatch(typeof(GameNetworkManager), "GetWeekNumber")]
        //[HarmonyPatch("GetWeekNumber")]
        static bool Prefix(GameNetworkManager __instance, ref int __result)
        {
            DateTime dateTime = new DateTime(2023, 12, 11);
            DateTime dateTime2;

            try
            {
                dateTime2 = DateTime.UtcNow;
            }
            catch (Exception arg)
            {
                Debug.LogError($"Unable to get UTC time; defaulting to system date time; {arg}");
                dateTime2 = DateTime.Today;
            }

            __result = ChangeMoonGUI.selectedMoon;

            return false;
        }
    }
}
