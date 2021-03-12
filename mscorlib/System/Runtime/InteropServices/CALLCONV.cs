using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000974 RID: 2420
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.CALLCONV instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum CALLCONV
	{
		// Token: 0x04002BB5 RID: 11189
		CC_CDECL = 1,
		// Token: 0x04002BB6 RID: 11190
		CC_MSCPASCAL,
		// Token: 0x04002BB7 RID: 11191
		CC_PASCAL = 2,
		// Token: 0x04002BB8 RID: 11192
		CC_MACPASCAL,
		// Token: 0x04002BB9 RID: 11193
		CC_STDCALL,
		// Token: 0x04002BBA RID: 11194
		CC_RESERVED,
		// Token: 0x04002BBB RID: 11195
		CC_SYSCALL,
		// Token: 0x04002BBC RID: 11196
		CC_MPWCDECL,
		// Token: 0x04002BBD RID: 11197
		CC_MPWPASCAL,
		// Token: 0x04002BBE RID: 11198
		CC_MAX
	}
}
