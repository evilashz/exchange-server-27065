using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x0200029E RID: 670
	[Flags]
	internal enum RequiredProperty
	{
		// Token: 0x04000CA1 RID: 3233
		None = 0,
		// Token: 0x04000CA2 RID: 3234
		Server = 1,
		// Token: 0x04000CA3 RID: 3235
		Domain = 2,
		// Token: 0x04000CA4 RID: 3236
		Target = 4,
		// Token: 0x04000CA5 RID: 3237
		Data = 8,
		// Token: 0x04000CA6 RID: 3238
		Exception = 16
	}
}
