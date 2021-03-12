using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x02000036 RID: 54
	[DataContract]
	internal abstract class UpdateStorageEntityCommand<TEntitySet, TEntity> : UpdateEntityCommand<TEntitySet, TEntity> where TEntitySet : IStorageEntitySetScope<IStoreSession> where TEntity : IEntity, IVersioned
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00004728 File Offset: 0x00002928
		protected override void SetETag(string eTag)
		{
			TEntity entity = base.Entity;
			entity.ChangeKey = eTag;
		}
	}
}
