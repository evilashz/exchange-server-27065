using System;

namespace Microsoft.Exchange.Server.Storage.MapiDisp
{
	// Token: 0x0200001A RID: 26
	[Flags]
	internal enum ConnectFlags
	{
		// Token: 0x0400018D RID: 397
		None = 0,
		// Token: 0x0400018E RID: 398
		UseAdminPrivilege = 1,
		// Token: 0x0400018F RID: 399
		UseDelegatedAuthPrivilege = 256,
		// Token: 0x04000190 RID: 400
		UseTransportPrivilege = 1024
	}
}
