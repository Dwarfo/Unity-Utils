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
    private Dictionary<string, float[,]> maskMap;

    private Octave onlyOct;

    async void Start()
    {
        nameToParameter = new Dictionary<string, LayerParameter>();
        resultMap = new Dictionary<string, float[,]>();
        maskMap = new Dictionary<string, float[,]>();
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
        yield return StartCoroutine(GenerateMasks());   
    }

    public IEnumerator GenerateMasks(){
        List<LayerParameter> lpList = new List<LayerParameter>();
        foreach (LayerParameter element in gp.parameters){
            if(element.generateMask){
                maskMap.Add(element.layerName, new float[this.size, this.size]);
                lpList.Add(element);
            }
        }

        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){
                foreach (LayerParameter element in lpList){
                    maskMap[element.layerName][i,j] = resultMap[element.layerName][i,j] > element.maskThreshold ? 1f: 0f;
                }
                yield return null;
            }
        }

        visualizer.setMaskData(maskMap);
    }
}
