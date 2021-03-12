using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Misc
{
	// Token: 0x02000021 RID: 33
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct PROPBAG2
	{
		// Token: 0x0400007F RID: 127
		public int dwType;

		// Token: 0x04000080 RID: 128
		public ushort vt;

		// Token: 0x04000081 RID: 129
		private IntPtr cfType;

		// Token: 0x04000082 RID: 130
		public int dwHint;

		// Token: 0x04000083 RID: 131
		[MarshalAs(UnmanagedType.LPWStr)]
		public string pstrName;

		// Token: 0x04000084 RID: 132
		public Guid clsid;
	}
}
