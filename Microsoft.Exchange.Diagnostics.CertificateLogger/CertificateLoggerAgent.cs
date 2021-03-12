using System;
using Microsoft.Exchange.Diagnostics.Service;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.CertificateLogger
{
	// Token: 0x02000004 RID: 4
	public sealed class CertificateLoggerAgent : RetentionAgent
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002600 File Offset: 0x00000800
		public CertificateLoggerAgent(string enforcedDirectory, TimeSpan retentionPeriod, int maxDirectorySizeMBytes, TimeSpan checkInterval, bool logDataLossMessage) : base(enforcedDirectory, retentionPeriod, maxDirectorySizeMBytes, checkInterval, logDataLossMessage)
		{
			TimeSpan configTimeSpan = Configuration.GetConfigTimeSpan("CertificateLogger_MonitoringInterval", TimeSpan.FromDays(0.0), TimeSpan.MaxValue, TimeSpan.FromDays(1.0));
			long maxDirectorySize = (long)(maxDirectorySizeMBytes * 1024 * 1024);
			long configLong = Configuration.GetConfigLong("CertificateLogger_MaxFileSize", 1048576L, 104857600L, 10485760L);
			int configInt = Configuration.GetConfigInt("CertificateLogger_MaxBufferSize", 0, 10485760, 1024);
			TimeSpan configTimeSpan2 = Configuration.GetConfigTimeSpan("CertificateLogger_StreamFlushInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromMinutes(60.0), TimeSpan.FromSeconds(5.0));
			this.certLogger = new CertificateLogger(enforcedDirectory, configTimeSpan, maxDirectorySize, configLong, configInt, configTimeSpan2);
			Logger.LogInformationMessage("Started certificate log monitor.", new object[0]);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000026E0 File Offset: 0x000008E0
		protected override void InternalDispose(bool disposing)
		{
			if (this.certLogger != null)
			{
				this.certLogger.Dispose();
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x04000009 RID: 9
		private const string MonitoringIntervalConfigurationLabel = "CertificateLogger_MonitoringInterval";

		// Token: 0x0400000A RID: 10
		private const string MaxFileSizeConfigurationLabel = "CertificateLogger_MaxFileSize";

		// Token: 0x0400000B RID: 11
		private const string MaxBufferSizeConfigurationLabel = "CertificateLogger_MaxBufferSize";

		// Token: 0x0400000C RID: 12
		private const string StreamFlushIntervalConfigurationLabel = "CertificateLogger_StreamFlushInterval";

		// Token: 0x0400000D RID: 13
		private readonly CertificateLogger certLogger;
	}
}
