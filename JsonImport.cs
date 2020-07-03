
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public class JsonImport : MonoBehaviour
{
    public GameObject cell;
    public GameObject canvas;
    int heigthSc = 0;
    int widthSc = 0;
    int widthBox = 0;
    int heigthBox = 0;
    List<GameObject> CellsHead;
    List<GameObject> Cellsbody;

    // Start is called before the first frame update
    void Start()
    {
        heigthSc = Screen.height;
        widthSc = Screen.width;

        widthBox = 120;
        heigthBox = 25;
        CellsHead = new List<GameObject>();
        Cellsbody = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void jsonImport()
    {
        

         foreach (GameObject cell in CellsHead)
         {
             Destroy(cell);

         }

        foreach (GameObject cell in Cellsbody)
        {
            Destroy(cell);

        }
               

        string jsvar = "";
        try
        {
           
            using (StreamReader sr = new StreamReader(Application.dataPath+"/StreamingAssets/JsonChallenge.json"))
            {
                string line;                
                while ((line = sr.ReadLine()) != null)
                {
                    jsvar += line;
                }            
            }
        }
        catch (Exception e)
        {            
            Debug.Log("el json no a sido leido");
            Debug.Log(e.Message);
        }
        
        totalINf elementos = JsonConvert.DeserializeObject<totalINf>(jsvar);

        GameObject cellinstHead;
        GameObject Cellsinsbody;
        int posicionx = 0;
        
        foreach (string ele in elementos.ColumnHeaders)
        {
            cellinstHead = Instantiate(cell);
            cellinstHead.transform.SetParent(canvas.transform, false);
            cellinstHead.GetComponent<UnityEngine.UI.Text>().fontStyle = FontStyle.Bold;
            cellinstHead.GetComponent<UnityEngine.UI.Text>().text = ele;
            cellinstHead.transform.position = new Vector3(100 + (posicionx* widthBox), heigthSc - 100 , 0);
            CellsHead.Add(cellinstHead);
            posicionx++;
        }


        int posiciony = 1;
       

        foreach (Dictionary<string, string> ele in elementos.Data)
        {
            posicionx = 0;
            foreach (var ele2 in ele)
            {
                Cellsinsbody = Instantiate(cell);
                Cellsinsbody.transform.SetParent(canvas.transform, false);
                Cellsinsbody.GetComponent<UnityEngine.UI.Text>().text = ele2.Value;
                Cellsinsbody.transform.position = new Vector3(100 + (posicionx * widthBox), (heigthSc) - 100 - (heigthBox * posiciony), 0);
                Cellsbody.Add(Cellsinsbody);
                posicionx++;

            }
            posiciony++;


        }


    }

    [System.Serializable]
    public class totalINf
    {
        public string Title;
        public List<string> ColumnHeaders;
        public List<Dictionary<string, string>> Data;
        

    }
}
