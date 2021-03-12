using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000027 RID: 39
	public interface ITracer
	{
		// Token: 0x0600015F RID: 351
		void TraceError(string format, params object[] args);

		// Token: 0x06000160 RID: 352
		void TraceWarning(string format, params object[] args);

		// Token: 0x06000161 RID: 353
		void TraceInformation(string format, params object[] args);
	}
}
