using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public float threshold;
    public float bias;
    public float seed;
    public float scale;
    public int[,] mask;
    public int size;
    public float[,] biasMatrix;
}
