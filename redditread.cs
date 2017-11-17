using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files



public class redditread : MonoBehaviour {

    public Vector3 start;
    public string url;
	// Use this for initialization
	void Start () {
        string path = "Assets/DataSphere/Scripts/redditlow.json";
        
        string dataAsJson = File.ReadAllText(path);
        RootObject loadedData = JsonUtility.FromJson<RootObject>(dataAsJson);

        Vector3 start = GetComponent<Transform>().position;
        Vector3 pos = new Vector3(0, 1, 0);

        int row;
        for (int i=0;i<300;i++)
        {

            row = i / 10;

            Vector3 addition = new Vector3(1.5f, row, 0);

            url = loadedData.data[i].url1;
            string title = loadedData.data[i].titel;


            GameObject gobj = GameObject.CreatePrimitive(PrimitiveType.Quad);
            gobj.name = title;

            pos = new Vector3( i%30,1.0f+2*(int)(i/30),0);
            gobj.transform.position = pos;
            //gobj.AddComponent<Renderer>();

            StartCoroutine(here(gobj));
            new WaitForSecondsRealtime(1);
            //StartCoroutine(here(url));


            //Debug.Log(loadedData.data[i].url1);

        }

    }

    // Update is called once per frame
    void Update () {
		
	}

    [System.Serializable]
    public class Datum
    {
        public int id;
        public string titel;
        public object author;
        public string url;
        public string url1;
    }

    [System.Serializable]
    public class RootObject
    {
        public List<Datum> data;
    }



    //IEnumerator GetTexture(GameObject gobj,string url, Texture myTexture)
    //{
    //    Texture2D tex;
    //    tex = new Texture2D(100, 100);
    //    WWW www = new WWW(url);
    //    yield return www;
    //    www.LoadImageIntoTexture(tex);
    //    Renderer rend = gobj.GetComponent<Renderer>();
    //    rend.material = new Material(Shader.Find("Unlit/Texture"));
    //    rend.material.mainTexture = tex;






    //}

    IEnumerator here(GameObject gobj)
    {
       
        // Start a download of the given URL
        WWW www = new WWW(url);
        yield return www;
        // Wait for download to complete


        
        // assign texture
        Renderer renderer = gobj.GetComponent<Renderer>();
        renderer.material.mainTexture = www.texture;
        renderer.material.shader = Shader.Find("Unlit/Texture");
      
    }
}
