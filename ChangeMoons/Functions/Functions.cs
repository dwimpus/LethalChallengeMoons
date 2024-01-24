using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ChangeMoons.Functions
{
    public class Functions : MonoBehaviour
    {
        /*
         * Zeekerss exact GetWeekNumber() function
         * GameNetworkManager/GetWeekNumber
         */
        public static int GetWeekNumber()
        {
            DateTime dateTime = new DateTime(2023, 12, 11);
            DateTime dateTime2;

            try
            {
                dateTime2 = DateTime.UtcNow;
            }
            catch (Exception arg)
            {
                dateTime2 = DateTime.Today;
            }

            return (int)((dateTime2 - dateTime).TotalDays / 7.0);
        }
    }
}
