using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x02000032 RID: 50
	internal abstract class StorageEntitySet<TEntitySet, TEntity, TSession> : StorageEntitySet<TEntitySet, TEntity, IEntityCommandFactory<TEntitySet, TEntity>, TSession> where TEntitySet : class, IEntitySet<TEntity> where TEntity : class, IEntity where TSession : class, IStoreSession
	{
		// Token: 0x06000104 RID: 260 RVA: 0x000045C3 File Offset: 0x000027C3
		protected StorageEntitySet(IStorageEntitySetScope<TSession> parentScope, string relativeName, IEntityCommandFactory<TEntitySet, TEntity> commandFactory) : base(parentScope, relativeName, commandFactory)
		{
		}
	}
}
