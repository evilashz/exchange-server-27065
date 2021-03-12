using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000477 RID: 1143
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ADPersonToContactConverterSet
	{
		// Token: 0x060032FF RID: 13055 RVA: 0x000CF3F5 File Offset: 0x000CD5F5
		private ADPersonToContactConverterSet(ADPersonToContactConverter[] converters, PropertyDefinition[] personProperties, ADPropertyDefinition[] adProperties)
		{
			this.converters = converters;
			this.personProperties = personProperties;
			this.adProperties = adProperties;
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06003300 RID: 13056 RVA: 0x000CF412 File Offset: 0x000CD612
		public static ADPersonToContactConverterSet OrganizationalContactProperties
		{
			get
			{
				return ADPersonToContactConverterSet.organizationalContactProperties;
			}
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x000CF419 File Offset: 0x000CD619
		public static ADPersonToContactConverterSet PersonSchemaProperties
		{
			get
			{
				return ADPersonToContactConverterSet.personSchemaProperties;
			}
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06003302 RID: 13058 RVA: 0x000CF420 File Offset: 0x000CD620
		public ADPropertyDefinition[] ADProperties
		{
			get
			{
				return this.adProperties;
			}
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x000CF428 File Offset: 0x000CD628
		public PropertyDefinition[] PersonProperties
		{
			get
			{
				return this.personProperties;
			}
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000CF430 File Offset: 0x000CD630
		public static ADPersonToContactConverterSet FromPersonProperties(PropertyDefinition[] personProperties, IEnumerable<PropertyDefinition> additionalContactProperties)
		{
			ADPersonToContactConverter[] array = ADPersonToContactConverterSet.GetConverters(personProperties, additionalContactProperties);
			ADPropertyDefinition[] adproperties = ADPersonToContactConverterSet.GetADProperties(array, Array<ADPropertyDefinition>.Empty);
			return new ADPersonToContactConverterSet(array, personProperties, adproperties);
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000CF45C File Offset: 0x000CD65C
		public static ADPersonToContactConverterSet FromContactProperties(PropertyDefinition[] personProperties, HashSet<PropertyDefinition> contactProperties)
		{
			ADPersonToContactConverter[] array = ADPersonToContactConverterSet.GetConverters(contactProperties);
			ADPropertyDefinition[] adproperties = ADPersonToContactConverterSet.GetADProperties(array, Array<ADPropertyDefinition>.Empty);
			return new ADPersonToContactConverterSet(array, personProperties, adproperties);
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000CF484 File Offset: 0x000CD684
		public void Convert(ADRawEntry adObject, IStorePropertyBag contact)
		{
			foreach (ADPersonToContactConverter adpersonToContactConverter in this.converters)
			{
				adpersonToContactConverter.Convert(adObject, contact);
			}
			contact[ContactSchema.PartnerNetworkId] = WellKnownNetworkNames.GAL;
			contact[ContactSchema.GALLinkID] = adObject.Id.ObjectGuid;
			contact[ItemSchema.ParentDisplayName] = string.Empty;
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000CF4F0 File Offset: 0x000CD6F0
		public IStorePropertyBag Convert(ADRawEntry adObject)
		{
			MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag();
			IStorePropertyBag storePropertyBag = memoryPropertyBag.AsIStorePropertyBag();
			this.Convert(adObject, storePropertyBag);
			memoryPropertyBag.SetAllPropertiesLoaded();
			return storePropertyBag;
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000CF51C File Offset: 0x000CD71C
		private static ADPersonToContactConverterSet Create(params ADPropertyDefinition[] additionalADProperties)
		{
			PropertyDefinition[] array = new PropertyDefinition[PersonSchema.Instance.AllProperties.Count];
			PersonSchema.Instance.AllProperties.CopyTo(array, 0);
			ADPersonToContactConverter[] array2 = ADPersonToContactConverterSet.GetConverters(array, null);
			ADPropertyDefinition[] adproperties = ADPersonToContactConverterSet.GetADProperties(array2, additionalADProperties);
			return new ADPersonToContactConverterSet(array2, array, adproperties);
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000CF568 File Offset: 0x000CD768
		private static ADPersonToContactConverter[] GetConverters(ICollection<PropertyDefinition> personProperties, IEnumerable<PropertyDefinition> additionalContactProperties)
		{
			new List<ADPersonToContactConverter>(personProperties.Count);
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in personProperties)
			{
				ApplicationAggregatedProperty applicationAggregatedProperty = propertyDefinition as ApplicationAggregatedProperty;
				if (applicationAggregatedProperty != null)
				{
					foreach (PropertyDependency propertyDependency in applicationAggregatedProperty.Dependencies)
					{
						hashSet.Add(propertyDependency.Property);
					}
				}
			}
			if (additionalContactProperties != null)
			{
				foreach (PropertyDefinition item in additionalContactProperties)
				{
					hashSet.Add(item);
				}
			}
			return ADPersonToContactConverterSet.GetConverters(hashSet);
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000CF640 File Offset: 0x000CD840
		private static ADPersonToContactConverter[] GetConverters(HashSet<PropertyDefinition> contactProperties)
		{
			List<ADPersonToContactConverter> list = new List<ADPersonToContactConverter>(contactProperties.Count);
			foreach (PropertyDefinition key in contactProperties)
			{
				ADPersonToContactConverter item;
				if (ADPersonToContactConverterSet.contactPropertyToConverterMap.TryGetValue(key, out item))
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000CF6B0 File Offset: 0x000CD8B0
		private static ADPropertyDefinition[] GetADProperties(ADPersonToContactConverter[] converters, ADPropertyDefinition[] additionalProperties)
		{
			HashSet<ADPropertyDefinition> hashSet = new HashSet<ADPropertyDefinition>();
			foreach (ADPersonToContactConverter adpersonToContactConverter in converters)
			{
				hashSet.UnionWith(adpersonToContactConverter.ADProperties);
			}
			ADPropertyDefinition[] array = new ADPropertyDefinition[hashSet.Count];
			hashSet.CopyTo(array);
			return array;
		}

		// Token: 0x04001B83 RID: 7043
		private static readonly Dictionary<PropertyDefinition, ADPersonToContactConverter> contactPropertyToConverterMap = ADPersonToContactConverter.AllConverters.ToDictionary((ADPersonToContactConverter item) => item.ContactProperty, (ADPersonToContactConverter item) => item);

		// Token: 0x04001B84 RID: 7044
		private static readonly ADPersonToContactConverterSet personSchemaProperties = ADPersonToContactConverterSet.Create(new ADPropertyDefinition[0]);

		// Token: 0x04001B85 RID: 7045
		private static readonly ADPersonToContactConverterSet organizationalContactProperties = ADPersonToContactConverterSet.Create(new ADPropertyDefinition[]
		{
			ADRecipientSchema.RecipientDisplayType,
			ADRecipientSchema.LegacyExchangeDN
		});

		// Token: 0x04001B86 RID: 7046
		private readonly ADPropertyDefinition[] adProperties;

		// Token: 0x04001B87 RID: 7047
		private readonly PropertyDefinition[] personProperties;

		// Token: 0x04001B88 RID: 7048
		private readonly ADPersonToContactConverter[] converters;
	}
}
