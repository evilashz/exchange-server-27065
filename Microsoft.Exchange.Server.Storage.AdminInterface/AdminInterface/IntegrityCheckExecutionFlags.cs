using System;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x0200005D RID: 93
	[Flags]
	public enum IntegrityCheckExecutionFlags : uint
	{
		// Token: 0x040001D1 RID: 465
		None = 0U,
		// Token: 0x040001D2 RID: 466
		OnlineIntegrityCheckAssistant = 1U,
		// Token: 0x040001D3 RID: 467
		ScheduledIntegrityCheckAssistant = 2U
	}
}
