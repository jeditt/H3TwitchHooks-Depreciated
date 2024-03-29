﻿using Deli.Setup;
using UnityEngine;
using Valve.VR;
using System.Collections;
using FistVR;
using System.Collections.Generic;
using System.Linq;

namespace H3TwitchHooks
{
    public class H3TwitchHooks : DeliBehaviour
    {
        //Keyword "const" means that it cannot be changed afterwards
        private const float SlowdownFactor = .001f;
        private const float SlowdownLength = 6f;
        public string SlomoStatus = "Off";
        private const float MaxSlomo = .1f;
        private const float SlomoWaitTime = 2f;
        private List<string> SkittySubGuns = new List<string>();
        private string Magazine = "None";


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

            SkittySubGuns.Add("M3Greasegun");
            SkittySubGuns.Add("KP31");
            SkittySubGuns.Add("MP40");
            SkittySubGuns.Add("StenMk5");
            SkittySubGuns.Add("PPSh41");
        }


        private void Update()
        {
            //This was the old way that time returned via twitch hook
            //if (Time.timeScale != 1)
            //{
            //    //return time to normal at a gradient
            //    Time.timeScale += (1f / SlowdownLength) * Time.unscaledDeltaTime;
            //    Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency;
            //    Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            //}

            //This was the old way that I was expecting a keypress from Instructbot. Combined with on-demand slomo now.
            //if (Input.GetKeyDown(KeyCode.Space)) //Use keycode here, less things can go wrong
            //{
            //    DoSlowmotion();
            //}



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

            if (GM.CurrentMovementManager.Hands[1].Input.AXButtonDown || Input.GetKeyDown(KeyCode.Space))
            {
                //Logger.LogInfo("Detected Right A Press!");
                SlomoStatus = "Slowing";
            }

            if (SlomoStatus == "Slowing")
            {
                //Logger.LogInfo("Slowing!");
                SlomoScaleDown();
            }

            if (SlomoStatus == "Wait")
            {
                //Logger.LogInfo("Waiting!");
                SlomoStatus = "Paused";
                StartCoroutine(SlomoWait());
            }

            if (SlomoStatus == "Return")
            {
                //Logger.LogInfo("Returning!");
                SlomoReturn();
            }

            if (Time.timeScale == 1)
            {
                SlomoStatus = ("Off");
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                DestroyHeld();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                SpawnSkittySubGun();
            }

        }



        //This was the old version of the slomotion method itself
        //private void DoSlowmotion()
        //{
        //    Time.timeScale = SlowdownFactor;
        //    Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency;

        //}

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

        private void SlomoScaleDown()
        {
            if (Time.timeScale > MaxSlomo)
            {
                Time.timeScale -= (1f) * Time.unscaledDeltaTime;
                Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            }

            if (Time.timeScale <= MaxSlomo)
            {
                SlomoStatus = ("Wait");
            }
        }

        private void SlomoReturn()
        {
            if (Time.timeScale != 1)
            {
                Time.timeScale += (1f / 3f) * Time.unscaledDeltaTime;
                Time.fixedDeltaTime = Time.timeScale / SteamVR.instance.hmd_DisplayFrequency;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            }
        }

        IEnumerator SlomoWait()
        {
            yield return new WaitForSecondsRealtime(SlomoWaitTime);
            SlomoStatus = "Return";
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

        private void DestroyHeld()

        {
            if (GM.CurrentMovementManager.Hands[1].CurrentInteractable != null && GM.CurrentMovementManager.Hands[1].CurrentInteractable is FVRPhysicalObject)
            {
                Destroy(GM.CurrentMovementManager.Hands[1].CurrentInteractable.gameObject);
            }
        }

        private void SpawnSkittySubGun()
        {
            SkittySubGuns.Shuffle();
            string TopGun = SkittySubGuns.ElementAt(0);

            //check to see what gun was picked and find the magazine for it
            if (TopGun == "PPSh41")
                {
                    Magazine = "MagazinePPSH41";
                }
            else if (TopGun == "KP31")
                {
                    Magazine = "MagazineKP31";
                }
            else if (TopGun == "M3Greasegun")
                {
                    Magazine = "MagazineM3Greasegun";
                }
            else if (TopGun == "MP40")
                {
                    Magazine = "MagazineMP40";
                }
            else if (TopGun == "StenMk5")
                {
                    Magazine = "MagazineStenMk5";
                }

            // Get the object you want to spawn
            FVRObject obj = IM.OD[TopGun];
            FVRObject obj2 = IM.OD[Magazine];

            // Instantiate (spawn) the object above the player's head
            GameObject go = Instantiate(obj.GetGameObject(), new Vector3(0f, .25f, 0f) + GM.CurrentPlayerBody.Head.position, GM.CurrentPlayerBody.Head.rotation);
            GameObject go2 = Instantiate(obj2.GetGameObject(), new Vector3(0f, .25f, 0f) + GM.CurrentPlayerBody.Head.position, GM.CurrentPlayerBody.Head.rotation);

            //add some speeeeen
            go.GetComponent<Rigidbody>().AddTorque(new Vector3(.25f, .25f, .25f));
            go2.GetComponent<Rigidbody>().AddTorque(new Vector3(.25f, .25f, .25f));

            //add force
            go.GetComponent<Rigidbody>().AddForce(GM.CurrentPlayerBody.Head.forward * 100);
            go2.GetComponent<Rigidbody>().AddForce(GM.CurrentPlayerBody.Head.forward * 100);

        }

    }



}