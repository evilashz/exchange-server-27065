using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.StoreIntegrityCheck
{
	// Token: 0x020001D3 RID: 467
	[Flags]
	public enum IntegrityCheckExecutionFlags : uint
	{
		// Token: 0x04000B10 RID: 2832
		OnlineIntegrityCheckAssistant = 1U,
		// Token: 0x04000B11 RID: 2833
		ScheduledIntegrityCheckAssistant = 2U
	}
}
