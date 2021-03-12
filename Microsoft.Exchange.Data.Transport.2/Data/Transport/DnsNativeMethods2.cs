using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x020000B3 RID: 179
	[ComVisible(false)]
	[SuppressUnmanagedCodeSecurity]
	internal static class DnsNativeMethods2
	{
		// Token: 0x060003EA RID: 1002
		[DllImport("dnsapi.dll", CharSet = CharSet.Unicode, EntryPoint = "DnsValidateName_W")]
		internal static extern int ValidateName([In] string name, int format);

		// Token: 0x04000225 RID: 549
		private const string DNSAPI = "dnsapi.dll";
	}
}
