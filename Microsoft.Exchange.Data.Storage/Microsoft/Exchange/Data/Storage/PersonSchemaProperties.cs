using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CA0 RID: 3232
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PersonSchemaProperties
	{
		// Token: 0x060070C5 RID: 28869 RVA: 0x001F44B8 File Offset: 0x001F26B8
		public PersonSchemaProperties(PropertyDefinition[] extendedProperties, params IEnumerable<PropertyDefinition>[] otherPropertySets)
		{
			if (extendedProperties != null)
			{
				StorePropertyDefinition[] array = new StorePropertyDefinition[extendedProperties.Length];
				for (int i = 0; i < extendedProperties.Length; i++)
				{
					array[i] = (StorePropertyDefinition)extendedProperties[i];
				}
				ApplicationAggregatedProperty item = new ApplicationAggregatedProperty(PersonSchema.ExtendedProperties, PersonPropertyAggregationStrategy.CreateExtendedPropertiesAggregation(array));
				HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
				hashSet.Add(item);
				if (otherPropertySets != null)
				{
					foreach (ICollection<PropertyDefinition> collection in otherPropertySets)
					{
						if (collection != null)
						{
							foreach (PropertyDefinition propertyDefinition in collection)
							{
								if (!object.Equals(propertyDefinition, PersonSchema.ExtendedProperties))
								{
									hashSet.Add(propertyDefinition);
								}
							}
						}
					}
				}
				this.All = new PropertyDefinition[hashSet.Count];
				hashSet.CopyTo(this.All);
				return;
			}
			this.All = PropertyDefinitionCollection.Merge<PropertyDefinition>(otherPropertySets);
		}

		// Token: 0x17001E3D RID: 7741
		// (get) Token: 0x060070C6 RID: 28870 RVA: 0x001F45B8 File Offset: 0x001F27B8
		// (set) Token: 0x060070C7 RID: 28871 RVA: 0x001F45C0 File Offset: 0x001F27C0
		public PropertyDefinition[] All { get; private set; }
	}
}
