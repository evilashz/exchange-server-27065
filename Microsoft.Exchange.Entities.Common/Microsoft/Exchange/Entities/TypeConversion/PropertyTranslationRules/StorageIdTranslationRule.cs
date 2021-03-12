using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyTranslationRules
{
	// Token: 0x02000078 RID: 120
	internal class StorageIdTranslationRule<TStoreObject, TEntity, TEntitySchema> : IStorageTranslationRule<!0, !1>, ITranslationRule<TStoreObject, TEntity> where TStoreObject : IStoreObject where TEntity : StorageEntity<TEntitySchema> where TEntitySchema : StorageEntitySchema, new()
	{
		// Token: 0x06000290 RID: 656 RVA: 0x00008A60 File Offset: 0x00006C60
		public StorageIdTranslationRule(IdConverter idConverter)
		{
			this.StorageDependencies = new Microsoft.Exchange.Data.PropertyDefinition[]
			{
				StorageIdTranslationRule<TStoreObject, TEntity, TEntitySchema>.ItemId,
				StorageIdTranslationRule<TStoreObject, TEntity, TEntitySchema>.FolderId
			};
			this.StoragePropertyGroup = null;
			Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[] array = new Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[1];
			Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[] array2 = array;
			int num = 0;
			TEntitySchema schemaInstance = SchematizedObject<TEntitySchema>.SchemaInstance;
			array2[num] = schemaInstance.IdProperty;
			this.EntityProperties = array;
			this.IdConverter = idConverter;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00008AC3 File Offset: 0x00006CC3
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00008ACB File Offset: 0x00006CCB
		public IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> EntityProperties { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00008AD4 File Offset: 0x00006CD4
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00008ADC File Offset: 0x00006CDC
		public IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> StorageDependencies { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00008AE5 File Offset: 0x00006CE5
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00008AED File Offset: 0x00006CED
		public PropertyChangeMetadata.PropertyGroup StoragePropertyGroup { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00008AF6 File Offset: 0x00006CF6
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00008AFE File Offset: 0x00006CFE
		private protected IdConverter IdConverter { protected get; private set; }

		// Token: 0x06000299 RID: 665 RVA: 0x00008B2C File Offset: 0x00006D2C
		public void FromLeftToRightType(TStoreObject left, TEntity right)
		{
			this.FromLeftToRight(right, left.Session, delegate(out StoreId value)
			{
				value = left.Id;
				return value != null;
			});
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00008BE0 File Offset: 0x00006DE0
		public void FromPropertyValues(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, IStoreSession session, TEntity right)
		{
			this.FromLeftToRight(right, session, delegate(out StoreId value)
			{
				int index;
				value = (propertyIndices.TryGetValue(StorageIdTranslationRule<TStoreObject, TEntity, TEntitySchema>.ItemId, out index) ? (values[index] as StoreId) : null);
				if (value == null && propertyIndices.TryGetValue(StorageIdTranslationRule<TStoreObject, TEntity, TEntitySchema>.FolderId, out index))
				{
					value = (values[index] as StoreId);
				}
				return value != null;
			});
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00008C16 File Offset: 0x00006E16
		public void FromRightToLeftType(TStoreObject left, TEntity right)
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00008C18 File Offset: 0x00006E18
		private void FromLeftToRight(TEntity entity, IStoreSession session, StorageIdTranslationRule<TStoreObject, TEntity, TEntitySchema>.TryGetValueFunc<StoreId> getId)
		{
			StoreId storeId;
			if (getId != null && getId(out storeId))
			{
				string changeKey;
				entity.Id = this.IdConverter.ToStringId(storeId, session, out changeKey);
				entity.ChangeKey = changeKey;
				entity.StoreId = storeId;
			}
		}

		// Token: 0x040000F4 RID: 244
		private static readonly Microsoft.Exchange.Data.PropertyDefinition ItemId = ItemSchema.Id;

		// Token: 0x040000F5 RID: 245
		private static readonly Microsoft.Exchange.Data.PropertyDefinition FolderId = FolderSchema.Id;

		// Token: 0x02000079 RID: 121
		// (Invoke) Token: 0x0600029F RID: 671
		public delegate bool TryGetValueFunc<TValue>(out TValue value);
	}
}
