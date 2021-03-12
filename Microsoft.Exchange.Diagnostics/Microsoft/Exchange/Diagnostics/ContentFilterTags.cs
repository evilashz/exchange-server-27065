using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000281 RID: 641
	public struct ContentFilterTags
	{
		// Token: 0x0400112C RID: 4396
		public const int Initialization = 0;

		// Token: 0x0400112D RID: 4397
		public const int ScanMessage = 1;

		// Token: 0x0400112E RID: 4398
		public const int BypassedSenders = 2;

		// Token: 0x0400112F RID: 4399
		public const int ComInterop = 3;

		// Token: 0x04001130 RID: 4400
		public static Guid guid = new Guid("A1FD20D2-933F-4505-A0C4-C1FBFFCB9E62");
	}
}
