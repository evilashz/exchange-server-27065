using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200094D RID: 2381
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.BIND_OPTS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	public struct BIND_OPTS
	{
		// Token: 0x04002B26 RID: 11046
		public int cbStruct;

		// Token: 0x04002B27 RID: 11047
		public int grfFlags;

		// Token: 0x04002B28 RID: 11048
		public int grfMode;

		// Token: 0x04002B29 RID: 11049
		public int dwTickCountDeadline;
	}
}
