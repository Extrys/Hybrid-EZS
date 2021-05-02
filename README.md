# Hybrid-EZS

Welcome!</br>
This repository has been created for help to streamline the transition from MonoBehaviour to DOTS in a simple and modular way , 
making the ECS Hybridation much easer for the developer who works with Monobehaviours, 
giving in this way a better performance and architecture to your MonoBehaviour based projects

<details>
<summary>Table Of Contents</summary>

  - [Introduction](#introduction)

  - [Release Notes](#release-notes)
  - [License](#license)

</details>


<b>This is a WIP README</b></br>
    · <a href="## Util for Collisions and triggers for avoid repetitions">Utils for Collisions</a></br>
    · <a href="## DOTS EZ Quaternion Extensions">Quaternion Extensions</a></br>

meanwhile this repo is growing i will put here one of the first utils that is made for DOTS Physics collisions and triggers


## Util for Collisions and triggers for avoid repetitions
for example i will leave a job for detecting collisions that comes with Unity DOTS Physics Samples
```csharp
    [BurstCompile]
    struct CollisionEventImpulseJob : ICollisionEventsJob
    {
        [ReadOnly]
        public ComponentDataFromEntity<CollisionEventImpulse> ColliderEventImpulseGroup;
        public ComponentDataFromEntity<PhysicsVelocity> PhysicsVelocityGroup;

        public void Execute(CollisionEvent collisionEvent)
        {
            Entity entityA = collisionEvent.Entities.EntityA;
            Entity entityB = collisionEvent.Entities.EntityB;

            bool isBodyADynamic = PhysicsVelocityGroup.Exists(entityA);
            bool isBodyBDynamic = PhysicsVelocityGroup.Exists(entityB);

            bool isBodyARepulser = ColliderEventImpulseGroup.Exists(entityA);
            bool isBodyBRepulser = ColliderEventImpulseGroup.Exists(entityB);

            if(isBodyARepulser && isBodyBDynamic)
            {
                var impulseComponent = ColliderEventImpulseGroup[entityA];
                var velocityComponent = PhysicsVelocityGroup[entityB];
                velocityComponent.Linear = impulseComponent.Impulse;
                PhysicsVelocityGroup[entityB] = velocityComponent;
            }
            if (isBodyBRepulser && isBodyADynamic)
            {
                var impulseComponent = ColliderEventImpulseGroup[entityB];
                var velocityComponent = PhysicsVelocityGroup[entityA];
                velocityComponent.Linear = impulseComponent.Impulse;
                PhysicsVelocityGroup[entityA] = velocityComponent;
            }
        }
    }
```
This is not completly bad but for me atleast the repetition hurts and my eyes bleed
also if i modify anything i have to do it too inside the condition below
this is made this way to detect interactions in two directions
so if i have a Sword that is Breaker and breakable that collides with another one
both swords needs to be broken
this is made this way just for doing this (weaponA breaks weaponB) <-> (weaponB breaks weaponA)
In most of cases you will need to use it in the two directions
so you will need unnecesarly duplicate the code

i came with a solution for this
<b>Method A:</b> you can do this way with TryInteract Extension this will made it two directional by default and you dont create noisy A&B variablesit will iterate 2 times, one for each direction
```csharp
[BurstCompile]
struct CollisionEventImpulseJob : ICollisionEventsJob
{
    [ReadOnly]
    public ComponentDataFromEntity<CollisionEventImpulse> impulseGetter;
    public ComponentDataFromEntity<PhysicsVelocity> velocityGetter;

    public void Execute(CollisionEvent collisionEvent)
    {
        for (int i = 0; collisionEvent.Entities.TryInteract(ref i, impulseGetter, velocityGetter, out Entity impulseEntity, out Entity velocityEntity);)
        {
            var impulseComponent = impulseGetter[impulseEntity];
            var velocityComponent = velocityGetter[velocityEntity];
            velocityComponent.Linear = impulseComponent.Impulse;
            velocityGetter[velocityEntity] = velocityComponent;
        }
    }
}
```
<br/>but one of the problems of this is that the "for" line can be so long is for that i created also another way to doing so

<b>Method B:</b> this way will need you take the copy of the pair because it will switch the values between the value pairs.
It uses an extension metod called ``SwitchPair`` that you can use at you will
```csharp
        var pair = collisionEvent.Entities;
        for (int i = 0; i<2; i++)
        {
            if( pair.SwitchAndTryInteract(impulseGetter, velocityGetter))
            {
                var impulseEntity = pair.EntityA; 
                var velocityEntity = pair.EntityB;

                var impulseComponent = impulseGetter[impulseEntity];
                var velocityComponent = velocityGetter[velocityEntity];
                velocityComponent.Linear = impulseComponent.Impulse;
                velocityGetter[velocityEntity] = velocityComponent;
            }
        }
```
what im doing in both is Just create a "for" loop that iterates 2 times and call the switch after one iteration so the second will do the same iteration but with the entities take away the repetition with the loop which is simply a dual direction check



I will be updating the Documentation and adding more functionality to this library so it will become larger with time to make your life a bit easier, if it helps you i will be happy


## DOTS EZ Quaternion Extensions
Some extensions to work with quaternions for DOTS
