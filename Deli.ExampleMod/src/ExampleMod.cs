using System;
using Deli.Immediate;
using Deli.Setup;
using Deli.VFS;
using Deli.VFS.Disk;
using UnityEngine.SceneManagement;
using HarmonyLib;
using UnityEngine;
using Valve.VR;
using FistVR;




namespace TwitchSloMo
{
    // DeliBehaviours are just MonoBehaviours that get added to a global game object when the game first starts.
    public class TwitchSloMo : DeliBehaviour
    {
        float slowdownFactor = .001f;
        float slowdownLength = 6f;

        

        private void Update()
        {
         
           if (Input.GetKeyDown("space"))

            {
                DoSlowmotion();
            }
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            


        }


        

        void DoSlowmotion()
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency;
            
        }
    }
}