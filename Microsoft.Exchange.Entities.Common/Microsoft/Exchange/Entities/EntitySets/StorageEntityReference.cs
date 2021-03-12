using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x02000053 RID: 83
	internal abstract class StorageEntityReference<TEntitySet, TEntity, TStoreSession> : StorageEntitySetScope<TStoreSession>, IEntityReference<TEntity> where TEntitySet : IEntitySet<TEntity>, IStorageEntitySetScope<TStoreSession> where TEntity : class, IStorageEntity where TStoreSession : class, IStoreSession
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00006831 File Offset: 0x00004A31
		protected StorageEntityReference(TEntitySet entitySet) : base(entitySet)
		{
			this.EntitySet = entitySet;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006846 File Offset: 0x00004A46
		protected StorageEntityReference(TEntitySet entitySet, string entityKey) : this(entitySet)
		{
			this.entityKey = entityKey;
			this.storeId = base.IdConverter.ToStoreObjectId(this.entityKey);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000686D File Offset: 0x00004A6D
		protected StorageEntityReference(TEntitySet entitySet, StoreId entityStoreId) : this(entitySet)
		{
			this.storeId = entityStoreId;
			this.entityKey = base.IdConverter.ToStringId(entityStoreId, base.StoreSession);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000689A File Offset: 0x00004A9A
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x000068A2 File Offset: 0x00004AA2
		public TEntitySet EntitySet { get; private set; }

		// Token: 0x060001B9 RID: 441 RVA: 0x000068AB File Offset: 0x00004AAB
		public StoreId GetStoreId()
		{
			if (this.storeId == null)
			{
				this.Resolve();
			}
			return this.storeId;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000068C1 File Offset: 0x00004AC1
		public string GetKey()
		{
			if (this.entityKey == null)
			{
				this.Resolve();
			}
			return this.entityKey;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000068D8 File Offset: 0x00004AD8
		public TEntity Read(CommandContext context)
		{
			TEntitySet entitySet = this.EntitySet;
			return entitySet.Read(this.GetKey(), context);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006900 File Offset: 0x00004B00
		public override string ToString()
		{
			string result;
			if ((result = this.description) == null)
			{
				result = (this.description = string.Format("{0}{1}", this.EntitySet, this.GetRelativeDescription()));
			}
			return result;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000693B File Offset: 0x00004B3B
		protected virtual StoreId ResolveReference()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00006942 File Offset: 0x00004B42
		protected virtual string GetRelativeDescription()
		{
			return '[' + this.GetKey() + ']';
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006960 File Offset: 0x00004B60
		private void Resolve()
		{
			StoreId storeId = this.ResolveReference();
			string text = base.IdConverter.ToStringId(storeId, base.StoreSession);
			this.storeId = storeId;
			this.entityKey = text;
		}

		// Token: 0x04000082 RID: 130
		private string description;

		// Token: 0x04000083 RID: 131
		private string entityKey;

		// Token: 0x04000084 RID: 132
		private StoreId storeId;
	}
}
