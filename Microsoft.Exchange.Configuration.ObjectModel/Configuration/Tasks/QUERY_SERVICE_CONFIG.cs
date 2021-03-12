using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000C7 RID: 199
	[StructLayout(LayoutKind.Sequential)]
	internal class QUERY_SERVICE_CONFIG
	{
		// Token: 0x040001FB RID: 507
		[MarshalAs(UnmanagedType.U4)]
		public uint dwServiceType;

		// Token: 0x040001FC RID: 508
		[MarshalAs(UnmanagedType.U4)]
		public uint dwStartType;

		// Token: 0x040001FD RID: 509
		[MarshalAs(UnmanagedType.U4)]
		public uint dwErrorControl;

		// Token: 0x040001FE RID: 510
		[MarshalAs(UnmanagedType.LPWStr)]
		public string binaryPathName;

		// Token: 0x040001FF RID: 511
		[MarshalAs(UnmanagedType.LPWStr)]
		public string loadOrderGroup;

		// Token: 0x04000200 RID: 512
		[MarshalAs(UnmanagedType.U4)]
		public uint dwTagId;

		// Token: 0x04000201 RID: 513
		[MarshalAs(UnmanagedType.LPWStr)]
		public string dependencies;

		// Token: 0x04000202 RID: 514
		[MarshalAs(UnmanagedType.LPWStr)]
		public string serviceStartName;

		// Token: 0x04000203 RID: 515
		[MarshalAs(UnmanagedType.LPWStr)]
		public string displayName;
	}
}
