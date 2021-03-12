using System;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x0200005C RID: 92
	[Flags]
	public enum IntegrityCheckQueryFlags : uint
	{
		// Token: 0x040001CC RID: 460
		None = 0U,
		// Token: 0x040001CD RID: 461
		OnlineIntegrityCheckAssistant = 1U,
		// Token: 0x040001CE RID: 462
		ScheduledIntegrityCheckAssistant = 2U,
		// Token: 0x040001CF RID: 463
		QueryJob = 4U
	}
}
