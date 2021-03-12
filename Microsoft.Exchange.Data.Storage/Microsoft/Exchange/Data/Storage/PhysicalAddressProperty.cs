using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CA2 RID: 3234
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class PhysicalAddressProperty : AtomRuleCompositeProperty
	{
		// Token: 0x060070CE RID: 28878 RVA: 0x001F46FC File Offset: 0x001F28FC
		internal PhysicalAddressProperty(string displayName, NativeStorePropertyDefinition compositeProperty, NativeStorePropertyDefinition streetProperty, NativeStorePropertyDefinition cityProperty, NativeStorePropertyDefinition stateProperty, NativeStorePropertyDefinition postalProperty, NativeStorePropertyDefinition countryProperty) : base(displayName, compositeProperty, new NativeStorePropertyDefinition[]
		{
			streetProperty,
			cityProperty,
			stateProperty,
			postalProperty,
			countryProperty
		})
		{
			this.placeholderCodeToPropDef = PhysicalAddressProperty.CreateMapping(streetProperty, cityProperty, stateProperty, postalProperty, countryProperty);
		}

		// Token: 0x060070CF RID: 28879 RVA: 0x001F4748 File Offset: 0x001F2948
		private static Dictionary<string, NativeStorePropertyDefinition> CreateMapping(NativeStorePropertyDefinition streetProperty, NativeStorePropertyDefinition cityProperty, NativeStorePropertyDefinition stateProperty, NativeStorePropertyDefinition postalProperty, NativeStorePropertyDefinition countryProperty)
		{
			return Util.AddElements<Dictionary<string, NativeStorePropertyDefinition>, KeyValuePair<string, NativeStorePropertyDefinition>>(new Dictionary<string, NativeStorePropertyDefinition>(), new KeyValuePair<string, NativeStorePropertyDefinition>[]
			{
				new KeyValuePair<string, NativeStorePropertyDefinition>("Street", streetProperty),
				new KeyValuePair<string, NativeStorePropertyDefinition>("City", cityProperty),
				new KeyValuePair<string, NativeStorePropertyDefinition>("State", stateProperty),
				new KeyValuePair<string, NativeStorePropertyDefinition>("Postal", postalProperty),
				new KeyValuePair<string, NativeStorePropertyDefinition>("Region", countryProperty)
			});
		}

		// Token: 0x060070D0 RID: 28880 RVA: 0x001F47DC File Offset: 0x001F29DC
		protected override string GenerateCompositePropertyValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			AtomRuleCompositeProperty.FormattedSentenceContext formattedSentenceContext = new AtomRuleCompositeProperty.FormattedSentenceContext(propertyBag, this.placeholderCodeToPropDef);
			FormattedSentence formattedSentence = PhysicalAddressProperty.enUsPattern;
			string text = formattedSentenceContext.ResolvePlaceholder("Region");
			if (text != null)
			{
				Dictionary<string, string> regionMap = PhysicalAddressProperty.addressData.Value.RegionMap;
				text = text.Trim();
				string key;
				if (regionMap.TryGetValue(text, out key))
				{
					Dictionary<string, FormattedSentence> formatMap = PhysicalAddressProperty.addressData.Value.FormatMap;
					FormattedSentence formattedSentence2;
					if (formatMap.TryGetValue(key, out formattedSentence2))
					{
						formattedSentence = formattedSentence2;
					}
				}
			}
			return formattedSentence.Evaluate(formattedSentenceContext);
		}

		// Token: 0x04004E68 RID: 20072
		private static readonly FormattedSentence enUsPattern = new FormattedSentence("{Street}\r\n<<{City}, {State}> {Postal}>\r\n{Region}");

		// Token: 0x04004E69 RID: 20073
		private readonly Dictionary<string, NativeStorePropertyDefinition> placeholderCodeToPropDef;

		// Token: 0x04004E6A RID: 20074
		private static LazilyInitialized<PhysicalAddressData> addressData = new LazilyInitialized<PhysicalAddressData>(() => new PhysicalAddressData());
	}
}
