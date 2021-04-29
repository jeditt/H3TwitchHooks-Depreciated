using Deli.Setup;
using UnityEngine;
using Valve.VR;
using System.Collections;
using FistVR;

namespace H3TwitchHooks 
{
    public class H3TwitchHooks : DeliBehaviour
    {
        //Keyword "const" means that it cannot be changed afterwards
        private const float SlowdownFactor = .001f;
        private const float SlowdownLength = 6f;


        ///This is a constructor, it is called when a new instance of the object is being made! For example
        /// <code>
        /// H3TwitchHooks obj = new H3TwitchHooks();
        /// </code>
        /// In the example when obj is made, the constructor of H3TwitchHooks is called
        public H3TwitchHooks()
        {
            Logger.LogInfo("Loading H3TwitchHooks");
        }



        private void Awake()
        {
            Logger.LogInfo("Successfully loaded H3TwitchHooks!");
        }


        private void Update()
        { 
            if (Input.GetKeyDown(KeyCode.Space)) //Use keycode here, less things can go wrong
              {
                    
             DoSlowmotion();
                
                }
            //return time to normal at a gradient
             Time.timeScale += (1f / SlowdownLength) * Time.unscaledDeltaTime; 
             Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency; 
             Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
              

            //wonderful toy spawn
             if (Input.GetKeyDown(KeyCode.H))

            {
                SpawnWonderfulToy();

                
               
            }

             //body pillow spawn
              if (Input.GetKeyDown(KeyCode.J))

            {
                SpawnPillow();

                
               
            }

              //flash spawn
              if (Input.GetKeyDown(KeyCode.K))
            {
                SpawnFlash();
            }
        }


        

        private void DoSlowmotion()
        {
            Time.timeScale = SlowdownFactor;
            Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency;
            
        }

        IEnumerator StopTimeRandomly()
        {

            // Freeze time
            Time.timeScale = 0f;

            // Pick a random duration from 3 to 10 seconds and wait for it to elapse 

            float duration = UnityEngine.Random.Range(3, 10); yield return new WaitForSecondsRealtime(duration);

            // Return time to normal 
            Time.timeScale = 1f;
        }

        private void SpawnWonderfulToy()
        {
            // Get the object you want to spawn
            FVRObject obj = IM.OD["TippyToyAnton"];
          

            // Instantiate (spawn) the object above the player's right hand
            GameObject go = Instantiate(obj.GetGameObject(), new Vector3(0f, .25f, 0f) + GM.CurrentPlayerBody.Head.position, GM.CurrentPlayerBody.Head.rotation);

            //add some speeeeen
            go.GetComponent<Rigidbody>().AddTorque (new Vector3(.25f, .25f, .25f));
           

            //add force
            go.GetComponent<Rigidbody>().AddForce (GM.CurrentPlayerBody.Head.forward * 25);
        }

        private void SpawnPillow()
        {
            // Get the object you want to spawn
            FVRObject obj = IM.OD["BodyPillow"];
          

            // Instantiate (spawn) the object above the player head
            GameObject go = Instantiate(obj.GetGameObject(), new Vector3(0f, .25f, 0f) + GM.CurrentPlayerBody.Head.position, GM.CurrentPlayerBody.Head.rotation);
           

            //add force
            go.GetComponent<Rigidbody>().AddForce (GM.CurrentPlayerBody.Head.forward * 10000);
        }
        
        //we want to spawn a flashbang infront of the player with little notice
        private void SpawnFlash()
        {
            // Get the object you want to spawn
            FVRObject obj = IM.OD["PinnedGrenadeXM84"];


            // Instantiate (spawn) the object above the player head
            Logger.LogInfo("Spawned Object");
            GameObject go = Instantiate(obj.GetGameObject(), new Vector3(0f, .25f, 0f) + GM.CurrentPlayerBody.Head.position, GM.CurrentPlayerBody.Head.rotation);
            

            //prime the flash object
            Logger.LogInfo("Getting Component");
            PinnedGrenade grenade = go.GetComponentInChildren<PinnedGrenade>();
            Logger.LogInfo("Releasing Lever");
            grenade.ReleaseLever();
            
           

            //add force
            Logger.LogInfo("Adding Force");
            go.GetComponent<Rigidbody>().AddForce (GM.CurrentPlayerBody.Head.forward * 500);
        }
        
       

   
    }
}