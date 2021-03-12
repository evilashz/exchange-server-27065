using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000B7 RID: 183
	internal class OrganizationLookup : PropertyCache, IPropertyLookup
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x000197B5 File Offset: 0x000179B5
		public OrganizationLookup(Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]> getOrganizationProperties, QueryFilter scopeFilter) : base(getOrganizationProperties, OrganizationLookup.OrganizationProperties)
		{
			this.scopeFilter = scopeFilter;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000197CC File Offset: 0x000179CC
		public override IEnumerable<ADObjectId> GetObjectIds(PropertyBag propertyBag)
		{
			return new ADObjectId[]
			{
				ProcessorHelper.GetTenantOU(propertyBag)
			};
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000197EA File Offset: 0x000179EA
		protected override bool MeetsAdditionalCriteria(ADRawEntry entry)
		{
			return OpathFilterEvaluator.FilterMatches(this.scopeFilter, entry);
		}

		// Token: 0x040002DD RID: 733
		private static readonly ADPropertyDefinition[] OrganizationProperties = new ADPropertyDefinition[]
		{
			ExchangeConfigurationUnitSchema.TargetForest,
			ExchangeConfigurationUnitSchema.RelocationSourceForestRaw,
			ExchangeConfigurationUnitSchema.WhenOrganizationStatusSet,
			ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId,
			ExchangeConfigurationUnitSchema.OrganizationStatus,
			OrganizationSchema.ExcludedFromBackSync,
			ExchangeConfigurationUnitSchema.ProgramId,
			SyncObjectSchema.Deleted,
			OrganizationSchema.IsDirSyncRunning,
			OrganizationSchema.DirSyncStatus,
			TenantRelocationRequestSchema.TenantRelocationCompletionTargetVector
		};

		// Token: 0x040002DE RID: 734
		private readonly QueryFilter scopeFilter;
	}
}
