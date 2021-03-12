using System;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000248 RID: 584
	internal class WSManTenantBudgetManager : WSManBudgetManagerBase<WSManTenantBudget>
	{
		// Token: 0x0600149A RID: 5274 RVA: 0x0004CE36 File Offset: 0x0004B036
		private WSManTenantBudgetManager()
		{
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0004CE3E File Offset: 0x0004B03E
		internal static WSManTenantBudgetManager Instance
		{
			get
			{
				return WSManTenantBudgetManager.instance;
			}
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0004CE45 File Offset: 0x0004B045
		internal static string CreateKeyForTenantBudget(AuthZPluginUserToken userToken)
		{
			return userToken.OrgIdInString;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0004CE4D File Offset: 0x0004B04D
		protected override string CreateKey(AuthZPluginUserToken userToken)
		{
			return WSManTenantBudgetManager.CreateKeyForTenantBudget(userToken);
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0004CE55 File Offset: 0x0004B055
		protected override IPowerShellBudget CreateBudget(AuthZPluginUserToken userToken)
		{
			return userToken.CreateBudget(BudgetType.WSManTenant);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0004CE60 File Offset: 0x0004B060
		protected override void UpdateBudgetsPerfCounter(int size)
		{
			RemotePowershellPerformanceCountersInstance remotePowershellPerfCounter = ExchangeAuthorizationPlugin.RemotePowershellPerfCounter;
			if (remotePowershellPerfCounter != null)
			{
				remotePowershellPerfCounter.PerTenantBudgetsDicSize.RawValue = (long)size;
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0004CE84 File Offset: 0x0004B084
		protected override void UpdateKeyToRemoveBudgetsPerfCounter(int size)
		{
			RemotePowershellPerformanceCountersInstance remotePowershellPerfCounter = ExchangeAuthorizationPlugin.RemotePowershellPerfCounter;
			if (remotePowershellPerfCounter != null)
			{
				remotePowershellPerfCounter.PerTenantKeyToRemoveBudgetsCacheSize.RawValue = (long)size;
			}
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0004CEA7 File Offset: 0x0004B0A7
		protected override void UpdateConnectionLeakPerfCounter(int leakedConnection)
		{
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0004CEA9 File Offset: 0x0004B0A9
		protected override bool ShouldThrottling(AuthZPluginUserToken userToken)
		{
			return userToken.OrgId != null && !userToken.OrgId.Equals(OrganizationId.ForestWideOrgId);
		}

		// Token: 0x040005F2 RID: 1522
		private static readonly WSManTenantBudgetManager instance = new WSManTenantBudgetManager();
	}
}
