using System;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006BD RID: 1725
	[Flags]
	internal enum UriFlags
	{
		// Token: 0x040025FA RID: 9722
		Sharepoint = 1,
		// Token: 0x040025FB RID: 9723
		Unc = 2,
		// Token: 0x040025FC RID: 9724
		DocumentLibrary = 4,
		// Token: 0x040025FD RID: 9725
		List = 8,
		// Token: 0x040025FE RID: 9726
		Document = 16,
		// Token: 0x040025FF RID: 9727
		Folder = 32,
		// Token: 0x04002600 RID: 9728
		Other = 64,
		// Token: 0x04002601 RID: 9729
		SharepointList = 9,
		// Token: 0x04002602 RID: 9730
		SharepointDocumentLibrary = 5,
		// Token: 0x04002603 RID: 9731
		SharepointDocument = 17,
		// Token: 0x04002604 RID: 9732
		SharepointFolder = 33,
		// Token: 0x04002605 RID: 9733
		UncDocumentLibrary = 6,
		// Token: 0x04002606 RID: 9734
		UncDocument = 18,
		// Token: 0x04002607 RID: 9735
		UncFolder = 34
	}
}
