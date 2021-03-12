using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006B9 RID: 1721
	[StructLayout(LayoutKind.Sequential)]
	internal class COMServerEntry
	{
		// Token: 0x04002284 RID: 8836
		public Guid Clsid;

		// Token: 0x04002285 RID: 8837
		public uint Flags;

		// Token: 0x04002286 RID: 8838
		public Guid ConfiguredGuid;

		// Token: 0x04002287 RID: 8839
		public Guid ImplementedClsid;

		// Token: 0x04002288 RID: 8840
		public Guid TypeLibrary;

		// Token: 0x04002289 RID: 8841
		public uint ThreadingModel;

		// Token: 0x0400228A RID: 8842
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RuntimeVersion;

		// Token: 0x0400228B RID: 8843
		[MarshalAs(UnmanagedType.LPWStr)]
		public string HostFile;
	}
}
