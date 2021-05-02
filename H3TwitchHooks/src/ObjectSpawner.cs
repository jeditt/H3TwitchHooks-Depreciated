using System;
using FistVR;
using UnityEngine;

namespace H3TwitchHooks
{
    public class ObjectSpawner
    {
        private bool _isGrenade;
        private string _objID;
        private GameObject _obj;
        private GameObject? _spawnedObj; //The ? declares it as nullable (is able to be nothing)
        private bool _primeGrenade;

        public ObjectSpawner(string objectID, bool primeGrenadeOnSpawn = false)
        {
            _objID = objectID;
            _primeGrenade = primeGrenadeOnSpawn;

            //Try to get the object
            try
            {
                _obj = IM.OD[_objID].GetGameObject();
            }
            catch (Exception e)
            {
                Debug.Log($"Could not find object with objectID of {_objID}");
                Debug.Log(e.Message);
                Debug.Log("Defaulting to \"TippyToyAnton\"");
                _obj = IM.OD["TippyToyAnton"].GetGameObject();
            }

            //Checks of the object is a grenade
            if(IM.OD[_objID].Category is FVRObject.ObjectCategory.Explosive or FVRObject.ObjectCategory.Thrown)
            {
                _isGrenade = true;
            }
        }

        public void Spawn(Vector3 location, Quaternion rotation, Vector3 speen, Vector3 force)
        {
            _spawnedObj = GameObject.Instantiate(_obj, location, rotation);
            //Assign `GetComponent`s to a variable, because `GetComponent` is resource intensive
            Rigidbody objphys = _spawnedObj.GetComponent<Rigidbody>();

            objphys.AddTorque(speen);
            
            objphys.AddForce(force);

            //If it aint a grenade, dont touch it, or if you dont want it to go BOOM
            if (!_isGrenade || !_primeGrenade) return;
            
            PinnedGrenade grenade;
            try
            {
                grenade = _spawnedObj.GetComponent<PinnedGrenade>();
            }
            catch (Exception e)
            {
                Debug.Log($"Could not find PinnedGrenade component on {_spawnedObj.name}");
                Debug.Log(e.Message);
                return;
            }
            grenade.ReleaseLever();
        }
    }
}