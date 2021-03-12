using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x02000128 RID: 296
	internal static class ReplayRpcErrorCode
	{
		// Token: 0x060006D2 RID: 1746 RVA: 0x000055B8 File Offset: 0x000049B8
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool IsRpcConnectionError(int errorCode)
		{
			return errorCode == 1722 || errorCode == 1753 || errorCode == 1717 || errorCode == 1723 || errorCode == 1726 || errorCode == 1727;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000055F8 File Offset: 0x000049F8
		[return: MarshalAs(UnmanagedType.U1)]
		public static bool IsRpcTimeoutError(int errorCode)
		{
			return errorCode == 1818;
		}
	}
}
