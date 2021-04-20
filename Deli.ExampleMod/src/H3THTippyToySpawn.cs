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
    public class H3THTippyToySpawn : DeliBehaviour
    {




        private void Update()
        {

            if (Input.GetKeyDown("j"))

            {
                SpawnTippyToy();
            }



        }

        private void SpawnTippyToy()
        
        {
            Instantiate(FVRObject.)
    

        }

        

      
            
       
    }
}