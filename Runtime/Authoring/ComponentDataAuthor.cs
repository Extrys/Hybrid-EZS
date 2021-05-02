using Unity.Entities;
using UnityEngine;

namespace HybridEZS
{
	public abstract class ComponentDataAuthor : MonoBehaviour
	{
		public abstract void InsertAuthoredComponentToEntity(Entity entity);
		void Reset() => TryToAddIntoLocalInjector();
		void TryToAddIntoLocalInjector()
		{
			if (TryGetComponent(out EntityInjector eInjector))
				eInjector.dataAuthors.AddUniqueAuthor(this);
		}
	}

	public abstract class ComponentDataAuthor<T> : ComponentDataAuthor where T : struct, IComponentData
	{
		public abstract T GetAuthoredComponentData();
		public override void InsertAuthoredComponentToEntity(Entity entity) => entity.AddComponentData(GetAuthoredComponentData());
	}
}