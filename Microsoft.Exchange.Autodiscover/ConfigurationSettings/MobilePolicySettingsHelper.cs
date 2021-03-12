using System;
using Microsoft.Exchange.AirSync;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x02000039 RID: 57
	internal class MobilePolicySettingsHelper
	{
		// Token: 0x0600018C RID: 396 RVA: 0x000084F8 File Offset: 0x000066F8
		internal static string GetPolicyDataForUser(ADUser user, IBudget budget)
		{
			PolicyData policyData = MobilePolicySettingsHelper.GetPolicyData(user, budget);
			if (policyData != null)
			{
				bool flag;
				return ProvisionCommandPhaseOne.BuildEASProvisionDoc(121, out flag, policyData);
			}
			ExTraceGlobals.FrameworkTracer.TraceDebug<string>(0L, "[MobilePolicySettingsHelper.GetPolicyDataForUser()] No explicit or default policy found for user {0}", user.Alias);
			return null;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008534 File Offset: 0x00006734
		private static PolicyData GetPolicyData(ADUser user, IBudget budget)
		{
			PolicyData policyData = null;
			if (user.ActiveSyncMailboxPolicy != null)
			{
				policyData = MobilePolicySettingsHelper.GetPolicySetting(user, budget);
			}
			if (policyData == null)
			{
				policyData = MobilePolicySettingsHelper.GetDefaultPolicySetting(user, budget);
			}
			return policyData;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008560 File Offset: 0x00006760
		private static PolicyData GetPolicySetting(ADUser user, IBudget budget)
		{
			PolicyData result = null;
			try
			{
				result = MobilePolicySettingsHelper.LoadPolicySetting(MobilePolicySettingsHelper.CreateScopedADSession(user, budget), user.ActiveSyncMailboxPolicy);
			}
			catch (ADTransientException arg)
			{
				ExTraceGlobals.FrameworkTracer.TraceError<string, ADTransientException>(0L, "MobilePolicySettingsHelper.GetPolicySetting -- AD lookup returned transient error for user \"{0}\": {1}", user.Alias, arg);
			}
			return result;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000085B0 File Offset: 0x000067B0
		private static PolicyData LoadPolicySetting(IConfigurationSession scopedSession, ADObjectId policyId)
		{
			MobileMailboxPolicy mobileMailboxPolicy = scopedSession.Read<MobileMailboxPolicy>(policyId);
			if (mobileMailboxPolicy != null)
			{
				return MobilePolicySettingsHelper.CreatePolicyData(mobileMailboxPolicy);
			}
			return null;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000085D0 File Offset: 0x000067D0
		private static PolicyData GetDefaultPolicySetting(ADUser user, IBudget budget)
		{
			PolicyData result = null;
			try
			{
				result = MobilePolicySettingsHelper.LoadDefaultPolicySetting(MobilePolicySettingsHelper.CreateScopedADSession(user, budget), user);
			}
			catch (ADTransientException arg)
			{
				ExTraceGlobals.FrameworkTracer.TraceError<string, ADTransientException>(0L, "MobilePolicySettingsHelper.GetDefaultPolicySetting -- AD lookup returned transient error for user \"{0}\": {1}", user.Alias, arg);
			}
			return result;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000861C File Offset: 0x0000681C
		private static PolicyData LoadDefaultPolicySetting(IConfigurationSession scopedSession, ADUser user)
		{
			MobileMailboxPolicy[] array = scopedSession.Find<MobileMailboxPolicy>(scopedSession.GetOrgContainerId(), QueryScope.SubTree, MobilePolicySettingsHelper.mobileMailboxPolicyFilter, MobilePolicySettingsHelper.mobileMailboxPolicySortBy, 3);
			if (array != null && array.Length > 0)
			{
				return MobilePolicySettingsHelper.CreatePolicyData(array[0]);
			}
			ExTraceGlobals.FrameworkTracer.TraceDebug<OrganizationId>(0L, "[MobilePolicySettingsHelper.LoadPolicySetting()] No default policy found for organization {0}", user.OrganizationId);
			return null;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000866C File Offset: 0x0000686C
		private static PolicyData CreatePolicyData(MobileMailboxPolicy mobileMaiboxPolicy)
		{
			return new PolicyData(mobileMaiboxPolicy, false);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008678 File Offset: 0x00006878
		private static IConfigurationSession CreateScopedADSession(ADUser user, IBudget budget)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(user.OrganizationId), 224, "CreateScopedADSession", "f:\\15.00.1497\\sources\\dev\\autodisc\\src\\ConfigurationSettings\\MobilePolicySettingsHelper.cs");
			tenantOrTopologyConfigurationSession.SessionSettings.AccountingObject = budget;
			return tenantOrTopologyConfigurationSession;
		}

		// Token: 0x04000195 RID: 405
		private const bool IrmEnabledSetting = false;

		// Token: 0x04000196 RID: 406
		private const int AirSyncProtocolVersion = 121;

		// Token: 0x04000197 RID: 407
		private static readonly QueryFilter mobileMailboxPolicyFilter = new BitMaskAndFilter(MobileMailboxPolicySchema.MobileFlags, 4096UL);

		// Token: 0x04000198 RID: 408
		private static readonly SortBy mobileMailboxPolicySortBy = new SortBy(ADObjectSchema.WhenChanged, SortOrder.Descending);
	}
}
