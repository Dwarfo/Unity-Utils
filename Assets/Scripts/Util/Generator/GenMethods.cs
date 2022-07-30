using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GenMethods{
    public float[,] Normalize(float[,] toNormalize, float valMin, float valMax){

        int size = toNormalize.GetLength(0);
        float[,] valMatrix = new float[size,size];

        for(int i = 0; i < size; i++){
            for(int j = 0; j < size; j++){
                
                valMatrix[i,j] = (toNormalize[i,j] - valMin)/(valMax - valMin);
            }
        }

        return valMatrix;
    }
}

public class GetValue : IGeneratorMethod
{
    public float Execute(GeneratorArgs args, int i , int j){
        return Mathf.PerlinNoise((float)i * args.scale + args.seed, (float)j * args.scale + args.seed) + args.bias;
    }
}

public class GetMaskedVal : IGeneratorMethod
{
    public float Execute(GeneratorArgs args, int i , int j){
        return args.mask[i,j] == 0 ? 0 : Mathf.PerlinNoise((float)i * args.scale + args.seed, (float)j * args.scale + args.seed) + args.bias;
    }
}

public class GetMaskVal : IGeneratorMethod
{
    public float Execute(GeneratorArgs args, int i , int j){
        return args.biasMatrix[i,j] > args.threshold ? 1 : 0;
        //return Mathf.PerlinNoise((float)i * args.scale + args.seed, (float)j * args.scale + args.seed) + args.bias > args.threshold ? 1f : 0;
    }
}
