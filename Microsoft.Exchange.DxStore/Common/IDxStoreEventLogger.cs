using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200007E RID: 126
	public interface IDxStoreEventLogger
	{
		// Token: 0x060004F5 RID: 1269
		void Log(DxEventSeverity severity, int id, string formatString, params object[] args);

		// Token: 0x060004F6 RID: 1270
		void LogPeriodic(string periodicKey, TimeSpan periodicDuration, DxEventSeverity severity, int id, string formatString, params object[] args);
	}
}
