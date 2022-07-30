using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGeneratorMethod
{
    float Execute(GeneratorArgs args, int i, int j);
}
