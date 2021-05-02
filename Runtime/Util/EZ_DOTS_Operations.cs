using Unity.Entities;

public static class EZ_DOTS_Operations
{
	public static void AddHybridComponentsToNewEntity(out Entity entity, params object[] objects)
	{
		EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
		entity = manager.CreateEntity();

		int iterations = objects.Length;
		for (int i = 0; i < iterations; i++)
			manager.AddComponentObject(entity, objects[i]);
	}
	public static void AddArrayOfHybridComponentsToNewEntity(out Entity entity, object[] objects)
	{
		EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
		entity = manager.CreateEntity();

		int iterations = objects.Length;
		for (int i = 0; i < iterations; i++)
			manager.AddComponentObject(entity, objects[i]);
	}

	public static void AddComponentObject(this Entity entity, params object[] objects)
	{
		EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
		int iterations = objects.Length;
		for (int i = 0; i < iterations; i++)
			manager.AddComponentObject(entity, objects[i]);
	}
	public static void AddComponentObjects(this Entity entity, object[] objects)
	{
		EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
		int iterations = objects.Length;
		for (int i = 0; i < iterations; i++)
			manager.AddComponentObject(entity, objects[i]);
	}

	public static void AddComponentDatasToNewEntity<T>(out Entity entity, params T[] objects) where T : struct, IComponentData
	{
		EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
		entity = manager.CreateEntity();

		int iterations = objects.Length;
		for (int i = 0; i < iterations; i++)
			manager.AddComponentData(entity, objects[i]);
	}
	public static void AddArrayOfComponentDatasToNewEntity<T>(out Entity entity, T[] objects) where T : struct, IComponentData
	{
		EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
		entity = manager.CreateEntity();

		int iterations = objects.Length;
		for (int i = 0; i < iterations; i++)
			manager.AddComponentData(entity, objects[i]);
	}

	public static void AddComponentData<T>(this Entity entity, params T[] objects) where T : struct, IComponentData
	{
		EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
		int iterations = objects.Length;
		for (int i = 0; i < iterations; i++)
			manager.AddComponentData(entity, objects[i]);
	}

	public static void AddOrUpdateComponentData<T>(this Entity entity, params T[] objects) where T : struct, IComponentData
	{
		int iterations = objects.Length;
		EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;

		if (manager.HasComponent<T>(entity))
		{
			for (int i = 0; i < iterations; i++)
				manager.SetComponentData(entity, objects[i]);

			return;
		}

		for (int i = 0; i < iterations; i++)
			manager.AddComponentData(entity, objects[i]);
	}

	public static void AddComponentData<T>(this Entity entity) where T : struct, IComponentData
	{
		World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentData(entity, new T());
	}

	public static T GetComponentData<T>(this Entity entity) where T : struct, IComponentData
	{
		return World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<T>(entity);
	}

	public static bool HasComponentData<T>(this Entity entity) where T : struct, IComponentData
	{
		return World.DefaultGameObjectInjectionWorld.EntityManager.HasComponent<T>(entity);
	}

	public static void AddSharedComponentData<T>(this Entity entity, T data) where T : struct, ISharedComponentData
	{
		World.DefaultGameObjectInjectionWorld.EntityManager.AddSharedComponentData(entity, data);
	}

	public static void SetComponentData<T>(this Entity entity, T data) where T : struct, IComponentData
	{
		World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(entity, data);
	}
	public static void SetSharedComponentData<T>(this Entity entity, T data) where T : struct, ISharedComponentData
	{
		World.DefaultGameObjectInjectionWorld.EntityManager.SetSharedComponentData(entity, data);
	}


	public static void AddHybridComponentsToNewEntity(params object[] objects) => AddHybridComponentsToNewEntity(out _, objects);
	public static void AddHybridComponentsToNewEntity<T>(params T[] objects) where T : struct, IComponentData => AddComponentDatasToNewEntity(out _, objects);



	public static void InsertSystem<T>(this T sys) where T : SystemBase
	{
		World world = World.DefaultGameObjectInjectionWorld;
		world.AddSystem(sys);
		SimulationSystemGroup group = world.GetExistingSystem<SimulationSystemGroup>();
		group.AddSystemToUpdateList(sys);
		group.SortSystems();
	}
}