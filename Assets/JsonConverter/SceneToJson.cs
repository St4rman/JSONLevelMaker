using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


/// <summary>
/// Add Cubes to the GroundRoot object and set StartPoint EndPOint's transform. 
/// dont change names in the inspector, if you can thanks. 
/// One cube = 1 meter so scale is treated like that :]
/// </summary>


public class SceneToJson : MonoBehaviour
{
    [SerializeField] string levelName;

    [Serializable]
    class GameObjectPrimitive { 
        public GameObjectPrimitive() { }
        public GameObjectPrimitive(string pMesh, Vector3 dims, Quaternion rot, Vector3 pos, float invMass, 
                                string phType,  string volType, Vector3 colExt, float colRadius, bool shouldNet){
            mesh =  pMesh;
            dimensions = dims;
            rotation = rot;
            position = pos;
            inverseMass = invMass;
            physicType = phType;
            colliderExtents = colExt;
            colliderRadius = colRadius;
            position   = pos;
            shouldNetwork = false;
        }

        public string mesh;
        public Vector3 dimensions;
        public Quaternion rotation;
        public Vector3 position;
        public float inverseMass;
        public string physicType;
        public string volumeType;
        public Vector3 colliderExtents;
        public float colliderRadius;
        public bool shouldNetwork;
    
    }

    [Serializable]
    class Oscillaters : GameObjectPrimitive {
        public Oscillaters(string pMesh, Vector3 dims, Quaternion rot, Vector3 pos, string phType, string volType, Vector3 colExt, float colRadius, float tPeriod, float pDist, Vector3 pDir, float pcooldown, float pwait)
        {
            mesh = pMesh;
            dimensions = dims;
            rotation = rot;
            position = pos;
            inverseMass = 0.0f;
            physicType = phType;
            colliderExtents = colExt;
            colliderRadius = colRadius;
            position = pos;
            shouldNetwork = true;
            timePeriod = tPeriod;
            dist = pDist;
            direction = pDir;
            cooldown = pcooldown;
            waitDelay = pwait;
        }
        public float timePeriod;
        public float dist;
        public Vector3 direction;
        public float cooldown;
        public float waitDelay;
        
    }

    [Serializable]
    class Swinging : GameObjectPrimitive {
        public Swinging(string pMesh, Vector3 dims, Quaternion rot, Vector3 pos, string phType, string volType, Vector3 colExt, float colRadius, float tPeriod, float pcooldown, float pwait, float pRadius, bool pAxisChange, bool pDirectionChange)
        {
            mesh = pMesh;
            dimensions = dims;
            rotation = rot;
            position = pos;
            inverseMass = 0.0f;
            physicType = phType;
            colliderExtents = colExt;
            colliderRadius = colRadius;
            position = pos;
            shouldNetwork = true;
            timePeriod = tPeriod;
            cooldown = pcooldown;
            waitDelay = pwait;
            radius = pRadius;
            changeAxis = pAxisChange;
            changeDirection = pDirectionChange;
        }
        public float timePeriod;
        public float cooldown;
        public float waitDelay;
        public float radius;
        public bool changeAxis;
        public bool changeDirection;

    }

    [Serializable]
    class SpringInfo : GameObjectPrimitive
    {
        public SpringInfo(string pMesh, Vector3 dims, Quaternion rot, Vector3 pos, string phType, string volType, Vector3 colExt, float colRadius,
            Vector3 dir, float f, float activeT, bool continuous, float continuousF)
        {
            mesh = pMesh;
            dimensions = dims;
            rotation = rot;
            position = pos;
            inverseMass = 0.0f;
            physicType = phType;
            colliderExtents = colExt;
            colliderRadius = colRadius;
            position = pos;
            shouldNetwork = false;
            direction = dir;
            force = f;
            activeTime = activeT;
            isContinuous = continuous;
            continuousForce = continuousF;

        }
        public Vector3 direction;
        public float force;
        public float activeTime;
        public bool isContinuous;
        public float continuousForce;

    }

    [Serializable]
    class SpeedblockInfo : GameObjectPrimitive
    {
        public SpeedblockInfo(string pMesh, Vector3 dims, Quaternion rot, Vector3 pos, string phType, string volType, Vector3 colExt, float colRadius)
        {
            mesh = pMesh;
            dimensions = dims;
            rotation = rot;
            position = pos;
            inverseMass = 0.0f;
            physicType = phType;
            colliderExtents = colExt;
            colliderRadius = colRadius;
            position = pos;
            shouldNetwork = false;
        }
    }

    [Serializable]
    class BridgeInfo : GameObjectPrimitive
    {
        public BridgeInfo(string pMesh, Vector3 dims, Quaternion rot, Vector3 pos, string phType, string volType, Vector3 colExt, float colRadius)
        {
            mesh = pMesh;
            dimensions = dims;
            rotation = rot;
            position = pos;
            inverseMass = 0.0f;
            physicType = phType;
            colliderExtents = colExt;
            colliderRadius = colRadius;
            position = pos;
            shouldNetwork = false;
        }
    }

    [Serializable]
    class TrapblockInfo : GameObjectPrimitive
    {
        public TrapblockInfo(string pMesh, Vector3 dims, Quaternion rot, Vector3 pos, string phType, string volType, Vector3 colExt, float colRadius)
        {
            mesh = pMesh;
            dimensions = dims;
            rotation = rot;
            position = pos;
            inverseMass = 0.0f;
            physicType = phType;
            colliderExtents = colExt;
            colliderRadius = colRadius;
            position = pos;
            shouldNetwork = false;
        }
    }

    [Serializable]
    class RayenemyInfo : GameObjectPrimitive
    {
        public RayenemyInfo(string pMesh, Vector3 dims, Quaternion rot, Vector3 pos, string phType, string volType, Vector3 colExt, float colRadius)
        {
            mesh = pMesh;
            dimensions = dims;
            rotation = rot;
            position = pos;
            inverseMass = 0.0f;
            physicType = phType;
            colliderExtents = colExt;
            colliderRadius = colRadius;
            position = pos;
            shouldNetwork = false;
        }
    }

    [Serializable]
    class RayEnemyTriggerInfo : GameObjectPrimitive
    {
        public RayEnemyTriggerInfo(string pMesh, Vector3 dims, Quaternion rot, Vector3 pos, string phType, string volType, Vector3 colExt, float colRadius)
        {
            mesh = pMesh;
            dimensions = dims;
            rotation = rot;
            position = pos;
            inverseMass = 0.0f;
            physicType = phType;
            colliderExtents = colExt;
            colliderRadius = colRadius;
            position = pos;
            shouldNetwork = false;
        }
    }

    [Serializable]
    class BridgeTriggerInfo : GameObjectPrimitive
    {
        public BridgeTriggerInfo(string pMesh, Vector3 dims, Quaternion rot, Vector3 pos, string phType, string volType, Vector3 colExt, float colRadius)
        {
            mesh = pMesh;
            dimensions = dims;
            rotation = rot;
            position = pos;
            inverseMass = 0.0f;
            physicType = phType;
            colliderExtents = colExt;
            colliderRadius = colRadius;
            position = pos;
            shouldNetwork = false;
        }
    }

    [Serializable]
    class LightInfo
    {
        public Vector3 position;
        public Vector4 colour;
        public float radius;
    }

    [Serializable]
    class Stage {
        public Stage(Vector3 StartPos, Vector3 EndPos, Vector3 DeathPlane){
            StartPoint =        StartPos;
            EndPoint =          EndPos;
            this.DeathPlane =        DeathPlane;

            primitiveGameObject =   new List<GameObjectPrimitive>();
            oscList =               new List<Oscillaters>();
            harmOscList =               new List<Oscillaters>();
            checkPoints =           new List<Vector3>();
            pointLights = new List<LightInfo>();
            springs = new List<SpringInfo>();
            medalInfo = new MedalsInfo();
            speedBlockList = new List<SpeedblockInfo>();
            bridgeList = new List<BridgeInfo>();
            trapBlockList = new List<TrapblockInfo>();
            rayenemyList = new List<RayenemyInfo>();
            rayenemytriList = new List<RayEnemyTriggerInfo>();
            bridgetriList = new List<BridgeTriggerInfo>();
            swingingList = new List<Swinging>();
        }
        public int getListCount(){
            return primitiveGameObject.Count;
        }
        public Vector3 StartPoint;
        public Vector3 EndPoint;
        public Vector3 DeathPlane;
        public List<Vector3> checkPoints;
        public List<GameObjectPrimitive> primitiveGameObject;
        public List<Oscillaters> oscList;
        public List<Oscillaters> harmOscList;
        public List<Swinging> swingingList;
        public List<LightInfo> pointLights;
        public List<SpringInfo> springs;
        public MedalsInfo medalInfo;

        public List<SpeedblockInfo> speedBlockList;
        public List<BridgeInfo> bridgeList;
        public List<TrapblockInfo> trapBlockList;
        public List<RayenemyInfo> rayenemyList;
        public List<RayEnemyTriggerInfo> rayenemytriList;
        public List<BridgeTriggerInfo> bridgetriList;

    }

    [Serializable]
    class MedalsInfo
    {
        public float platinum;
        public float gold;
        public float silver;
    }

    Stage level;

    private void Start(){

        Debug.Log("WORKING!!!");
        GameObject GroundR  = GameObject.Find("GroundRoot");
        GameObject Start    = GameObject.Find("StartPoint");
        GameObject End      = GameObject.Find("EndPoint");
        GameObject OscR     = GameObject.Find("OscillatingPlatforms");
        GameObject CPR      = GameObject.Find("Checkpoints");
        GameObject DP       = GameObject.Find("DeathPlane");
        GameObject HarmOscR       = GameObject.Find("HarmfulOscillators");
        GameObject SwingObjs       = GameObject.Find("SwingingObjects");
        GameObject LightR       = GameObject.Find("Lights");
        GameObject SpringR       = GameObject.Find("Springs");

        GameObject MedalsObj = GameObject.Find("Medals");

        GameObject SpeedblockR       = GameObject.Find("SpeedBlocks");
        GameObject BridgeR  =   GameObject.Find("Bridges");
        GameObject TrapBlockR = GameObject.Find("TrapBlocks");
        GameObject RayEnemyR = GameObject.Find("RayEnemys");
        GameObject RayTriR = GameObject.Find("EnemyTrigger");
        GameObject BridgeTriR = GameObject.Find("BridgeTrigger");


        if (GroundR == null || Start == null || End == null || OscR == null || CPR == null || DP == null || HarmOscR == null || LightR == null || SpringR == null || SpeedblockR== null || BridgeR == null || TrapBlockR == null || RayEnemyR == null || RayTriR == null || BridgeTriR == null)
        {
            Debug.LogError("No essestial objects. Check for ground, start, or end");
            return;
        }

        level = new Stage(Start.transform.position, End.transform.position, DP.transform.position);

        CreateGroundObjects     (GroundR.transform);
        CreateOscillatorObjects (OscR.transform);
        CreateHarmfulOscillatorObjects(HarmOscR.transform);
        CreateSwingingObjects(SwingObjs.transform);
        CreateCheckPoints(CPR.transform);
        CreateMedalsObject(MedalsObj);
        CreateLights(LightR.transform);
        CreateSprings(SpringR.transform);

        CreateSpeedblocks(SpeedblockR.transform);
        CreatBridges (BridgeR.transform);
        CreateTrapBlocks(TrapBlockR.transform);
        CreateRayEnemys (RayEnemyR.transform);
        CreateRayEnemyTrigger(RayTriR.transform);
        CreateBridgeTrigger(BridgeTriR.transform);

        Debug.Log("Loaded!");
        string json = JsonUtility.ToJson(level);
        WriteJson(json);

    }
    

    private void WriteJson(string json){
        File.WriteAllText(Application.dataPath + "/" + levelName + ".json", json);
    }

    //iterate through all kdis of groundroot and add to level
    private void CreateGroundObjects(Transform GroundRoot){     
        if(GroundRoot.childCount ==0){
            Debug.LogError("No ground Objects!");
            return;
        }

        foreach(Transform child in GroundRoot){

            // Debug.Log(child.GetComponent<MeshFilter>().sharedMesh.name);
            GameObjectPrimitive temp = new GameObjectPrimitive(
            GetMeshName(child.gameObject),
            child.transform.localScale, 
            child.transform.rotation, 
            child.transform.position,
            child.GetComponent<Rigidbody>().mass, child.tag, child.GetComponent<Collider>().GetType().ToString(), new Vector3(0,0,0), 0, true);

            if(child.GetComponent<Collider>().GetType() == typeof(BoxCollider)){
                temp.volumeType = "box";
                temp.colliderExtents = Vector3.Scale(child.transform.localScale, child.GetComponent<BoxCollider>().size);
                temp.colliderRadius = 0;
                // Debug.Log("box");

            } else if (child.GetComponent<Collider>().GetType() == typeof(SphereCollider)){
                temp.volumeType = "sphere";
                temp.colliderRadius = child.transform.localScale.x  * child.GetComponent<SphereCollider>().radius;
                temp.colliderExtents = new Vector3(0,0,0);
                // Debug.Log("circle");
            }
            level.primitiveGameObject.Add(temp);

            Debug.Log("Ground Added");
        } 

        if(GroundRoot.childCount != level.getListCount()){
            Debug.LogError("Error in Ground Block Children");
        }else{

        }
    }

    private void CreateMedalsObject(GameObject obj)
    {
        Medals m = obj.GetComponent<Medals>();
        Debug.Log(m);
        level.medalInfo.platinum = m.platinumTime;
        level.medalInfo.gold = m.goldTime;
        level.medalInfo.silver= m.silverTime;

    }
    
   private void CreateOscillatorObjects(Transform OscillatorRoot){
        if(OscillatorRoot.childCount == 0){
            Debug.Log("No Oscillators in level");
            return;
        }

        foreach(Transform child in OscillatorRoot){
            OscPlat data = child.GetComponent<OscPlat>();
            
            Oscillaters tempOs = new Oscillaters(
                GetMeshName(child.gameObject), 
                data.dimensions,
                child.rotation,
                data.position,
                "",
                child.GetComponent<Collider>().GetType().ToString(),
                new Vector3(0, 0, 0),
                0,
                data.timePeriod,
                data.dist,
                data.direction,
                data.cooldown,
                data.waitDelay
            );

            if (child.GetComponent<Collider>().GetType() == typeof(BoxCollider))
            {
                tempOs.volumeType = "box";
                tempOs.colliderExtents = Vector3.Scale(child.transform.localScale,child.GetComponent<BoxCollider>().size);
                tempOs.colliderRadius = 0;
                // Debug.Log("box");

            }
            else if (child.GetComponent<Collider>().GetType() == typeof(SphereCollider))
            {
                tempOs.volumeType = "sphere";
                tempOs.colliderRadius = child.GetComponent<SphereCollider>().radius;
                tempOs.colliderExtents = new Vector3(0, 0, 0);
                // Debug.Log("circle");
            }
            level.oscList.Add(tempOs);
            Debug.Log("Added Oscillator");
        }
   }

    private void CreateSwingingObjects(Transform SwingingRoot)
    {
        if (SwingingRoot.childCount == 0)
        {
            Debug.Log("No swinging objects in level");
            return;
        }

        foreach (Transform child in SwingingRoot)
        {
            SwingingObject data = child.GetComponent<SwingingObject>();

            Swinging tempSwing = new Swinging(
                GetMeshName(child.gameObject),
                data.dimensions,
                child.rotation,
                data.position,
                "",
                child.GetComponent<Collider>().GetType().ToString(),
                new Vector3(0, 0, 0),
                0,
                data.timePeriod,
                data.cooldown,
                data.waitDelay,
                data.radius,
                data.changeAxis,
                data.changeDirection
            );

            if (child.GetComponent<Collider>().GetType() == typeof(BoxCollider))
            {
                tempSwing.volumeType = "box";
                tempSwing.colliderExtents = Vector3.Scale(child.transform.localScale, child.GetComponent<BoxCollider>().size);
                tempSwing.colliderRadius = 0;
                // Debug.Log("box");

            }
            else if (child.GetComponent<Collider>().GetType() == typeof(SphereCollider))
            {
                tempSwing.volumeType = "sphere";
                tempSwing.colliderRadius = child.GetComponent<SphereCollider>().radius;
                tempSwing.colliderExtents = new Vector3(0, 0, 0);
                // Debug.Log("circle");
            }
            level.swingingList.Add(tempSwing);
            Debug.Log("Added swinging object");
        }
    }

    private void CreateHarmfulOscillatorObjects(Transform harmfulOscillatorRoot)
    {
        if (harmfulOscillatorRoot.childCount == 0)
        {
            Debug.Log("No Harmful Oscillators in level");
            return;
        }

        foreach (Transform child in harmfulOscillatorRoot)
        {
            OscPlat data = child.GetComponent<OscPlat>();

            Oscillaters tempOs = new Oscillaters(
                GetMeshName(child.gameObject),
                data.dimensions,
                child.rotation,
                data.position,
                "",
                child.GetComponent<Collider>().GetType().ToString(),
                new Vector3(0, 0, 0),
                0,
                data.timePeriod,
                data.dist,
                data.direction,
                data.cooldown,
                data.waitDelay
            );
            if (child.GetComponent<Collider>().GetType() == typeof(BoxCollider))
            {
                tempOs.volumeType = "box";
                tempOs.colliderExtents = Vector3.Scale(child.transform.localScale, child.GetComponent<BoxCollider>().size);
                tempOs.colliderRadius = 0;
                // Debug.Log("box");

            }
            else if (child.GetComponent<Collider>().GetType() == typeof(SphereCollider))
            {
                tempOs.volumeType = "sphere";
                tempOs.colliderRadius = child.GetComponent<SphereCollider>().radius;
                tempOs.colliderExtents = new Vector3(0, 0, 0);
                // Debug.Log("circle");
            }
            level.harmOscList.Add(tempOs);
            Debug.Log("Added Harmful Oscillator");
        }
    }

    private void CreateCheckPoints(Transform CheckRoot){
        if(CheckRoot.childCount == 0){
            Debug.LogError("No CheckPoints");
            return;
        }

        foreach(Transform child in CheckRoot){
            level.checkPoints.Add(child.transform.position);
            Debug.Log("Added CheckPoint");

        }

   }

    void CreateLights(Transform root)
    {
        foreach (Transform child in root)
        {
            Light lightComponent = child.GetComponent<Light>();
            LightInfo lightInfo = new LightInfo();
            lightInfo.radius = lightComponent.range;
            lightInfo.colour = lightComponent.color;
            lightInfo.position = child.transform.position;

            level.pointLights.Add(lightInfo);
            Debug.Log("ADDED LIGHT!");
        }
    }

    void CreateSprings(Transform root)
    {
        foreach (Spring child in root.GetComponentsInChildren<Spring>())
        {
            SpringInfo spring = new SpringInfo(
                GetMeshName(child.gameObject),
                child.dimensions,
                child.transform.rotation,
                child.position,
                "",
                child.GetComponent<Collider>().GetType().ToString(),
                new Vector3(0, 0, 0),
                0,
                child.direction,
                child.force,
                child.activeTime,
                child.isContinuous,
                child.continuousForce
            );

            if (child.GetComponent<Collider>().GetType() == typeof(BoxCollider))
            {
                spring.volumeType = "box";
                spring.colliderExtents = Vector3.Scale(child.transform.localScale, child.GetComponent<BoxCollider>().size);
                spring.colliderRadius = 0;
                // Debug.Log("box");

            }
            else if (child.GetComponent<Collider>().GetType() == typeof(SphereCollider))
            {
                spring.volumeType = "sphere";
                spring.colliderRadius = child.GetComponent<SphereCollider>().radius;
                spring.colliderExtents = new Vector3(0, 0, 0);
                // Debug.Log("circle");
            }
            Debug.Log("Added Spring!");
            level.springs.Add(spring);
        }

    }



    void CreateSpeedblocks(Transform root)
    {
        foreach (Speedblock child in root.GetComponentsInChildren<Speedblock>())
        {
            SpeedblockInfo speedblock = new SpeedblockInfo(
            GetMeshName(child.gameObject),
            child.transform.localScale,
            child.transform.rotation,
            child.transform.position, child.tag, child.GetComponent<Collider>().GetType().ToString(), new Vector3(0, 0, 0), 0);

            if (child.GetComponent<Collider>().GetType() == typeof(BoxCollider))
            {
                speedblock.volumeType = "box";
                speedblock.colliderExtents = Vector3.Scale(child.transform.localScale, child.GetComponent<BoxCollider>().size);
                speedblock.colliderRadius = 0;
                // Debug.Log("box");

            }
            else if (child.GetComponent<Collider>().GetType() == typeof(SphereCollider))
            {
                speedblock.volumeType = "sphere";
                speedblock.colliderRadius = child.transform.localScale.x * child.GetComponent<SphereCollider>().radius;
                speedblock.colliderExtents = new Vector3(0, 0, 0);
                // Debug.Log("circle");
            }

            Debug.Log("Speedblock Added");
            level.speedBlockList.Add(speedblock);
        }

    }

    void CreatBridges(Transform root)
    {
        foreach (Bridge child in root.GetComponentsInChildren<Bridge>())
        {
            BridgeInfo bridge = new BridgeInfo(GetMeshName(child.gameObject),
                child.transform.localScale,
                child.transform.rotation,
                child.transform.position, child.tag, child.GetComponent<Collider>().GetType().ToString(), new Vector3(0, 0, 0), 0);
            if (child.GetComponent<Collider>().GetType() == typeof(BoxCollider))
            {
                bridge.volumeType = "box";
                bridge.colliderExtents = Vector3.Scale(child.transform.localScale, child.GetComponent<BoxCollider>().size);
                bridge.colliderRadius = 0;
                // Debug.Log("box");

            }
            else if (child.GetComponent<Collider>().GetType() == typeof(SphereCollider))
            {
                bridge.volumeType = "sphere";
                bridge.colliderRadius = child.transform.localScale.x * child.GetComponent<SphereCollider>().radius;
                bridge.colliderExtents = new Vector3(0, 0, 0);
                // Debug.Log("circle");
            }

            Debug.Log("Bridge Added");
            level.bridgeList.Add(bridge);
        }
    }
    void CreateTrapBlocks(Transform root)
    {
        foreach (Trapblock child in root.GetComponentsInChildren<Trapblock>())
        {
            TrapblockInfo trapblock = new TrapblockInfo(
            GetMeshName(child.gameObject),
            child.transform.localScale,
            child.transform.rotation,
            child.transform.position, child.tag, child.GetComponent<Collider>().GetType().ToString(), new Vector3(0, 0, 0), 0);

            if (child.GetComponent<Collider>().GetType() == typeof(BoxCollider))
            {
                trapblock.volumeType = "box";
                trapblock.colliderExtents = Vector3.Scale(child.transform.localScale, child.GetComponent<BoxCollider>().size);
                trapblock.colliderRadius = 0;
                // Debug.Log("box");

            }
            else if (child.GetComponent<Collider>().GetType() == typeof(SphereCollider))
            {
                trapblock.volumeType = "sphere";
                trapblock.colliderRadius = child.transform.localScale.x * child.GetComponent<SphereCollider>().radius;
                trapblock.colliderExtents = new Vector3(0, 0, 0);
                // Debug.Log("circle");
            }

            Debug.Log("Trapblock Added");
            level.trapBlockList.Add(trapblock);
        }
    }

    void CreateRayEnemys(Transform root)
    {
        foreach (RayEnemy child in root.GetComponentsInChildren<RayEnemy>())
        {
            RayenemyInfo rayenemy = new RayenemyInfo(
            GetMeshName(child.gameObject),
            child.transform.localScale,
            child.transform.rotation,
            child.transform.position, child.tag, child.GetComponent<Collider>().GetType().ToString(), new Vector3(0, 0, 0), 0);

            if (child.GetComponent<Collider>().GetType() == typeof(BoxCollider))
            {
                rayenemy.volumeType = "box";
                rayenemy.colliderExtents = Vector3.Scale(child.transform.localScale, child.GetComponent<BoxCollider>().size);
                rayenemy.colliderRadius = 0;
                // Debug.Log("box");

            }
            else if (child.GetComponent<Collider>().GetType() == typeof(SphereCollider))
            {
                rayenemy.volumeType = "sphere";
                rayenemy.colliderRadius = child.transform.localScale.x * child.GetComponent<SphereCollider>().radius;
                rayenemy.colliderExtents = new Vector3(0, 0, 0);
                // Debug.Log("circle");
            }

            Debug.Log("Rayenemy Added");
            level.rayenemyList.Add(rayenemy);
        }
    }

    void CreateRayEnemyTrigger(Transform root)
    {
        foreach (RayEnemyTrigger child in root.GetComponentsInChildren<RayEnemyTrigger>())
        {
            RayEnemyTriggerInfo rayenemytri = new RayEnemyTriggerInfo(
            GetMeshName(child.gameObject),
            child.transform.localScale,
            child.transform.rotation,
            child.transform.position, child.tag, child.GetComponent<Collider>().GetType().ToString(), new Vector3(0, 0, 0), 0);

            if (child.GetComponent<Collider>().GetType() == typeof(BoxCollider))
            {
                rayenemytri.volumeType = "box";
                rayenemytri.colliderExtents = Vector3.Scale(child.transform.localScale, child.GetComponent<BoxCollider>().size);
                rayenemytri.colliderRadius = 0;
                // Debug.Log("box");

            }
            else if (child.GetComponent<Collider>().GetType() == typeof(SphereCollider))
            {
                rayenemytri.volumeType = "sphere";
                rayenemytri.colliderRadius = child.transform.localScale.x * child.GetComponent<SphereCollider>().radius;
                rayenemytri.colliderExtents = new Vector3(0, 0, 0);
                // Debug.Log("circle");
            }

            Debug.Log("Trapblock Added");
            level.rayenemytriList.Add(rayenemytri);
        }
    }

    void CreateBridgeTrigger(Transform root)
    {
        foreach (BridgeTrigger child in root.GetComponentsInChildren<BridgeTrigger>())
        {
            BridgeTriggerInfo bridgetri = new BridgeTriggerInfo(
            GetMeshName(child.gameObject),
            child.transform.localScale,
            child.transform.rotation,
            child.transform.position, child.tag, child.GetComponent<Collider>().GetType().ToString(), new Vector3(0, 0, 0), 0);

            if (child.GetComponent<Collider>().GetType() == typeof(BoxCollider))
            {
                bridgetri.volumeType = "box";
                bridgetri.colliderExtents = Vector3.Scale(child.transform.localScale, child.GetComponent<BoxCollider>().size);
                bridgetri.colliderRadius = 0;
                // Debug.Log("box");

            }
            else if (child.GetComponent<Collider>().GetType() == typeof(SphereCollider))
            {
                bridgetri.volumeType = "sphere";
                bridgetri.colliderRadius = child.transform.localScale.x * child.GetComponent<SphereCollider>().radius;
                bridgetri.colliderExtents = new Vector3(0, 0, 0);
                // Debug.Log("circle");
            }

            Debug.Log("Trapblock Added");
            level.bridgetriList.Add(bridgetri);
        }
    }


    string GetMeshName(GameObject obj)
    {
        string meshName = "";
        MeshSelector meshInfo;
        if (obj.TryGetComponent<MeshSelector>(out meshInfo)){
            meshName += meshInfo.mesh.ToString();
            meshName += ".";
            meshName += meshInfo.extension.ToString();
            return meshName;
        }

        return "Cube.msh";
    }
}