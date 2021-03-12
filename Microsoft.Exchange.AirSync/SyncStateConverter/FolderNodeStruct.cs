using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.AirSync.SyncStateConverter
{
	// Token: 0x02000275 RID: 629
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct FolderNodeStruct
	{
		// Token: 0x04000E47 RID: 3655
		public IntPtr Next;

		// Token: 0x04000E48 RID: 3656
		public string ServerID;

		// Token: 0x04000E49 RID: 3657
		public string DisplayName;

		// Token: 0x04000E4A RID: 3658
		public string ParentID;

		// Token: 0x04000E4B RID: 3659
		public string ContentClass;

		// Token: 0x04000E4C RID: 3660
		public string FolderURL;
	}
}
