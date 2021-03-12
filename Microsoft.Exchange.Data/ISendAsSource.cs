using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000228 RID: 552
	public interface ISendAsSource
	{
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600133D RID: 4925
		Guid SourceGuid { get; }

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600133E RID: 4926
		SmtpAddress UserEmailAddress { get; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600133F RID: 4927
		bool IsEnabled { get; }
	}
}
