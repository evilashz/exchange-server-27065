using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D10 RID: 3344
	[Cmdlet("Enable", "UMMailbox", SupportsShouldProcess = true)]
	public sealed class EnableUMMailbox : EnableUMMailboxBase<MailboxIdParameter>
	{
	}
}
