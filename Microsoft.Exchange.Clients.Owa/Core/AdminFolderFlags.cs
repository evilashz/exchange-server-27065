using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000B8 RID: 184
	[Flags]
	public enum AdminFolderFlags
	{
		// Token: 0x040004BC RID: 1212
		Provisioned = 1,
		// Token: 0x040004BD RID: 1213
		Protected = 2,
		// Token: 0x040004BE RID: 1214
		MustDisplayComment = 4,
		// Token: 0x040004BF RID: 1215
		Quota = 8,
		// Token: 0x040004C0 RID: 1216
		ELCRoot = 16
	}
}
