using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using ChangeMoons.Functions;

namespace ChangeMoons.GUI
{
    public class ChangeMoonGUI : MonoBehaviour
    {
        public static bool showMenu = false;
        public static bool lockedCursor = false;

        private const int GUI_WIDTH = 250;
        private const int GUI_HEIGHT = 300;
        private static float GUI_X_POS = (Screen.width / 2) - (GUI_WIDTH / 2);
        private static float GUI_T_POS = (Screen.height / 2) - (GUI_HEIGHT / 2);

        private Rect MAIN_WINDOW = new Rect(Screen.width / 2, Screen.height / 2, GUI_WIDTH, GUI_HEIGHT - 75);

        private float scrollPos = 0f;
        private UnityEngine.Vector2 scrollPosition = UnityEngine.Vector2.zero;
        private Rect scrollViewRect = new Rect(5, 45, GUI_WIDTH - 20, GUI_HEIGHT - 145);

        public static int selectedMoon = Functions.Functions.GetWeekNumber();
        private static string weekNumText = "Challenge week number";

        private void Awake()
        {
            Loader.mls.LogInfo("[+] GUI Loaded!");
        }

        private void Update()
        {
            if (Keyboard.current[Key.Insert].wasPressedThisFrame)
            {
                showMenu = !showMenu;
                Cursor.visible = showMenu;
                Cursor.lockState = (CursorLockMode)2;

                QuickMenuManager menuManager = GameObject.FindObjectOfType<QuickMenuManager>();

                if (menuManager == null) return;
                menuManager.isMenuOpen = showMenu;
            }
        }

        private void OnGUI()
        {
            if(showMenu)
            {
                MAIN_WINDOW = UnityEngine.GUI.Window(0, MAIN_WINDOW, doWindow, "Challenge Moon Selector");
            }
        }

        private void doWindow(int windowID)
        {
            GUILayout.BeginVertical();
            {
                GUILayout.BeginHorizontal();
                {
                    scrollPos = UnityEngine.GUI.VerticalScrollbar(new Rect(GUI_WIDTH - 20, GUI_HEIGHT - 280, GUI_WIDTH, GUI_HEIGHT - 165), scrollPos, 50, 1, 1300);
                    scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(scrollViewRect.width), GUILayout.Height(scrollViewRect.height));

                    UnityEngine.GUI.Box(new Rect(5, 20, GUI_WIDTH - 30, GUI_HEIGHT - 165), "Upcoming moons: ");

                    float yPosUpdate = 0;

                    GUILayout.BeginArea(scrollViewRect);
                    GUILayout.BeginVertical();

                    if(GameNetworkManager.Instance != null)
                    {
                        int getCurrWeekNum = Functions.Functions.GetWeekNumber();
                        for (int i = getCurrWeekNum; i <= 50; i++)
                        {
                            string moonName = GameNetworkManager.Instance.GetNameForWeekNumber(i);
                            string moonWeekNumber = Functions.Functions.GetWeekNumber().ToString();

                            if (UnityEngine.GUI.Button(new Rect(5, yPosUpdate - scrollPos, GUI_WIDTH - 40, 30), i + ": " + moonName))
                            {
                                selectedMoon = i;
                            }

                            yPosUpdate += 30;
                        }
                    }
                    
                    GUILayout.EndArea();
                    GUILayout.EndVertical();
                    GUILayout.EndScrollView();

                    weekNumText = UnityEngine.GUI.TextField(new Rect(5, GUI_HEIGHT - 140, GUI_WIDTH - 10, GUI_HEIGHT - 275), weekNumText);

                    if(UnityEngine.GUI.Button(new Rect(5, GUI_HEIGHT - 110, GUI_WIDTH - 10, 30), "Send")) 
                    {
                        if (int.Parse(weekNumText) > int.MaxValue) return;
                        try
                        {
                            selectedMoon = int.Parse(weekNumText);
                        }
                        catch(FormatException)
                        {
                            return;
                        }
                    }
                }
            }

            GUILayout.EndVertical();
            UnityEngine.GUI.DragWindow();
        }
    }
}
