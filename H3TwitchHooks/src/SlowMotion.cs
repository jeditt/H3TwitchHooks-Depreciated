using FistVR;
using UnityEngine;
using Valve.VR;

namespace H3TwitchHooks
{
    public static class SlowMotion
    {
        private const float SLOWDOWN_FACTOR = .001f;
        private const float SLOWDOWN_LENGTH = 6f;

        public static void Execute()
        {
            Time.timeScale = SLOWDOWN_FACTOR;
            Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency;
            
            //return time to normal at a gradient
            Time.timeScale += (1f / SLOWDOWN_LENGTH) * Time.unscaledDeltaTime; 
            Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency; 
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }

        public static void Execute(float slowdownFactor, float slowdownLength)
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency;

            //return time to normal at a gradient
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            
        }
    }
}