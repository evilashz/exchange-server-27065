using System;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x0200001E RID: 30
	public interface ICreateEntityCommand<TScope, TEntity> : IEntityCommand<TScope, TEntity> where TEntity : IEntity
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000BC RID: 188
		// (set) Token: 0x060000BD RID: 189
		TEntity Entity { get; set; }
	}
}
