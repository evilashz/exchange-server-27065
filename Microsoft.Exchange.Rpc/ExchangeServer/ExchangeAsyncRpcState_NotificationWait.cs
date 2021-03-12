using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x02000207 RID: 519
	internal class ExchangeAsyncRpcState_NotificationWait : BaseAsyncRpcState<Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcState_NotificationWait,Microsoft::Exchange::Rpc::ExchangeServer::ExchangeAsyncRpcServer_AsyncEMSMDB,Microsoft::Exchange::Rpc::IExchangeAsyncDispatch>
	{
		// Token: 0x06000B3E RID: 2878 RVA: 0x0001E34C File Offset: 0x0001D74C
		public override int PoolSize()
		{
			return 65536;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002001C File Offset: 0x0001F41C
		public void Initialize(SafeRpcAsyncStateHandle asyncState, ExchangeAsyncRpcServer_AsyncEMSMDB asyncServer, IntPtr contextHandle, uint flagsIn, IntPtr pFlagsOut)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flagsIn = flagsIn;
			this.pFlagsOut = pFlagsOut;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0001E360 File Offset: 0x0001D760
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flagsIn = 0U;
			this.pFlagsOut = IntPtr.Zero;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00020048 File Offset: 0x0001F448
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			bool flag = false;
			IntPtr intPtr = this.contextHandle;
			base.AsyncServer.RegisterConnectionDroppedNotification(base.AsyncState, intPtr);
			try
			{
				base.AsyncDispatch.BeginNotificationWait(null, this.contextHandle, (int)this.flagsIn, asyncCallback, this);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					base.AsyncServer.UnregisterConnectionDroppedNotification(base.AsyncState);
				}
			}
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000200CC File Offset: 0x0001F4CC
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			int result;
			try
			{
				int val = 0;
				int num = base.AsyncDispatch.EndNotificationWait(asyncResult, out val);
				Marshal.WriteInt32(this.pFlagsOut, val);
				result = num;
			}
			finally
			{
				base.AsyncServer.UnregisterConnectionDroppedNotification(base.AsyncState);
			}
			return result;
		}

		// Token: 0x04000C51 RID: 3153
		private IntPtr contextHandle;

		// Token: 0x04000C52 RID: 3154
		private uint flagsIn;

		// Token: 0x04000C53 RID: 3155
		private IntPtr pFlagsOut;
	}
}
