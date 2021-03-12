using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200005E RID: 94
	internal static class ZipConstants
	{
		// Token: 0x04000165 RID: 357
		public const uint EndOfCentralDirectorySignature = 101010256U;

		// Token: 0x04000166 RID: 358
		public const int ZipEntrySignature = 67324752;

		// Token: 0x04000167 RID: 359
		public const int ZipEntryDataDescriptorSignature = 134695760;

		// Token: 0x04000168 RID: 360
		public const int ZipDirEntrySignature = 33639248;
	}
}
