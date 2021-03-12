using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001D9 RID: 473
	internal interface IAggregateSession
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600135D RID: 4957
		// (set) Token: 0x0600135E RID: 4958
		MbxReadMode MbxReadMode { get; set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600135F RID: 4959
		// (set) Token: 0x06001360 RID: 4960
		BackendWriteMode BackendWriteMode { get; set; }
	}
}
