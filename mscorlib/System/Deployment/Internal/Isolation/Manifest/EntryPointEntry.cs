using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006CE RID: 1742
	[StructLayout(LayoutKind.Sequential)]
	internal class EntryPointEntry
	{
		// Token: 0x040022C5 RID: 8901
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x040022C6 RID: 8902
		[MarshalAs(UnmanagedType.LPWStr)]
		public string CommandLine_File;

		// Token: 0x040022C7 RID: 8903
		[MarshalAs(UnmanagedType.LPWStr)]
		public string CommandLine_Parameters;

		// Token: 0x040022C8 RID: 8904
		public IReferenceIdentity Identity;

		// Token: 0x040022C9 RID: 8905
		public uint Flags;
	}
}
