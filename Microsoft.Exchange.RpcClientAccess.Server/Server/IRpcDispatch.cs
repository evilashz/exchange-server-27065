using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000025 RID: 37
	internal interface IRpcDispatch : IDisposable
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F2 RID: 242
		int MaximumConnections { get; }

		// Token: 0x060000F3 RID: 243
		void ReportBytesRead(long bytesRead, long uncompressedBytesRead);

		// Token: 0x060000F4 RID: 244
		void ReportBytesWritten(long bytesWritten, long uncompressedBytesWritten);

		// Token: 0x060000F5 RID: 245
		int Connect(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, out IntPtr contextHandle, string userDn, int flags, int connectionModulus, int codePage, int stringLocale, int sortLocale, out TimeSpan pollsMax, out int retryCount, out TimeSpan retryDelay, out string dnPrefix, out string displayName, short[] clientVersion, ArraySegment<byte> auxIn, ArraySegment<byte> auxOut, out int sizeAuxOut, IStandardBudget budget);

		// Token: 0x060000F6 RID: 246
		int Disconnect(ProtocolRequestInfo protocolRequestInfo, ref IntPtr contextHandle, bool rundown);

		// Token: 0x060000F7 RID: 247
		int Execute(ProtocolRequestInfo protocolRequestInfo, ref IntPtr contextHandle, IList<ArraySegment<byte>> ropInArray, ArraySegment<byte> ropOut, out int sizeRopOut, ArraySegment<byte> auxIn, ArraySegment<byte> auxOut, out int sizeAuxOut, bool isFake, out byte[] fakeOut);

		// Token: 0x060000F8 RID: 248
		int NotificationConnect(ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, out IntPtr asynchronousContextHandle);

		// Token: 0x060000F9 RID: 249
		int Dummy(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding);

		// Token: 0x060000FA RID: 250
		void NotificationWait(ProtocolRequestInfo protocolRequestInfo, IntPtr asynchronousContextHandle, uint flags, Action<bool, int> completion);

		// Token: 0x060000FB RID: 251
		void DroppedConnection(IntPtr asynchronousContextHandle);
	}
}
