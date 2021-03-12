using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x0200001A RID: 26
	public abstract class StorageEntitySchema : EntitySchema
	{
		// Token: 0x06000052 RID: 82 RVA: 0x000028E4 File Offset: 0x00000AE4
		protected StorageEntitySchema()
		{
			base.RegisterPropertyDefinition(StorageEntitySchema.StaticChangeKeyProperty);
			base.RegisterPropertyDefinition(StorageEntitySchema.StaticStoreIdProperty);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002902 File Offset: 0x00000B02
		public TypedPropertyDefinition<string> ChangeKeyProperty
		{
			get
			{
				return StorageEntitySchema.StaticChangeKeyProperty;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002909 File Offset: 0x00000B09
		internal TypedPropertyDefinition<StoreId> StoreIdProperty
		{
			get
			{
				return StorageEntitySchema.StaticStoreIdProperty;
			}
		}

		// Token: 0x0400001F RID: 31
		private static readonly TypedPropertyDefinition<string> StaticChangeKeyProperty = new TypedPropertyDefinition<string>("StorageEntity.ChangeKey", null, true);

		// Token: 0x04000020 RID: 32
		private static readonly TypedPropertyDefinition<StoreId> StaticStoreIdProperty = new TypedPropertyDefinition<StoreId>("StorageEntity.StoreId", null, true);
	}
}
