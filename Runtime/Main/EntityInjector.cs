using System.Security.Cryptography.X509Certificates;
using Unity.Entities;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Collections.Generic;
using Unity.Collections;
using System.Linq;

namespace HybridEZS
{
	public class EntityInjector : MonoBehaviour, IEntityConverted
	{
		Entity entity;
		EntityManager manager;

		public Entity PrimaryEntity => entity == Entity.Null ? CreateEntity() : entity;

		public Object[] objects;

		public List<ComponentDataAuthor> dataAuthors = new List<ComponentDataAuthor>();

		[SerializeField] List<EntityInjector> referencedEntityInjectors;

		void Awake()
		{
			dataAuthors.InsertComponentDatasIntoEntity(PrimaryEntity);
			PrimaryEntity.AddComponentObjects(objects);
			AddReferencedPrimaryEntities();
		}


		public void AddReferencedPrimaryEntities()
		{
			if (referencedEntityInjectors != null && referencedEntityInjectors.Count > 0)
			{
				NativeArray<EntityHolderElementData> entities = new NativeArray<EntityHolderElementData>(referencedEntityInjectors.Select(x => new EntityHolderElementData { entity = x.PrimaryEntity }).ToArray(), Allocator.Persistent);
				manager.AddBuffer<EntityHolderElementData>(PrimaryEntity).AddRange(entities);
				entities.Dispose();
			}
		}

		Entity CreateEntity()
		{
			manager = World.DefaultGameObjectInjectionWorld.EntityManager;
			entity = manager.CreateEntity();
			return entity;
		}

		public bool ContainsObject<T>(T instance) where T : Object => objects.Contains(instance);

		void OnDestroy() => World.DefaultGameObjectInjectionWorld?.EntityManager.DestroyEntity(PrimaryEntity);
	}

	public struct EntityHolderElementData : IBufferElementData
	{
		public Entity entity;

		public static explicit operator Entity(EntityHolderElementData eh) => eh.entity;
		public static implicit operator EntityHolderElementData(Entity e) => new EntityHolderElementData() { entity = e };
	}

	public interface IEntityConverted
	{
		Entity PrimaryEntity { get; }
	}
}
