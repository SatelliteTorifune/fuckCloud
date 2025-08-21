using System;
using ModApi.Flight.Sim;
using UnityEngine;

namespace Assets.Scripts
{
    public class CloudManager:MonoBehaviour
    {
        public GameObject FakeCloudSphere;
        private bool isDroo;
        public string ShaderParam;
        public float ShaderParamvalue;

        void Start()
        {
            CheckCloudSphere();
        }
        private void OnPlanetChangedSOI(IOrbitNode source)
        {
            CheckCloudSphere();
        }
        

        void Update()
        {
            if (isDroo)
            {
                //FakeCloudSphere.transform.position = Game.Instance.FlightScene.CraftNode.ReferenceFrame.PlanetToFramePosition(Vector3d.zero);
                //FakeCloudSphere.transform.position = Game.Instance.FlightScene.CraftNode.CraftScript.Transform.position;
                //FakeCloudSphere.transform.rotation =Game.Instance.FlightScene.CraftNode.CraftScript.Transform.rotation;
                FakeCloudSphere.transform.position =
                    Game.Instance.FlightScene.ViewManager.GameView.GameCamera.Transform.transform.position
                    ;
            }
        }
        private void SettingChanged(object sender, EventArgs e)
        {
            CheckCloudSphere();
        }
        
        
        public void SetValue(Material m)
        {
            m.SetFloat(ShaderParam, ShaderParamvalue);
        }

        private void CheckCloudSphere()
        {
            Debug.Log("checking cloud sphere");
            isDroo = Game.Instance.FlightScene.CraftNode.Parent.Name == "Droo"|| Game.Instance.FlightScene.CraftNode.Parent.Name == "Earth";
            if (isDroo)
            {
                Debug.Log("real cloud sphere");
                if (FakeCloudSphere == null)
                {
                    FakeCloudSphere = Instantiate(Mod.Instance.ResourceLoader.LoadAsset<GameObject>("FakeCloudSphere"));
                    Debug.Log("我操这啥");
                }

                var Clouds = FakeCloudSphere.GetComponent<Material>();

                FakeCloudSphere.transform.localScale = new Vector3(1e5f, 1e5f, 1e5f);
               
                //FakeCloudSphere.transform.localScale =  (2 + ModSettings.Instance.CloudHeight) * (float)Game.Instance.FlightScene.CraftNode.Parent.PlanetData.Radius * Vector3.one;
            }
            else if (FakeCloudSphere != null)
            {
                Destroy(FakeCloudSphere);
            }
        }
    }
}