using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200005A RID: 90
	[Cmdlet("Enable", "InboxRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class EnableInboxRule : EnableDisableInboxRuleBase
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x00018E6A File Offset: 0x0001706A
		public EnableInboxRule() : base(true)
		{
		}
	}
}
