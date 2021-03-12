using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000037 RID: 55
	public interface IBinaryLogger : IDisposable
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000437 RID: 1079
		bool IsLoggingEnabled { get; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000438 RID: 1080
		bool IsDisposed { get; }

		// Token: 0x06000439 RID: 1081
		void Start();

		// Token: 0x0600043A RID: 1082
		void Stop();

		// Token: 0x0600043B RID: 1083
		bool TryWrite(TraceBuffer buffer, int retries, TimeSpan timeToWait);

		// Token: 0x0600043C RID: 1084
		bool TryWrite(TraceBuffer buffer);
	}
}
