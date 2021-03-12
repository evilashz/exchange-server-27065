using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020000A3 RID: 163
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MonitoringLogConfiguration : ILogConfiguration
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x00022194 File Offset: 0x00020394
		public MonitoringLogConfiguration(string logRelativePath) : this(logRelativePath, MonitoringLogConfiguration.LogPrefixValue)
		{
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000221A4 File Offset: 0x000203A4
		public MonitoringLogConfiguration(string logRelativePath, string logPrefix)
		{
			this.IsLoggingEnabled = true;
			this.LogPath = Path.Combine(Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\Monitoring\\"), logRelativePath);
			this.MaxLogAge = TimeSpan.FromDays(7.0);
			this.MaxLogDirectorySizeInBytes = (long)ByteQuantifiedSize.FromMB(100UL).ToBytes();
			this.MaxLogFileSizeInBytes = (long)ByteQuantifiedSize.FromMB(10UL).ToBytes();
			this.LogPrefix = logPrefix;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00022220 File Offset: 0x00020420
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00022228 File Offset: 0x00020428
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00022231 File Offset: 0x00020431
		public bool IsActivityEventHandler
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00022234 File Offset: 0x00020434
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0002223C File Offset: 0x0002043C
		public string LogPath { get; private set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00022245 File Offset: 0x00020445
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0002224D File Offset: 0x0002044D
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00022256 File Offset: 0x00020456
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0002225E File Offset: 0x0002045E
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00022267 File Offset: 0x00020467
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0002226F File Offset: 0x0002046F
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00022278 File Offset: 0x00020478
		public string LogComponent
		{
			get
			{
				return "MonitoringLog";
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0002227F File Offset: 0x0002047F
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x00022287 File Offset: 0x00020487
		public string LogPrefix { get; private set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00022290 File Offset: 0x00020490
		public string LogType
		{
			get
			{
				return "Active Monitoring Log";
			}
		}

		// Token: 0x0400039F RID: 927
		private const string LogTypeValue = "Active Monitoring Log";

		// Token: 0x040003A0 RID: 928
		private const string LogComponentValue = "MonitoringLog";

		// Token: 0x040003A1 RID: 929
		private const string DefaultRelativeFilePath = "Logging\\Monitoring\\";

		// Token: 0x040003A2 RID: 930
		public static readonly string LogPrefixValue = "Monitoring";
	}
}
