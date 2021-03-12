using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001F9 RID: 505
	internal abstract class InternalNativeMethods
	{
		// Token: 0x020001FA RID: 506
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct HDITEM
		{
			// Token: 0x04000877 RID: 2167
			public uint mask;

			// Token: 0x04000878 RID: 2168
			public int cxy;

			// Token: 0x04000879 RID: 2169
			public string pszText;

			// Token: 0x0400087A RID: 2170
			public IntPtr hbm;

			// Token: 0x0400087B RID: 2171
			public int cchTextMax;

			// Token: 0x0400087C RID: 2172
			public int fmt;

			// Token: 0x0400087D RID: 2173
			public IntPtr lParam;

			// Token: 0x0400087E RID: 2174
			public int iImage;

			// Token: 0x0400087F RID: 2175
			public int iOrder;

			// Token: 0x04000880 RID: 2176
			public uint type;

			// Token: 0x04000881 RID: 2177
			public IntPtr pvFilter;
		}
	}
}
