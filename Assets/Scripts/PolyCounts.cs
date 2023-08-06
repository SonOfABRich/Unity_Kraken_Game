using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyCounts : MonoBehaviour
{
    /* private void Awake()
     {
         GatherAllObjects();

     }

     public int totalPolygons = 0;

     private void GatherAllObjects()
     {
          GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
         print("There are " + allObjects.Length + " objects in this scene! That does include this Empty Object, the Main Camera(s), and the Directional Light! Be sure to account for those!");

         CountPolygons(allObjects);

         print("You currently have " + totalPolygons + " in this scene!");

         //print(UnityEditor.UnityStats.vertices);
     }

     private int CountPolygons(GameObject[] listOfObjects)
     {

         for(int i = 0; i < listOfObjects.Length; i++)
         {
             //int objectTris = listOfObjects[i].GetComponent<Mesh>().triangles;
             //totalPolygons += objectTris;
         }

         return totalPolygons;
     }
    */


    public bool Show_Stats;
    public bool Show_FPS;
    public bool Show_Tris;
    public bool Show_Verts;
    public static int verts;
    public static int tris;

    public float updateInterval = 0.5F;

    private float accum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval
    public float fps;

    // Use this for initialization
    void Start()
    {
        timeleft = updateInterval;
    }

    // Update is called once per frame
    void Update()
    {
        /*Show_Stats = GameObject.Find("Player").GetComponent<Pause_Menu>().Show_Stats;
        Show_FPS = GameObject.Find("Player").GetComponent<Pause_Menu>().Show_FPS;
        Show_Tris = GameObject.Find("Player").GetComponent<Pause_Menu>().Show_Tris;
        Show_Verts = GameObject.Find("Player").GetComponent<Pause_Menu>().Show_Verts;*/
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            fps = accum / frames;
            string format = System.String.Format("{0:F2} FPS", fps);
            //  DebugConsole.Log(format,level);
            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
            GetObjectStats();
        }

        print(fps);
        print(verts/3);
        print(tris/2);
    }

    void OnGUI()
    {
        if (Show_Stats)
            ShowStatistics();
    }

    void ShowStatistics()
    {
        GUILayout.BeginArea(new Rect(0, 0, 100, 100));
        if (Show_FPS)
        {
            string fpsdisplay = fps.ToString("#,##0 fps");
            GUILayout.Label(fpsdisplay);
        }
        if (Show_Tris)
        {
            string trisdisplay = tris.ToString("#,##0 tris");
            GUILayout.Label(trisdisplay);
        }
        if (Show_Verts)
        {
            string vertsdisplay = verts.ToString("#,##0 verts");
            GUILayout.Label(vertsdisplay);
        }
        GUILayout.EndArea();
    }

    void GetObjectStats()
    {
        verts = 0;
        tris = 0;
        GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject obj in ob)
        {
            GetObjectStats(obj);
        }
    }

    void GetObjectStats(GameObject obj)
    {
        Component[] filters;
        filters = obj.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter f in filters)
        {
            tris += f.sharedMesh.triangles.Length / 3;
            verts += f.sharedMesh.vertexCount;
        }
    }
}
