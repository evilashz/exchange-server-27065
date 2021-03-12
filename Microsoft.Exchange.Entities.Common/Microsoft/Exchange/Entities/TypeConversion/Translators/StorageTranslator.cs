using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.TypeConversion.Converters;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Entities.TypeConversion.Translators
{
	// Token: 0x02000081 RID: 129
	internal abstract class StorageTranslator<TStoreObject, TEntity> : IStorageTranslator<TStoreObject, TEntity>
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x00009588 File Offset: 0x00007788
		protected StorageTranslator(IList<ITranslationRule<TStoreObject, TEntity>> additionalRules)
		{
			this.Strategy = new TranslationStrategy<TStoreObject, Microsoft.Exchange.Data.PropertyDefinition, TEntity>(additionalRules);
			IEnumerable<IStorageTranslationRule<TStoreObject, TEntity>> enumerable = StorageTranslator<TStoreObject, TEntity>.BuildMappings(this.Strategy);
			this.edmPropertyNameToStorageDependencies = new Dictionary<string, IEnumerable<Microsoft.Exchange.Data.PropertyDefinition>>();
			List<Tuple<PropertyChangeMetadata.PropertyGroup, IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition>>> list = new List<Tuple<PropertyChangeMetadata.PropertyGroup, IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition>>>();
			foreach (IStorageTranslationRule<TStoreObject, TEntity> storageTranslationRule in enumerable)
			{
				Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[] array = storageTranslationRule.EntityProperties.ToArray<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition>();
				IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> dependencies = storageTranslationRule.StorageDependencies ?? ((IEnumerable<Microsoft.Exchange.Data.PropertyDefinition>)new Microsoft.Exchange.Data.PropertyDefinition[0]);
				this.edmPropertyNameToStorageDependencies.AddRange(from entityProperty in array
				select new KeyValuePair<string, IEnumerable<Microsoft.Exchange.Data.PropertyDefinition>>(entityProperty.Name, dependencies));
				if (storageTranslationRule.StoragePropertyGroup != null)
				{
					Tuple<PropertyChangeMetadata.PropertyGroup, IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition>> item = new Tuple<PropertyChangeMetadata.PropertyGroup, IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition>>(storageTranslationRule.StoragePropertyGroup, array);
					list.Add(item);
				}
			}
			this.StoragePropertyGroupsToEdmProperties = SimpleMappingConverter<PropertyChangeMetadata.PropertyGroup, IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition>>.CreateRelaxedConverter(list);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00009674 File Offset: 0x00007874
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000967C File Offset: 0x0000787C
		private protected SimpleMappingConverter<PropertyChangeMetadata.PropertyGroup, IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition>> StoragePropertyGroupsToEdmProperties { protected get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00009685 File Offset: 0x00007885
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000968D File Offset: 0x0000788D
		private protected TranslationStrategy<TStoreObject, Microsoft.Exchange.Data.PropertyDefinition, TEntity> Strategy { protected get; private set; }

		// Token: 0x060002D9 RID: 729 RVA: 0x00009698 File Offset: 0x00007898
		public IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> Map(string entityPropertyName)
		{
			IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> result;
			if (!this.edmPropertyNameToStorageDependencies.TryGetValue(entityPropertyName, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000096B8 File Offset: 0x000078B8
		public IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> Map(IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> properties)
		{
			HashSet<Microsoft.Exchange.Data.PropertyDefinition> hashSet = new HashSet<Microsoft.Exchange.Data.PropertyDefinition>();
			foreach (Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition propertyDefinition in properties)
			{
				IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> enumerable = this.Map(propertyDefinition.Name);
				if (enumerable == null)
				{
					throw new ArgumentNotSupportedException(string.Format("properties:{0}", propertyDefinition.Name), "Mapped Properties");
				}
				hashSet.AddRange(enumerable);
			}
			return hashSet;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00009734 File Offset: 0x00007934
		public void SetPropertiesFromStorageObjectOnEntity(TStoreObject sourceStoreObject, TEntity destinationEntity)
		{
			this.Strategy.FromLeftToRightType(sourceStoreObject, destinationEntity);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00009743 File Offset: 0x00007943
		public void SetPropertiesFromEntityOnStorageObject(TEntity sourceEntity, TStoreObject destinationStoreObject)
		{
			this.Strategy.FromRightToLeftType(destinationStoreObject, sourceEntity);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00009752 File Offset: 0x00007952
		public virtual void SetPropertiesFromStoragePropertyValuesOnEntity(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, IStoreSession session, TEntity destinationEntity)
		{
			this.Strategy.FromPropertyValues(propertyIndices, values, destinationEntity);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00009764 File Offset: 0x00007964
		public TEntity ConvertToEntity(TStoreObject sourceStoreObject)
		{
			TEntity tentity = this.CreateEntity();
			this.SetPropertiesFromStorageObjectOnEntity(sourceStoreObject, tentity);
			return tentity;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00009784 File Offset: 0x00007984
		public TEntity ConvertToEntity(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, IStoreSession session)
		{
			TEntity tentity = this.CreateEntity();
			this.SetPropertiesFromStoragePropertyValuesOnEntity(propertyIndices, values, session, tentity);
			return tentity;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000097A3 File Offset: 0x000079A3
		public virtual TEntity ConvertToEntity(TStoreObject sourceStoreObject1, TStoreObject sourceStoreObject2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002E1 RID: 737
		protected abstract TEntity CreateEntity();

		// Token: 0x060002E2 RID: 738 RVA: 0x000097C8 File Offset: 0x000079C8
		private static IEnumerable<IStorageTranslationRule<TStoreObject, TEntity>> BuildMappings(IEnumerable<ITranslationRule<TStoreObject, TEntity>> ruleSet)
		{
			return from rule in ruleSet.EnumerateRules<TStoreObject, TEntity>()
			select rule as IStorageTranslationRule<!0, !1> into storageRule
			where storageRule != null && storageRule.EntityProperties != null
			select storageRule;
		}

		// Token: 0x04000106 RID: 262
		private readonly Dictionary<string, IEnumerable<Microsoft.Exchange.Data.PropertyDefinition>> edmPropertyNameToStorageDependencies;
	}
}
