using System;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x0200002C RID: 44
	public interface IUpdateEntityCommand<TScope, TEntity> : IKeyedEntityCommand<TScope, TEntity>, IEntityCommand<TScope, TEntity> where TEntity : IEntity
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000DE RID: 222
		// (set) Token: 0x060000DF RID: 223
		TEntity Entity { get; set; }
	}
}
