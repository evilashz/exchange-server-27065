using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200097A RID: 2426
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPELIBATTR instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		// Token: 0x04002BE4 RID: 11236
		public Guid guid;

		// Token: 0x04002BE5 RID: 11237
		public int lcid;

		// Token: 0x04002BE6 RID: 11238
		public SYSKIND syskind;

		// Token: 0x04002BE7 RID: 11239
		public short wMajorVerNum;

		// Token: 0x04002BE8 RID: 11240
		public short wMinorVerNum;

		// Token: 0x04002BE9 RID: 11241
		public LIBFLAGS wLibFlags;
	}
}
