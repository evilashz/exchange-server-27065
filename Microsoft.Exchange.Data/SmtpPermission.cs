using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000A0 RID: 160
	[Flags]
	internal enum SmtpPermission
	{
		// Token: 0x0400024A RID: 586
		None = 0,
		// Token: 0x0400024B RID: 587
		Submit = 1,
		// Token: 0x0400024C RID: 588
		Relay = 2,
		// Token: 0x0400024D RID: 589
		SendAs = 4,
		// Token: 0x0400024E RID: 590
		Default = 1
	}
}
