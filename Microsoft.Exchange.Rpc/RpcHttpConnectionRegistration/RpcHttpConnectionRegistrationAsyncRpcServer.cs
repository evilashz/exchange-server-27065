using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration
{
	// Token: 0x020003C9 RID: 969
	internal abstract class RpcHttpConnectionRegistrationAsyncRpcServer : RpcServerBase
	{
		// Token: 0x060010C9 RID: 4297 RVA: 0x00053848 File Offset: 0x00052C48
		public RpcHttpConnectionRegistrationAsyncRpcServer()
		{
		}

		// Token: 0x060010CA RID: 4298
		public abstract IRpcHttpConnectionRegistrationAsyncDispatch GetRpcHttpConnectionRegistrationAsyncDispatch();

		// Token: 0x060010CB RID: 4299
		[return: MarshalAs(UnmanagedType.U1)]
		public abstract bool IsShuttingDown();

		// Token: 0x04000FE1 RID: 4065
		public static readonly IntPtr RpcIntfHandle = (IntPtr)<Module>.RpcHttpConnectionRegistrationAsync_v1_0_s_ifspec;
	}
}
