using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002B9 RID: 697
	internal sealed class CountryListSchema : ADConfigurationObjectSchema
	{
		// Token: 0x06001FFB RID: 8187 RVA: 0x0008D350 File Offset: 0x0008B550
		internal static void CountriesSetter(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = null;
			MultiValuedProperty<CountryInfo> multiValuedProperty2 = (MultiValuedProperty<CountryInfo>)value;
			if (multiValuedProperty2 != null)
			{
				multiValuedProperty = new MultiValuedProperty<string>();
				foreach (CountryInfo countryInfo in multiValuedProperty2)
				{
					multiValuedProperty.Add(countryInfo.Name);
				}
			}
			propertyBag[CountryListSchema.RawCountries] = multiValuedProperty;
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0008D3C4 File Offset: 0x0008B5C4
		internal static object CountriesGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<CountryInfo> multiValuedProperty = null;
			MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)propertyBag[CountryListSchema.RawCountries];
			if (multiValuedProperty2 != null)
			{
				multiValuedProperty = new MultiValuedProperty<CountryInfo>();
				foreach (string name in multiValuedProperty2)
				{
					multiValuedProperty.Add(CountryInfo.Parse(name));
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x04001334 RID: 4916
		internal static readonly ExchangeObjectVersion ObjectVersion = ExchangeObjectVersion.Exchange2010;

		// Token: 0x04001335 RID: 4917
		public static readonly ADPropertyDefinition RawCountries = new ADPropertyDefinition("RawCountries", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchCountries", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001336 RID: 4918
		public static readonly ADPropertyDefinition Countries = new ADPropertyDefinition("Countries", ExchangeObjectVersion.Exchange2010, typeof(CountryInfo), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			CountryListSchema.RawCountries
		}, null, new GetterDelegate(CountryListSchema.CountriesGetter), new SetterDelegate(CountryListSchema.CountriesSetter), null, null);
	}
}
