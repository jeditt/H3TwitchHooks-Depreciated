using Deli.Setup;
using UnityEngine;
using Valve.VR;
using System.Collections;
using FistVR;
using System.Collections.Generic;

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
            if (Time.timeScale != 1)
            {
                //return time to normal at a gradient
                Time.timeScale += (1f / SlowdownLength) * Time.unscaledDeltaTime;
                Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            }

            if (Input.GetKeyDown(KeyCode.Space)) //Use keycode here, less things can go wrong
            {
                DoSlowmotion();
            }



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

            //shuri spawn
            if (Input.GetKey(KeyCode.B))
            {
                SpawnShuri();
            }

            //nade spawn
            if (Input.GetKeyDown(KeyCode.V))
            {
                SpawnNadeRain();
            }

            //hydration spawn
            if (Input.GetKeyDown(KeyCode.I))
            {
                SpawnHydration();
            }

            //jedit tt spawn
            if (Input.GetKeyDown(KeyCode.U))
            {
                SpawnJeditToy();
            }

        }




        private void DoSlowmotion()
        {
            Time.timeScale = SlowdownFactor;
            Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency;

        }

        private void SpawnWonderfulToy()
        {
            // Get the object you want to spawn
            FVRObject obj = IM.OD["TippyToyAnton"];


            // Instantiate (spawn) the object above the player's right hand
            GameObject go = Instantiate(obj.GetGameObject(), new Vector3(0f, .25f, 0f) + GM.CurrentPlayerBody.Head.position, GM.CurrentPlayerBody.Head.rotation);

            //add some speeeeen
            go.GetComponent<Rigidbody>().AddTorque(new Vector3(.25f, .25f, .25f));


            //add force
            go.GetComponent<Rigidbody>().AddForce(GM.CurrentPlayerBody.Head.forward * 25);
        }

        private void SpawnJeditToy()
        {
            // Get the object you want to spawn
            FVRObject obj = IM.OD["JediTippyToy"];


            // Instantiate (spawn) the object above the player's right hand
            GameObject go = Instantiate(obj.GetGameObject(), new Vector3(0f, .25f, 0f) + GM.CurrentPlayerBody.Head.position, GM.CurrentPlayerBody.Head.rotation);

            //add some speeeeen
            go.GetComponent<Rigidbody>().AddTorque(new Vector3(.25f, .25f, .25f));


            //add force
            go.GetComponent<Rigidbody>().AddForce(GM.CurrentPlayerBody.Head.forward * 25);
        }

        private void SpawnPillow()
        {
            // Get the object you want to spawn
            FVRObject obj = IM.OD["BodyPillow"];


            // Instantiate (spawn) the object above the player head
            GameObject go = Instantiate(obj.GetGameObject(), new Vector3(0f, .25f, 0f) + GM.CurrentPlayerBody.Head.position, GM.CurrentPlayerBody.Head.rotation);


            //add force
            go.GetComponent<Rigidbody>().AddForce(GM.CurrentPlayerBody.Head.forward * 4000);
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
            go.GetComponent<Rigidbody>().AddForce(GM.CurrentPlayerBody.Head.forward * 500);
        }

        private void SpawnNadeRain()
        {
            //    //Set cartridge speed
            float howFast = 15.0f;

            //    //Set max angle
            float maxAngle = 4.0f;

            Transform PointingTransfrom = transform;

            //    //Get Random direction for bullet
            Vector2 randRot = Random.insideUnitCircle;

            // Random number for pull chance
            int pullChance = Random.Range(1, 11);
            Logger.LogInfo(pullChance);

            // Get the object you want to spawn
            FVRObject obj = IM.OD["PinnedGrenadeM67"];

            //Set Object Position
            Vector3 grenadePosition0 = GM.CurrentPlayerBody.Head.position + (GM.CurrentPlayerBody.Head.up * 0.02f);

            // Instantiate (spawn) the object above the player head
            Logger.LogInfo("Spawned Object");
            GameObject go = Instantiate(obj.GetGameObject(), grenadePosition0, Quaternion.LookRotation(GM.CurrentPlayerBody.Head.up));

            //Set Object Direction
            go.transform.Rotate(new Vector3(randRot.x * maxAngle, randRot.y * maxAngle, 0.0f), Space.Self);

            //add force
            Logger.LogInfo("Adding Force");
            go.GetComponent<Rigidbody>().velocity = go.transform.forward * howFast;


            if (pullChance == 10)
            {
                //prime the grenade object
                Logger.LogInfo("Getting Component");
                PinnedGrenade grenade = go.GetComponentInChildren<PinnedGrenade>();
                Logger.LogInfo("Releasing Lever");
                grenade.ReleaseLever();
            }

        }

        private void SpawnShuri()

            {
                //Set cartridge speed
                float howFast = 30.0f;

                //Set max angle
                float maxAngle = 4.0f;

                Transform PointingTransfrom = transform;



                //Get Random direction for bullet
                Vector2 randRot = Random.insideUnitCircle;

                // Get the object I want to spawnz
                FVRObject obj = IM.OD["Shuriken"];

                //Set Object Position
                Vector3 shuriPosition0 = GM.CurrentPlayerBody.Head.position + (GM.CurrentPlayerBody.Head.forward * 0.02f);
         

                //Create Bullet
                //GameObject go0 = Instantiate(obj.GetGameObject(), bulletPosition0, Quaternion.LookRotation(-GM.CurrentPlayerBody.LeftHand.upxx));
                //GameObject go1 = Instantiate(obj.GetGameObject(), bulletPosition0, Quaternion.LookRotation(-GM.CurrentPlayerBody.LeftHand.up));

                //old spray
                GameObject go0 = Instantiate(obj.GetGameObject(), shuriPosition0, Quaternion.LookRotation(GM.CurrentPlayerBody.Head.forward));


                //Set Object Direction
                go0.transform.Rotate(new Vector3(randRot.x * maxAngle, randRot.y * maxAngle, 0.0f), Space.Self);


                //Add Force


                //go0.GetComponent<Rigidbody>().velocity = GM.CurrentPlayerBody.LeftHand.forward * howFast;
                //go1.GetComponent<Rigidbody>().velocity = GM.CurrentPlayerBody.LeftHand.forward * howFast;

                //old spray
                go0.GetComponent<Rigidbody>().velocity = go0.transform.forward * howFast;


            }

        //private void SpawnNade()
        //{

        //}

        private void SpawnHydration()
        {
            // Get the object you want to spawn
            FVRObject obj = IM.OD["SuppressorBottle"];


            // Instantiate (spawn) the object above the player's right hand
            GameObject go = Instantiate(obj.GetGameObject(), new Vector3(0f, .25f, 0f) + GM.CurrentPlayerBody.Head.position, GM.CurrentPlayerBody.Head.rotation);

            //add some speeeeen
            go.GetComponent<Rigidbody>().AddTorque(new Vector3(.25f, .25f, .25f));


            //add force
            go.GetComponent<Rigidbody>().AddForce(GM.CurrentPlayerBody.Head.forward * 25);
        }


        //private void BulletHose()
        //{
        //    //Set cartridge speed
        //    float howFast = 5.0f;

        //    //Set max angle
        //    float maxAngle = 4.0f;

        //    Transform PointingTransfrom = transform;



        //    //Get Random direction for bullet
        //    Vector2 randRot = Random.insideUnitCircle;

        //    // Get the object I want to spawn
        //    FVRObject obj = IM.OD["762x39mmCartridgeFMJ"];

        //    //Set Object Position
        //    Vector3 bulletPosition0 = GM.CurrentPlayerBody.LeftHand.position + (GM.CurrentPlayerBody.LeftHand.forward * 0.02f);
        //    Vector3 bulletPosition1 = GM.CurrentPlayerBody.LeftHand.position + (GM.CurrentPlayerBody.LeftHand.forward * 0.02f + GM.CurrentPlayerBody.LeftHand.up * 0.01f);

        //    //Create Bullet
        //    //GameObject go0 = Instantiate(obj.GetGameObject(), bulletPosition0, Quaternion.LookRotation(-GM.CurrentPlayerBody.LeftHand.up));
        //    //GameObject go1 = Instantiate(obj.GetGameObject(), bulletPosition0, Quaternion.LookRotation(-GM.CurrentPlayerBody.LeftHand.up));

        //    //old spray
        //    GameObject go0 = Instantiate(obj.GetGameObject(), bulletPosition0, Quaternion.LookRotation(GM.CurrentPlayerBody.LeftHand.forward));
        //    GameObject go1 = Instantiate(obj.GetGameObject(), bulletPosition0, Quaternion.LookRotation(GM.CurrentPlayerBody.LeftHand.forward));

        //    //Set Object Direction
        //    go0.transform.Rotate(new Vector3(randRot.x * maxAngle, randRot.y * maxAngle, 0.0f), Space.Self);
        //    go1.transform.Rotate(new Vector3(randRot.x * maxAngle, randRot.y * maxAngle, 0.0f), Space.Self);

        //    //Add Force


        //    //go0.GetComponent<Rigidbody>().velocity = GM.CurrentPlayerBody.LeftHand.forward * howFast;
        //    //go1.GetComponent<Rigidbody>().velocity = GM.CurrentPlayerBody.LeftHand.forward * howFast;

        //    //old spray
        //    go0.GetComponent<Rigidbody>().velocity = go1.transform.forward * howFast;
        //    go1.GetComponent<Rigidbody>().velocity = go1.transform.forward * howFast;


        //}

        //private void SkittySpawn()
        //{
        //    //Set Gun Spawn Speed
        //    float howFast = 5.0f;

        //    //List of possible Weapons
        //    List<string> skittySubGuns = new();

        //    string StenMk2 = "StenMk2";
        //    string ThompsonM1A1 = "ThompsonM1A1";
        //    string PPSh41 = "PPSh41";
        //    string MP40 = "MP40";
        //    string MP18 = "MP18";
        //    string KP31 = "KP31";


        //    skittySubGuns.Add(StenMk2);
        //    skittySubGuns.Add(ThompsonM1A1);
        //    skittySubGuns.Add(PPSh41);
        //    skittySubGuns.Add(MP40);
        //    skittySubGuns.Add(MP18);
        //    skittySubGuns.Add(KP31);

        //    //Get Random Number
        //    int v = Random.Range(0, 6);
        //    int randomNumber = v;





        //}
    }

}