using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVisualizer : MonoBehaviour
{
    public Button nextRes;
    public Button nextMap;
    public Text visualText;
    public Text resultNameText;
    float[,] valMatrix;
    float[,] maskMatrix;
    Dictionary<string, float[,]> resultMap;
    Dictionary<string, float[,]> maskMap;
    int page = 0;
    int pageMask = 0;
    List<string> resultList;
    List<string> maskList;

    void Start() {
        nextRes.onClick.AddListener(ShowNext);
        nextMap.onClick.AddListener(ShowNextMap);
    }

    public void setData(float[,] valMatrix, float[,] maskMatrix){
        string matrixStr = "";
        string additive = "";

        for(int i = 0; i < valMatrix.GetLength(0); i++){
            for(int j = 0; j < valMatrix.GetLength(1); j++){
                additive = valMatrix[i,j].ToString();
                additive = additive.Substring(0,4);
                matrixStr += additive + " ";
            }
            matrixStr += "\n";
        }

        visualText.text = matrixStr;
    }

    public void setMatrData(Dictionary<string, float[,]> resultMap){
        this.resultMap = resultMap;
        this.resultList = new List<string>();
        foreach(KeyValuePair<string, float[,]> entry in resultMap)
        {
            this.resultList.Add(entry.Key);
        }

        ShowNext();
    }

    public void setMaskData(Dictionary<string, float[,]> maskMap){
        this.maskMap = maskMap;
        this.maskList = new List<string>();
        foreach(KeyValuePair<string, float[,]> entry in maskMap)
        {
            this.maskList.Add(entry.Key);
        }

        ShowNextMap();
    }

    public void ShowNextMap(){
        string matrixStr = "";
        string additive = "";
        resultNameText.text = maskList[pageMask];
        
        float[,] valMatrix = maskMap[maskList[pageMask]];

        for(int i = 0; i < valMatrix.GetLength(0); i++){
            for(int j = 0; j < valMatrix.GetLength(1); j++){
                additive = valMatrix[i,j].ToString();
                matrixStr += additive + " ";
            }
            matrixStr += "\n";
        }

        visualText.text = matrixStr;
        pageMask++;
        if(pageMask >= maskList.Count){
            pageMask = 0;
        }
    }

    public void ShowNext(){
        string matrixStr = "";
        string additive = "";
        resultNameText.text = resultList[page];

        float[,] valMatrix = resultMap[resultList[page]];

        for(int i = 0; i < valMatrix.GetLength(0); i++){
            for(int j = 0; j < valMatrix.GetLength(1); j++){
                additive = valMatrix[i,j].ToString();
                additive = additive.Substring(0,4);
                matrixStr += additive + " ";
            }
            matrixStr += "\n";
        }

        visualText.text = matrixStr;
        page++;
        if(page >= resultList.Count){
            page = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
