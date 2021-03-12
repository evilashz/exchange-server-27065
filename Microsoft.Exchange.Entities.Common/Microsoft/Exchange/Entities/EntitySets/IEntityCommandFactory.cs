using System;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x02000037 RID: 55
	public interface IEntityCommandFactory<TScope, TEntity> where TEntity : IEntity
	{
		// Token: 0x06000119 RID: 281
		ICreateEntityCommand<TScope, TEntity> CreateCreateCommand(TEntity entity, TScope scope);

		// Token: 0x0600011A RID: 282
		IDeleteEntityCommand<TScope> CreateDeleteCommand(string key, TScope scope);

		// Token: 0x0600011B RID: 283
		IFindEntitiesCommand<TScope, TEntity> CreateFindCommand(IEntityQueryOptions queryOptions, TScope scope);

		// Token: 0x0600011C RID: 284
		IReadEntityCommand<TScope, TEntity> CreateReadCommand(string key, TScope scope);

		// Token: 0x0600011D RID: 285
		IUpdateEntityCommand<TScope, TEntity> CreateUpdateCommand(string key, TEntity entity, TScope scope);
	}
}
