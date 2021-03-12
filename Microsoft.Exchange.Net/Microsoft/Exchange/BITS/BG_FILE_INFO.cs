using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.BITS
{
	// Token: 0x02000667 RID: 1639
	internal struct BG_FILE_INFO
	{
		// Token: 0x04001DDE RID: 7646
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RemoteName;

		// Token: 0x04001DDF RID: 7647
		[MarshalAs(UnmanagedType.LPWStr)]
		public string LocalName;
	}
}
