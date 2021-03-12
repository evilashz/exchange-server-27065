using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000718 RID: 1816
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum StreamingContextStates
	{
		// Token: 0x040023A9 RID: 9129
		CrossProcess = 1,
		// Token: 0x040023AA RID: 9130
		CrossMachine = 2,
		// Token: 0x040023AB RID: 9131
		File = 4,
		// Token: 0x040023AC RID: 9132
		Persistence = 8,
		// Token: 0x040023AD RID: 9133
		Remoting = 16,
		// Token: 0x040023AE RID: 9134
		Other = 32,
		// Token: 0x040023AF RID: 9135
		Clone = 64,
		// Token: 0x040023B0 RID: 9136
		CrossAppDomain = 128,
		// Token: 0x040023B1 RID: 9137
		All = 255
	}
}
