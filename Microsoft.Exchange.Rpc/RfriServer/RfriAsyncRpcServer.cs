using System;

namespace Microsoft.Exchange.Rpc.RfriServer
{
	// Token: 0x020003A8 RID: 936
	internal abstract class RfriAsyncRpcServer : BaseAsyncRpcServer<Microsoft::Exchange::Rpc::IRfriAsyncDispatch>
	{
		// Token: 0x0600107D RID: 4221 RVA: 0x0004CA50 File Offset: 0x0004BE50
		public override void DroppedConnection(IntPtr contextHandle)
		{
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0004CA60 File Offset: 0x0004BE60
		public override void RundownContext(IntPtr contextHandle)
		{
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0004CA70 File Offset: 0x0004BE70
		public override void StartRundownQueue()
		{
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0004CA80 File Offset: 0x0004BE80
		public override void StopRundownQueue()
		{
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0004DC78 File Offset: 0x0004D078
		public RfriAsyncRpcServer()
		{
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0004CAA4 File Offset: 0x0004BEA4
		// Note: this type is marked as 'beforefieldinit'.
		static RfriAsyncRpcServer()
		{
			IntPtr rpcIntfHandle = new IntPtr(<Module>.rfri_v1_0_s_ifspec);
			RfriAsyncRpcServer.RpcIntfHandle = rpcIntfHandle;
		}

		// Token: 0x04000FA7 RID: 4007
		public static readonly IntPtr RpcIntfHandle;
	}
}
