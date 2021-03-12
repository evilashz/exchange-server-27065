using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel
{
	// Token: 0x02000014 RID: 20
	public abstract class StorageEntity<TSchema> : Entity<TSchema>, IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned where TSchema : StorageEntitySchema, new()
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000026C8 File Offset: 0x000008C8
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000026F0 File Offset: 0x000008F0
		public string ChangeKey
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<string>(schema.ChangeKeyProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<string>(schema.ChangeKeyProperty, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002718 File Offset: 0x00000918
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002740 File Offset: 0x00000940
		internal StoreId StoreId
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<StoreId>(schema.StoreIdProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<StoreId>(schema.StoreIdProperty, value);
			}
		}

		// Token: 0x02000015 RID: 21
		public new static class Accessors
		{
			// Token: 0x06000047 RID: 71 RVA: 0x00002794 File Offset: 0x00000994
			// Note: this type is marked as 'beforefieldinit'.
			static Accessors()
			{
				TSchema schemaInstance = SchematizedObject<TSchema>.SchemaInstance;
				StorageEntity<TSchema>.Accessors.ChangeKey = new EntityPropertyAccessor<IStorageEntity, string>(schemaInstance.ChangeKeyProperty, (IStorageEntity entity) => entity.ChangeKey, delegate(IStorageEntity entity, string s)
				{
					entity.ChangeKey = s;
				});
				TSchema schemaInstance2 = SchematizedObject<TSchema>.SchemaInstance;
				StorageEntity<TSchema>.Accessors.StoreId = new EntityPropertyAccessor<StorageEntity<TSchema>, StoreId>(schemaInstance2.StoreIdProperty, (StorageEntity<TSchema> entity) => entity.StoreId, delegate(StorageEntity<TSchema> entity, StoreId id)
				{
					entity.StoreId = id;
				});
			}

			// Token: 0x04000017 RID: 23
			public static readonly EntityPropertyAccessor<IStorageEntity, string> ChangeKey;

			// Token: 0x04000018 RID: 24
			internal static readonly EntityPropertyAccessor<StorageEntity<TSchema>, StoreId> StoreId;
		}
	}
}
