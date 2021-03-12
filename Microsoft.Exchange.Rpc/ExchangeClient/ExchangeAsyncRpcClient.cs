using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.ExchangeClient
{
	// Token: 0x020001E8 RID: 488
	internal class ExchangeAsyncRpcClient : RpcClientBase, IExchangeAsyncDispatch
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x0001CE04 File Offset: 0x0001C204
		public ExchangeAsyncRpcClient(RpcBindingInfo bindingInfo) : base(bindingInfo.UseKerberosSpn("exchangeMDB", null))
		{
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001DDB8 File Offset: 0x0001D1B8
		public virtual ICancelableAsyncResult BeginConnect(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, string userDn, int flags, int conMod, int cpid, int lcidString, int lcidSort, short[] clientVersion, ArraySegment<byte> segmentExtendedAuxIn, ArraySegment<byte> segmentExtendedAuxOut, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ClientAsyncCallState_Connect clientAsyncCallState_Connect = null;
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				IntPtr pRpcBindingHandle = (IntPtr)base.BindingHandle;
				clientAsyncCallState_Connect = new ClientAsyncCallState_Connect(asyncCallback, asyncState, pRpcBindingHandle, userDn, flags, conMod, cpid, lcidString, lcidSort, clientVersion, segmentExtendedAuxIn, segmentExtendedAuxOut);
				clientAsyncCallState_Connect.Begin();
				flag = true;
				result = clientAsyncCallState_Connect;
			}
			finally
			{
				if (!flag && clientAsyncCallState_Connect != null)
				{
					((IDisposable)clientAsyncCallState_Connect).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001D984 File Offset: 0x0001CD84
		public virtual int EndConnect(ICancelableAsyncResult result, out IntPtr contextHandle, out TimeSpan pollsMax, out int retryCount, out TimeSpan retryDelay, out string dnPrefix, out string displayName, out short[] serverVersion, out ArraySegment<byte> segmentExtendedAuxOut)
		{
			int result2;
			using (ClientAsyncCallState_Connect clientAsyncCallState_Connect = (ClientAsyncCallState_Connect)result)
			{
				result2 = clientAsyncCallState_Connect.End(out contextHandle, out pollsMax, out retryCount, out retryDelay, out dnPrefix, out displayName, out serverVersion, out segmentExtendedAuxOut);
			}
			return result2;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001DE2C File Offset: 0x0001D22C
		public virtual ICancelableAsyncResult BeginDisconnect(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ClientAsyncCallState_Disconnect clientAsyncCallState_Disconnect = null;
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				clientAsyncCallState_Disconnect = new ClientAsyncCallState_Disconnect(asyncCallback, asyncState, contextHandle);
				clientAsyncCallState_Disconnect.Begin();
				flag = true;
				result = clientAsyncCallState_Disconnect;
			}
			finally
			{
				if (!flag && clientAsyncCallState_Disconnect != null)
				{
					((IDisposable)clientAsyncCallState_Disconnect).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001D9D8 File Offset: 0x0001CDD8
		public virtual int EndDisconnect(ICancelableAsyncResult result, out IntPtr contextHandle)
		{
			int result2;
			using (ClientAsyncCallState_Disconnect clientAsyncCallState_Disconnect = (ClientAsyncCallState_Disconnect)result)
			{
				result2 = clientAsyncCallState_Disconnect.End(out contextHandle);
			}
			return result2;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001DE80 File Offset: 0x0001D280
		public virtual ICancelableAsyncResult BeginExecute(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, int flags, ArraySegment<byte> segmentExtendedRopIn, ArraySegment<byte> segmentExtendedRopOut, ArraySegment<byte> segmentExtendedAuxIn, ArraySegment<byte> segmentExtendedAuxOut, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ClientAsyncCallState_Execute clientAsyncCallState_Execute = null;
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				clientAsyncCallState_Execute = new ClientAsyncCallState_Execute(asyncCallback, asyncState, contextHandle, flags, segmentExtendedRopIn, segmentExtendedRopOut, segmentExtendedAuxIn, segmentExtendedAuxOut);
				clientAsyncCallState_Execute.Begin();
				flag = true;
				result = clientAsyncCallState_Execute;
			}
			finally
			{
				if (!flag && clientAsyncCallState_Execute != null)
				{
					((IDisposable)clientAsyncCallState_Execute).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001DA20 File Offset: 0x0001CE20
		public virtual int EndExecute(ICancelableAsyncResult result, out IntPtr contextHandle, out ArraySegment<byte> segmentExtendedRopOut, out ArraySegment<byte> segmentExtendedAuxOut)
		{
			int result2;
			using (ClientAsyncCallState_Execute clientAsyncCallState_Execute = (ClientAsyncCallState_Execute)result)
			{
				result2 = clientAsyncCallState_Execute.End(out contextHandle, out segmentExtendedRopOut, out segmentExtendedAuxOut);
			}
			return result2;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001DEE0 File Offset: 0x0001D2E0
		public virtual ICancelableAsyncResult BeginDummy(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ClientAsyncCallState_Dummy clientAsyncCallState_Dummy = null;
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				IntPtr pRpcBindingHandle = (IntPtr)base.BindingHandle;
				clientAsyncCallState_Dummy = new ClientAsyncCallState_Dummy(asyncCallback, asyncState, pRpcBindingHandle);
				clientAsyncCallState_Dummy.Begin();
				flag = true;
				result = clientAsyncCallState_Dummy;
			}
			finally
			{
				if (!flag && clientAsyncCallState_Dummy != null)
				{
					((IDisposable)clientAsyncCallState_Dummy).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001DA6C File Offset: 0x0001CE6C
		public virtual int EndDummy(ICancelableAsyncResult result)
		{
			int result2;
			using (ClientAsyncCallState_Dummy clientAsyncCallState_Dummy = (ClientAsyncCallState_Dummy)result)
			{
				result2 = clientAsyncCallState_Dummy.CheckCompletion();
			}
			return result2;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001BC9C File Offset: 0x0001B09C
		public virtual ICancelableAsyncResult BeginNotificationConnect(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new Exception("Not Implemented");
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001BCB8 File Offset: 0x0001B0B8
		public virtual int EndNotificationConnect(ICancelableAsyncResult result, out IntPtr notificationContextHandle)
		{
			throw new Exception("Not Implemented");
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001BCD4 File Offset: 0x0001B0D4
		public virtual ICancelableAsyncResult BeginNotificationWait(ProtocolRequestInfo protocolRequestInfo, IntPtr notificationContextHandle, int flagsIn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new Exception("Not Implemented");
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001BCF0 File Offset: 0x0001B0F0
		public virtual int EndNotificationWait(ICancelableAsyncResult result, out int flagsOut)
		{
			throw new Exception("Not Implemented");
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001BD0C File Offset: 0x0001B10C
		public virtual ICancelableAsyncResult BeginRegisterPushNotification(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, ArraySegment<byte> segmentContext, int adviseBits, ArraySegment<byte> segmentClientBlob, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new Exception("Not Implemented");
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001BD28 File Offset: 0x0001B128
		public virtual int EndRegisterPushNotification(ICancelableAsyncResult asyncResult, out IntPtr contextHandle, out int notificationHandle)
		{
			throw new Exception("Not Implemented");
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001BD44 File Offset: 0x0001B144
		public virtual ICancelableAsyncResult BeginUnregisterPushNotification(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, int notificationHandle, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new Exception("Not Implemented");
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001BD60 File Offset: 0x0001B160
		public virtual int EndUnregisterPushNotification(ICancelableAsyncResult asyncResult, out IntPtr contextHandle)
		{
			throw new Exception("Not Implemented");
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001BD7C File Offset: 0x0001B17C
		public virtual void ContextHandleRundown(IntPtr contextHandle)
		{
			throw new Exception("Not Implemented");
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001BD94 File Offset: 0x0001B194
		public virtual void NotificationContextHandleRundown(IntPtr notificationContextHandle)
		{
			throw new Exception("Not Implemented");
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001BDAC File Offset: 0x0001B1AC
		public virtual void DroppedConnection(IntPtr notificationContextHandle)
		{
			throw new Exception("Not Implemented");
		}
	}
}
