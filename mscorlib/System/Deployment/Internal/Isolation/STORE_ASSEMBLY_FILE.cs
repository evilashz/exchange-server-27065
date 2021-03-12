using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200064C RID: 1612
	internal struct STORE_ASSEMBLY_FILE
	{
		// Token: 0x04002134 RID: 8500
		public uint Size;

		// Token: 0x04002135 RID: 8501
		public uint Flags;

		// Token: 0x04002136 RID: 8502
		[MarshalAs(UnmanagedType.LPWStr)]
		public string FileName;

		// Token: 0x04002137 RID: 8503
		public uint FileStatusFlags;
	}
}
