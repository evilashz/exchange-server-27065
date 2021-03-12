using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002AB RID: 683
	internal class AuthRedirectSchema : ADNonExchangeObjectSchema
	{
		// Token: 0x06001F96 RID: 8086 RVA: 0x0008C214 File Offset: 0x0008A414
		public static object AuthSchemeGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[AuthRedirectSchema.Keywords];
			string value = multiValuedProperty.Find((string x) => !string.Equals(x, AuthRedirect.AuthRedirectKeywords, StringComparison.OrdinalIgnoreCase));
			AuthScheme authScheme = Microsoft.Exchange.Data.Directory.SystemConfiguration.AuthScheme.Unknown;
			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					authScheme = (AuthScheme)Enum.Parse(typeof(AuthScheme), value, true);
				}
				catch (ArgumentException)
				{
				}
			}
			return authScheme;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0008C294 File Offset: 0x0008A494
		private static void AuthSchemeSetter(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[AuthRedirectSchema.Keywords];
			multiValuedProperty.Clear();
			string item = value.ToString();
			multiValuedProperty.Add(AuthRedirect.AuthRedirectKeywords);
			multiValuedProperty.Add(item);
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x0008C2D4 File Offset: 0x0008A4D4
		private static QueryFilter AuthSchemeFilterBuilder(SinglePropertyFilter filter)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			switch (comparisonFilter.ComparisonOperator)
			{
			case ComparisonOperator.Equal:
				return new AndFilter(new QueryFilter[]
				{
					AuthRedirect.AuthRedirectKeywordsFilter,
					new ComparisonFilter(ComparisonOperator.Equal, AuthRedirectSchema.Keywords, comparisonFilter.PropertyValue.ToString())
				});
			case ComparisonOperator.NotEqual:
				return new AndFilter(new QueryFilter[]
				{
					AuthRedirect.AuthRedirectKeywordsFilter,
					new ComparisonFilter(ComparisonOperator.NotEqual, AuthRedirectSchema.Keywords, comparisonFilter.PropertyValue.ToString())
				});
			default:
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
		}

		// Token: 0x040012C5 RID: 4805
		public new static readonly ADPropertyDefinition ExchangeVersion = new ADPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2003, typeof(ExchangeObjectVersion), null, ADPropertyDefinitionFlags.TaskPopulated | ADPropertyDefinitionFlags.DoNotProvisionalClone, ExchangeObjectVersion.Exchange2003, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040012C6 RID: 4806
		public static readonly ADPropertyDefinition Keywords = new ADPropertyDefinition("Keywords", ExchangeObjectVersion.Exchange2003, typeof(string), "keywords", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040012C7 RID: 4807
		public static readonly ADPropertyDefinition TargetUrl = new ADPropertyDefinition("TargetUrl", ExchangeObjectVersion.Exchange2003, typeof(string), "serviceBindingInformation", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040012C8 RID: 4808
		public static readonly ADPropertyDefinition AuthScheme = new ADPropertyDefinition("AuthScheme", ExchangeObjectVersion.Exchange2003, typeof(AuthScheme), null, ADPropertyDefinitionFlags.Calculated, Microsoft.Exchange.Data.Directory.SystemConfiguration.AuthScheme.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			AuthRedirectSchema.Keywords
		}, new CustomFilterBuilderDelegate(AuthRedirectSchema.AuthSchemeFilterBuilder), new GetterDelegate(AuthRedirectSchema.AuthSchemeGetter), new SetterDelegate(AuthRedirectSchema.AuthSchemeSetter), null, null);
	}
}
