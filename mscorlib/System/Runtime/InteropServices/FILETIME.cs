using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200095A RID: 2394
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FILETIME instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	public struct FILETIME
	{
		// Token: 0x04002B2C RID: 11052
		public int dwLowDateTime;

		// Token: 0x04002B2D RID: 11053
		public int dwHighDateTime;
	}
}
