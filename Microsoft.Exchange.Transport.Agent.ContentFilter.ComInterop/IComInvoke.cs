using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Data.Transport.Interop
{
	// Token: 0x02000003 RID: 3
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("786E6730-5D95-3D9D-951B-5C9ABD1E158D")]
	[SuppressUnmanagedCodeSecurity]
	public interface IComInvoke
	{
		// Token: 0x06000008 RID: 8
		void ComAsyncInvoke(IProxyCallback callback);
	}
}
