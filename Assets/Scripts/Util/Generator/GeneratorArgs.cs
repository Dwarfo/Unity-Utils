using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GeneratorParameters{
    //public float scale;
    //public float constant;
    //public float offset;
    public int size;
    //public string s;
    public List<LayerParameter> parameters;
}

[Serializable]
 public struct NamedImage {
    public string name;
    public LayerParameter parameter;
 }

[Serializable]
public class LayerParameter {
    public string layerName;
    public string maskToUseName;
    public bool useMask;
    public bool generateMask;
    public float scale;
    public float bias;
    
}

public class GeneratorArgs
{
    public GeneratorArgs(){}

    public GeneratorArgs(
        float threshold, 
        float bias, 
        float seed, 
        float scale, 
        int[,] mask, 
        int size, 
        float[,] biasMatrix){

        this.threshold = threshold;
        this.bias = bias;
        this.seed = seed;
        this.scale = scale;
        this.mask = mask;
        this.size = size;
        this.biasMatrix = biasMatrix;
    }


    public float threshold {get; set;}
    public float bias {get; set;}
    public float seed {get; set;}
    public float scale {get; set;}
    public int[,] mask {get; set;}
    public int size {get; set;}
    public float[,] biasMatrix {get; set;}
}
