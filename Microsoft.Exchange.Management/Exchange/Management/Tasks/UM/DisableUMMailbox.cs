using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D0A RID: 3338
	[Cmdlet("Disable", "UMMailbox", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableUMMailbox : DisableUMMailboxBase<MailboxIdParameter>
	{
	}
}
