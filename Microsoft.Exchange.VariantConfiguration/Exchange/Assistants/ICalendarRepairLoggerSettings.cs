using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000029 RID: 41
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface ICalendarRepairLoggerSettings : ISettings
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000B6 RID: 182
		bool InsightLogEnabled { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B7 RID: 183
		string InsightLogDirectoryName { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000B8 RID: 184
		TimeSpan InsightLogFileAgeInDays { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000B9 RID: 185
		ulong InsightLogDirectorySizeLimit { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000BA RID: 186
		ulong InsightLogFileSize { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000BB RID: 187
		ulong InsightLogCacheSize { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000BC RID: 188
		TimeSpan InsightLogFlushIntervalInSeconds { get; }
	}
}
