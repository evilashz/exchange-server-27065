using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D0B RID: 3339
	[Cmdlet("Disable", "UMMailboxPlan", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableUMMailboxPlan : DisableUMMailboxBase<MailboxPlanIdParameter>
	{
	}
}
