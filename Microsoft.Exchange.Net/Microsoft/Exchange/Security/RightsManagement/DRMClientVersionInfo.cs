using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009A1 RID: 2465
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class DRMClientVersionInfo
	{
		// Token: 0x04002DB6 RID: 11702
		public uint StructVersion = 1U;

		// Token: 0x04002DB7 RID: 11703
		public uint V1;

		// Token: 0x04002DB8 RID: 11704
		public uint V2;

		// Token: 0x04002DB9 RID: 11705
		public uint V3;

		// Token: 0x04002DBA RID: 11706
		public uint V4;

		// Token: 0x04002DBB RID: 11707
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string Hierarchy;

		// Token: 0x04002DBC RID: 11708
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string ProductId;

		// Token: 0x04002DBD RID: 11709
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string ProductDescription;
	}
}
