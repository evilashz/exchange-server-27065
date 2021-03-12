using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020000A2 RID: 162
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MonitoringDiagnosticLogConfiguration : ILogConfiguration
	{
		// Token: 0x060005A6 RID: 1446 RVA: 0x000220B0 File Offset: 0x000202B0
		public MonitoringDiagnosticLogConfiguration(string logRelativePath)
		{
			this.IsLoggingEnabled = true;
			this.LogPath = Path.Combine("D:\\MonitoringDiagnosticLogs", logRelativePath);
			this.MaxLogAge = TimeSpan.FromDays(7.0);
			this.MaxLogDirectorySizeInBytes = (long)ByteQuantifiedSize.FromMB(100UL).ToBytes();
			this.MaxLogFileSizeInBytes = (long)ByteQuantifiedSize.FromMB(10UL).ToBytes();
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0002211B File Offset: 0x0002031B
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00022123 File Offset: 0x00020323
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0002212C File Offset: 0x0002032C
		public bool IsActivityEventHandler
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0002212F File Offset: 0x0002032F
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x00022137 File Offset: 0x00020337
		public string LogPath { get; private set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00022140 File Offset: 0x00020340
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x00022148 File Offset: 0x00020348
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00022151 File Offset: 0x00020351
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x00022159 File Offset: 0x00020359
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00022162 File Offset: 0x00020362
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0002216A File Offset: 0x0002036A
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00022173 File Offset: 0x00020373
		public string LogComponent
		{
			get
			{
				return "MSExchangeHMHost";
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0002217A File Offset: 0x0002037A
		public string LogPrefix
		{
			get
			{
				return MonitoringDiagnosticLogConfiguration.LogPrefixValue;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00022181 File Offset: 0x00020381
		public string LogType
		{
			get
			{
				return "MSExchangeHMHost diagnostic log";
			}
		}

		// Token: 0x04000396 RID: 918
		private const string LogTypeValue = "MSExchangeHMHost diagnostic log";

		// Token: 0x04000397 RID: 919
		private const string LogComponentValue = "MSExchangeHMHost";

		// Token: 0x04000398 RID: 920
		private const string RootLogFilePath = "D:\\MonitoringDiagnosticLogs";

		// Token: 0x04000399 RID: 921
		public static readonly string LogPrefixValue = "MSExchangeHMHost";
	}
}
