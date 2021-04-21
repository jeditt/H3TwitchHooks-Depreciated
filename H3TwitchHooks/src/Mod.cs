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
         
          //      This is to call the IEnumerator instead. This function currently fights with the StopTimeRandomly one
          //      because I have the math to increase until 1 being called every frame, regardless of what calls it
          //      StartCoroutine(BumpTime());

            
            DoSlowmotion();
            
             Time.timeScale += (1f / SlowdownLength) * Time.unscaledDeltaTime; 
             Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency; 
             Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
               }

             if (Input.GetKeyDown(KeyCode.H))

            {
                //this is currently not working, see DoSlowmotion comment
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

    // The return math doesn't run at update here, so it stops after the first tick. Need to find a way to do this.
    //    IEnumerator BumpTime()
    //    {
    //        Time.timeScale = SlowdownFactor;
    //        Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency;
    //        float bumpDuration = UnityEngine.Random.Range(1, 3); yield return new WaitForSecondsRealtime(bumpDuration);
    //        Time.timeScale += (1f / SlowdownLength) * Time.unscaledDeltaTime; 
    //        Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency; 
    //        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    //    }
    }
}