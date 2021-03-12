using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.EdgeSync
{
	// Token: 0x020001D6 RID: 470
	internal abstract class EdgeSyncRpcServer : RpcServerBase
	{
		// Token: 0x060009D6 RID: 2518
		public abstract StartResults StartSyncNow(string targetServer, [MarshalAs(UnmanagedType.U1)] bool forceFullSync, [MarshalAs(UnmanagedType.U1)] bool forceUpdateCookie);

		// Token: 0x060009D7 RID: 2519
		public abstract byte[] GetSyncNowResult(ref GetResultResults continueFlag);

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001B544 File Offset: 0x0001A944
		public EdgeSyncRpcServer()
		{
		}

		// Token: 0x04000B82 RID: 2946
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.ISyncNow_v1_0_s_ifspec;
	}
}
