using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200005D RID: 93
	public interface ILoggerFactory : IDisposable
	{
		// Token: 0x0600053B RID: 1339
		IBinaryLogger GetLoggerInstance(LoggerType type);

		// Token: 0x0600053C RID: 1340
		bool IsTracingEnabled(LoggerType type);
	}
}
