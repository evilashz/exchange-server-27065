using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000016 RID: 22
	internal sealed class RegisterPushNotificationDispatchTask : ExchangeDispatchTask
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00004A7D File Offset: 0x00002C7D
		public RegisterPushNotificationDispatchTask(IExchangeDispatch exchangeDispatch, CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, ArraySegment<byte> segmentContext, int adviseBits, ArraySegment<byte> segmentClientBlob) : base("RegisterPushNotificationDispatchTask", exchangeDispatch, protocolRequestInfo, asyncCallback, asyncState)
		{
			this.contextHandleIn = contextHandle;
			this.contextHandleOut = contextHandle;
			this.segmentContext = segmentContext;
			this.adviseBits = adviseBits;
			this.segmentClientBlob = segmentClientBlob;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004AB7 File Offset: 0x00002CB7
		internal override IntPtr ContextHandle
		{
			get
			{
				return this.contextHandleIn;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004ABF File Offset: 0x00002CBF
		internal override int? InternalExecute()
		{
			return new int?(base.ExchangeDispatch.RegisterPushNotification(base.ProtocolRequestInfo, ref this.contextHandleOut, this.segmentContext, this.adviseBits, this.segmentClientBlob, out this.notificationHandle));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004AF8 File Offset: 0x00002CF8
		public int End(out IntPtr contextHandle, out int notificationHandle)
		{
			int result = base.CheckCompletion();
			contextHandle = this.contextHandleOut;
			notificationHandle = this.notificationHandle;
			return result;
		}

		// Token: 0x04000084 RID: 132
		private ArraySegment<byte> segmentContext;

		// Token: 0x04000085 RID: 133
		private int adviseBits;

		// Token: 0x04000086 RID: 134
		private ArraySegment<byte> segmentClientBlob;

		// Token: 0x04000087 RID: 135
		private IntPtr contextHandleIn;

		// Token: 0x04000088 RID: 136
		private IntPtr contextHandleOut;

		// Token: 0x04000089 RID: 137
		private int notificationHandle;
	}
}
