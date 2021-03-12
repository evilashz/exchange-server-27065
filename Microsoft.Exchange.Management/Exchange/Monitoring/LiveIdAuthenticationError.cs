using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000026 RID: 38
	public enum LiveIdAuthenticationError
	{
		// Token: 0x040000B9 RID: 185
		None,
		// Token: 0x040000BA RID: 186
		OverThreshold,
		// Token: 0x040000BB RID: 187
		CommunicationException,
		// Token: 0x040000BC RID: 188
		InvalidOperationException,
		// Token: 0x040000BD RID: 189
		LoginFailure,
		// Token: 0x040000BE RID: 190
		OtherException = 9
	}
}
