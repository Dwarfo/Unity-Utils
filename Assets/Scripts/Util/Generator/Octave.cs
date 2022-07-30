using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octave
{

    public Octave(){
        
    }

    public Octave(float seed, float scale, float bias){
        this.seed = seed;
        this.scale = scale;
        this.bias = bias;
    }

    float seed;
    float scale;
    float bias;
    public float[,] valuesMatrix;

    public float getValForCoords(int x, int y){
        return Mathf.PerlinNoise((float)x * scale + seed, (float)y * scale + seed) + bias;
    }

    public float[,] getValMatrix(int size){
        float valMin = 1;
        float valMax = 0;

        float[,] valMatrix = new float[size,size];

        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){
                float val = Mathf.PerlinNoise((float)i * scale + seed, (float)j * scale + seed) + bias;
                
                valMatrix[i,j] = val;
            }
        }

        valuesMatrix = valMatrix;
        return valMatrix;
    }


}
