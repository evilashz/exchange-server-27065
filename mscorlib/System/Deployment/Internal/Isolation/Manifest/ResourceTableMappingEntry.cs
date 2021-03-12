using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006CB RID: 1739
	[StructLayout(LayoutKind.Sequential)]
	internal class ResourceTableMappingEntry
	{
		// Token: 0x040022C1 RID: 8897
		[MarshalAs(UnmanagedType.LPWStr)]
		public string id;

		// Token: 0x040022C2 RID: 8898
		[MarshalAs(UnmanagedType.LPWStr)]
		public string FinalStringMapped;
	}
}
