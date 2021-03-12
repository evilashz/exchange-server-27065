using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200006E RID: 110
	internal sealed class MailboxGovernor : ThrottleGovernor
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0000FFEA File Offset: 0x0000E1EA
		public MailboxGovernor(Governor parentGovernor, Throttle throttle) : base(parentGovernor, throttle)
		{
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000FFF4 File Offset: 0x0000E1F4
		protected override bool IsFailureRelevant(AITransientException exception)
		{
			return exception is TransientMailboxException;
		}
	}
}
