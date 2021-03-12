using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000059 RID: 89
	[Cmdlet("Disable", "InboxRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableInboxRule : EnableDisableInboxRuleBase
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x00018E61 File Offset: 0x00017061
		public DisableInboxRule() : base(false)
		{
		}
	}
}
