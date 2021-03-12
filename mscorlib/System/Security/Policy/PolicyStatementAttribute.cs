using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x0200033B RID: 827
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum PolicyStatementAttribute
	{
		// Token: 0x040010D6 RID: 4310
		Nothing = 0,
		// Token: 0x040010D7 RID: 4311
		Exclusive = 1,
		// Token: 0x040010D8 RID: 4312
		LevelFinal = 2,
		// Token: 0x040010D9 RID: 4313
		All = 3
	}
}
