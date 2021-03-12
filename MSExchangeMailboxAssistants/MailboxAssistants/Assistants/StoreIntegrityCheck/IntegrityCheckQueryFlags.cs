using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.StoreIntegrityCheck
{
	// Token: 0x020001D2 RID: 466
	[Flags]
	public enum IntegrityCheckQueryFlags : uint
	{
		// Token: 0x04000B0D RID: 2829
		OnlineIntegrityCheckAssistant = 1U,
		// Token: 0x04000B0E RID: 2830
		ScheduledIntegrityCheckAssistant = 2U
	}
}
