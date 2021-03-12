using System;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x02000053 RID: 83
	public interface IEntityReference<out TEntity> where TEntity : class, IEntity
	{
		// Token: 0x060002D9 RID: 729
		string GetKey();

		// Token: 0x060002DA RID: 730
		TEntity Read(CommandContext context = null);
	}
}
