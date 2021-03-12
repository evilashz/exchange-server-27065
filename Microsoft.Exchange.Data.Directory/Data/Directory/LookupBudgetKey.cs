using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009B7 RID: 2487
	internal abstract class LookupBudgetKey : BudgetKey
	{
		// Token: 0x060072B2 RID: 29362 RVA: 0x0017B906 File Offset: 0x00179B06
		public LookupBudgetKey(BudgetType budgetType, bool isServiceAccount) : base(budgetType, isServiceAccount)
		{
		}

		// Token: 0x060072B3 RID: 29363 RVA: 0x0017B910 File Offset: 0x00179B10
		internal IThrottlingPolicy Lookup()
		{
			if (BudgetKey.LookupPolicyForTest != null)
			{
				return BudgetKey.LookupPolicyForTest(this);
			}
			return this.InternalLookup();
		}

		// Token: 0x060072B4 RID: 29364
		internal abstract IThrottlingPolicy InternalLookup();

		// Token: 0x060072B5 RID: 29365 RVA: 0x0017B948 File Offset: 0x00179B48
		protected internal IThrottlingPolicy ADRetryLookup(Func<IThrottlingPolicy> policyLookup)
		{
			IThrottlingPolicy policy = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				policy = policyLookup();
			});
			if (!adoperationResult.Succeeded)
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceError<Exception>((long)this.GetHashCode(), "[LookupBudgetKey.ADRetryLookup] Failed to lookup throttling policy.  Failed with exception '{0}'", adoperationResult.Exception);
				return ThrottlingPolicyCache.Singleton.GetGlobalThrottlingPolicy();
			}
			return policy;
		}

		// Token: 0x060072B6 RID: 29366 RVA: 0x0017B9B0 File Offset: 0x00179BB0
		protected IThrottlingPolicy GetPolicyForRecipient(MiniRecipient recipient)
		{
			if (recipient == null)
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceDebug<LookupBudgetKey>((long)this.GetHashCode(), "[LookupBudgetKey.GetPolicyForRecipient] Passed identifier did not resolve to an AD account: '{0}'.  Using global policy.", this);
				return ThrottlingPolicyCache.Singleton.GetGlobalThrottlingPolicy();
			}
			return recipient.ReadThrottlingPolicy();
		}
	}
}
