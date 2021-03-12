using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.TypeConversion.Converters;
using Microsoft.Exchange.Entities.TypeConversion.PropertyTranslationRules;

namespace Microsoft.Exchange.Entities.TypeConversion.Translators
{
	// Token: 0x02000084 RID: 132
	internal class StorageEntityTranslator<TStoreObject, TEntity, TEntitySchema> : StorageTranslator<TStoreObject, TEntity> where TStoreObject : IStoreObject where TEntity : StorageEntity<TEntitySchema>, new() where TEntitySchema : StorageEntitySchema, new()
	{
		// Token: 0x060002ED RID: 749 RVA: 0x00009A06 File Offset: 0x00007C06
		protected StorageEntityTranslator(IEnumerable<ITranslationRule<TStoreObject, TEntity>> additionalRules = null) : base(StorageEntityTranslator<TStoreObject, TEntity, TEntitySchema>.CreateTranslationRules().AddRules(additionalRules))
		{
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00009A19 File Offset: 0x00007C19
		public static StorageEntityTranslator<TStoreObject, TEntity, TEntitySchema> Instance
		{
			get
			{
				return StorageEntityTranslator<TStoreObject, TEntity, TEntitySchema>.SingletonInstance;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00009A20 File Offset: 0x00007C20
		public override void SetPropertiesFromStoragePropertyValuesOnEntity(IDictionary<PropertyDefinition, int> propertyIndices, IList values, IStoreSession session, TEntity destinationEntity)
		{
			StorageEntityTranslator<TStoreObject, TEntity, TEntitySchema>.IdTranslationRule.FromPropertyValues(propertyIndices, values, session, destinationEntity);
			base.SetPropertiesFromStoragePropertyValuesOnEntity(propertyIndices, values, session, destinationEntity);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00009A3C File Offset: 0x00007C3C
		protected override TEntity CreateEntity()
		{
			return Activator.CreateInstance<TEntity>();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00009A44 File Offset: 0x00007C44
		private static List<ITranslationRule<TStoreObject, TEntity>> CreateTranslationRules()
		{
			return new List<ITranslationRule<TStoreObject, TEntity>>
			{
				StorageEntityTranslator<TStoreObject, TEntity, TEntitySchema>.IdTranslationRule
			};
		}

		// Token: 0x0400010D RID: 269
		private static readonly StorageEntityTranslator<TStoreObject, TEntity, TEntitySchema> SingletonInstance = new StorageEntityTranslator<TStoreObject, TEntity, TEntitySchema>(null);

		// Token: 0x0400010E RID: 270
		private static readonly StorageIdTranslationRule<TStoreObject, TEntity, TEntitySchema> IdTranslationRule = new StorageIdTranslationRule<TStoreObject, TEntity, TEntitySchema>(IdConverter.Instance);
	}
}
