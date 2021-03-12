using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000023 RID: 35
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct VERSION_INFORMATION_DETAILS
	{
		// Token: 0x04000038 RID: 56
		public uint MajorVersion;

		// Token: 0x04000039 RID: 57
		public uint MinorVersion;

		// Token: 0x0400003A RID: 58
		public uint BuildNumber;

		// Token: 0x0400003B RID: 59
		public uint RevisionNumber;
	}
}
