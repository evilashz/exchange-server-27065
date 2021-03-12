using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000247 RID: 583
	internal class WSManBudgetManager : WSManBudgetManagerBase<WSManBudget>
	{
		// Token: 0x06001495 RID: 5269 RVA: 0x0004CE05 File Offset: 0x0004B005
		private WSManBudgetManager()
		{
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x0004CE0D File Offset: 0x0004B00D
		internal static WSManBudgetManager Instance
		{
			get
			{
				return WSManBudgetManager.instance;
			}
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0004CE14 File Offset: 0x0004B014
		protected override string CreateRelatedBudgetKey(AuthZPluginUserToken userToken)
		{
			return WSManTenantBudgetManager.CreateKeyForTenantBudget(userToken);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0004CE1C File Offset: 0x0004B01C
		protected override void CorrectRelatedBudgetWhenLeak(string key, int delta)
		{
			WSManTenantBudgetManager.Instance.CorrectRunspacesLeakPassively(key, delta);
		}

		// Token: 0x040005F1 RID: 1521
		private static readonly WSManBudgetManager instance = new WSManBudgetManager();
	}
}
