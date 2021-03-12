using System;

namespace Microsoft.Exchange.Rpc.NotificationsBroker
{
	// Token: 0x020002DC RID: 732
	internal abstract class NotificationsBrokerAsyncRpcServer : BaseAsyncRpcServer<Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>
	{
		// Token: 0x06000D00 RID: 3328 RVA: 0x00031E70 File Offset: 0x00031270
		public override void DroppedConnection(IntPtr contextHandle)
		{
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00031E80 File Offset: 0x00031280
		public override void RundownContext(IntPtr contextHandle)
		{
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00031E90 File Offset: 0x00031290
		public override void StartRundownQueue()
		{
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00031EA0 File Offset: 0x000312A0
		public override void StopRundownQueue()
		{
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0003397C File Offset: 0x00032D7C
		public NotificationsBrokerAsyncRpcServer()
		{
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00031EC4 File Offset: 0x000312C4
		// Note: this type is marked as 'beforefieldinit'.
		static NotificationsBrokerAsyncRpcServer()
		{
			IntPtr rpcIntfHandle = new IntPtr(<Module>.INotificationsBrokerRpc_v1_0_s_ifspec);
			NotificationsBrokerAsyncRpcServer.RpcIntfHandle = rpcIntfHandle;
		}

		// Token: 0x04000D93 RID: 3475
		public static readonly IntPtr RpcIntfHandle;
	}
}
