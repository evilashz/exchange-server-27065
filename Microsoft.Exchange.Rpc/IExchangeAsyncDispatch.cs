using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020001DC RID: 476
	internal interface IExchangeAsyncDispatch
	{
		// Token: 0x060009F2 RID: 2546
		ICancelableAsyncResult BeginConnect(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, string userDn, int flags, int conMod, int cpid, int lcidString, int lcidSort, short[] clientVersion, ArraySegment<byte> segmentExtendedAuxIn, ArraySegment<byte> segmentExtendedAuxOut, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x060009F3 RID: 2547
		int EndConnect(ICancelableAsyncResult result, out IntPtr contextHandle, out TimeSpan pollsMax, out int retryCount, out TimeSpan retryDelay, out string dnPrefix, out string displayName, out short[] serverVersion, out ArraySegment<byte> segmentExtendedAuxOut);

		// Token: 0x060009F4 RID: 2548
		ICancelableAsyncResult BeginDisconnect(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x060009F5 RID: 2549
		int EndDisconnect(ICancelableAsyncResult result, out IntPtr contextHandle);

		// Token: 0x060009F6 RID: 2550
		ICancelableAsyncResult BeginExecute(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, int flags, ArraySegment<byte> segmentExtendedRopIn, ArraySegment<byte> segmentExtendedRopOut, ArraySegment<byte> segmentExtendedAuxIn, ArraySegment<byte> segmentExtendedAuxOut, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x060009F7 RID: 2551
		int EndExecute(ICancelableAsyncResult result, out IntPtr contextHandle, out ArraySegment<byte> segmentExtendedRopOut, out ArraySegment<byte> segmentExtendedAuxOut);

		// Token: 0x060009F8 RID: 2552
		ICancelableAsyncResult BeginNotificationConnect(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x060009F9 RID: 2553
		int EndNotificationConnect(ICancelableAsyncResult result, out IntPtr notificationContextHandle);

		// Token: 0x060009FA RID: 2554
		ICancelableAsyncResult BeginNotificationWait(ProtocolRequestInfo protocolRequestInfo, IntPtr notificationContextHandle, int flagsIn, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x060009FB RID: 2555
		int EndNotificationWait(ICancelableAsyncResult result, out int flagsOut);

		// Token: 0x060009FC RID: 2556
		ICancelableAsyncResult BeginRegisterPushNotification(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, ArraySegment<byte> segmentContext, int adviseBits, ArraySegment<byte> segmentClientBlob, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x060009FD RID: 2557
		int EndRegisterPushNotification(ICancelableAsyncResult asyncResult, out IntPtr contextHandle, out int notificationHandle);

		// Token: 0x060009FE RID: 2558
		ICancelableAsyncResult BeginUnregisterPushNotification(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, int notificationHandle, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x060009FF RID: 2559
		int EndUnregisterPushNotification(ICancelableAsyncResult asyncResult, out IntPtr contextHandle);

		// Token: 0x06000A00 RID: 2560
		ICancelableAsyncResult BeginDummy(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, CancelableAsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000A01 RID: 2561
		int EndDummy(ICancelableAsyncResult result);

		// Token: 0x06000A02 RID: 2562
		void ContextHandleRundown(IntPtr contextHandle);

		// Token: 0x06000A03 RID: 2563
		void NotificationContextHandleRundown(IntPtr notificationContextHandle);

		// Token: 0x06000A04 RID: 2564
		void DroppedConnection(IntPtr notificationContextHandle);
	}
}
