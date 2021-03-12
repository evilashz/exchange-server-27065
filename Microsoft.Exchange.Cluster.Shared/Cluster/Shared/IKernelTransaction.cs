using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200007E RID: 126
	[Guid("79427A2B-F895-40e0-BE79-B57DC82ED231")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IKernelTransaction
	{
		// Token: 0x06000365 RID: 869
		int GetHandle(out IntPtr pHandle);
	}
}
