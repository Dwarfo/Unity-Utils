using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    public UIVisualizer visualizer;
    public GameObject terrainObj;
    public List<GameObject> tileToWeight;
    public float scale = 1;
    public float constant = 0;
    public float offsetX = 10.1f;
    public float offsetY = 10.1f;
    public int size = 10;
    public string seed;
    public GeneratorParameters gp;

    private Dictionary<string, LayerParameter> nameToParameter;
    private Dictionary<string, float[,]> resultMap;

    private Octave onlyOct;

    async void Start()
    {
        nameToParameter = new Dictionary<string, LayerParameter>();
        resultMap = new Dictionary<string, float[,]>();
        foreach (LayerParameter element in gp.parameters){
            //element.
            nameToParameter.Add(element.layerName, element);
            resultMap.Add(element.layerName, new float[this.size, this.size]);
        }

        StartCoroutine(GenerateMap());

    }

    public IEnumerator GenerateMap(){

        GetValueRaw method = new GetValueRaw();

        float fseed = (float)(seed.GetHashCode() / 10000);
        int seedDivider;

        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){
                seedDivider = 1;
                foreach (LayerParameter element in gp.parameters){
                    resultMap[element.layerName][i,j] = method.Execute(element.scale, fseed/seedDivider, element.bias, i , j);
                    seedDivider++;
                }
                
                yield return null;
            }
        }
        visualizer.setMatrData(resultMap);
        //yield return StartCoroutine();   
    }

    public IEnumerator GetMask(float[,] valMatrix){

        float valMin = 1;
        float valMax = 0;

        float[,] maskMatrix = new float[size,size];

        GetMaskVal maskMethod = new GetMaskVal();

        GeneratorArgs gArgs = new GeneratorArgs();
        gArgs.scale = this.scale;
        gArgs.seed = this.offsetX;
        gArgs.biasMatrix = valMatrix;
        gArgs.threshold = 0.6f;
        gArgs.bias = 0;

        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){

                //place method to do
                
                float val = maskMethod.Execute(gArgs, i , j);

                maskMatrix[i,j] = val;
                Debug.Log(val);
                yield return null;
            }
        }

        visualizer.setData(valMatrix, maskMatrix);
    }
}
