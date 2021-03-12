using System;
using System.Net;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Logging
{
	// Token: 0x02000080 RID: 128
	internal interface IProtocolLog
	{
		// Token: 0x0600039D RID: 925
		void Configure(LocalLongFullPath path, TimeSpan ageQuota, Unlimited<ByteQuantifiedSize> sizeQuota, Unlimited<ByteQuantifiedSize> perFileSizeQuota, int bufferSize, TimeSpan streamFlushInterval, TimeSpan backgroundWriteInterval);

		// Token: 0x0600039E RID: 926
		void Configure(string path, TimeSpan ageQuota, long sizeQuota, long perFileSizeQuota, int bufferSize, TimeSpan streamFlushInterval, TimeSpan backgroundWriteInterval);

		// Token: 0x0600039F RID: 927
		IProtocolLogSession OpenSession(string connectorId, ulong sessionId, IPEndPoint remoteEndPoint, IPEndPoint localEndPoint, ProtocolLoggingLevel loggingLevel);

		// Token: 0x060003A0 RID: 928
		void Flush();

		// Token: 0x060003A1 RID: 929
		void Close();
	}
}
