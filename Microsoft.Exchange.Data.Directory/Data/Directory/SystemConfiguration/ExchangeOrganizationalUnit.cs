using System;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000359 RID: 857
	[Serializable]
	public class ExchangeOrganizationalUnit : ADConfigurationObject, IProvisioningCacheInvalidation
	{
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06002790 RID: 10128 RVA: 0x000A6A94 File Offset: 0x000A4C94
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExchangeOrganizationalUnit.schema;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x000A6A9B File Offset: 0x000A4C9B
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExchangeOrganizationalUnit.mostDerivedClass;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x000A6AA4 File Offset: 0x000A4CA4
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADOrganizationalUnit.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADContainer.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADBuiltinDomain.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, MesoContainer.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADDomain.MostDerivedClass)
				});
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002794 RID: 10132 RVA: 0x000A6B25 File Offset: 0x000A4D25
		// (set) Token: 0x06002795 RID: 10133 RVA: 0x000A6B37 File Offset: 0x000A4D37
		internal ADObjectId HierarchicalAddressBookRoot
		{
			get
			{
				return (ADObjectId)this[ExchangeOrganizationalUnitSchema.HABRootDepartmentLink];
			}
			set
			{
				this[ExchangeOrganizationalUnitSchema.HABRootDepartmentLink] = value;
			}
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000A6B45 File Offset: 0x000A4D45
		internal bool ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			orgId = null;
			keys = null;
			if (base.ObjectState == ObjectState.Deleted)
			{
				keys = new Guid[1];
				keys[0] = CannedProvisioningCacheKeys.OrganizationalUnitDictionary;
				return true;
			}
			return false;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000A6B73 File Offset: 0x000A4D73
		bool IProvisioningCacheInvalidation.ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			return this.ShouldInvalidProvisioningCache(out orgId, out keys);
		}

		// Token: 0x04001817 RID: 6167
		private static ExchangeOrganizationalUnitSchema schema = ObjectSchema.GetInstance<ExchangeOrganizationalUnitSchema>();

		// Token: 0x04001818 RID: 6168
		private static string mostDerivedClass = "organizationalUnit";
	}
}
