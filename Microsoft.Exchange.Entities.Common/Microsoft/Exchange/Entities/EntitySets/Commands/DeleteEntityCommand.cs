using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x02000024 RID: 36
	[DataContract]
	internal abstract class DeleteEntityCommand<TEntitySet> : KeyedEntityCommand<TEntitySet, VoidResult>, IDeleteEntityCommand<TEntitySet>, IKeyedEntityCommand<TEntitySet, VoidResult>, IEntityCommand<TEntitySet, VoidResult> where TEntitySet : IStorageEntitySetScope<IStoreSession>
	{
	}
}
