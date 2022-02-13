using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[AlwaysSynchronizeSystem]
public class PaddleMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        float yBound = GameManager.main.yBound;

        ////
        //  The code below will run on worker threads and 
        //  it takes a longer time than on the main thread
        ////
        // JobHandle myJob = Entities.ForEach((ref Translation trans, in PaddleMovementData data) =>   // system query
        // {
        //     // execution
        //     trans.Value.y = math.clamp(trans.Value.y + (data.speed * data.direction * deltaTime), -yBound, yBound);
        // }).Schedule(inputDeps); // this will be scheduled to run on worker threads

        // return myJob;

        Entities.ForEach((ref Translation trans, in PaddleMovementData data) =>   // system query
        {
            // execution
            trans.Value.y = math.clamp(trans.Value.y + (data.speed * data.direction * deltaTime), -yBound, yBound);
        }).Run();

        return default;
    }
}