using System;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x02000029 RID: 41
	public interface IReadEntityCommand<TScope, out TEntity> : IKeyedEntityCommand<TScope, TEntity>, IEntityCommand<TScope, TEntity> where TEntity : IEntity
	{
	}
}
