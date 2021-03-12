using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.BITS
{
	// Token: 0x02000665 RID: 1637
	internal struct BG_BASIC_CREDENTIALS
	{
		// Token: 0x04001DD4 RID: 7636
		[MarshalAs(UnmanagedType.LPWStr)]
		public string UserName;

		// Token: 0x04001DD5 RID: 7637
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Password;
	}
}
