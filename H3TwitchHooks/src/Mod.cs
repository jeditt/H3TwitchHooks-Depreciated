using Deli.Setup;
using UnityEngine;
using Valve.VR;
using System.Collections;
using FistVR;

namespace H3TwitchHooks 
{
    public class H3TwitchHooks : DeliBehaviour
    {
        private ObjectSpawner _spawner;
        
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
            
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SlowMotion.Execute();
            }

            //wonderful toy spawn
            if (Input.GetKeyDown(KeyCode.H))
            {
                _spawner = new ObjectSpawner("TippyToyAnton");
                _spawner.Spawn
                    (
                        new Vector3(0f, .25f, 0f) + GM.CurrentPlayerBody.Head.position, 
                        GM.CurrentPlayerBody.Head.rotation, 
                        new Vector3(.25f, .25f, .25f), 
                        GM.CurrentPlayerBody.Head.forward * 25
                    );
            }

            //body pillow spawn
            if (Input.GetKeyDown(KeyCode.J))
            {
                _spawner = new ObjectSpawner("BodyPillow");
                _spawner.Spawn
                (
                    new Vector3(0f, .25f, 0f) + GM.CurrentPlayerBody.Head.position, 
                    GM.CurrentPlayerBody.Head.rotation,
                    Vector3.zero, 
                    GM.CurrentPlayerBody.Head.forward * 10000
                );
            }

            //flash spawn
            if (Input.GetKeyDown(KeyCode.K))
            {
                _spawner = new ObjectSpawner("PinnedGrenadeXM84", true);
                _spawner.Spawn
                (
                    new Vector3(0f, .25f, 0f) + GM.CurrentPlayerBody.Head.position, 
                    GM.CurrentPlayerBody.Head.rotation,
                    Vector3.zero, 
                    GM.CurrentPlayerBody.Head.forward * 500
                );
            }
        }

        #region Old code

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
            GameObject go = Instantiate(obj.GetGameObject(),
                new Vector3(0f, .25f, 0f) + GM.CurrentPlayerBody.Head.position, GM.CurrentPlayerBody.Head.rotation);


            //prime the flash object
            Logger.LogInfo("Getting Component");
            PinnedGrenade grenade = go.GetComponentInChildren<PinnedGrenade>();
            Logger.LogInfo("Releasing Lever");
            grenade.ReleaseLever();



            //add force
            Logger.LogInfo("Adding Force");
            go.GetComponent<Rigidbody>().AddForce(GM.CurrentPlayerBody.Head.forward * 500);
        }
        
        #endregion
   
    }
}