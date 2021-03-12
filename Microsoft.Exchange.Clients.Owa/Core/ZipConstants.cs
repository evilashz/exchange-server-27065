using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000298 RID: 664
	internal static class ZipConstants
	{
		// Token: 0x040012A2 RID: 4770
		public const uint EndOfCentralDirectorySignature = 101010256U;

		// Token: 0x040012A3 RID: 4771
		public const int ZipEntrySignature = 67324752;

		// Token: 0x040012A4 RID: 4772
		public const int ZipEntryDataDescriptorSignature = 134695760;

		// Token: 0x040012A5 RID: 4773
		public const int ZipDirEntrySignature = 33639248;
	}
}
