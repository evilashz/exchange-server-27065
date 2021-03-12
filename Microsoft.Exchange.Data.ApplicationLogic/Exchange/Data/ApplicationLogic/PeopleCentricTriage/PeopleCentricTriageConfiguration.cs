using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleCentricTriage
{
	// Token: 0x02000172 RID: 370
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PeopleCentricTriageConfiguration
	{
		// Token: 0x06000E78 RID: 3704 RVA: 0x0003BE58 File Offset: 0x0003A058
		public PeopleCentricTriageConfiguration(string exchangeInstallPath, TimeSpan skipMailboxInactivityThreshold)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("exchangeInstallPath", exchangeInstallPath);
			this.exchangeInstallPath = exchangeInstallPath;
			this.skipMailboxInactivityThreshold = skipMailboxInactivityThreshold;
			this.assistantLoggingPath = this.ComputeAssistantLoggingPath();
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0003BE85 File Offset: 0x0003A085
		public PeopleCentricTriageConfiguration(string exchangeInstallPath) : this(exchangeInstallPath, PeopleCentricTriageConfiguration.DefaultSkipMailboxInactivityThreshold)
		{
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0003BE93 File Offset: 0x0003A093
		public TimeSpan SkipMailboxInactivityThreshold
		{
			get
			{
				return this.skipMailboxInactivityThreshold;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0003BE9B File Offset: 0x0003A09B
		public string AssistantLoggingPath
		{
			get
			{
				return this.assistantLoggingPath;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0003BEA3 File Offset: 0x0003A0A3
		public TimeSpan LogFileMaxAge
		{
			get
			{
				return PeopleCentricTriageConfiguration.DefaultLogFileMaxAge;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0003BEAA File Offset: 0x0003A0AA
		public long LogDirectoryMaxSize
		{
			get
			{
				return 104857600L;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0003BEB2 File Offset: 0x0003A0B2
		public long LogFileMaxSize
		{
			get
			{
				return 10485760L;
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003BEBA File Offset: 0x0003A0BA
		private string ComputeAssistantLoggingPath()
		{
			return Path.Combine(this.exchangeInstallPath, "Logging\\PeopleCentricTriageAssistant");
		}

		// Token: 0x040007BB RID: 1979
		internal const string AssistantLoggingComponentName = "PeopleCentricTriageAssistant";

		// Token: 0x040007BC RID: 1980
		internal const string AssistantLogFilePrefix = "PeopleCentricTriageAssistant";

		// Token: 0x040007BD RID: 1981
		private const long DefaultLogDirectoryMaxSize = 104857600L;

		// Token: 0x040007BE RID: 1982
		private const long DefaultLogFileMaxSize = 10485760L;

		// Token: 0x040007BF RID: 1983
		private static readonly TimeSpan DefaultLogFileMaxAge = TimeSpan.FromDays(90.0);

		// Token: 0x040007C0 RID: 1984
		private static readonly TimeSpan DefaultSkipMailboxInactivityThreshold = TimeSpan.FromDays(60.0);

		// Token: 0x040007C1 RID: 1985
		private readonly string exchangeInstallPath;

		// Token: 0x040007C2 RID: 1986
		private readonly string assistantLoggingPath;

		// Token: 0x040007C3 RID: 1987
		private readonly TimeSpan skipMailboxInactivityThreshold;
	}
}
