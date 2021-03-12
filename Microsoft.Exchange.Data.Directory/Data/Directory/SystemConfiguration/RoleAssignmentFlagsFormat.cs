using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000452 RID: 1106
	internal static class RoleAssignmentFlagsFormat
	{
		// Token: 0x060031C2 RID: 12738 RVA: 0x000C8D30 File Offset: 0x000C6F30
		static RoleAssignmentFlagsFormat()
		{
			int num = 64;
			for (int i = 0; i <= 11; i++)
			{
				RoleAssignmentFlagsFormat.masks[i] = RoleAssignmentFlagsFormat.Mask(RoleAssignmentFlagsFormat.sizes[i]);
				num -= RoleAssignmentFlagsFormat.sizes[i];
				RoleAssignmentFlagsFormat.shifts[i] = num;
			}
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000C8DAB File Offset: 0x000C6FAB
		private static ulong Mask(int numberOfBits)
		{
			return ulong.MaxValue >> 64 - numberOfBits;
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000C8DB8 File Offset: 0x000C6FB8
		private static ulong GetBits(IPropertyBag propertyBag, RoleAssignmentFlagsFormat.Bitfields bitfield)
		{
			long num = (long)propertyBag[ExchangeRoleAssignmentSchema.ExchangeRoleAssignmentFlags];
			return (ulong)num >> RoleAssignmentFlagsFormat.shifts[(int)bitfield] & RoleAssignmentFlagsFormat.masks[(int)bitfield];
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000C8DEC File Offset: 0x000C6FEC
		private static void SetBits(ulong valueToSet, IPropertyBag propertyBag, RoleAssignmentFlagsFormat.Bitfields bitfield)
		{
			long num = (long)propertyBag[ExchangeRoleAssignmentSchema.ExchangeRoleAssignmentFlags];
			propertyBag[ExchangeRoleAssignmentSchema.ExchangeRoleAssignmentFlags] = ((num & (long)(~(long)((long)RoleAssignmentFlagsFormat.masks[(int)bitfield] << RoleAssignmentFlagsFormat.shifts[(int)bitfield]))) | (long)((long)(valueToSet & RoleAssignmentFlagsFormat.masks[(int)bitfield]) << RoleAssignmentFlagsFormat.shifts[(int)bitfield]));
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000C8E48 File Offset: 0x000C7048
		internal static ulong GetRawUInt64Bits(ulong valueToSet, RoleAssignmentFlagsFormat.Bitfields bitfield)
		{
			return (valueToSet & RoleAssignmentFlagsFormat.masks[(int)bitfield]) << RoleAssignmentFlagsFormat.shifts[(int)bitfield];
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000C8E6C File Offset: 0x000C706C
		private static QueryFilter RoleAssignmentFlagsFilterBuilder<T>(SinglePropertyFilter filter, RoleAssignmentFlagsFormat.Bitfields bitfield, RoleAssignmentFlagsFormat.ConvertToUlongDelegate convertor)
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
			ulong rawUInt64Bits = RoleAssignmentFlagsFormat.GetRawUInt64Bits(convertor(comparisonFilter.PropertyValue), bitfield);
			ulong mask = RoleAssignmentFlagsFormat.masks[(int)bitfield] << RoleAssignmentFlagsFormat.shifts[(int)bitfield] & ~rawUInt64Bits;
			QueryFilter queryFilter = new BitMaskAndFilter(ExchangeRoleAssignmentSchema.ExchangeRoleAssignmentFlags, rawUInt64Bits);
			QueryFilter queryFilter2 = new BitMaskOrFilter(ExchangeRoleAssignmentSchema.ExchangeRoleAssignmentFlags, mask);
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
					return new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new NotFilter(queryFilter2)
					});
				}
				return new OrFilter(new QueryFilter[]
				{
					new NotFilter(queryFilter),
					queryFilter2
				});
			}
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000C8FD4 File Offset: 0x000C71D4
		internal static GetterDelegate ScopeTypeGetterDelegate(RoleAssignmentFlagsFormat.Bitfields bitfield)
		{
			return (IPropertyBag propertyBag) => (ScopeType)RoleAssignmentFlagsFormat.GetBits(propertyBag, bitfield);
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000C9018 File Offset: 0x000C7218
		internal static SetterDelegate ScopeTypeSetterDelegate(RoleAssignmentFlagsFormat.Bitfields bitfield)
		{
			return delegate(object value, IPropertyBag propertyBag)
			{
				RoleAssignmentFlagsFormat.SetBits((ulong)((long)((ScopeType)value)), propertyBag, bitfield);
			};
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000C903E File Offset: 0x000C723E
		internal static object RecipientWriteScopeGetter(IPropertyBag propertyBag)
		{
			return (RecipientWriteScopeType)RoleAssignmentFlagsFormat.GetBits(propertyBag, RoleAssignmentFlagsFormat.Bitfields.RecipientWriteScope);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000C904E File Offset: 0x000C724E
		internal static void RecipientWriteScopeSetter(object value, IPropertyBag propertyBag)
		{
			RoleAssignmentFlagsFormat.SetBits((ulong)((long)((RecipientWriteScopeType)value)), propertyBag, RoleAssignmentFlagsFormat.Bitfields.RecipientWriteScope);
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000C9068 File Offset: 0x000C7268
		internal static QueryFilter RecipientWriteScopeFilterBuilder(SinglePropertyFilter filter)
		{
			return RoleAssignmentFlagsFormat.RoleAssignmentFlagsFilterBuilder<RecipientWriteScopeType>(filter, RoleAssignmentFlagsFormat.Bitfields.RecipientWriteScope, (object propertyValue) => (ulong)((long)((RecipientWriteScopeType)propertyValue)));
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000C908F File Offset: 0x000C728F
		internal static void ConfigWriteScopeSetter(object value, IPropertyBag propertyBag)
		{
			RoleAssignmentFlagsFormat.SetBits((ulong)((long)((ConfigWriteScopeType)value)), propertyBag, RoleAssignmentFlagsFormat.Bitfields.ConfigWriteScope);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000C909F File Offset: 0x000C729F
		internal static object ConfigWriteScopeGetter(IPropertyBag propertyBag)
		{
			return (ConfigWriteScopeType)RoleAssignmentFlagsFormat.GetBits(propertyBag, RoleAssignmentFlagsFormat.Bitfields.ConfigWriteScope);
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000C90B7 File Offset: 0x000C72B7
		internal static QueryFilter ConfigWriteScopeFilterBuilder(SinglePropertyFilter filter)
		{
			return RoleAssignmentFlagsFormat.RoleAssignmentFlagsFilterBuilder<ConfigWriteScopeType>(filter, RoleAssignmentFlagsFormat.Bitfields.ConfigWriteScope, (object propertyValue) => (ulong)((long)((ConfigWriteScopeType)propertyValue)));
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000C90DD File Offset: 0x000C72DD
		internal static object RoleAssignmentDelegationTypeGetter(IPropertyBag propertyBag)
		{
			return (RoleAssignmentDelegationType)RoleAssignmentFlagsFormat.GetBits(propertyBag, RoleAssignmentFlagsFormat.Bitfields.RoleAssignmentDelegationType);
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000C90EC File Offset: 0x000C72EC
		internal static void RoleAssignmentDelegationTypeSetter(object value, IPropertyBag propertyBag)
		{
			RoleAssignmentFlagsFormat.SetBits((ulong)((long)((RoleAssignmentDelegationType)value)), propertyBag, RoleAssignmentFlagsFormat.Bitfields.RoleAssignmentDelegationType);
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x000C9105 File Offset: 0x000C7305
		internal static QueryFilter RoleAssignmentDelegationFilterBuilder(SinglePropertyFilter filter)
		{
			return RoleAssignmentFlagsFormat.RoleAssignmentFlagsFilterBuilder<RoleAssignmentDelegationType>(filter, RoleAssignmentFlagsFormat.Bitfields.RoleAssignmentDelegationType, (object propertyValue) => (ulong)((long)((RoleAssignmentDelegationType)propertyValue)));
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000C912B File Offset: 0x000C732B
		internal static object EnabledGetter(IPropertyBag propertyBag)
		{
			return RoleAssignmentFlagsFormat.GetBits(propertyBag, RoleAssignmentFlagsFormat.Bitfields.IsEnabled) != 0UL;
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000C9141 File Offset: 0x000C7341
		internal static void EnabledSetter(object value, IPropertyBag propertyBag)
		{
			RoleAssignmentFlagsFormat.SetBits((ulong)(((bool)value) ? 1L : 0L), propertyBag, RoleAssignmentFlagsFormat.Bitfields.IsEnabled);
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000C9166 File Offset: 0x000C7366
		internal static QueryFilter RoleAssignmentEnabledFilterBuilder(SinglePropertyFilter filter)
		{
			return RoleAssignmentFlagsFormat.RoleAssignmentFlagsFilterBuilder<bool>(filter, RoleAssignmentFlagsFormat.Bitfields.IsEnabled, (object propertyValue) => ((bool)propertyValue) ? 1UL : 0UL);
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000C918C File Offset: 0x000C738C
		internal static object RoleAssigneeTypeGetter(IPropertyBag propertyBag)
		{
			return (RoleAssigneeType)RoleAssignmentFlagsFormat.GetBits(propertyBag, RoleAssignmentFlagsFormat.Bitfields.RoleAssigneeType);
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000C919B File Offset: 0x000C739B
		internal static void RoleAssigneeTypeSetter(object value, IPropertyBag propertyBag)
		{
			RoleAssignmentFlagsFormat.SetBits((ulong)((long)((RoleAssigneeType)value)), propertyBag, RoleAssignmentFlagsFormat.Bitfields.RoleAssigneeType);
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000C91B4 File Offset: 0x000C73B4
		internal static QueryFilter RoleAssigneeTypeFilterBuilder(SinglePropertyFilter filter)
		{
			return RoleAssignmentFlagsFormat.RoleAssignmentFlagsFilterBuilder<RoleAssigneeType>(filter, RoleAssignmentFlagsFormat.Bitfields.RoleAssigneeType, (object propertyValue) => (ulong)((long)((RoleAssigneeType)propertyValue)));
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000C91DC File Offset: 0x000C73DC
		internal static QueryFilter GetPartnerFilter(bool partnerMode)
		{
			if (RoleAssignmentFlagsFormat.partnerFilter == null || RoleAssignmentFlagsFormat.notPartnerFilter == null)
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.ConfigWriteScope, ConfigWriteScopeType.PartnerDelegatedTenantScope);
				RoleAssignmentFlagsFormat.partnerFilter = filter;
				RoleAssignmentFlagsFormat.notPartnerFilter = new NotFilter(filter);
			}
			if (!partnerMode)
			{
				return RoleAssignmentFlagsFormat.notPartnerFilter;
			}
			return RoleAssignmentFlagsFormat.partnerFilter;
		}

		// Token: 0x0400221D RID: 8733
		private static int[] sizes = new int[]
		{
			1,
			4,
			7,
			4,
			3,
			5,
			3,
			5,
			3,
			5,
			19,
			5
		};

		// Token: 0x0400221E RID: 8734
		private static ulong[] masks = new ulong[RoleAssignmentFlagsFormat.sizes.Length];

		// Token: 0x0400221F RID: 8735
		private static int[] shifts = new int[RoleAssignmentFlagsFormat.sizes.Length];

		// Token: 0x04002220 RID: 8736
		private static QueryFilter partnerFilter;

		// Token: 0x04002221 RID: 8737
		private static QueryFilter notPartnerFilter;

		// Token: 0x02000453 RID: 1107
		internal enum Bitfields
		{
			// Token: 0x04002228 RID: 8744
			IsEnabled,
			// Token: 0x04002229 RID: 8745
			RoleAssigneeType,
			// Token: 0x0400222A RID: 8746
			Reserved,
			// Token: 0x0400222B RID: 8747
			RoleAssignmentDelegationType,
			// Token: 0x0400222C RID: 8748
			Reserved2,
			// Token: 0x0400222D RID: 8749
			ConfigReadScope,
			// Token: 0x0400222E RID: 8750
			Reserved3,
			// Token: 0x0400222F RID: 8751
			ConfigWriteScope,
			// Token: 0x04002230 RID: 8752
			Reserved4,
			// Token: 0x04002231 RID: 8753
			RecipientReadScope,
			// Token: 0x04002232 RID: 8754
			Reserved5,
			// Token: 0x04002233 RID: 8755
			RecipientWriteScope
		}

		// Token: 0x02000454 RID: 1108
		// (Invoke) Token: 0x060031E0 RID: 12768
		private delegate ulong ConvertToUlongDelegate(object propertyValue);
	}
}
