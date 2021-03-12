using System;

namespace Microsoft.Forefront.ActiveDirectoryConnector
{
	// Token: 0x02000002 RID: 2
	public struct FilteringSettings
	{
		// Token: 0x04000001 RID: 1
		public int MalwareFilteringUpdateFrequency;

		// Token: 0x04000002 RID: 2
		public int MalwareFilteringUpdateTimeout;

		// Token: 0x04000003 RID: 3
		public string MalwareFilteringPrimaryUpdatePath;

		// Token: 0x04000004 RID: 4
		public string MalwareFilteringSecondaryUpdatePath;
	}
}
