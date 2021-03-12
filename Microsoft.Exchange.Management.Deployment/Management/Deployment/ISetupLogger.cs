using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200001D RID: 29
	public interface ISetupLogger
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000043 RID: 67
		// (set) Token: 0x06000044 RID: 68
		bool IsPrereqLogging { get; set; }

		// Token: 0x06000045 RID: 69
		void StartLogging();

		// Token: 0x06000046 RID: 70
		void StopLogging();

		// Token: 0x06000047 RID: 71
		void Log(LocalizedString localizedString);

		// Token: 0x06000048 RID: 72
		void LogWarning(LocalizedString localizedString);

		// Token: 0x06000049 RID: 73
		void LogError(Exception e);

		// Token: 0x0600004A RID: 74
		void TraceEnter(params object[] arguments);

		// Token: 0x0600004B RID: 75
		void TraceExit();

		// Token: 0x0600004C RID: 76
		void IncreaseIndentation(LocalizedString tag);

		// Token: 0x0600004D RID: 77
		void DecreaseIndentation();

		// Token: 0x0600004E RID: 78
		void LogForDataMining(string task, DateTime startTime);
	}
}
