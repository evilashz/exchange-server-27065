﻿using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000975 RID: 2421
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum FUNCFLAGS : short
	{
		// Token: 0x04002BC0 RID: 11200
		FUNCFLAG_FRESTRICTED = 1,
		// Token: 0x04002BC1 RID: 11201
		FUNCFLAG_FSOURCE = 2,
		// Token: 0x04002BC2 RID: 11202
		FUNCFLAG_FBINDABLE = 4,
		// Token: 0x04002BC3 RID: 11203
		FUNCFLAG_FREQUESTEDIT = 8,
		// Token: 0x04002BC4 RID: 11204
		FUNCFLAG_FDISPLAYBIND = 16,
		// Token: 0x04002BC5 RID: 11205
		FUNCFLAG_FDEFAULTBIND = 32,
		// Token: 0x04002BC6 RID: 11206
		FUNCFLAG_FHIDDEN = 64,
		// Token: 0x04002BC7 RID: 11207
		FUNCFLAG_FUSESGETLASTERROR = 128,
		// Token: 0x04002BC8 RID: 11208
		FUNCFLAG_FDEFAULTCOLLELEM = 256,
		// Token: 0x04002BC9 RID: 11209
		FUNCFLAG_FUIDEFAULT = 512,
		// Token: 0x04002BCA RID: 11210
		FUNCFLAG_FNONBROWSABLE = 1024,
		// Token: 0x04002BCB RID: 11211
		FUNCFLAG_FREPLACEABLE = 2048,
		// Token: 0x04002BCC RID: 11212
		FUNCFLAG_FIMMEDIATEBIND = 4096
	}
}