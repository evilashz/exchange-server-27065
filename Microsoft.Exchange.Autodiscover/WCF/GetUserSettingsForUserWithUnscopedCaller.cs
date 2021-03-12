using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000080 RID: 128
	internal sealed class GetUserSettingsForUserWithUnscopedCaller : GetUserSettingsCommandBase
	{
		// Token: 0x06000366 RID: 870 RVA: 0x000159F1 File Offset: 0x00013BF1
		internal GetUserSettingsForUserWithUnscopedCaller(SecurityIdentifier callerSid, CallContext callContext) : base(callContext)
		{
			this.callerSid = callerSid;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00015A01 File Offset: 0x00013C01
		protected override IStandardBudget AcquireBudget()
		{
			return StandardBudget.Acquire(this.callerSid, BudgetType.Ews, Common.GetSessionSettingsForCallerScope());
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00015A14 File Offset: 0x00013C14
		protected override void AddToQueryList(UserResultMapping userResultMapping, IBudget budget)
		{
			base.AddToADQueryList(userResultMapping, OrganizationId.ForestWideOrgId, null, budget);
		}

		// Token: 0x0400030F RID: 783
		private SecurityIdentifier callerSid;
	}
}
