using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009B8 RID: 2488
	internal class DelegatedPrincipalBudgetKey : LookupBudgetKey
	{
		// Token: 0x060072B7 RID: 29367 RVA: 0x0017B9E0 File Offset: 0x00179BE0
		public DelegatedPrincipalBudgetKey(DelegatedPrincipal principal, BudgetType budgetType) : base(budgetType, false)
		{
			this.cachedToString = string.Format("Delegated~{0}~{1}~{2}", principal.DelegatedOrganization, principal.UserId, budgetType);
			this.principal = principal;
			this.cachedHashCode = (base.BudgetType.GetHashCode() ^ this.principal.DelegatedOrganization.GetHashCode() ^ this.principal.UserId.GetHashCode());
		}

		// Token: 0x060072B8 RID: 29368 RVA: 0x0017BA58 File Offset: 0x00179C58
		public override bool Equals(object obj)
		{
			DelegatedPrincipalBudgetKey delegatedPrincipalBudgetKey = obj as DelegatedPrincipalBudgetKey;
			return !(delegatedPrincipalBudgetKey == null) && (delegatedPrincipalBudgetKey.BudgetType == base.BudgetType && delegatedPrincipalBudgetKey.principal.DelegatedOrganization == this.principal.DelegatedOrganization) && delegatedPrincipalBudgetKey.principal.UserId == this.principal.UserId;
		}

		// Token: 0x060072B9 RID: 29369 RVA: 0x0017BC24 File Offset: 0x00179E24
		internal override IThrottlingPolicy InternalLookup()
		{
			ExchangeConfigurationUnit cu = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				PartitionId partitionIdByAcceptedDomainName = ADAccountPartitionLocator.GetPartitionIdByAcceptedDomainName(this.principal.DelegatedOrganization);
				if (partitionIdByAcceptedDomainName != null)
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionIdByAcceptedDomainName);
					ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 86, "InternalLookup", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\throttling\\DelegatedPrincipalBudgetKey.cs");
					tenantConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
					cu = tenantConfigurationSession.GetExchangeConfigurationUnitByNameOrAcceptedDomain(this.principal.DelegatedOrganization);
				}
				if (cu == null)
				{
					throw new CannotResolveTenantNameException(DirectoryStrings.CannotResolveTenantNameByAcceptedDomain(this.principal.DelegatedOrganization));
				}
			});
			if (!adoperationResult.Succeeded)
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceError<string, string, Exception>((long)this.GetHashCode(), "[DelegatedPrincipalBudgetKey.Lookup] Failed to find Delegated Organization: '{0}' for user '{1}', using global throttling policy.  Exception: '{2}'", this.principal.DelegatedOrganization, this.principal.UserId, adoperationResult.Exception);
				return ThrottlingPolicyCache.Singleton.GetGlobalThrottlingPolicy();
			}
			OrganizationId orgId = cu.OrganizationId;
			if (cu.SupportedSharedConfigurations.Count > 0)
			{
				SharedConfiguration sharedConfig = null;
				adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					sharedConfig = SharedConfiguration.GetSharedConfiguration(cu.OrganizationId);
				});
				if (!adoperationResult.Succeeded)
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceError<OrganizationId, Exception>((long)this.GetHashCode(), "[DelegatedPrincipalBudgetKey.Lookup] Failed to get Shared Configuration Organization: '{0}', using global throttling policy.  Exception: '{1}'", cu.OrganizationId, adoperationResult.Exception);
					return ThrottlingPolicyCache.Singleton.GetGlobalThrottlingPolicy();
				}
				if (sharedConfig != null)
				{
					orgId = sharedConfig.SharedConfigId;
				}
			}
			return base.ADRetryLookup(delegate
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(orgId);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 149, "InternalLookup", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\throttling\\DelegatedPrincipalBudgetKey.cs");
				tenantOrTopologyConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
				ThrottlingPolicy organizationThrottlingPolicy = tenantOrTopologyConfigurationSession.GetOrganizationThrottlingPolicy(orgId, false);
				if (organizationThrottlingPolicy == null)
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceError<string, string>((long)this.GetHashCode(), "[DelegatedPrincipalBudgetKey.Lookup] Failed to find Default Throttling Policy for '{0}\\{1}', using global throttling policy", this.principal.DelegatedOrganization, this.principal.UserId);
					return ThrottlingPolicyCache.Singleton.GetGlobalThrottlingPolicy();
				}
				return organizationThrottlingPolicy.GetEffectiveThrottlingPolicy(true);
			});
		}

		// Token: 0x060072BA RID: 29370 RVA: 0x0017BD4B File Offset: 0x00179F4B
		public override string ToString()
		{
			return this.cachedToString;
		}

		// Token: 0x060072BB RID: 29371 RVA: 0x0017BD53 File Offset: 0x00179F53
		public override int GetHashCode()
		{
			return this.cachedHashCode;
		}

		// Token: 0x04004A4C RID: 19020
		private const string ToStringFormat = "Delegated~{0}~{1}~{2}";

		// Token: 0x04004A4D RID: 19021
		private readonly string cachedToString;

		// Token: 0x04004A4E RID: 19022
		private readonly int cachedHashCode;

		// Token: 0x04004A4F RID: 19023
		private DelegatedPrincipal principal;
	}
}
