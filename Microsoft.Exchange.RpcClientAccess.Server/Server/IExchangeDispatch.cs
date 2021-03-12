using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000023 RID: 35
	internal interface IExchangeDispatch
	{
		// Token: 0x060000D6 RID: 214
		int Connect(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, out IntPtr contextHandle, string userDn, int flags, int connectionModulus, int codePage, int stringLocale, int sortLocale, out TimeSpan pollsMax, out int retryCount, out TimeSpan retryDelay, out string dnPrefix, out string displayName, short[] clientVersion, out short[] serverVersion, ArraySegment<byte> segmentExtendedAuxIn, ArraySegment<byte> segmentExtendedAuxOut, out ArraySegment<byte> auxOutData, IStandardBudget budget);

		// Token: 0x060000D7 RID: 215
		int Disconnect(ProtocolRequestInfo protocolRequestInfo, ref IntPtr contextHandle, bool rundown);

		// Token: 0x060000D8 RID: 216
		int Execute(ProtocolRequestInfo protocolRequestInfo, ref IntPtr contextHandle, int flags, ArraySegment<byte> segmentExtendedRopIn, ArraySegment<byte> segmentExtendedRopOut, out ArraySegment<byte> ropOutData, ArraySegment<byte> segmentExtendedAuxIn, ArraySegment<byte> segmentExtendedAuxOut, out ArraySegment<byte> auxOutData);

		// Token: 0x060000D9 RID: 217
		int NotificationConnect(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, out IntPtr asynchronousContextHandle);

		// Token: 0x060000DA RID: 218
		void NotificationWait(ProtocolRequestInfo protocolRequestInfo, IntPtr asynchronousContextHandle, int flags, Action<bool, int> completion);

		// Token: 0x060000DB RID: 219
		int RegisterPushNotification(ProtocolRequestInfo protocolRequestInfo, ref IntPtr contextHandle, ArraySegment<byte> segmentContext, int adviseBits, ArraySegment<byte> segmentClientBlob, out int notificationHandle);

		// Token: 0x060000DC RID: 220
		int UnregisterPushNotification(ProtocolRequestInfo protocolRequestInfo, ref IntPtr contextHandle, int notificationHandle);

		// Token: 0x060000DD RID: 221
		int Dummy(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding);

		// Token: 0x060000DE RID: 222
		void DroppedConnection(IntPtr asynchronousContextHandle);
	}
}
