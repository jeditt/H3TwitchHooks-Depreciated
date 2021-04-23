using Deli.Setup;
using UnityEngine;
using Valve.VR;
using System.Collections;

namespace H3TwitchHooks 
{
    public class H3TwitchHooks : DeliBehaviour
    {
        //Keyword "const" means that it cannot be changed afterwards
        private const float SlowdownFactor = .001f;
        private const float SlowdownLength = 5f;


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
      
            //return time to normal at a gradient
             Time.timeScale += (1f / SlowdownLength) * Time.unscaledDeltaTime; 
             Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency; 
             Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
               }

             if (Input.GetKeyDown(KeyCode.H))

            {
                //this is currently not working, as the return time code above is kicking in every frame, overriding the wait that the coroutine wants to perform
                StartCoroutine(StopTimeRandomly());

                
               
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

   
    }
}