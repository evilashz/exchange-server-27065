using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000246 RID: 582
	internal abstract class WSManBudgetManagerBase<T> : BudgetManager where T : PowerShellBudget
	{
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0004CD07 File Offset: 0x0004AF07
		protected override TimeSpan BudgetTimeout
		{
			get
			{
				return WSManBudgetManagerBase<T>.budgetTimeout;
			}
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0004CD10 File Offset: 0x0004AF10
		internal CostHandle StartRunspace(AuthZPluginUserToken userToken)
		{
			if (!this.ShouldThrottling(userToken))
			{
				return null;
			}
			CostHandle result;
			lock (base.InstanceLock)
			{
				result = this.StartRunspaceImpl(userToken);
			}
			return result;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0004CD60 File Offset: 0x0004AF60
		internal CostHandle StartCmdlet(AuthZPluginUserToken userToken)
		{
			if (!this.ShouldThrottling(userToken))
			{
				return null;
			}
			lock (base.InstanceLock)
			{
				IPowerShellBudget budget = base.GetBudget(userToken, true, true);
				if (budget != null)
				{
					ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string>(0L, "Start budget tracking for Cmdlet, key {0}", this.CreateKey(userToken));
					return budget.StartCmdlet(null);
				}
				ExTraceGlobals.PublicPluginAPITracer.TraceError<string>(0L, "Try to start budget tracking for Cmdlet, key {0} But the budget doesn't exist.", this.CreateKey(userToken));
			}
			return null;
		}

		// Token: 0x040005F0 RID: 1520
		private static readonly TimeSpan budgetTimeout = TimeSpan.FromMinutes(10.0);
	}
}
