using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200095F RID: 2399
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.STATSTG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		// Token: 0x04002B2E RID: 11054
		public string pwcsName;

		// Token: 0x04002B2F RID: 11055
		public int type;

		// Token: 0x04002B30 RID: 11056
		public long cbSize;

		// Token: 0x04002B31 RID: 11057
		public FILETIME mtime;

		// Token: 0x04002B32 RID: 11058
		public FILETIME ctime;

		// Token: 0x04002B33 RID: 11059
		public FILETIME atime;

		// Token: 0x04002B34 RID: 11060
		public int grfMode;

		// Token: 0x04002B35 RID: 11061
		public int grfLocksSupported;

		// Token: 0x04002B36 RID: 11062
		public Guid clsid;

		// Token: 0x04002B37 RID: 11063
		public int grfStateBits;

		// Token: 0x04002B38 RID: 11064
		public int reserved;
	}
}
