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
using System.Collections;




namespace TwitchSloMo
{
    // DeliBehaviours are just MonoBehaviours that get added to a global game object when the game first starts.
    public class H3THTimeFreeze : DeliBehaviour
    {
        float slowdownFactor = .001f;
        float slowdownLength = 6f;



        private void Update()
        {

            if (Input.GetKeyDown("h"))

            {
                StopTimeRandomly();
            }



        }





        IEnumerator StopTimeRandomly()
        {

            // Freeze time
            Time.timeScale = 0f;

            // Pick a random duration from 3 to 10 seconds and wait for it to elapse 

            float duration = UnityEngine.Random.Range(3, 10); yield return new WaitForSeconds(duration);

            // Return time to normal 
            Time.timeScale = 1f;
        }
    }

}
