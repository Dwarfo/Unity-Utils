using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    public GameObject terrainObj;
    public List<GameObject> tileToWeight;
    public float scale = 1;
    public float constant = 0;
    public float offsetX = 10.1f;
    public float offsetY = 10.1f;
    public int size = 10;
    public string seed;

    private Octave onlyOct;

    async void Start()
    {
        onlyOct = new Octave(offsetX, scale, constant);
        StartCoroutine(GenerateMap());

    }

    public IEnumerator GenerateMap(){

        float valMin = 1;
        float valMax = 0;

        float[,] valMatrix = new float[size,size];
        float[,] maskMatrix = new float[size,size];

        GetValue method = new GetValue();
        GetMaskVal maskMethod = new GetMaskVal();

        GeneratorArgs gArgs = new GeneratorArgs();
        gArgs.scale = this.scale;
        gArgs.seed = (float)(seed.GetHashCode() / 100);
        gArgs.threshold = 0.6f;
        gArgs.bias = 0;

        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){

                //place method to do
                
                float val = method.Execute(gArgs, i , j);
                if(val > valMax){
                    valMax = val;
                }
                if(val < valMin){
                    valMin = val;
                }

                valMatrix[i,j] = val;

                if(valMatrix[i,j] > 0.7f){
                    Instantiate(tileToWeight[0], new Vector3(i,j,0), Quaternion.identity).transform.parent = terrainObj.transform;
                }
                else if(valMatrix[i,j] > 0.5f){
                    Instantiate(tileToWeight[1], new Vector3(i,j,0), Quaternion.identity).transform.parent = terrainObj.transform;
                }
                else{
                    Instantiate(tileToWeight[2], new Vector3(i,j,0), Quaternion.identity).transform.parent = terrainObj.transform;
                }
                
                yield return null;
            }
        }

        yield return StartCoroutine(GetMask(valMatrix));   
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
    }

    /*
    public float GenerateLandmass(int i, int j){
        
        float val = Mathf.PerlinNoise((float)i * args.scale + args.seed, (float)j * args.scale + args.seed) + args.bias;

        return val;
    }

    public float[,] Normalize(GeneratorArgs args){
        float valMin = 1;
        float valMax = 0;

        float[,] valMatrix = new float[args.size, args.size];

        for(int i = 0; i < args.size; i++){
            for(int j = 0; j < args.size; j++){
                float val = Mathf.PerlinNoise((float)i * args.scale + args.seed, (float)j * args.scale + args.seed) + args.bias;
                
                valMatrix[i,j] = val;
            }
        }

        return valMatrix;
    }

    public int[,] GenerateMask(GeneratorArgs args){
        int[,] valMatrix = new float[size,size];

        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){
                valMatrix[i,j] = map[i,j] > threshold ? 1 : 0;
            }
        }

        return valMatrix;
    }
    
    public float[,] GenerateWithMask(GeneratorArgs args){
        float valMin = 1;
        float valMax = 0;

        float[,] valMatrix = new float[size,size];

        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){
                float val = Mathf.PerlinNoise((float)i * scale + seed, (float)j * scale + seed) + bias;
                
                valMatrix[i,j] = val * mask[i,j];
            }
        }

        return valMatrix;
    }*/
}
