using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006AD RID: 1709
	[StructLayout(LayoutKind.Sequential)]
	internal class FileAssociationEntry
	{
		// Token: 0x0400226C RID: 8812
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Extension;

		// Token: 0x0400226D RID: 8813
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x0400226E RID: 8814
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ProgID;

		// Token: 0x0400226F RID: 8815
		[MarshalAs(UnmanagedType.LPWStr)]
		public string DefaultIcon;

		// Token: 0x04002270 RID: 8816
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Parameter;
	}
}
