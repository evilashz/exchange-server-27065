using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.LogSearch
{
	// Token: 0x0200027C RID: 636
	internal abstract class LogSearchRpcServer : RpcServerBase
	{
		// Token: 0x06000BDC RID: 3036
		public abstract byte[] Search(string logName, byte[] queryData, [MarshalAs(UnmanagedType.U1)] bool continueInBackground, int resultLimit, ref int resultSize, ref Guid sessionId, ref bool more, ref int progress, string clientName);

		// Token: 0x06000BDD RID: 3037
		public abstract byte[] SearchExtensibleSchema(string clientVersion, string logName, byte[] queryData, [MarshalAs(UnmanagedType.U1)] bool continueInBackground, int resultLimit, ref int resultSize, ref Guid sessionId, ref bool more, ref int progress, string clientName);

		// Token: 0x06000BDE RID: 3038
		public abstract byte[] Continue(Guid sessionId, [MarshalAs(UnmanagedType.U1)] bool continueInBackground, int resultLimit, ref int resultSize, ref bool more, ref int progress);

		// Token: 0x06000BDF RID: 3039
		public abstract void Cancel(Guid sessionId);

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002A174 File Offset: 0x00029574
		public LogSearchRpcServer()
		{
		}

		// Token: 0x04000D0A RID: 3338
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.ILogSearch_v1_0_s_ifspec;
	}
}
