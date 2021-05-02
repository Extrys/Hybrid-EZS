using Unity.Entities;

namespace HybridEZS.Examples
{
	public class ProjectileComponentDataAuthor : ComponentDataAuthor<ProjectileComponentTag>
	{
		public float speed;


		public override ProjectileComponentTag GetAuthoredComponentData()
		{
			var t = new ProjectileComponentTag();
			t.speed = speed;
			return t;
		}
	}

	public struct ProjectileComponentTag : IComponentData { public float speed; }
}