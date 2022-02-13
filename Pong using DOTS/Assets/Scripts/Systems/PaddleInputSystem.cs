using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class PaddleInputSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        // ref - reference, means writing to moveData; in - input, means reading from inputData
        Entities.ForEach((ref PaddleMovementData moveData, in PaddleInputData inputData) => 
        {    
            moveData.direction = 0;

            moveData.direction += Input.GetKey(inputData.upKey) ? 1 : 0;
            moveData.direction -= Input.GetKey(inputData.downKey) ? 1 : 0;
        }).Run();   // this will run on the main thread

        return default;
    }
}