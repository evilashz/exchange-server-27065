using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000459 RID: 1113
	internal struct ScopeFlagsFormat
	{
		// Token: 0x06003210 RID: 12816 RVA: 0x000CABDC File Offset: 0x000C8DDC
		internal static object ScopeRestrictionTypeGetter(IPropertyBag propertyBag)
		{
			ScopeRestrictionType scopeRestrictionType = (ScopeRestrictionType)((int)propertyBag[ManagementScopeSchema.ScopeRestrictionFlags] & 255);
			return (scopeRestrictionType == ScopeRestrictionType.DomainScope_Obsolete) ? ScopeRestrictionType.RecipientScope : scopeRestrictionType;
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000CAC10 File Offset: 0x000C8E10
		internal static void ScopeRestrictionTypeSetter(object value, IPropertyBag propertyBag)
		{
			uint num = (uint)((int)propertyBag[ManagementScopeSchema.ScopeRestrictionFlags]);
			propertyBag[ManagementScopeSchema.ScopeRestrictionFlags] = (int)((num & 4294967040U) | (uint)((ScopeRestrictionType)value));
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000CAC4C File Offset: 0x000C8E4C
		internal static object ExclusiveTypeGetter(IPropertyBag propertyBag)
		{
			return (uint)((int)propertyBag[ManagementScopeSchema.ScopeRestrictionFlags]) >> 31 != 0U;
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000CAC6C File Offset: 0x000C8E6C
		internal static void ExclusiveTypeSetter(object value, IPropertyBag propertyBag)
		{
			uint num = (uint)((int)propertyBag[ManagementScopeSchema.ScopeRestrictionFlags]);
			uint num2 = ((bool)value) ? 1U : 0U;
			propertyBag[ManagementScopeSchema.ScopeRestrictionFlags] = (int)(num2 << 31 | (num & 255U));
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x000CACB4 File Offset: 0x000C8EB4
		private static QueryFilter ScopeRestrictionFlagsFilterBuilder<T>(SinglePropertyFilter filter, ScopeFlagsFormat.ConvertToMaskDelegate convertor)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			if (comparisonFilter.ComparisonOperator != ComparisonOperator.Equal && ComparisonOperator.NotEqual != comparisonFilter.ComparisonOperator)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
			if (!(comparisonFilter.PropertyValue is T))
			{
				throw new ArgumentException("filter.PropertyValue");
			}
			QueryFilter queryFilter = new BitMaskAndFilter(ManagementScopeSchema.ScopeRestrictionFlags, convertor(comparisonFilter.PropertyValue));
			if (comparisonFilter.PropertyValue is bool && !(bool)comparisonFilter.PropertyValue)
			{
				if (ComparisonOperator.NotEqual != comparisonFilter.ComparisonOperator)
				{
					return new NotFilter(queryFilter);
				}
				return queryFilter;
			}
			else
			{
				if (ComparisonOperator.NotEqual != comparisonFilter.ComparisonOperator)
				{
					return queryFilter;
				}
				return new NotFilter(queryFilter);
			}
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x000CADAB File Offset: 0x000C8FAB
		internal static QueryFilter ScopeRestrictionTypeFilterBuilder(SinglePropertyFilter filter)
		{
			return ScopeFlagsFormat.ScopeRestrictionFlagsFilterBuilder<ScopeRestrictionType>(filter, (object propertyValue) => (ulong)((ScopeRestrictionType)propertyValue & (ScopeRestrictionType)255));
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000CADD8 File Offset: 0x000C8FD8
		internal static QueryFilter ExclusiveTypeFilterBuilder(SinglePropertyFilter filter)
		{
			return ScopeFlagsFormat.ScopeRestrictionFlagsFilterBuilder<bool>(filter, (object propertyValue) => (ulong)int.MinValue);
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000CAE4C File Offset: 0x000C904C
		internal static GetterDelegate FilterGetterDelegate(ScopeRestrictionType filterType)
		{
			return delegate(IPropertyBag bag)
			{
				ScopeRestrictionType scopeRestrictionType = (ScopeRestrictionType)bag[ManagementScopeSchema.ScopeRestrictionType];
				if (scopeRestrictionType == ScopeRestrictionType.DomainScope_Obsolete)
				{
					scopeRestrictionType = ScopeRestrictionType.RecipientScope;
				}
				if (scopeRestrictionType != filterType)
				{
					return string.Empty;
				}
				return (string)bag[ManagementScopeSchema.Filter];
			};
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x000CAEC0 File Offset: 0x000C90C0
		internal static SetterDelegate FilterSetterDelegate(ScopeRestrictionType filterType)
		{
			return delegate(object value, IPropertyBag bag)
			{
				ScopeRestrictionType scopeRestrictionType = (ScopeRestrictionType)bag[ManagementScopeSchema.ScopeRestrictionType];
				if (scopeRestrictionType == ScopeRestrictionType.DomainScope_Obsolete)
				{
					scopeRestrictionType = ScopeRestrictionType.RecipientScope;
				}
				if (scopeRestrictionType != filterType)
				{
					throw new ArgumentException();
				}
				bag[ManagementScopeSchema.Filter] = (string)value;
			};
		}

		// Token: 0x04002250 RID: 8784
		internal const uint ScopeRestrictionTypeMask = 255U;

		// Token: 0x04002251 RID: 8785
		internal const uint ExclusiveBitMask = 2147483648U;

		// Token: 0x04002252 RID: 8786
		internal const int ExclusiveBitShift = 31;

		// Token: 0x0200045A RID: 1114
		// (Invoke) Token: 0x0600321C RID: 12828
		private delegate ulong ConvertToMaskDelegate(object propertyValue);
	}
}
