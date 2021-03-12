using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200050F RID: 1295
	[Serializable]
	public class NtdsDsa : ADNonExchangeObject
	{
		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06003937 RID: 14647 RVA: 0x000DCF13 File Offset: 0x000DB113
		internal override ADObjectSchema Schema
		{
			get
			{
				return NtdsDsa.schema;
			}
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06003938 RID: 14648 RVA: 0x000DCF1A File Offset: 0x000DB11A
		internal override string MostDerivedObjectClass
		{
			get
			{
				return NtdsDsa.mostDerivedClass;
			}
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06003939 RID: 14649 RVA: 0x000DCF24 File Offset: 0x000DB124
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, NtdsDsa.mostDerivedClassForRodc)
				});
			}
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000DCF68 File Offset: 0x000DB168
		internal static object DsIsRodcGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.ObjectCategory];
			return adobjectId.Rdn.Equals(NtdsDsa.roName);
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000DCF9C File Offset: 0x000DB19C
		internal static QueryFilter DsIsRodcFilterBuilder(SinglePropertyFilter filter)
		{
			Database.InternalAssertComparisonFilter(filter, NtdsDsaSchema.DsIsRodc);
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, ADObjectSchema.ObjectCategory, ((bool)comparisonFilter.PropertyValue) ? NtdsDsa.mostDerivedClassForRodc : NtdsDsa.mostDerivedClass);
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x0600393C RID: 14652 RVA: 0x000DCFE4 File Offset: 0x000DB1E4
		public bool DsIsRodc
		{
			get
			{
				return (bool)this[NtdsDsaSchema.DsIsRodc];
			}
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x0600393D RID: 14653 RVA: 0x000DCFF6 File Offset: 0x000DB1F6
		public Guid InvocationId
		{
			get
			{
				return (Guid)this[NtdsDsaSchema.InvocationId];
			}
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x0600393E RID: 14654 RVA: 0x000DD008 File Offset: 0x000DB208
		internal NtdsdsaOptions Options
		{
			get
			{
				return (NtdsdsaOptions)this[NtdsDsaSchema.Options];
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x0600393F RID: 14655 RVA: 0x000DD01A File Offset: 0x000DB21A
		internal MultiValuedProperty<ADObjectId> MasterNCs
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[NtdsDsaSchema.MasterNCs];
			}
		}

		// Token: 0x0400271D RID: 10013
		private static NtdsDsaSchema schema = ObjectSchema.GetInstance<NtdsDsaSchema>();

		// Token: 0x0400271E RID: 10014
		private static string mostDerivedClass = "ntdsDsa";

		// Token: 0x0400271F RID: 10015
		private static string mostDerivedClassForRodc = "ntdsDsaRo";

		// Token: 0x04002720 RID: 10016
		private static AdName roName = new AdName("CN", "NTDS-DSA-RO");
	}
}
