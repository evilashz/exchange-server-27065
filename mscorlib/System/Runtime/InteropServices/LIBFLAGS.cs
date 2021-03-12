using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000979 RID: 2425
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.LIBFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum LIBFLAGS : short
	{
		// Token: 0x04002BE0 RID: 11232
		LIBFLAG_FRESTRICTED = 1,
		// Token: 0x04002BE1 RID: 11233
		LIBFLAG_FCONTROL = 2,
		// Token: 0x04002BE2 RID: 11234
		LIBFLAG_FHIDDEN = 4,
		// Token: 0x04002BE3 RID: 11235
		LIBFLAG_FHASDISKIMAGE = 8
	}
}
