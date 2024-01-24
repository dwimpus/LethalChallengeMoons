using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.SceneManagement;
using ChangeMoons.GUI;

namespace ChangeMoons
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Loader : BaseUnityPlugin
    {
        private const string GUID = "ChangeChallenge.V1";
        private const string NAME = "ChangeMoons";
        private const string VERSION = "1.0.0";

        private readonly Harmony harmony = new Harmony(GUID);
        private static Loader Instance;
        public static ManualLogSource mls;

        public static ChangeMoonGUI myGUI;

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(GUID);
            mls.LogInfo($"[+] {NAME} {VERSION} loaded!");
            harmony.PatchAll();
            harmony.PatchAll(typeof(PatchChallengeWeek));

            GameObject GUI_OBJECT = new GameObject("ChangeMoons GUI");
            DontDestroyOnLoad(GUI_OBJECT);
            GUI_OBJECT.hideFlags = HideFlags.HideAndDontSave;
            GUI_OBJECT.AddComponent<ChangeMoonGUI>();
            myGUI = (ChangeMoonGUI)GUI_OBJECT.GetComponent("ChangeMoons GUI");
        }
    }
}
