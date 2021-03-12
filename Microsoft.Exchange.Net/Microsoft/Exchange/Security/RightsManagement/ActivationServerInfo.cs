using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x0200099E RID: 2462
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[StructLayout(LayoutKind.Sequential)]
	internal class ActivationServerInfo
	{
		// Token: 0x04002D9F RID: 11679
		public uint Version;

		// Token: 0x04002DA0 RID: 11680
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string PubKey = string.Empty;

		// Token: 0x04002DA1 RID: 11681
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string Url = string.Empty;
	}
}
