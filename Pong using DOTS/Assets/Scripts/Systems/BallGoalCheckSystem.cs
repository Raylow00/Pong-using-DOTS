using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;

[AlwaysSynchronizeSystem]
public class BallGoalCheckSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);

        Entities
            .WithAll<BallTag>()
            .WithoutBurst()
            .ForEach((Entity entity, in Translation trans) => 
        {
            float3 pos = trans.Value;
            float bound = GameManager.main.xBound;

            if(pos.x >= bound)
            {
                GameManager.main.PlayerScored(0);
                ecb.DestroyEntity(entity);  // put this command into the buffer
            }
            else if(pos.x <= -bound)
            {
                GameManager.main.PlayerScored(1);
                ecb.DestroyEntity(entity);
            }
        }).Run();   // since the GameManager and EntityManager are on the main thread, this has to be on the main thread too

        ecb.Playback(EntityManager);    // after running the main thread, run the commands in the buffer
        ecb.Dispose();

        return default;
    }
}