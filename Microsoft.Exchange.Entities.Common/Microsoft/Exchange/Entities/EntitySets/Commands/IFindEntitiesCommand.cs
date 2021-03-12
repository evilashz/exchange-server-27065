using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x02000026 RID: 38
	public interface IFindEntitiesCommand<TScope, out TEntity> : IEntityCommand<TScope, IEnumerable<TEntity>> where TEntity : IEntity
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D1 RID: 209
		// (set) Token: 0x060000D2 RID: 210
		IEntityQueryOptions QueryOptions { get; set; }
	}
}
