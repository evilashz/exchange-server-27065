using System;

namespace Microsoft.Exchange.Inference.Common.Diagnostics
{
	// Token: 0x02000008 RID: 8
	public class DiagnosticLogConfig : LogConfig, IDiagnosticLogConfig, ILogConfig
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002544 File Offset: 0x00000744
		public DiagnosticLogConfig(bool isLoggingEnabled, string logType, string logPrefix, string logPath, ulong? maxLogDirectorySize, ulong? maxLogFileSize, TimeSpan? maxLogAge, LoggingLevel loggingLevel) : base(isLoggingEnabled, logType, logPrefix, logPath, maxLogDirectorySize, maxLogFileSize, maxLogAge, 4096)
		{
			this.LoggingLevel = loggingLevel;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000256F File Offset: 0x0000076F
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002577 File Offset: 0x00000777
		public LoggingLevel LoggingLevel { get; private set; }
	}
}
