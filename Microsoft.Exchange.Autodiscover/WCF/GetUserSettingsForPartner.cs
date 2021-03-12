using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200007D RID: 125
	internal sealed class GetUserSettingsForPartner : GetUserSettingsCommandBase
	{
		// Token: 0x06000359 RID: 857 RVA: 0x0001565E File Offset: 0x0001385E
		internal GetUserSettingsForPartner(SecurityIdentifier callerSid, CallContext callContext) : base(callContext)
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "GetUserSettingsForPartner constructor called for '{0}'.", callerSid);
			this.callerSid = callerSid;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00015685 File Offset: 0x00013885
		protected override IStandardBudget AcquireBudget()
		{
			return StandardBudget.Acquire(this.callerSid, BudgetType.Ews, Common.GetSessionSettingsForCallerScope());
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00015698 File Offset: 0x00013898
		protected override void AddToQueryList(UserResultMapping userResultMapping, IBudget budget)
		{
			OrganizationId organizationId;
			if (base.TryGetOrganizationId(userResultMapping, out organizationId))
			{
				base.AddToADQueryList(userResultMapping, organizationId, null, budget);
				return;
			}
			this.AddToMServeQueryList(userResultMapping);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000156C4 File Offset: 0x000138C4
		private void AddToMServeQueryList(UserResultMapping userResultMapping)
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<string>((long)this.GetHashCode(), "AddToMServeQueryList() called for '{0}'.", userResultMapping.Mailbox);
			if (this.mServeQueryList == null)
			{
				this.mServeQueryList = new MServeQueryList();
				this.queryLists.Add(this.mServeQueryList);
			}
			this.mServeQueryList.Add(userResultMapping);
		}

		// Token: 0x04000309 RID: 777
		private SecurityIdentifier callerSid;

		// Token: 0x0400030A RID: 778
		private MServeQueryList mServeQueryList;
	}
}
